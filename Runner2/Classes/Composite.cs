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
    public abstract class Clothing
    {
        public string name;

        public Clothing(string name)
        {
            this.name = name;
        }

        public abstract void Add(Clothing c);
        public abstract void Remove(Clothing c);
        public abstract float CalculatePointsModifier();

        public abstract void Display(int indent);
    }

    /// <summary>
    /// Leaft
    /// </summary>
    public class ClothingUpgrade : Clothing
    {
        public ClothingUpgrade(string name) : base(name)
        {

        }

        public override void Add(Clothing c)
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

        public override void Remove(Clothing c)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Composite
    /// </summary>
    public class ClothingComposite : Clothing
    {
        List<Clothing> elements = new List<Clothing>();

        public ClothingComposite(string name) : base(name)
        {

        }

        public override void Add(Clothing c)
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
            foreach (Clothing c in elements)
            {
                c.Display(indent + 2);
            }
        }

        public override void Remove(Clothing c)
        {
            elements.Remove(c);
        }
    }
}
