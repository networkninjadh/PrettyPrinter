using System;
using Tokens;
using Tree;
using System.Runtime.Remoting.Messaging;
using System.Linq.Expressions;

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
			if (tok == null)
				return emptyList;
			else if (tok.getType () == TokenType.LPAREN) 
			{
				return parseRest ();
			} 
			else if (tok.getType () == TokenType.FALSE) 
			{
				return new BoolLit (false);
			} 
			else if (tok.getType () == TokenType.TRUE) 
			{
				return new BoolLit (true);
			} 
			else if (tok.getType () == TokenType.INT) 
			{
				return new IntLit (tok.getIntVal ());
			} 
			else if (tok.getType () == TokenType.IDENT) 
			{
				return new Ident (tok.getName ());
			} 
			else if (tok.getType () == TokenType.STRING) 
			{
				return new StringLit (tok.getStringVal ());
			} 
			else if (tok.getType () == TokenType.QUOTE) 
			{
				return new Cons (new Ident("'"),parseExp(scanner.getNextToken()));
			} 
			else if (tok.getType () == TokenType.DOT) 
			{
				Console.WriteLine("Unexpected Token: .");
				return null;
			}
			else  
			{
				Console.WriteLine ("Token does not exist");
				return null;
			}
		}

		private Node parseRest()
		{
			Token tok = scanner.getNextToken ();
			if (tok == null)
				return null;
			if (tok.getType () == TokenType.RPAREN)
			{
				return emptyList;
			} 
			else if (tok.getType () == TokenType.DOT) 
			{
				return new Cons (parseExp (), parseRest ());
			}
			else 
			{
				return new Cons (parseExp (tok), parseRest ());
			}
		}
	}
}