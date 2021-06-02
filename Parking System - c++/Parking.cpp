/* Citation and Sources...
Final Project Milestone
Module: Parking
Filename: Parking.cpp
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
#include <iostream>
#include <cstring>
#include <fstream>
#include "Utils.h"
#include "Parking.h"
////adjust the inclustion header files below
#include "Car.h"
#include "Motorcycle.h"
using namespace std;

namespace sdds
{
	Parking::Parking(const char* filename, int noOfSpot)
	{
		setEmpty();
		if (filename != nullptr && strlen(filename) > 1 && noOfSpot>9 && noOfSpot<=MAX_LOT_SIZE)
		{
			m_filename = new char[strlen(filename) + 1];
			strcpy(m_filename, filename);
			m_noOfSpot = noOfSpot;
			if (loadData())
			{
				parkingMenu = new Menu("Parking Menu, select an action:");
				parkingMenu->add("Park Vehicle");
				parkingMenu->add("Return Vehicle");
				parkingMenu->add("List Parked Vehicles");
				parkingMenu->add("Close Parking (End of day)");
				parkingMenu->add("Exit Program");

				vehicleMenu = new Menu("Select type of the vehicle:", 1);
				vehicleMenu->add("Car");
				vehicleMenu->add("Motorcycle");
				vehicleMenu->add("Cancel");
			}
			else
			{
				cout << "Error in data file" << endl;
				setEmpty();
			}
		}
		else
		{
			cout << "Error in data file" << endl;
			setEmpty();
		}
	}
	int Parking::run()
	{
		int result = 0;
		int returnValue = 0;
		int selection = 0;
		
		if (!isEmpty())
		{
			while (!result)
			{
				parkingStatus();
				selection = parkingMenu->run();
				if (selection == 1)
					parkingVehicle();
				else if (selection == 2)
					returnVehicle();
				else if (selection == 3)
					parkList();
				else if (selection == 4)
				{
					if (closeParking())
					{
						result = 1;
					}
				}
				else if (selection == 5)
				{
					if (exitApp())
					{
						cout << "Exiting program!" << endl;
						result = 1;
					}
				}

			}
		}
		else
			returnValue = 1;

		return returnValue;
	}
	Parking::~Parking()
	{
		save();
		delete[] m_filename;
		m_filename = nullptr;
		delete parkingMenu;
		parkingMenu = nullptr;
		delete vehicleMenu;
		vehicleMenu = nullptr;
		for (int i = 0; i < MAX_LOT_SIZE; i++)
		{
			delete parkingSpot[i];
			parkingSpot[i] = nullptr;
		}
	}
	bool Parking::isEmpty() const
	{
		return m_filename == nullptr;
	}
	void Parking::setEmpty()
	{
		m_filename = nullptr;
		parkingMenu = nullptr;
		vehicleMenu = nullptr;
		m_noOfSpot = 0;
		m_parkedNum = 0;

		for(int i=0; i<MAX_LOT_SIZE; i++)
			parkingSpot[i] = nullptr;
	}
	void Parking::parkingStatus() const
	{
		cout << "****** Seneca Valet Parking ******" << endl;
		cout << "*****  Available spots: ";
		cout.width(4);
		cout.setf(ios::left);
		cout << m_noOfSpot-m_parkedNum << " *****" << endl;
	}
	void Parking::parkingVehicle()
	{
		int selection = 0;
		if(m_noOfSpot==m_parkedNum)
			cout << "Parking is full" << endl;
		else
		{
			selection = vehicleMenu->run();

			if (selection == 3)
				cout << "Parking cancelled" << endl;
			else
			{
				for (int i = 0; i < MAX_LOT_SIZE; i++)
				{
					if (parkingSpot[i] == nullptr)
					{
						if (selection == 1)
						{
							parkingSpot[i] = new Car();
						}
						else if (selection == 2)
						{
							parkingSpot[i] = new Motorcycle();
						}
						parkingSpot[i]->setCsv(false);
						cin >> *parkingSpot[i];
						parkingSpot[i]->setParkingSpot(i + 1);
						m_parkedNum++;
						cout << endl << "Parking Ticket" << endl;
						cout << *parkingSpot[i] << endl;

						i = MAX_LOT_SIZE;
					}
				}
			}
		}
	}
	void Parking::returnVehicle()
	{
		char temp[9];
		cout << "Return Vehicle" << endl;
		cout << "Enter Licence Plate Number: ";
		cin >> temp;
		cin.clear();
		cin.ignore(2000, '\n');
		while (strlen(temp) < 1 || strlen(temp) > MAX_PLATE)
		{
			cout << "Invalid Licence Plate, try again: ";
			cin >> temp;
			cin.clear();
			cin.ignore(2000, '\n');
		}
		int count = 0;
		for (int i = 0; i < m_noOfSpot; i++)
		{
			if (parkingSpot[i] != nullptr)
			{
				if (*parkingSpot[i] == temp)
				{
					parkingSpot[i]->setCsv(false);
					count++;
					cout << endl << "Returning: " << endl;
					cout << *parkingSpot[i] << endl;

					delete parkingSpot[i];
					parkingSpot[i] = nullptr;

					m_parkedNum--;

					i = m_noOfSpot;
				}
			}
			if (i == m_noOfSpot-1 && count == 0)
				cout << "License plate " << temp << " Not found" << endl;
			
		}
	}
	void Parking::parkList()
	{
		cout << "*** List of parked vehicles ***" << endl;
		for (int i = 0; i < m_noOfSpot; i++)
		{
			if (parkingSpot[i] != nullptr)
			{
				parkingSpot[i]->setCsv(false);
				cout << *parkingSpot[i];
				cout << "-------------------------------" << endl;
			}
		}
	}
	bool Parking::closeParking()
	{
		bool returnValue = true;

		if(m_parkedNum==MAX_LOT_SIZE)
			cout << "Closing Parking" << endl;
		else
		{
			cout << "This will Remove and tow all remaining vehicles from the parking!" <<endl<<
				"Are you sure? (Y)es/(N)o: ";
			if (Utils::getChar())
			{
				cout << "Closing Parking"<<endl;
				for (int i = 0; i < m_noOfSpot; i++)
				{
					if (parkingSpot[i] != nullptr)
					{
						cout  <<endl << "Towing request" << endl;
						cout << "*********************" << endl;
						cout << *parkingSpot[i] ;

						delete parkingSpot[i];
						parkingSpot[i] = nullptr;
					}
				}
				m_parkedNum = 0;
			}
			else
			{
				cout << "Aborted!" << endl;
				returnValue = false;
			}
		}
		return returnValue;
	}
	bool Parking::exitApp()
	{
		bool returnValue=false;
		cout << "This will terminate the program!" << endl;
		cout << "Are you sure? (Y)es/(N)o: ";
		if (Utils::getChar())
			returnValue = true;
		
		return returnValue;
	}
	bool Parking::loadData()
	{
		bool returnValue = true;
		bool flag = true;
		char MorC;
		if (!isEmpty())
		{
			ifstream fin(m_filename);
			if (fin.is_open())
			{
				Vehicle* v = nullptr;
				do //(fin.good())
					//while(!fin.eof())
				{
					MorC = fin.get();
					fin.ignore(256, ',');
					
					if (MorC == 'M')
						v = new Motorcycle();
					else if (MorC == 'C')
						v = new Car();
					else
						flag = false;
					
					if (flag)
					{
						v->setCsv(true);
						if (!(fin >> *v).fail())
						{
							parkingSpot[(v->getParkingSpot()) - 1] = v;
							m_parkedNum++;
						}
						else
						{
							delete v;
							v = nullptr;
						}
					}

				} while (m_parkedNum<m_noOfSpot && fin);
			}
			fin.close();
		}
		else
			returnValue = false;

		return returnValue;
	}
	void Parking::save()
	{
		ofstream fout(m_filename);
		if (fout.is_open())
		{
			for (int i = 0; i < m_noOfSpot; i++)
			{
				if (parkingSpot[i] != nullptr)
				{
					parkingSpot[i]->setCsv(true);
					fout << *parkingSpot[i];
				}
			}
		}
		fout.close();
	}
}