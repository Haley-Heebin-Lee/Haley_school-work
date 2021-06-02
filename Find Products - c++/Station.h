/* ===========================================================
Student: Heebin Lee
ID: 130464191
Date: 2020.11.28
-----------------------------------------------------------
I have done all the coding by myself and only copied the code
that my professor provided to complete my workshops and assignments.
-----------------------------------------------------------*/
#ifndef STATION_H 
#define STATION_H 
#include <cstddef>
#include <string>
#include <iostream>

	class Station {
		size_t m_id = 0;
		std::string m_item;
		std::string m_desc;
		unsigned int m_serial = 0;
		unsigned int m_stockLeft = 0;

		static size_t m_widthField;
		static size_t id_generator;

	public:
		Station(const std::string& str);
		const std::string& getItemName() const;
		unsigned int getNextSerialNumber();
		unsigned int getQuantity() const;
		void updateQuantity();
		void display(std::ostream& os, bool full) const;
	};
#endif