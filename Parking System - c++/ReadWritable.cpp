/* Citation and Sources...
Final Project Milestone
Module: ReadWritable
Filename: ReadWritable.cpp
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
#include "ReadWritable.h"
using namespace std;

namespace sdds
{
	ReadWritable::ReadWritable()
	{
		commaFlag = false;
	}
	ReadWritable::~ReadWritable()
	{

	}
	bool ReadWritable::isCsv()const
	{
		return commaFlag;
	}
	void ReadWritable::setCsv(bool value)
	{
		commaFlag = value;
	}
	std::istream& ReadWritable::read(std::istream& is)
	{
		return is;
	}
	std::ostream& ReadWritable::write(std::ostream& os) const
	{
		return os;
	}

	std::istream& operator>>(std::istream& is, ReadWritable& rw)
	{
		return rw.read(is);
	}
	std::ostream& operator<<(std::ostream& os, ReadWritable& rw)
	{
		return rw.write(os);
	}
}