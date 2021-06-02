/* ===========================================================
Student: Heebin Lee
ID: 130464191
Date: 2020.11.28
-----------------------------------------------------------
I have done all the coding by myself and only copied the code
that my professor provided to complete my workshops and assignments.
-----------------------------------------------------------*/

#include <iomanip>
#include <algorithm>
#include "CustomerOrder.h"
#include "Utilities.h"
using namespace std;

size_t CustomerOrder::m_widthField = 0;
CustomerOrder::CustomerOrder()
{
	m_name = "";
	m_product = "";
	m_cntItem = 0;
	m_lstItem = nullptr;
}
CustomerOrder::CustomerOrder(const std::string& str)
{
	Utilities utility;
	size_t pos = 0;
	bool TorF = false;
	string temp;

	if (!str.empty())
	{
		m_name = utility.extractToken(str, pos, TorF);

		if (TorF) 
		{
			m_product = utility.extractToken(str, pos, TorF);
		}

		temp = str.substr(pos, str.length() - pos);
		m_cntItem = count(temp.begin(), temp.end(), utility.getDelimiter()) + 1;
		m_lstItem = new Item * [m_cntItem];

		for (size_t i = 0; i < m_cntItem && TorF; i++)
			m_lstItem[i] = new Item(utility.extractToken(str, pos, TorF));

		if (m_widthField < utility.getFieldWidth())
			m_widthField = utility.getFieldWidth();
	}

}
CustomerOrder::CustomerOrder(const CustomerOrder& c)
{
	throw "ERROR: Cannot make copies.";
}
CustomerOrder::CustomerOrder(CustomerOrder&& c) noexcept
{
	*this = std::move(c);
}
CustomerOrder& CustomerOrder::operator=(CustomerOrder&& c) noexcept
{
	if (this != &c)
	{
		for (size_t i = 0; i < m_cntItem; ++i)
			delete m_lstItem[i];
		delete[] m_lstItem;

		m_name = c.m_name;
		m_product = c.m_product;
		m_cntItem = c.m_cntItem;
		m_lstItem = c.m_lstItem;
	
		c.m_lstItem = nullptr;
		c.m_cntItem = 0;
	}
	return *this;
}
CustomerOrder::~CustomerOrder()
{
	for (size_t i = 0; i < m_cntItem; ++i)
		delete m_lstItem[i];
	delete[] m_lstItem;
}
bool CustomerOrder::isOrderFilled() const
{
	bool returnValue = true;
	for (size_t i = 0; i < m_cntItem; ++i)
	{
		if (!m_lstItem[i]->m_isFilled)
			returnValue = false;
	}
	return returnValue;
}
bool CustomerOrder::isItemFilled(const std::string& itemName) const
{
	for (size_t i = 0; i < m_cntItem; ++i)
	{
		if (m_lstItem[i]->m_itemName == itemName)
	
			return m_lstItem[i]->m_isFilled;
	}
	return true;
}
void CustomerOrder::fillItem(Station& station, std::ostream& os)
{
	for (size_t i = 0; i < m_cntItem; ++i)
	{
		if (m_lstItem[i]->m_itemName == station.getItemName())
		{
			if (station.getQuantity() > 0)
			{
				m_lstItem[i]->m_serialNumber = station.getNextSerialNumber();
				m_lstItem[i]->m_isFilled = true;
				station.updateQuantity();

				os << "    Filled " << m_name << ", " << m_product << " [" << m_lstItem[i]->m_itemName << "]" << endl;
			}
			else
			{
				os << "    Unable to fill "<<m_name<<", " << m_product << " [" << m_lstItem[i]->m_itemName << "]" << endl;
			}
		}
	}
}
void CustomerOrder::display(std::ostream& os) const
{
	os << m_name << " - " << m_product << endl;
	for (size_t i = 0; i < m_cntItem; i++)
	{
		os << "[" << setw(6) << setfill('0') << right << m_lstItem[i]->m_serialNumber << "] " << setfill(' ') << setw(m_widthField) << left << m_lstItem[i]->m_itemName << " - ";
		if (m_lstItem[i]->m_isFilled)
			os << "FILLED" << endl;
		else
			os << "MISSING" << endl;
	}
}