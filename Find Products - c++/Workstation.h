/* ===========================================================
Student: Heebin Lee
ID: 130464191
Date: 2020.11.28
-----------------------------------------------------------
I have done all the coding by myself and only copied the code
that my professor provided to complete my workshops and assignments.
-----------------------------------------------------------*/
#ifndef WORKSTATION_H 
#define WORKSTATION_H 
#include <deque>
#include <iostream>
#include <string>
#include "Station.h"
#include"CustomerOrder.h"

class Workstation : public Station{
	std::deque<CustomerOrder> m_orders;
	Workstation* m_pNextStation;

public:
	Workstation(const std::string& str);

	Workstation(Workstation& w) = delete;
	Workstation& operator=(Workstation& w) = delete;
	Workstation(Workstation && w) = delete;
	Workstation& operator=(Workstation&& w) = delete;

	void runProcess(std::ostream&);
	bool moveOrder();
	void setNextStation(Workstation& station);
	const Workstation* getNextStation() const;
	bool getIfCompleted(CustomerOrder& order);
	void display(std::ostream&) const;
	Workstation& operator+=(CustomerOrder&&);
};
#endif	