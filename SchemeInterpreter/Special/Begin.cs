// Begin -- Parse tree node strategy for printing the special form begin

using System;

namespace Tree
{
    public class Begin : Special
    {
	public Begin() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printBegin(t, n, p);
        }
        
        public override Node eval (Node exp, Environment env)
        {
            Node statement = exp;
            Node nextStatement = statement.getCdr();
            while (!nextStatement.isNull())
            {
                statement.getCdr().eval(statement, env);
                statement = nextStatement;
                nextStatement = nextStatement.getCdr();
            }
            return statement.getCar().eval(statement, env);
            //Console.Error.WriteLine("Error: Eval not implemented for Begin:Special");
            //return Nil.getInstance();
        }
    }
}

