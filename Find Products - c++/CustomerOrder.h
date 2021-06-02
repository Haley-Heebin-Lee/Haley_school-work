/* ===========================================================
Student: Heebin Lee
ID: 130464191
Date: 2020.11.28
-----------------------------------------------------------
I have done all the coding by myself and only copied the code
that my professor provided to complete my workshops and assignments.
-----------------------------------------------------------*/
#ifndef CUSTOMERORDER_H 
#define CUSTOMERORDER_H 
#include <cstddef>
#include <iostream>
#include <string>
#include "Station.h"
struct Item
{
	std::string m_itemName;
	unsigned int m_serialNumber = 0;
	bool m_isFilled = false;
	Item(const std::string& src) : m_itemName(src) {};
};


class CustomerOrder{
	std::string m_name;
	std::string m_product;
	unsigned int m_cntItem = 0;
	Item** m_lstItem = nullptr;

	static size_t m_widthField;
public:
	CustomerOrder();
	CustomerOrder(const std::string& str);

	CustomerOrder(const CustomerOrder& c);
	CustomerOrder& operator=(const CustomerOrder& c) = delete;
	CustomerOrder(CustomerOrder&& c) noexcept;
	CustomerOrder& operator=(CustomerOrder&& c) noexcept;

	~CustomerOrder();

	bool isOrderFilled() const;
	bool isItemFilled(const std::string& itemName) const;
	void fillItem(Station& station, std::ostream& os);
	void display(std::ostream& os) const;
};
#endif