/* Citation and Sources...
Final Project Milestone
Module: Utils
Filename: Utils.h
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

#ifndef SDDS_UTILS_H 
#define SDDS_UTILS_H 

namespace sdds
{
	struct Utils
	{
		char* upper(char* c);
		char* lower(char* c);
		static bool getChar();
		static void getInt(int& input, int min, int max);
	};
}
#endif