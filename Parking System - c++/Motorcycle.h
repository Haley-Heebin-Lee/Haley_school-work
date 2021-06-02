/* Citation and Sources...
Final Project Milestone
Module: Motorcycle
Filename: Motorcycle.h
Version 6.0
Student: Heebin Lee
ID: 130464191
-----------------------------------------------------------
Date Reason
2020/6/27 Preliminary release
2020/8/8 Debugged DMA
-----------------------------------------------------------
I have done all the coding by myself and only copied the code
that my professor provided to complete my workshops and assignments.
-----------------------------------------------------------*/
#ifndef SDDS_MOTORCYCLE_H 
#define SDDS_MOTORCYCLE_H 
#include <iostream>
#include "Vehicle.h"

namespace sdds
{
	class Motorcycle : public Vehicle {
		int m_sideCar;
	public:
		Motorcycle();
		Motorcycle(const char* plate, const char* makeNmodel);
		Motorcycle(const Motorcycle&) = delete;
		void operator=(const Motorcycle&) = delete;
		std::istream& read(std::istream& is);
		std::ostream& write(std::ostream& os) const;
	};
}
#endif