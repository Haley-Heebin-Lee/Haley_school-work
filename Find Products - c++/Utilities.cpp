/* ===========================================================
Student: Heebin Lee
ID: 130464191
Date: 2020.11.28
-----------------------------------------------------------
I have done all the coding by myself and only copied the code
that my professor provided to complete my workshops and assignments.
-----------------------------------------------------------*/
#include "Utilities.h"
using namespace std;

char Utilities::m_delimiter;
	void Utilities::setFieldWidth(size_t newWidth)
	{
		m_widthField = newWidth;
	}
	size_t Utilities::getFieldWidth() const
	{
		return m_widthField;
	}
	string Utilities::extractToken(const string& str, size_t& next_pos, bool& more)
	{
		string token;
		size_t first_pos = next_pos;
		next_pos = str.find(m_delimiter, first_pos);
			if (next_pos != string::npos)
			{
				if (first_pos == next_pos)
				{
					more = false;
					throw "Invalid Token !!";
				}
				else {
					token = str.substr(first_pos, next_pos- first_pos);
					if (m_widthField < token.length())
						setFieldWidth(token.length());
					more = true;
				}
				
			}
			else
			{
				token = str.substr(first_pos);
				if (m_widthField < token.length())
					setFieldWidth(token.length());
				more = false;
			}
			next_pos++;

		
		return token;
	}
	void Utilities::setDelimiter(char newDelimiter)
	{
		m_delimiter = newDelimiter;
	}
	char Utilities::getDelimiter()
	{
		return m_delimiter;
	}