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
    /// <summary>
    /// Product
    /// </summary>
    public abstract class Player
    {
        public abstract int SkinType { get;  }
        public abstract int Points { get; set; }
        public abstract float Speed { get; set; }
    }

    /// <summary>
    /// concrete product
    /// </summary>
    class PinkMonster : Player
    {
        private readonly int _skinType;
        private int _points;
        private float _speed;
        private ImageBrush _image = new ImageBrush();

        public PinkMonster(float speed)
        {
            _skinType = 1;
            _points = 0;
            _speed = speed;
            _image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pinkjump4.png"));
            var gameWin = (Application.Current.MainWindow.FindName("MainWin") as Canvas).Children[2] as Canvas;
            var player = gameWin.Children[3] as Rectangle;
            player.Fill = _image;
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
    /// concrete product2
    /// </summary>
    class OwlMonster : Player
    {
        private readonly int _skinType;
        private int _points;
        private float _speed;

        public OwlMonster(float speed)
        {
            _skinType = 2;
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
    /// concrete product3
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
    /// "creator"
    /// </summary>
    abstract class Creator
    {
        public abstract Player FactoryMethod(string type);
    }

    /// <summary>
    /// "concrete creator"
    /// </summary>
    class ConcreteCreator : Creator
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
