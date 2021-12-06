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
    /// Abstract decorator
    /// </summary>
    public abstract class Decoratorr : Player
    {
        public Player _player;
        public Canvas gameWin { get; }
        public UIElement player { get; }
        public Decoratorr(Player aPlayer, string name)
        {
            this._player = aPlayer;
            gameWin = (Application.Current.MainWindow.FindName("MainWin") as Canvas).Children[2] as Canvas;
            player = (UIElement) gameWin.FindName(name);
        }

        public override int SkinType
        {
            get { return _player.SkinType; }
        }

        public override PointsCounter Points
        {
            get { return _player.Points; }
            set { _player.Points = value; }
        }

        public override float Speed
        {
            get { return _player.Speed; }
            set { _player.Speed = value; }
        }
        //public override State State
        //{
        //    get { return _player.state; }
        //    set { _player.state = value; }
        //}
        public override void Update()
        {
        }
        public override void RemoveHats()
        {
        }
        public override void Request()
        {
        }
    }

    public class MagicHat : Decoratorr
    {
        //private ImageBrush _image = new ImageBrush();
        public int index { get; set; }
        FrameworkElement hat;
        public MagicHat(Player aPlayer, string name) : base(aPlayer, name)
        {
            _player = aPlayer;
            index = 6;
            hat = base.gameWin.Children[index] as FrameworkElement;
        }
        public void moveHat()
        {
            Canvas.SetTop(hat, Canvas.GetTop(base.player) - hat.Height / 2 - 5);
            Canvas.SetLeft(hat, Canvas.GetLeft(base.player) - hat.Width / 3 - 5);
        }
        public override int SkinType => base.SkinType;
        public override PointsCounter Points { get => base.Points; set => base.Points = value; }
        public override float Speed { get => base.Speed; set => base.Speed = value; }
        public override State state { get; set; }

        public override void Update()
        {
            moveHat();
            this._player.Update();
        }
        public override void RemoveHats()
        {
            base.gameWin.Children[index].Visibility = Visibility.Hidden;
            this._player.RemoveHats();
        }

        public override void Request()
        {
            base.Request();
        }
    }
    public class BaseballHat : Decoratorr
    {
        public int index { get; set; }
        FrameworkElement hat;
        public BaseballHat(Player aPlayer, string name) : base(aPlayer, name)
        {
            _player = aPlayer;
            index = 7;
            hat = base.gameWin.Children[index] as FrameworkElement;
        }
        public void moveHat()
        {
            Canvas.SetTop(hat, Canvas.GetTop(base.player)  - 5);
            Canvas.SetLeft(hat, Canvas.GetLeft(base.player));
        }
        public override int SkinType => base.SkinType;
        public override PointsCounter Points { get => base.Points; set => base.Points = value; }
        public override float Speed { get => base.Speed; set => base.Speed = value; }
        public override State state { get; set; }
        public override void Update()
        {
            moveHat();
            this._player.Update();
        }
        public override void RemoveHats()
        {
            base.gameWin.Children[index].Visibility = Visibility.Hidden;
            this._player.RemoveHats();
        }
        public override void Request()
        {
            base.Request();
        }
    }
    public class CowboyHat : Decoratorr
    {
        public int index { get; set; }
        FrameworkElement hat;
        public CowboyHat(Player aPlayer, string name) : base(aPlayer, name)
        {
            _player = aPlayer;
            index = 8;
            hat = base.gameWin.Children[index] as FrameworkElement;
        }
        public void moveHat()
        {
            Canvas.SetTop(hat, Canvas.GetTop(base.player) - hat.Height / 2 - 5);
            Canvas.SetLeft(hat, Canvas.GetLeft(base.player) - hat.Width / 2 - 5);
        }
        public override int SkinType => base.SkinType;
        public override PointsCounter Points { get => base.Points; set => base.Points = value; }
        public override float Speed { get => base.Speed; set => base.Speed = value; }
        public override State state { get; set; }
        public override void Update()
        {
            moveHat();
            this._player.Update();
        }
        public override void RemoveHats()
        {
            base.gameWin.Children[index].Visibility = Visibility.Hidden;
            this._player.RemoveHats();
        }
        public override void Request()
        {
            base.Request();
        }
    }
}
