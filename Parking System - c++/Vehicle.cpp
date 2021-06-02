/* Citation and Sources...
Final Project Milestone
Module: Vehicle
Filename: Vehicle.cpp
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
#define _CRT_SECURE_NO_WARNINGS
#include "Vehicle.h"

#include <cstring>
using namespace std;

namespace sdds
{
	void Vehicle::setEmpty()
	{
		m_plate[0] = '\0';
		m_makeNmodel = nullptr;
		m_parkingSpot = -1;
	}
	bool Vehicle::isEmpty()//if it's emptystate then return true
	{
		return m_makeNmodel == nullptr;
	}
	const char* Vehicle::getLicensePlate() const
	{
		return m_plate;
	}
	const char* Vehicle::getMakeModel() const
	{
		return m_makeNmodel;
	}
	void Vehicle::setMakeModel(const char* makeNmodel)
	{
		//setEmpty();
		if (makeNmodel != nullptr && strlen(makeNmodel) > 1 && strlen(makeNmodel) < 61)
		{
			delete[] m_makeNmodel;
			//m_makeNmodel = nullptr;

			m_makeNmodel = new char[strlen(makeNmodel) + 1];
			strcpy(m_makeNmodel, makeNmodel);
		}
		else
		{
			delete[] m_makeNmodel;
			setEmpty();
		}
	}

	Vehicle::Vehicle(){
		setEmpty();
	}
	Vehicle::Vehicle(const char* plate, const char* makeNmodel) : ReadWritable()
	{
		setEmpty();
		if (plate != nullptr && makeNmodel != nullptr && strlen(plate) < 9 && strlen(plate) > 0 && strlen(makeNmodel) > 1 && strlen(makeNmodel) < 61)
		{
			setMakeModel(makeNmodel);

			strncpy(m_plate, plate, MAX_PLATE+1);
			m_parkingSpot = 0;
		}
		else
			setEmpty();
	}
	Vehicle::~Vehicle()
	{
		delete[] m_makeNmodel;
		m_makeNmodel = nullptr;
	}
	int Vehicle::getParkingSpot() const
	{
		return m_parkingSpot;
	}
	void Vehicle::setParkingSpot(int spotnumber)
	{
		if (spotnumber > 0)
			m_parkingSpot = spotnumber;
		else
		{
			delete[] m_makeNmodel;
			setEmpty();
		}
	}
	bool Vehicle::operator==(const char* plate) const
	{
		bool returnV = true;
		if (m_plate != nullptr && plate != nullptr)
		{
			char temp[9] = { '\0' };
			strncpy(temp, plate, MAX_PLATE+1);
			returnV = !strcmp(utils->upper(temp), m_plate) || !strcmp(utils->lower(temp), m_plate);
		}
		else
			returnV = false;
		return returnV;
	}
	bool Vehicle::operator==(const Vehicle& v) const
	{
		char temp[9];
		strncpy(temp, m_plate, MAX_PLATE+1);
		return !strcmp(utils->upper(temp), v.m_plate) || !strcmp(utils->lower(temp), v.m_plate);
	}
	std::istream& Vehicle::read(std::istream& is)
	{
		char temp[100]; // temperary char array for plate
		char temp2[61]; // temperary char array for MnM
		char temp3[72]; // temperary char array for comma input
		if (!isCsv())
		{
			cout << "Enter Licence Plate Number: ";
			is.getline(temp, 100);
			is.clear();
			while (strlen(temp) > MAX_PLATE || strlen(temp) <= 0) {
				cout << "Invalid Licence Plate, try again: ";
				is.getline(temp, 100);
				is.clear();
			}
			strncpy(m_plate, utils->upper(temp), MAX_PLATE+1);

			cout << "Enter Make and Model: ";
			is.getline(temp2, 61);// it might contain white spaces
			while (strlen(temp2) < 2 || strlen(temp2) > 60) {
				is.clear();
				cout << "Invalid Make and model, try again: ";
				is.getline(temp2, 60);
			}
			setMakeModel(temp2);
			m_parkingSpot = 0;
		}
		else
		{
			is >> m_parkingSpot;
			is.ignore(256, ',');

			is.getline(temp3, 9, ',');
			strncpy(m_plate, utils->upper(temp3), MAX_PLATE+1);

			is.getline(temp2, 61, ',');
			setMakeModel(temp2);

		}

		return is;
	}
	std::ostream& Vehicle::write(std::ostream& os) const
	{
		if (m_plate[0] == '\0')
			os << "Invalid Vehicle Object" << endl;
		else
		{
			if (isCsv())
			{
				os << getParkingSpot() << "," << getLicensePlate() << "," << getMakeModel() << ",";
			}
			else
			{
				os << "Parking Spot Number: ";
				if (m_parkingSpot == 0)
					os << "N/A" << endl;
				else
					os << getParkingSpot() << endl;
				os << "Licence Plate: " << this->getLicensePlate() << endl;
				os << "Make and Model: " << getMakeModel() << endl;
			}
		}
		return os;
	}
}