/* ===========================================================
Student: Heebin Lee
ID: 130464191
Date: 2020.11.28
-----------------------------------------------------------
I have done all the coding by myself and only copied the code
that my professor provided to complete my workshops and assignments.
-----------------------------------------------------------*/
#include <fstream>
#include <cstddef>
#include <utility>
#include "LineManager.h"
#include "Utilities.h"
using namespace std;

LineManager::LineManager(const std::string& filename, std::vector<Workstation*>& v_ws, std::vector<CustomerOrder>& v_co)
{
	ifstream fs(filename);
	Utilities utility;
	
	if(!fs.is_open())
		throw string("Unable to open [") + filename + "] file.";

	string record;
	string currentStation;
	string nextStation;
	bool TorF = true;

	while (!fs.eof())
	{
		getline(fs, record);
		size_t pos = 0;

		currentStation = utility.extractToken(record, pos, TorF);
		if (TorF)
			nextStation = utility.extractToken(record, pos, TorF);
		else
			nextStation = "";

		for (size_t i = 0; i < v_ws.size(); i++)
		{
			if (v_ws[i]->getItemName() == currentStation)
			{
				for (size_t j = 0; j < v_ws.size(); j++)
				{
					if (v_ws[j]->getItemName() == nextStation)
					{
						v_ws[i]->setNextStation(*v_ws[j]);

						break; // escape the inner for loop
					}
				}
				if (m_firstStation == nullptr)
					m_firstStation = v_ws[i];
			}
		}
	}
	fs.close();

	for (size_t i = 0; i < v_co.size(); i++)
		ToBeFilled.push_back(move(v_co[i]));

	m_cntCustomerOrder = ToBeFilled.size();
	AssemblyLine = v_ws;
	
}
bool LineManager::run(std::ostream& os)
{
	static size_t count = 1;
	os << "Line Manager Iteration: " << count++ << endl;

	if (!ToBeFilled.empty())
	{
		*m_firstStation += move(ToBeFilled.front());
		ToBeFilled.pop_front();
	}

	for (size_t i = 0; i < AssemblyLine.size(); i++)
		AssemblyLine[i]->runProcess(os);

	for (size_t i = 0; i < AssemblyLine.size(); i++)
	{
		AssemblyLine[i]->moveOrder();
		CustomerOrder c;
		if (AssemblyLine[i]->getIfCompleted(c))
			Completed.push_back(move(c));
	}

	bool returnValue = false;
	if (Completed.size() == m_cntCustomerOrder)
		returnValue = true;

	return returnValue;
}
void LineManager::displayCompletedOrders(std::ostream& os) const
{
	for (size_t i = 0; i < Completed.size(); i++)
		Completed[i].display(os);
}
void LineManager::displayStations() const
{
	for (size_t i = 0; i < AssemblyLine.size(); i++)
		AssemblyLine[i]->display(cout);
}
void LineManager::displayStationsSorted() const
{
	const Workstation* temp = m_firstStation;
	for (size_t i = 0; i < AssemblyLine.size(); i++)
	{
		temp->display(cout);
		temp = temp->getNextStation();
	}
}