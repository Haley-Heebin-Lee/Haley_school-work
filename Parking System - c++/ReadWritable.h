/* Citation and Sources...
Final Project Milestone
Module: ReadWritable
Filename: ReadWritable.h
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
#ifndef SDDS_READWRITABLE_H 
#define SDDS_READWRITABLE_H 
#include <iostream>

namespace sdds
{
	class ReadWritable
	{
		bool commaFlag;

	public:
		ReadWritable();
		virtual ~ReadWritable();
		bool isCsv()const;
		void setCsv(bool value);
		virtual std::istream& read(std::istream& is) = 0;
		virtual std::ostream& write(std::ostream& os) const = 0;
	};
	std::istream& operator>>(std::istream& is, ReadWritable& rw);
	std::ostream& operator<<(std::ostream& os, ReadWritable& rw);
}
#endif