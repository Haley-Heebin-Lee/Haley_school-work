/* Citation and Sources...
Final Project Milestone
Module: Utils
Filename: Utils.cpp
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
#include <cctype>
#include <iostream>
#include <cstring>
#include "Utils.h"
using namespace std;

namespace sdds {
	char* Utils::upper(char* c)
	{
		int size = strlen(c);
		for (int i = 0; i < size; i++)
			c[i] = toupper(c[i]);
		return c;
	}
	char* Utils::lower(char* c)
	{
		int size = strlen(c);
		for (int i = 0; i < size; i++)
			c[i] = tolower(c[i]);
		return c;
	}
	bool Utils::getChar()
	{
		bool result = true;
		bool returnValue = false;
		string input;
		do {
			cin >> input;
			cin.clear();
			cin.ignore(1000, '\n');
			if (input.length() == 1)
			{
				if (input == "y" || input == "Y")
				{
					result = false;
					returnValue = true;
				}
				else if (input == "n" || input == "N")
					result = false;
				else
				{
					cout << "Invalid response, only (Y)es or (N)o are acceptable, retry: ";
				}
			}
			else
			{
				cout << "Invalid response, only (Y)es or (N)o are acceptable, retry: ";
			}
		} while (result);

		return returnValue;
	}
	void Utils::getInt(int& input, int min, int max)
	{
		bool result = true;
		char newline;
		do {
			cin >> input;
			newline = cin.get();
			if (newline != '\n' || cin.fail())
			{
				cin.clear();
				cin.ignore(1000, '\n');
				cout << "Invalid Integer, try again: ";
			}
			else if (input<min || input>max)
				cout << "Invalid selection, try again: ";
			else
				result = false;
		} while (result);
	}
}