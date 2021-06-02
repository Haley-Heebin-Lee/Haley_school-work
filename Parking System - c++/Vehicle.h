/* Citation and Sources...
Final Project Milestone
Module: Vehicle
Filename: Vehicle.h
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

#ifndef SDDS_VEHICLE_H 
#define SDDS_VEHICLE_H 
#include <iostream>
#include "ReadWritable.h"
#include "Utils.h"

namespace sdds
{
	const int MAX_PLATE = 8;
	class Vehicle : public ReadWritable
	{
		char m_plate[MAX_PLATE + 1];
		char* m_makeNmodel;
		int m_parkingSpot;
	protected:
		Utils* utils;
		void setEmpty();
		bool isEmpty();
		const char* getLicensePlate() const;
		const char* getMakeModel() const;
		void setMakeModel(const char* makeNmodel);

	public:
		Vehicle();
		Vehicle(const char* plate, const char* makeNmodel);
		Vehicle(const Vehicle&) = delete;
		void operator=(const Vehicle&) = delete;
		~Vehicle();

		int getParkingSpot() const;
		void setParkingSpot(int spotnumber);
		bool operator==(const char* plate) const;
		bool operator==(const Vehicle& v1) const;
		std::istream& read(std::istream& is);
		std::ostream& write(std::ostream& os) const;
	};
}
#endif