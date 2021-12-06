using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using static Runner2.MainWindow;

namespace Runner2.Classes
{
    public abstract class State
    {
        protected Player player;

        public Player Player
        {
            get { return player; }
            set { player = value; }
        }
        
        public abstract void Handle();
        public abstract void ChangeSize(int size);
    }
    public class NormalSizeState : State
    {
        public NormalSizeState(State state)
        {
            player = state.Player;
        }
        public NormalSizeState(Player playerr)
        {
            player = playerr;
        }


        public override void ChangeSize(int size)
        {
            var gameWin = (Application.Current.MainWindow.FindName("MainWin") as Canvas).Children[2] as Canvas;
            var player1 = gameWin.Children[4] as Rectangle;
            player1.Height = size;
            var text = gameWin.Children[10] as Label;
            text.Content = "State: " + player.state.ToString();
        }

        public override void Handle()
        {
            StateChangeCheck();
        }
        private void StateChangeCheck()
        {
            if (player.Points.points<0)
            {
                player.state = new SmallSizeState(this);
                ChangeSize(50);
            }
            else if (player.Points.points>0)
            {
                player.state = new MediumSizeState(this);
                ChangeSize(125);
            }
        }
        public override string ToString()
        {
            return "normal";
        }
    }
    public class SmallSizeState : State
    {
        public SmallSizeState(State state)
        {
            player = state.Player;
        }
        public SmallSizeState(Player playerr)
        {
            player = playerr;
        }

        public override void ChangeSize(int size)
        {
            var gameWin = (Application.Current.MainWindow.FindName("MainWin") as Canvas).Children[2] as Canvas;
            var player1 = gameWin.Children[4] as Rectangle;
            player1.Height = size;
            var text = gameWin.Children[10] as Label;
            text.Content = "State: " + player.state.ToString();
        }

        public override void Handle()
        {
            StateChangeCheck();
        }
        private void StateChangeCheck()
        {
            if (player.Points.points > -1)
            {
                player.state = new NormalSizeState(this);
                ChangeSize(100);
            }
        }
        public override string ToString()
        {
            return "small";
        }
    }
    public class MediumSizeState : State
    {
        public MediumSizeState(State state)
        {
            player = state.Player;
        }
        public MediumSizeState(Player playerr)
        {
            player = playerr;
        }

        public override void ChangeSize(int size)
        {
            var gameWin = (Application.Current.MainWindow.FindName("MainWin") as Canvas).Children[2] as Canvas;
            var player1 = gameWin.Children[4] as Rectangle;
            player1.Height = size;
            var text = gameWin.Children[10] as Label;
            text.Content = "State: " + player.state.ToString();
        }

        public override void Handle()
        {
            StateChangeCheck();
        }
        private void StateChangeCheck()
         {
            if (player.Points.points >3)
            {
                player.state = new LargeSizeState(this);
                ChangeSize(180);
            }
            else if (player.Points.points< 1)
            {
                player.state = new NormalSizeState(this);
                ChangeSize(100);
            }
        }
        public override string ToString()
        {
            return "medium";
        }
    } 
    public class LargeSizeState : State
    {
        public LargeSizeState(State state)
        {
            player = state.Player;
        }
        public LargeSizeState(Player playerr)
        {
            player = playerr;
        }

        public override void ChangeSize(int size)
        {
            var gameWin = (Application.Current.MainWindow.FindName("MainWin") as Canvas).Children[2] as Canvas;
            var player1 = gameWin.Children[4] as Rectangle;
            player1.Height = size;
            var text = gameWin.Children[10] as Label;
            text.Content = "State: " + player.state.ToString();
        }

        public override void Handle()
        {
            StateChangeCheck();
        }
        private void StateChangeCheck()
        {
            if (player.Points.points< 4)
            {
                player.state = new MediumSizeState(this);
                ChangeSize(125);
            }            
        }
        public override string ToString()
        {
            return "large";
        }
    }

}
