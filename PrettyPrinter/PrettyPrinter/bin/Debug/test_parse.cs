
			if (tok == null)
				return null;
			else if (tok.getType() == TokenType.RPAREN) 
			{
				return emptyList;
			}
			else
			{
				//parse the expression
				Node exp = new Node ();
				exp = new Cons(parseExp (tok),emptyList);
				//then figure out what R is
				if (scanner.getNextToken ().getType() == TokenType.DOT) //it is a dot expression 
				{ 
					Node exp2 = new Node ();
					exp2 = new Cons (new Ident("."),new Cons(parseExp(scanner.getNextToken()),emptyList)); 
					return new Cons (exp,exp2);
				} 
				else //its a rest 
				{
					Node rest = new Node ();
					rest = parseRest (scanner.getNextToken ());
					return new Cons (exp,rest);
					//parse the rest
				}
			}
			
