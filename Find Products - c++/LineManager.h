/* ===========================================================
Student: Heebin Lee
ID: 130464191
Date: 2020.11.28
-----------------------------------------------------------
I have done all the coding by myself and only copied the code
that my professor provided to complete my workshops and assignments.
-----------------------------------------------------------*/
#ifndef LINEMANAGER_H 
#define LINEMANAGER_H 

#include <iostream>
#include <string>
#include <vector>
#include<deque>
#include "Workstation.h"
class LineManager {
	std::vector<Workstation*> AssemblyLine;
	std::deque<CustomerOrder> ToBeFilled;
	std::deque<CustomerOrder> Completed;
	unsigned int m_cntCustomerOrder = 0;

	Workstation* m_firstStation = nullptr;
public:
	LineManager(const std::string& filename, std::vector<Workstation*>& v_ws, std::vector<CustomerOrder>& v_co );
	bool run(std::ostream& os);
	void displayCompletedOrders(std::ostream& os) const;
	void displayStations() const;
	void displayStationsSorted() const;
};
#endif