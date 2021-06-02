/* Citation and Sources...
Final Project Milestone
Module: Menu
Filename: Menu.h
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

#ifndef SDDS_MENU_H 
#define SDDS_MENU_H 
#include <iostream>

namespace sdds
{
	class Menu;
	class MenuItem
	{
		char* m_item;
		friend class Menu;
		MenuItem(const char* item);
		~MenuItem();
		MenuItem(const MenuItem& src) = delete;
		MenuItem& operator=(const MenuItem& src) = delete;
		void Item_display() const;
	};
	const int MAX_NO_OF_ITEMS = 10;

	class Menu {
		int m_NOofitem;
		char* m_title;
		MenuItem* m_menuitem[MAX_NO_OF_ITEMS];
		int m_indentation;
		int checkNull;

	public:
		Menu();
		Menu(const char* title, int indentation = 0);
		Menu(const Menu& src);
		~Menu();
		void setEmpty();

		operator bool() const;
		operator int() const;
		bool isEmpty() const;
		Menu& operator=(const Menu& src);
		Menu& operator=(const char* title);
		Menu& operator<<(const char* item);

		void init2(const Menu& src);
		void display() const;
		//void display(std::ostream& os=std::cout) const;
		void add(const char* item);
		int run() const;
		void indent() const;
	};
}
#endif