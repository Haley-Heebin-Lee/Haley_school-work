/* Citation and Sources...
Final Project Milestone
Module: Parking
Filename: Parking.h
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

#ifndef SDDS_PARKING_H 
#define SDDS_PARKING_H 
#include "Menu.h"
#include "Vehicle.h"


namespace sdds
{
	const int MAX_LOT_SIZE = 100;
	class Parking
	{
		int m_noOfSpot;
		Vehicle* parkingSpot[MAX_LOT_SIZE + 1];
		int m_parkedNum;
		char* m_filename;
		Menu* parkingMenu;
		Menu* vehicleMenu;
		//int repeat;
		//bool isfirst;

		bool isEmpty() const;
		void setEmpty();
		void parkingStatus() const;
		void parkingVehicle();
		void returnVehicle();
		void parkList();
		bool closeParking();
		bool exitApp();
		bool loadData();
		void save();
	public:
		Parking(const char* filename, int noOfSpots);
		Parking(const Parking&) = delete;
		void operator=(const Parking&) = delete;
		int run();
		~Parking();
		//void tryNo() const;
		//void ShowData();
	};
}
#endif