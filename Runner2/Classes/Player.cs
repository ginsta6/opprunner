using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Runner2.Classes
{
    public interface IClonable
    {
        IClonable shallowCopy();
        IClonable deepCopy();
    }
    /// <summary>
    /// Product
    /// </summary>
    public abstract class Player : IClonable
    {
        public abstract State state { get; set; }
        public abstract int SkinType { get; }
        public abstract PointsCounter Points { get; set; }
        public abstract float Speed { get; set; }

        public Symbol symbol;

        public abstract void Update();
        public abstract void RemoveHats();
        public IClonable deepCopy()
        {
            Player other = (Player)this.MemberwiseClone();
            other.Points = (PointsCounter)this.Points.deepCopy();
            return other;
        }

        public IClonable shallowCopy()
        {
            return (Player)this.MemberwiseClone();

        }
        public abstract void Request(int ind);

    }

    /// <summary>
    /// concrete product
    /// </summary>
    class PinkMonster : Player
    {
        private State _state;
        private readonly int _skinType;
        private PointsCounter _points;
        private float _speed;
        private ImageBrush _image = new ImageBrush();

        public PinkMonster(float speed)
        {
            _skinType = 1;
            _points = new PointsCounter();
            _speed = speed;
            _image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pinkjump4.png"));
            var gameWin = (Application.Current.MainWindow.FindName("MainWin") as Canvas).Children[2] as Canvas;
            var player = gameWin.Children[3] as Rectangle;
            player.Fill = _image;
            _state = new NormalSizeState(this);
        }

        public override int SkinType
        {
            get { return _skinType; }
        }

        public override PointsCounter Points
        {
            get { return _points; }
            set { _points = value; }

        }

        public override float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public override State state
        {
            get { return _state; }
            set { _state = value; }
        }

        public override void Update()
        {
        }
        public override void RemoveHats()
        {
        }
        public override void Request(int ind)
        {
            _state.Handle(ind);
        }

    }

    /// <summary>
    /// concrete product2
    /// </summary>
    class OwlMonster : Player
    {
        private State _state;
        private readonly int _skinType;
        private PointsCounter _points;
        private float _speed;

        public OwlMonster(float speed)
        {
            _skinType = 2;
            _points = new PointsCounter();
            _speed = speed;
            _state = new NormalSizeState(this);
        }

        public override int SkinType
        {
            get { return _skinType; }
        }

        public override PointsCounter Points
        {
            get { return _points; }
            set { _points = value; }

        }
        public override float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        public override void Update()
        {
        }
        public override void RemoveHats()
        {
        }
        public override State state
        {
            get { return _state; }
            set { _state = value; }
        }
        public override void Request(int ind)
        {
            _state.Handle(ind);
        }
    }
    /// <summary>
    /// concrete product3
    /// </summary>
    class DudeMonster : Player
    {
        private State _state;
        private readonly int _skinType;
        private PointsCounter _points;
        private float _speed;

        public DudeMonster(float speed)
        {
            _skinType = 3;
            _points = new PointsCounter();
            _speed = speed;
            _state = new NormalSizeState(this);
        }

        public override int SkinType
        {
            get { return _skinType; }
        }

        public override PointsCounter Points
        {
            get { return _points; }
            set { _points = value; }

        }
        public override float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        public override void Update()
        {
        }
        public override void RemoveHats()
        {
        }
        public override State state
        {
            get { return _state; }
            set { _state = value; }
        }
        public override void Request(int ind)
        {
            _state.Handle(ind);
        }
    }

    public class PointsCounter : IClonable
    {
        public int points;

        public PointsCounter()
        {
            points = 0;
        }
        public void ResetPoints()
        {
            points = 0;
        }
        public void AddPoints(int add)
        {
            points += add;
        }
        public void SubtractPoints(int sub)
        {
            points -= sub;
        }

        public IClonable shallowCopy()
        {
            return (IClonable)this.MemberwiseClone();
        }

        public IClonable deepCopy()
        {
            PointsCounter other = new PointsCounter();
            other.AddPoints(this.points);
            return other;
        }
    }

    /// <summary>
    /// "creator"
    /// </summary>
    public abstract class Creator
    {
        public abstract Player FactoryMethod(string type);
    }

    /// <summary>
    /// "concrete creator"
    /// </summary>
    public class ConcreteCreator : Creator
    {
        public override Player FactoryMethod(string type)
        {
            switch (type)
            {
                case "Pink":
                    return new PinkMonster(10);
                case "Owlet":
                    return new OwlMonster(12);
                case "Dude":
                    return new DudeMonster(8);
                default:
                    return null;
            }
        }
    }
}
