// Environment.java -- a data structure for Scheme environments

// An Environment is a list of frames.  Each frame represents a scope
// in the program and contains a set of name-value pairs.  The first
// frame in the environment represents the innermost scope.

// For the code below, I am assuming that a frame is implemented
// as an association list, i.e., a list of two-element lists.  E.g.,
// the association list ((x 1) (y 2)) associates the value 1 with x
// and the value 2 with y.

// To implement environments in an object-oriented style, it would
// be better to define a Frame class and make an Environment a list
// of such Frame objects or to implement a frame as a hash table.

// You need the following methods for modifying environments:
//  - constructors:
//      - create the empty environment (an environment with an empty frame)
//      - add an empty frame to the front of an existing environment
//  - lookup the value for a name (for implementing variable lookup)
//      if the name exists in the innermost scope, return the value
//      if it doesn't exist, look it up in the enclosing scope
//      if we don't find the name, it is an error
//  - define a name (for implementing define and parameter passing)
//      if the name already exists in the innermost scope, update the value
//      otherwise add a name-value pair as first element to the innermost scope
//  - assign to a name (for implementing set!)
//      if the name exists in the innermost scope, update the value
//      if it doesn't exist, perform the assignment in the enclosing scope
//      if we don't find the name, it is an error

using System;

namespace Tree
{
    public class Environment : Node
    {
        // An Environment is implemented like a Cons node, in which
        // every list element (every frame) is an association list.
        // Instead of Nil(), we use null to terminate the list.

        private Node frame;     	// the innermost scope, an assoc list
	    private Environment env;	// the enclosing environment
   
        public Environment()
        {
            frame = Nil.getInstance();
            env = null;
        }
   
        public Environment(Environment e)
	   {
            frame = Nil.getInstance();
            env = e;
        }

        public override void print(int n) {
            // there got to be a more efficient way to print n spaces
            for (int i = 0; i < n; i++)
                Console.Write(' ');
            Console.WriteLine("#{Environment");
            if (frame != null)
                frame.print(Math.Abs(n) + 4);
            if (env != null)
                env.print(Math.Abs(n) + 4);
            for (int i = 0; i < Math.Abs(n); i++)
                Console.Write(' ');
            Console.WriteLine('}');
        }

        // This is not in an object-oriented style, it's more or less a
        // translation of the Scheme assq function.
        private static Node find(Node id, Node alist)
        {
            if (! alist.isPair())
                return null;	// in Scheme we'd return #f
            else
            {
                Node bind = alist.getCar();
                if (id.getName().Equals(bind.getCar().getName()))
                    // return a list containing the value as only element
                    return bind.getCdr();
                else
                    return find(id, alist.getCdr());
            }
        }


        public Node lookup(Node id)
        {
            Node val = find(id, frame);
            if (val == null && env == null)
            {
                Console.Error.WriteLine("undefined variable " + id.getName());
                return null;
            }
            else if (val == null)
                // look up the identifier in the enclosing scope
                return env.lookup(id);
            else
                // get the value out of the list we got from find()
		return val.getCar();
        }


        public void define(Node id, Node val)
        {
            // TODO: Must add Nodes to the associated list of Nodes (Frame)
            // ASSOCIATED LIST: Cons:Node as root, with Car as Cons:Node of first variable and Cdr as next Cons:Node "root"
            //  **ALSO MUST CHECK to make sure that value doesn't already exist. Use Find function, if null then DNE, else reassign Car of returned node to the val Node (parameter)
            Node temp = find(id, frame);
            
            // Variable already exists... reassignment clause
            if(temp != null)
            {
                temp.setCar(val);
                return;
            }
            
            // Variable DOES NOT exist...
            // Create new Cons:Node with the following state:
            //      Cdr = old frame
            //      Car = Cons:Node with
            //              Car = Node id
            //              Cdr = Cons:Node with:
            //                  Car = Node val
            //                  Cdr = null
            Cons value = new Cons(val, null);
            Cons ident = new Cons(id, value);
            Cons root = new Cons(ident, frame);
            This.frame = root;
        }


        public void assign(Node id, Node val)
        {
            // TODO: Use the Find function to get the Cons:Node whose Car is the current value, reassign Car to val Node (parameter)
            //         If Find returns null, then no such variable exists
        
            Node temp = find(id, frame)
            
            // Variable exists...
            if(temp != null)
            {
                temp.setCdr(val);
                return;
            }
            
            // Else
            Console.Error.WriteLine("Error: Undefined Variable " + id.getName());
        }
        
        public Node eval(Node fun, Environment env) 
        {
            Console.Error.WriteLine("Error: Environment Cannot be eval()"); ; 
        }
    }
}
