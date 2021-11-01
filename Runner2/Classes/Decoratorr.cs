using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner2.Classes
{
    /// <summary>
    /// Abstract decorator
    /// </summary>
    public abstract class Decoratorr : Player
    {
        private Player _player;
        public Decoratorr(Player aPlayer)
        {
            this._player = aPlayer;
        }

        public override int SkinType
        {
            get { return _player.SkinType; }
        }

        public override int Points
        {
            get { return _player.Points; }
            set { _player.Points = value; }
        }

        public override float Speed
        {
            get { return _player.Speed; }
            set { _player.Speed = value; }
        }
    }

    public class MagicHat : Decoratorr
    {
        public MagicHat(Player aPlayer) : base(aPlayer)
        {
        }

        public override int SkinType => base.SkinType;

        public override int Points { get => base.Points+10; set => base.Points = value; }
        public override float Speed { get => base.Speed+5; set => base.Speed = value; }
    }
    public class BaseballHat : Decoratorr
    {
        public BaseballHat(Player aPlayer) : base(aPlayer)
        {
        }

        public override int SkinType => base.SkinType;
        public override int Points { get => base.Points+2; set => base.Points = value; }
        public override float Speed { get => base.Speed+2; set => base.Speed = value; }
    }
    public class CowboyHat : Decoratorr
    {
        public CowboyHat(Player aPlayer) : base(aPlayer)
        {
        }

        public override int SkinType => base.SkinType;
        public override int Points { get => base.Points+3; set => base.Points = value; }
        public override float Speed { get => base.Speed+3; set => base.Speed = value; }
    }
}
