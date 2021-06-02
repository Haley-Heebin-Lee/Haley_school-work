/* Citation and Sources...
Final Project Milestone
Module: Car
Filename: Car.h
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
#ifndef SDDS_CAR_H 
#define SDDS_CAR_H 
#include <iostream>
#include "Vehicle.h"

namespace sdds
{
	class Car :public Vehicle
	{
		int m_carWash;
	public:
		Car();
		Car(const char* plate, const char* makeNmodel);
		Car(const Car&) = delete;
		void operator=(const Car&) = delete;
		std::istream& read(std::istream& is);
		std::ostream& write(std::ostream& os) const;
	};
}
#endif