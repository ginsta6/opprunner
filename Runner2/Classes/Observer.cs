using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner2.Classes
{
    public interface IObserver
    {
        void Update<T>(T message);
    }

    public abstract class Subject
    {
        List<IObserver> Observers;

        public Subject()
        {
            Observers = new List<IObserver>();
        }

        public void Register(IObserver obs)
        {
            Observers.Add(obs);
        }
        public void Unregister(IObserver obs)
        {
            Observers.Remove(obs);
        }
        public void Notify<T>(T message)
        {
            Observers.ForEach(o => o.Update(message));
        }
    }
}
