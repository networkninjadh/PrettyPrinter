using System;
using Tokens;

namespace Tree
{
	public class Cons : Node
	{
		private Node car;
		private Node cdr;
		private Special form;

		public Cons(Node a, Node d)
		{
			car = a;
			cdr = d;
			parseList ();
		}
		public override Node getCar()
		{
			return this.car;
		}
		public override Node getCdr()
		{
			return this.cdr;
		}public override bool isPair ()
		{
			return true;
		}

		Special parseList()
		{

			if (car.isSymbol ()) {
				String name = car.getName ();
				if (name == "quote")
					return new Quote ();
				else if (name == "lambda")
					return new Lambda ();
				else if (name == "if")
					return new If ();
				else if (name == "begin")
					return new Begin ();
				else if (name == "let")
					return new Let ();
				else if (name == "cond")
					return new Cond ();
				else if (name == "define")
					return new Define ();
				else if (name == "set")
					return new Set ();
				else
					return new Regular ();
			} else
				return new Regular ();
		}
		public override bool isPair ()
		{
			return true;
		}

		public override void print(int n)
		{
			form.print (this, n, false);
		}

		public override void print(int n, bool p)
		{
			form.print (this, n, p);
		}
	}
}
