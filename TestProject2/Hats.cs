using System;
using Xunit;
using Runner2.Classes;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TestProject2
{

    public class Hats
    {
        [Fact]
        public void TestMagicHatMove()
        {
            Creator playerF = new ConcreteCreator();
            Player player = playerF.FactoryMethod("Owlet");
            CowboyHat playerD = new CowboyHat(player, "player");

            //var gameWin = (Application.Current.MainWindow.FindName("MainWin") as Canvas).Children[2] as Canvas;
            //player = (UIElement)gameWin.FindName(name);
            //var gamewin= playerD.gameWin;
            playerD.moveHat();

        }

    }
}
