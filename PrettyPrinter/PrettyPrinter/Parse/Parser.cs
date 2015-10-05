using System;
using Tokens;
using Tree;

namespace Parse
{
	public class Parser
	{
		private Scanner scanner;
		public Nil emptyList = new Nil ();

		public Parser(Scanner s) { scanner = s;}

		public Node parseExp()
		{
			return parseExp (scanner.getNextToken ());
		}
		private Node parseExp(Token tok)
		{
			if (tok.getType () == TokenType.LPAREN) {
				//open paren
				return parseRest(scanner.getNextToken ());

			} else if (tok.getType () == TokenType.FALSE) {
				//#f
			} else if (tok.getType () == TokenType.TRUE) {
				//#t 
			} else if (tok.getType () == TokenType.INT) {
				//a number
			} else if (tok.getType () == TokenType.IDENT) {
				//an identifier
			} else if (tok.getType () == TokenType.STRING) {
				//a string
			} else if (tok.getType () == TokenType.QUOTE) {
				// a quote
			} else  {
				return null;
			}
		}
		public Node parseRest()
		{
			
		}	
		private Node parseRest(Token tok)
		{
		}
	}
}