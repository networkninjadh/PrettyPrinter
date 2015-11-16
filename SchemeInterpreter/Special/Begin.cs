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
            Node statementRoot = exp.getCdr();
            Node statementExpression = Nil.getInstance();
            Node result = Nil.getInstance();
            
            while (statementRoot != Nil.getInstance())
            {
                statementExpression = statementRoot.getCar();
                
                result = statementExpression.eval(statementExpression, env);
                statementRoot = statementRoot.getCdr();
            }
            
            return result;
        }
    }
}

