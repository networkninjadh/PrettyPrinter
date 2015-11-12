// SPP -- The main program of the Scheme pretty printer.

using System;
using Parse;
using Tokens;
using Tree;

public class Scheme4101
{
    public static int Main(string[] args)
    {
        // Create scanner that reads from standard input
        Scanner scanner = new Scanner(Console.In);
        
        if (args.Length > 1 ||
            (args.Length == 1 && ! args[0].Equals("-d")))
        {
            Console.Error.WriteLine("Usage: mono SPP [-d]");
            return 1;
        }
        
        // If command line option -d is provided, debug the scanner.
        if (args.Length == 1 && args[0].Equals("-d"))
        {
            // Console.Write("Scheme 4101> ");
            Token tok = scanner.getNextToken();
            while (tok != null)
            {
                TokenType tt = tok.getType();

                Console.Write(tt);
                if (tt == TokenType.INT)
                    Console.WriteLine(", intVal = " + tok.getIntVal());
                else if (tt == TokenType.STRING)
                    Console.WriteLine(", stringVal = " + tok.getStringVal());
                else if (tt == TokenType.IDENT)
                    Console.WriteLine(", name = " + tok.getName());
                else
                    Console.WriteLine();

                // Console.Write("Scheme 4101> ");
                tok = scanner.getNextToken();
            }
            return 0;
        }

        // Create parser
        TreeBuilder builder = new TreeBuilder();
        Parser parser = new Parser(scanner, builder);
        Node root;

        // Create the built-in environment
        Tree.Environment biEnv = new Environment();
        
        // Built-In Binary Arithmetic Functions
        Ident funcName = new Ident("b+");
            biEnv.define(name, new BuiltIn(name));
        funcName = new Ident("b-");
            biEnv.define(name, new BuiltIn(name));
        funcName = new Ident("b*");
            biEnv.define(name, new BuiltIn(name));
        funcName = new Ident("b/");
            biEnv.define(name, new BuiltIn(name));
        funcName = new Ident("b=");
            biEnv.define(name, new BuiltIn(name));
        funcName = new Ident("b<");
            biEnv.define(name, new BuiltIn(name));
            
        // Built-In IO Functions
        funcName = new Ident("read");
            biEnv.define(name, new BuiltIn(name));
        funcName = new Ident("write");
            biEnv.define(name, new BuiltIn(name));
        funcName = new Ident("display");
            biEnv.define(name, new BuiltIn(name));
        funcName = new Ident("newline");
            biEnv.define(name, new BuiltIn(name));
            
        // Other Built-In Functions
        funcName = new Ident("car");
            biEnv.define(name, new BuiltIn(name));
        funcName = new Ident("cdr");
            biEnv.define(name, new BuiltIn(name));
        funcName = new Ident("set-car!");
            biEnv.define(name, new BuiltIn(name));
        funcName = new Ident("set-cdr!");
            biEnv.define(name, new BuiltIn(name));
        funcName = new Ident("symbol?");
            biEnv.define(name, new BuiltIn(name));
        funcName = new Ident("number?");
            biEnv.define(name, new BuiltIn(name));
        funcName = new Ident("null?");
            biEnv.define(name, new BuiltIn(name));
        funcName = new Ident("pair?");
            biEnv.define(name, new BuiltIn(name));
        funcName = new Ident("eq?");
            biEnv.define(name, new BuiltIn(name));
        funcName = new Ident("procedure?");
            biEnv.define(name, new BuiltIn(name));

        // TODO: Create Top-Level Environment

        // Read-eval-print loop

        // TODO: print prompt and evaluate the expression
        root = (Node) parser.parseExp();
        while (root != null) 
        {
            root.eval(env).print(0);
            root = (Node) parser.parseExp();
        }

        return 0;
    }
}
