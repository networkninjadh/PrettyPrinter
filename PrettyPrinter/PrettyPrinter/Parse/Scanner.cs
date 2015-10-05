using System;
using System.IO;
using Tokens;
using Tree;
using System.Globalization;
using System.Collections;

namespace Parse
{
	public class Scanner
	{
		private TextReader In;
		private const int BUFSIZE = 1000;
		private char[] buf = new char[BUFSIZE];
	
		public Scanner(TextReader i) { In = i;}
		public static bool isInitial(char c)
		{
			if (isLetter (c) || isSpecialInitial (c))
				return true;
			else
				return false;
		}
		public static bool isSpecialInitial(char c)
		{
			if (c == '!' || c == '?' || c == '$' ||
			    c == '%' || c == '&' || c == '*' || c == '/' || c == ':' || c == '<' ||
			    c == '=' || c == '>' || c == '^' || c == '_' || c == '~')
				return true;
			else
				return false;
		}
		public static bool isSubsequent(char c)
		{
			if (isNumber (c) || isInitial (c) || isSpecialSubsequent(c))
				return true;
			else
				return false;
		}
		public static bool isLetter(char c)
		{
			if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
				return true;
			else
				return false;
		}
		public static bool isSpecialSubsequent(char c)
		{
			if (c == '+' || c == '-' || c == '@' || c == '.')
				return true;
			else
				return false;
		}
		public static bool isPeculiarIdentifier(char c)
		{
			if (c == '+' || c == '-')
				return true;
			else
				return false;
		}
	
		public static bool isNumber(char c)
		{
			if ((c >= '0' && c <= '9'))
				return true;
			else
				return false;
		}
		public static bool isWhiteSpace(char c)
		{
			if (c == ' ' || c == '\t' || c == '\f' || c == '\r' || c == '\v' || c == '\n')
				return true;
			else
				return false;
		}
		public Token getNextToken()
		{
			int ch;
			try
			{
				ch = In.Read();
				// skip white space
				if (isWhiteSpace(Convert.ToChar(ch)))
				{
					while (isWhiteSpace(Convert.ToChar(ch)))
					{
						ch = In.Read();
					}
				}
				// skip single line comments
				if (ch == ';')
				{
					while (true)
					{
						ch = In.Read();
						if (ch == '\n')
						{
							ch = In.Read();
							break;
						}
					}
				}
				if (ch == -1)
					return null;
				// Special characters
				else if (ch == '\'')
					return new Token(TokenType.QUOTE);
				else if (ch == '(')
					return new Token(TokenType.LPAREN);
				else if (ch == ')')
					return new Token(TokenType.RPAREN);
				else if (ch == '.')
					return new Token(TokenType.DOT);

				// Boolean constants
				else if (ch == '#')
				{
					ch = In.Read();
					if (ch == '|') 
					{
						ch = In.Read();
						while (true)
						{
							ch = In.Read();
							if (ch == '|')
							{
								ch = In.Read();
								if (ch == '#')
								{
									ch = In.Read();
									break;
								}
							}
						}
					} 
					if (ch == 't')
						return new Token(TokenType.TRUE);
					else if (ch == 'f')
						return new Token(TokenType.FALSE);
					else if (ch == -1)
					{
						Console.Error.WriteLine("Unexpected EOF following #");
						return null;
					}
					else
					{
						Console.Error.WriteLine("Illegal character '" + 
							(char)ch + "' following #");
						return getNextToken();
					}
				}
				// String constants 
				else if (ch == '"')
				{
					ch = In.Read();
					int i = 0;
					while (true)
					{
						if (ch == '"')
							break;
						else
						{
							buf[i] = Convert.ToChar(ch);
							ch = In.Read();
							i++;
						}
					}
					return new StringToken(new String(buf, 0, i));
				}

				//Integer constants
				else if (ch >= '0'&& ch <= '9')
				{
					int[] nums = new int[1000];
					int i = 0;
					int finalNum = 0; 
					while (true)
					{
						nums[i] = ch - '0';
						i++;
						ch = In.Read();
						if (!isNumber(Convert.ToChar(ch)))
						{
							Console.WriteLine();
							Console.WriteLine("Non integer character will be ignored");
							break;
						}
						else if (isWhiteSpace(Convert.ToChar(ch)))
							break;
					}
					int power = i-1;
					for (int j = 0;j<i;j++)
					{
						finalNum = finalNum + nums[j] * Convert.ToInt32(Math.Pow (10, power));
						power--;
					}
						return new IntToken(finalNum);
				}

				// Identifiers
				if (isInitial(Convert.ToChar(ch)))
				{
					int i = 0;
					while (true)
					{
						buf[i] = Convert.ToChar(ch);
						i++;
						if (!isSubsequent(Convert.ToChar(ch)))
							break;
						ch = In.Read();
					}
					return new IdentToken(new String(buf,0,i));
				}
				else if (isPeculiarIdentifier(Convert.ToChar(ch)))
				{
					buf[0] = Convert.ToChar(ch);
					return new IdentToken(new string(buf,0,1));
				}

				// Illegal character
				else
				{
					Console.Error.WriteLine("Illegal input character '"
						+ (char)ch + '\'');
					return getNextToken();
				}
			}
			catch (IOException e)
			{
				Console.Error.WriteLine("IOExceptions: " + e.Message);
				return null;
			}
		}

	}
}
