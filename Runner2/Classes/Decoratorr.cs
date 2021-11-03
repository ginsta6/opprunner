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
        private Player _player;
        public Canvas gameWin { get; }
        public UIElement player { get; }
        public Decoratorr(Player aPlayer)
        {
            this._player = aPlayer;
            gameWin = (Application.Current.MainWindow.FindName("MainWin") as Canvas).Children[2] as Canvas;
            player = gameWin.Children[4];
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
        //private ImageBrush _image = new ImageBrush();
        public int index { get; set; }
        FrameworkElement hat;
        public MagicHat(Player aPlayer) : base(aPlayer)
        {
            index = 6;
            hat = base.gameWin.Children[index] as FrameworkElement;
        }
        public void moveHat()
        {
            Canvas.SetTop(hat, Canvas.GetTop(base.player) - hat.Height / 2 - 5);
            Canvas.SetLeft(hat, Canvas.GetLeft(base.player) - hat.Width / 3 - 5);
        }
        public override int SkinType => base.SkinType;
        public override int Points { get => base.Points; set => base.Points = value; }
        public override float Speed { get => base.Speed; set => base.Speed = value; }
    }
    public class BaseballHat : Decoratorr
    {
        public int index { get; set; }
        FrameworkElement hat;
        public BaseballHat(Player aPlayer) : base(aPlayer)
        {
            index = 7;
            hat = base.gameWin.Children[index] as FrameworkElement;
        }
        public void moveHat()
        {
            Canvas.SetTop(hat, Canvas.GetTop(base.player)  - 5);
            Canvas.SetLeft(hat, Canvas.GetLeft(base.player));
        }
        public override int SkinType => base.SkinType;
        public override int Points { get => base.Points; set => base.Points = value; }
        public override float Speed { get => base.Speed; set => base.Speed = value; }
    }
    public class CowboyHat : Decoratorr
    {
        public int index { get; set; }
        FrameworkElement hat;
        public CowboyHat(Player aPlayer) :  base(aPlayer)
        {
            index = 8;
            hat = base.gameWin.Children[index] as FrameworkElement;
        }
        public void moveHat()
        {
            Canvas.SetTop(hat, Canvas.GetTop(base.player) - hat.Height / 2 - 5);
            Canvas.SetLeft(hat, Canvas.GetLeft(base.player) - hat.Width / 2 - 5);
        }
        public override int SkinType => base.SkinType;
        public override int Points { get => base.Points; set => base.Points = value; }
        public override float Speed { get => base.Speed; set => base.Speed = value; }
    }
}
