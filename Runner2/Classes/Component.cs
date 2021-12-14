using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner2.Classes
{
    /// <summary>
    /// Component
    /// </summary>
    public abstract class Component
    {
        public string name;
        public int pointMult;

        public Component(string name)
        {
            this.name = name;
        }

        public abstract void Add(Component c);
        public abstract void Remove(string c);
        public abstract float CalculatePointsModifier();

        public abstract void Display(int indent);
    }

    /// <summary>
    /// Leaf
    /// </summary>
    public class Trinket : Component
    {
        public Trinket(string name) : base(name)
        {
            this.pointMult = 1;
        }

        public override void Add(Component c)
        {
            throw new NotImplementedException();
        }

        public override float CalculatePointsModifier()
        {
            throw new NotImplementedException();
        }

        public override void Display(int indent)
        {
            Console.WriteLine( new String('-', indent) + " " + name);
        }

        public override void Remove(string c)
        {
            throw new NotImplementedException();
        }
    }
    public class Pensil : Component
    {
        public Pensil(string name) : base(name)
        {
            this.pointMult = 3;
        }

        public override void Add(Component c)
        {
            throw new NotImplementedException();
        }

        public override float CalculatePointsModifier()
        {
            throw new NotImplementedException();
        }

        public override void Display(int indent)
        {
            Console.WriteLine( new String('-', indent) + " " + name);
        }

        public override void Remove(string c)
        {
            throw new NotImplementedException();
        }
    }
    
    public class Rubber : Component
    {
        public Rubber(string name) : base(name)
        {
            this.pointMult = 4;
        }

        public override void Add(Component c)
        {
            throw new NotImplementedException();
        }

        public override float CalculatePointsModifier()
        {
            throw new NotImplementedException();
        }

        public override void Display(int indent)
        {
            Console.WriteLine( new String('-', indent) + " " + name);
        }

        public override void Remove(string c)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Composite
    /// </summary>
    public class Composite : Component
    {
        public List<Component> elements = new List<Component>();

        public Composite(string name, int points) : base(name)
        {
            this.pointMult = points;
        }

        public override void Add(Component c)
        {
            elements.Add(c);
        }

        public override float CalculatePointsModifier()
        {
            throw new NotImplementedException();
        }

        public override void Display(int indent)
        {
            Console.WriteLine(new String('-', indent) +
                "+ " + name);
            // Display each child element on this node
            foreach (Component c in elements)
            {
                c.Display(indent + 2);
            }
        }

        public override void Remove(string c)
        {
            elements.RemoveAll(x => x.name == c);
        }
    }
}
