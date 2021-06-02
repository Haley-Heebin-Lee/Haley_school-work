/* ==========================================================
Student: Heebin Lee
ID : 130464191
Date : 2020.11.28
-----------------------------------------------------------
I have done all the coding by myself and only copied the code
that my professor provided to complete my workshops and assignments.
----------------------------------------------------------- */

#include<utility>
#include "Workstation.h"
using namespace std;

Workstation::Workstation(const string& str) : Station(str)
{
	m_pNextStation = nullptr;
}
void Workstation::runProcess(std::ostream& os)
{
	if (!m_orders.empty())
		m_orders.front().fillItem(*this, os);
}
bool Workstation::moveOrder()
{
	bool returnValue = false;
	if (m_orders.empty())
		returnValue = false;
	else if (m_orders.front().isItemFilled(this->getItemName()) && m_pNextStation)
	{
		*m_pNextStation += move(m_orders.front());
		m_orders.pop_front();
		returnValue = true;
	}
	else
		returnValue = false;

	return returnValue;
}
void Workstation::setNextStation(Workstation& station)//was Station
{
	m_pNextStation = &station;
}
const Workstation* Workstation::getNextStation() const
{
	return m_pNextStation;
}
bool Workstation::getIfCompleted(CustomerOrder& order)
{
	bool returnValue = false;


	if (!m_orders.empty())
	{
		if (m_orders.back().isOrderFilled())
		{
			order = move(m_orders.front());
			m_orders.pop_front();
			returnValue = true;
		}
	}

	return returnValue;
}
void Workstation::display(std::ostream& os) const
{
	os << this->getItemName() <<" --> ";
	if (m_pNextStation)
		os << m_pNextStation->getItemName() << endl;
	else
		os << "END OF LINE" << endl;
}
Workstation& Workstation::operator+=(CustomerOrder&& order)
{
	m_orders.push_back(move(order));

	return *this;
}