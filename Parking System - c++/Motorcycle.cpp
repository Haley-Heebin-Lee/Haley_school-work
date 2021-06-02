/* Citation and Sources...
Final Project Milestone
Module: Motorcycle
Filename: Motorcycle.cpp
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
#include "Motorcycle.h"
using namespace std;
namespace sdds
{
	Motorcycle::Motorcycle()
	{
		m_sideCar = -1;
	}
	Motorcycle::Motorcycle(const char* plate, const char* makeNmodel) : Vehicle(plate, makeNmodel)
	{
		m_sideCar = 0;
		////If one of the licence plate or make and model are pointing to null or are invalid values, the Car is set into an invalid empty state.
		if (plate != nullptr && makeNmodel != nullptr && strlen(plate) < 9 && strlen(plate) > 0 && strlen(makeNmodel) > 1 && strlen(makeNmodel) < 61)
			m_sideCar = -1;
	}
	std::istream& Motorcycle::read(std::istream& is)
	{
		if (isCsv())
		{
			Vehicle::read(is);
			is >> m_sideCar;
			is.clear();
			is.ignore(1000, '\n');
			// It ignores what ever character is left up to and including a newline character('\n').
		}
		else
		{
			cout << endl << "Motorcycle information entry" << endl;
			Vehicle::read(is);
			cout << "Does the Motorcycle have a side car? (Y)es/(N)o: ";
			if (utils->getChar())
				m_sideCar = 1;
			else
				m_sideCar = 0;
		}
		return is;
	}
	std::ostream& Motorcycle::write(std::ostream& os) const
	{
		if (m_sideCar == -1)
			os << "Invalid Motorcycle Object" << endl;
		else
		{
			if (isCsv())
				os << "M,";
			else
				os << "Vehicle type: Motorcycle" << endl;

			Vehicle::write(os);

			if (isCsv())
				os << m_sideCar << endl;
			else
			{
				if (m_sideCar == 1)
					os << "With Sidecar" << endl;
			}
		}
		return os;
	}
}