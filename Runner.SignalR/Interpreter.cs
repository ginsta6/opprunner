using Runner.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Runner.SignalR
{
    public class Interpreter
    {
        RunnerHub Hub;
        public Interpreter(RunnerHub hub)
        {
            Hub = hub;
        }

        public async void interpret(string input)
        {
            //splita realaus inputo
            var strings = input.Split(' ');

            Context context = new Context(strings[1]);

            //exp tree
            List<Expression> tree = new List<Expression>();
            tree.Add(new ThousandExpression());
            tree.Add(new HundredExpression());
            tree.Add(new TenExpression());
            tree.Add(new OneExpression());

            //antros dalies šifravima i skaiciu

            foreach (Expression exp in tree)
            {
                exp.Interpret(context);
            }

            Console.WriteLine(context.Output);

            //realaus inputo pirmos dalies patikrinima kad zinot kuria komanda leist
            //vykdymas komandos MEDIS
            if (strings[0] == "+")
            {
                await Hub.SendAddPointsSignal(context.Output);
            }
            else if (strings[0] == "-")
            {
                await Hub.SendRemovePointsSignal(context.Output);
            }


        }

    }

    public class Context
    {
        string input;
        int output;
        
        public Context(string input)
        {
            this.input = input;
        }
        public string Input
        {
            get { return input; }
            set { input = value; }
        }
        public int Output
        {
            get { return output; }
            set { output = value; }
        }
    }
   


    public abstract class Expression
    {
        public void Interpret(Context context)
        {
            if (context.Input.Length == 0)
                return;
            if (context.Input.StartsWith(Nine()))
            {
                context.Output += (9 * Multiplier());
                context.Input = context.Input.Substring(1);
            }
            else if (context.Input.StartsWith(Four()))
            {
                context.Output += (4 * Multiplier());
                context.Input = context.Input.Substring(1);
            }
            else if (context.Input.StartsWith(Five()))
            {
                context.Output += (5 * Multiplier());
                context.Input = context.Input.Substring(1);
            }
            while (context.Input.StartsWith(One()))
            {
                context.Output += (1 * Multiplier());
                context.Input = context.Input.Substring(1);
            }
        }
        public abstract string One();
        public abstract string Four();
        public abstract string Five();
        public abstract string Nine();
        public abstract int Multiplier();
    }
    


    public class ThousandExpression : Expression
    {
        public override string One() { return "p"; }
        public override string Four() { return "o"; }
        public override string Five() { return "n"; }
        public override string Nine() { return "m"; }
        public override int Multiplier() { return 1000; }
    }
    


    public class HundredExpression : Expression
    {
        public override string One() { return "l"; }
        public override string Four() { return "k"; }
        public override string Five() { return "j"; }
        public override string Nine() { return "i"; }
        public override int Multiplier() { return 100; }
    }
    


    public class TenExpression : Expression
    {
        public override string One() { return "h"; }
        public override string Four() { return "g"; }
        public override string Five() { return "f"; }
        public override string Nine() { return "e"; }
        public override int Multiplier() { return 10; }
    }
    


    public class OneExpression : Expression
    {
        public override string One() { return "d"; }
        public override string Four() { return "c"; }
        public override string Five() { return "b"; }
        public override string Nine() { return "a"; }
        public override int Multiplier() { return 1; }
    }
}


