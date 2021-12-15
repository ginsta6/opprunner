using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner2.Classes
{
    /// <summary>
    /// Abstract visitor
    /// </summary>
    public interface IVisitor
    {
        void VisitConcretePlayer(PinkMonster player);
        void VisitConcretePlayer(DudeMonster player);
        void VisitConcretePlayer(OwlMonster player);
    }


    /// <summary>
    /// Concrete Visitor
    /// </summary>
    public class PlayerVisitor : IVisitor
    {
        public void VisitConcretePlayer(PinkMonster player)
        {
            player.isInverted = true;
        }

        public void VisitConcretePlayer(DudeMonster player)
        {
            player.jumpInverted = true;
        }

        public void VisitConcretePlayer(OwlMonster player)
        {
            player.gravityInverted = true;
        }
    }

    /// <summary>
    /// Abstract element
    /// </summary>
    public interface IElement
    {
        void Accept(IVisitor visitor);
    }
}
