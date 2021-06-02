/* Citation and Sources...
Final Project Milestone
Module: Car
Filename: Car.cpp
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
#include <cstring>
#include "Car.h"
using namespace std;
namespace sdds
{
	Car::Car()
	{
		m_carWash = -1;
	}
	Car::Car(const char* plate, const char* makeNmodel) : Vehicle(plate, makeNmodel) {

		m_carWash = 0;
		////If one of the licence plate or make and model are pointing to null or are invalid values, the Car is set into an invalid empty state.
		if (plate != nullptr && makeNmodel != nullptr && strlen(plate) < 9 && strlen(plate) > 0 && strlen(makeNmodel) > 1 && strlen(makeNmodel) < 61)
			m_carWash = -1;
	}
	std::istream& Car::read(std::istream& is)
	{
		if (isCsv())
		{
			Vehicle::read(is);
			is >> m_carWash;
			is.clear();
			is.ignore(1000, '\n');
			// It ignores whatever character is left up to and including a newline character('\n').
		}
		else
		{
			cout << endl << "Car information entry" << endl;
			Vehicle::read(is);
			cout << "Carwash while parked? (Y)es/(N)o: ";
			if (utils->getChar())
				m_carWash = 1;
			else
				m_carWash = 0;
		}
		return is;
	}
	std::ostream& Car::write(std::ostream& os) const
	{
		if (m_carWash == -1)
			os << "Invalid Car Object" << endl;
		else
		{
			if (isCsv())
				os << "C,";
			else
				os << "Vehicle type: Car" << endl;

			Vehicle::write(os);

			if (isCsv())
				os << m_carWash << endl;
			else
			{
				if (m_carWash == 1)
					os << "With Carwash" << endl;
				else
					os << "Without Carwash" << endl;
			}
		}
		return os;
	}
}