using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner2.Classes
{
    public class Memento
    {
        State state;
        int size;

        public Memento(State state, int size)
        {
            this.state = state;
            this.size = size;
        }

        public State State
        {
            get { return state; }
        }
        public int Size
        {
            get { return size; }
        }
    }

    public class Caretaker
    {
        Memento memento;

        public Memento Memento
        {
            set { memento = value; }
            get { return memento; }
        }
    }
}
