/* ===========================================================
Student: Heebin Lee
ID: 130464191
Date: 2020.11.28
-----------------------------------------------------------
I have done all the coding by myself and only copied the code
that my professor provided to complete my workshops and assignments.
-----------------------------------------------------------*/

#include <iomanip>
#include "Station.h"
#include "Utilities.h"
using namespace std;

	size_t Station::m_widthField = 0;
	size_t Station::id_generator = 0;

	Station::Station(const string& str)
	{
		Utilities utility;
		size_t pos = 0;
		bool TorF = false;
		string temp;
		m_id = ++id_generator;

		if (!str.empty())
		{
			m_item = utility.extractToken(str, pos, TorF);

			if (TorF) //if item name succeed
			{
				temp = utility.extractToken(str, pos, TorF);
				m_serial = atoi(temp.c_str());
			}
			if(TorF) // if serial
			{
				temp = utility.extractToken(str, pos, TorF);
				m_stockLeft = atoi(temp.c_str());
			}
			if (m_widthField < utility.getFieldWidth())
				m_widthField = utility.getFieldWidth();

			if (TorF) // if quantity
				m_desc = utility.extractToken(str, pos, TorF);
		}
	}
	const std::string& Station::getItemName() const
	{
		return m_item;
	}
	unsigned int Station::getNextSerialNumber()
	{
		return m_serial++;
	}
	unsigned int Station::getQuantity() const
	{
		return m_stockLeft;
	}
	void Station::updateQuantity()
	{
		if (m_stockLeft > 0)
			m_stockLeft--;
	}
	void Station::display(std::ostream& os, bool full) const
	{
		
		os << "[" << setfill('0') << setw(3) << right << m_id << "] Item: ";
		os << setfill(' ') << setw(m_widthField) << left << getItemName();
		os << " [" << setfill('0') << setw(6) << right << m_serial << "]";
		if (full)
		{
			os << " Quantity: " << setfill(' ') << setw(m_widthField) << left << getQuantity();
			os << " Description: " << m_desc << endl;
		}
		else
			os << endl;
		
	}