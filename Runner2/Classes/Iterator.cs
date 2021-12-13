using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Runner2.Classes
{
    public interface IAbstractCollection
    {
        Iterator CreateIterator();
    }
    public class Collection : IAbstractCollection
    {
        public List<Rectangle> platforms = new List<Rectangle>();
        public Iterator CreateIterator()
        {
            return new Iterator(this);
        }
        // Gets Rectangle count
        public int Count
        {
            get { return platforms.Count; }
        }
        // Indexer
        public Rectangle this[int index]
        {
            get { return platforms[index]; }
            set { platforms.Add(value); }
        }
        
    }
    public interface IAbstractIterator
    {
        Rectangle First();
        Rectangle Next();
        bool IsDone { get; }
        Rectangle CurrentRectangle { get; }
    }
    /// <summary>
    /// The 'ConcreteIterator' class
    /// </summary>
    public class Iterator : IAbstractIterator
    {
        Collection collection;
        int current = 0;
        int step = 1;
        // Constructor
        public Iterator(Collection collection)
        {
            this.collection = collection;
        }
        // Gets first Rectangle
        public Rectangle First()
        {
            current = 0;
            return collection[current] as Rectangle;
        }
        // Gets next Rectangle
        public Rectangle Next()
        {
            current += step;
            if (!IsDone)
                return collection[current] as Rectangle;
            else
                return null;
        }
        // Gets or sets stepsize
        public int Step
        {
            get { return step; }
            set { step = value; }
        }
        // Gets current iterator Rectangle
        public Rectangle CurrentRectangle
        {
            get { return collection[current] as Rectangle; }
        }
        // Gets whether iteration is complete
        public bool IsDone
        {
            get { return current >= collection.Count; }
        }
    }

}
