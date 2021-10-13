using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner2.Classes
{
    /// <summary>
    /// "player" abstract class
    /// </summary>
    abstract class Player
    { 
        public abstract int SkinType { get; }
        public abstract int Points { get; set; }
        public abstract float Speed { get; set; }
    }

    /// <summary>
    /// concrete player class
    /// </summary>
    class PinkMonster : Player
    {
        private readonly int _skinType;
        private int _points;
        private float _speed;

        public PinkMonster(float speed)
        {
            _skinType = 0;
            _points = 0;
            _speed = speed;
        }

        public override int SkinType
        {
            get { return _skinType; }
        }

        public override int Points 
        { 
            get { return _points; }
            set { _points = value; }

        }

        public override float Speed 
        {
            get { return _speed; }
            set { _speed = value; }
        }
    }

    /// <summary>
    /// concrete player class
    /// </summary>
    class OwlMonster : Player
    {
        private readonly int _skinType;
        private int _points;
        private float _speed;

        public OwlMonster(float speed)
        {
            _skinType = 1;
            _points = 0;
            _speed = speed;
        }

        public override int SkinType
        {
            get { return _skinType; }
        }

        public override int Points 
        { 
            get { return _points; }
            set { _points = value; }

        }

        public override float Speed 
        {
            get { return _speed; }
            set { _speed = value; }
        }
    }
    /// <summary>
    /// concrete player class
    /// </summary>
    class DudeMonster : Player
    {
        private readonly int _skinType;
        private int _points;
        private float _speed;

        public DudeMonster(float speed)
        {
            _skinType = 3;
            _points = 0;
            _speed = speed;
        }

        public override int SkinType
        {
            get { return _skinType; }
        }

        public override int Points 
        { 
            get { return _points; }
            set { _points = value; }

        }

        public override float Speed 
        {
            get { return _speed; }
            set { _speed = value; }
        }
    }

    /// <summary>
    /// "creator" Abstract class
    /// </summary>
    abstract class PlayerFactory
    {
        public abstract Player GetPlayer();
    }

    /// <summary>
    /// "concrete creator" class
    /// </summary>
    class PinkMonsterFactory : PlayerFactory
    {
        private float _speed;

        public PinkMonsterFactory(float speed)
        {
            _speed = speed;
        }

        public override Player GetPlayer()
        {
            return new PinkMonster(_speed);
        }
    }

    /// <summary>
    /// "concrete creator" class
    /// </summary>
    class OwlMonsterFactory : PlayerFactory
    {
        private float _speed;

        public OwlMonsterFactory(float speed)
        {
            _speed = speed;
        }

        public override Player GetPlayer()
        {
            return new OwlMonster(_speed);
        }
    } 
    /// <summary>
    /// "concrete creator" class
    /// </summary>
    class DudeMonsterFactory : PlayerFactory
    {
        private float _speed;

        public DudeMonsterFactory(float speed)
        {
            _speed = speed;
        }

        public override Player GetPlayer()
        {
            return new DudeMonster(_speed);
        }
    }
}
