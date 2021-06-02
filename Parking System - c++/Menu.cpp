/* Citation and Sources...
Final Project Milestone
Module: Menu
Filename: Menu.cpp
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
#include "Menu.h"
#include "Utils.h"
using namespace std;

namespace sdds
{

	MenuItem::MenuItem(const char* item)
	{
		m_item = nullptr;


		if (item != nullptr)
		{
			if (m_item != nullptr)
			{
				delete[] m_item;
				m_item = nullptr;
			}
			m_item = new char[strlen(item) + 1];
			strcpy(m_item, item);
		}
		else
			m_item = nullptr;
	}
	MenuItem::~MenuItem()
	{
		delete[] m_item;
		m_item = nullptr;
	}
	void MenuItem::Item_display() const
	{
		if (m_item != nullptr)
		{
			cout << m_item << endl;
		}
	}
	Menu::Menu()
	{
		setEmpty();
	}
	Menu::Menu(const char* title, int indentation)
	{
		setEmpty();
		if (title != nullptr)
		{
			if (m_title != nullptr)
			{
				delete[] m_title;
				m_title = nullptr;
			}
			m_title = new char[strlen(title) + 1];
			strcpy(m_title, title);

			m_indentation = indentation;

		}
		else
			setEmpty();
	}
	void Menu::setEmpty()
	{
		m_indentation = 0;
		checkNull = 0;
		m_NOofitem = 0;
		m_title = nullptr;
		for (int i = 0; i < MAX_NO_OF_ITEMS; i++)
		{
			m_menuitem[i] = nullptr;
		}

	}
	Menu::operator bool() const
	{
		return !isEmpty();
	}
	Menu::Menu(const Menu& src)
	{
		setEmpty();
		init2(src);
	}
	Menu& Menu::operator=(const Menu& src)
	{
		if (this != &src) {
			delete[] m_title;
			for (int i = 0; i < m_NOofitem; i++)
			{
				delete m_menuitem[i];
			}
			setEmpty();
			init2(src);
		}
		return *this;
	}
	void Menu::init2(const Menu& src)
	{
		m_indentation = src.m_indentation;
		checkNull = src.checkNull;
		m_NOofitem = src.m_NOofitem;

		if (src.m_title != nullptr && src.m_menuitem != nullptr) {
			if (m_title != nullptr)
			{
				delete[] m_title;
				m_title = nullptr;
			}
			if (m_menuitem != nullptr)
			{
				for (int i = 0; i < m_NOofitem; i++)
				{
					delete m_menuitem[i];
					m_menuitem[i] = nullptr;
				}
			}
			m_title = new char[strlen(src.m_title) + 1];
			strcpy(m_title, src.m_title);

			for (int i = 0; i < src.m_NOofitem; i++)
			{
				m_menuitem[i] = new MenuItem(src.m_menuitem[i]->m_item);
			}

		}
		else
			setEmpty();
	}
	bool Menu::isEmpty() const
	{
		return m_title == nullptr;
	}
	void Menu::indent() const
	{
		string space = "    ";
		for (int i = 0; i < m_indentation; i++)
			cout << space;
	}
	void Menu::display() const
	{

		if (checkNull == 0)
		{
			if (isEmpty())
				cout << "Invalid Menu!" << endl;
			else if (m_menuitem[0] == nullptr)
			{
				indent();
				cout << m_title << endl;
				cout << "No Items to display!" << endl;
			}
			else
			{
				indent();
				cout << m_title << endl;

				for (int i = 0; i < MAX_NO_OF_ITEMS; i++)
				{
					if (m_menuitem[i] == nullptr)
					{
						indent();
						cout << "> ";
						i = MAX_NO_OF_ITEMS;
					}
					else
					{
						indent();
						cout << i + 1 << "- ";
						m_menuitem[i]->Item_display();
					}

				}
			}
		}
	}
	/*	std::ostream& operator<<(std::ostream& os, const Menu& menu)
		{
			menu.display1(os);
			return os;
		}*/
		/*void Menu::display() const
		{
			display1(cout);
		}*/
	Menu& Menu::operator=(const char* title)
	{
		if (title != nullptr)
		{
			if (m_title != nullptr)
			{
				delete[] m_title;
				m_title = nullptr;
			}
			m_title = new char[strlen(title) + 1];
			strcpy(m_title, title);
		}
		else
		{
			delete[] m_title;
			m_title = nullptr;
		}

		return *this;
	}
	Menu::~Menu()
	{
		delete[] m_title;
		m_title = nullptr;
		for (int i = 0; i < m_NOofitem; i++) {
			delete m_menuitem[i];
			m_menuitem[i] = nullptr;
		}

	}
	void Menu::add(const char* item)
	{
		if (item != nullptr && m_title != nullptr) //item is valid, menu is not empty
		{
			if (m_NOofitem < MAX_NO_OF_ITEMS)
			{
				m_menuitem[m_NOofitem] = new MenuItem(item); //dynamic allocation where it's empty while it copies item
				m_NOofitem++;				// Num++
			}
		}
		else
		{
			delete[] m_title;
			for (int i = 0; i < m_NOofitem; i++)
				delete m_menuitem[i];
			setEmpty();

		}
	}

	Menu& Menu::operator<<(const char* item)
	{

		if (item != nullptr)
		{
			if (checkNull == 0 && m_NOofitem <= MAX_NO_OF_ITEMS)
				add(item);
			else
				cout << "Invalid Menu!" << endl;
		}
		else
			checkNull++;
		return *this;
	}
	int Menu::run() const
	{
		int input = 0;

		display();
		if (!isEmpty() && m_menuitem[0] != nullptr)
		{
			Utils::getInt(input, 1, m_NOofitem);
		}
		return input;
	}
	Menu::operator int() const
	{
		return run();
	}
}