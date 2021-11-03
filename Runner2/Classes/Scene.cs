using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Runner2.Classes;
using Runner2.Commands;
using Runner2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Runner2.Classes
{
    /// <summary>
    /// "abstract factory" interface
    /// </summary>
    abstract class AbstractSceneFactory
    {
        public abstract Platform CreatePlatform();
        public abstract Background CreateBackground();
        public abstract Obstacle CreateObstacle();

        public abstract Item CreateItem();
    }

    /// <summary>
    /// "concrete factory1" class
    /// </summary>
    class SummerFactory : AbstractSceneFactory
    {
        int platformCount = 5;
        public override Platform CreatePlatform()
        {
            return new SummerPlatform();
        }

        public override Background CreateBackground()
        {
            return new SummerBackground();
        }
        public override Obstacle CreateObstacle()
        {
            return new SummerObstacle();
        }

        public override Item CreateItem()
        {
            return new SummerItem();
        }
    }

    /// <summary>
    /// "concrete factory2" class
    /// </summary>
    class WinterFactory : AbstractSceneFactory
    {
        public override Platform CreatePlatform()
        {
            return new WinterPlatform();
        }

        public override Background CreateBackground()
        {
            return new WinterBackground();
        }
        public override Obstacle CreateObstacle()
        {
            return new WinterObstacle();
        }

        public override Item CreateItem()
        {
            return new WinterItem();
        }
    }

    /// <summary>
    /// "abstract product A" interface
    /// </summary>
    abstract class Platform
    {
        public int[] width;
        public Color color;
        public int number;
        public int height;
        public int[] topPositions;
        public int[] leftPositions;
        public int startIndex;
        public int endIndex;
    }

    /// <summary>
    /// "abstract product B" interface
    /// </summary>
    abstract class Background
    {
        public string spritePath;
    }

    abstract class Obstacle
    {
        public int height;
        public string spritePath;
    }
    abstract class Item
    {
        public int height;
        public int width;
        public Color color;
        public string spritePath;
        public int number;
        public int[] topPositions;
        public int[] leftPositions;
        public int startIndex;
        public int endIndex;
    }

    /// <summary>
    /// "product a1" class
    /// </summary>
    class SummerPlatform : Platform
    {
        public SummerPlatform()
        {
            width = new int[] { 229, 299, 411, 200 };
            number = 4;
            height = 32;
            topPositions = new int[] { 510, 316, 310, 201 };
            leftPositions = new int[] { 397, 46, 789, 539 };
            color = Colors.Green;
            var gameWin = (Application.Current.MainWindow.FindName("MainWin") as Canvas).Children[2] as Canvas;
            for (int i = 0; i < number; i++)
            {
                Rectangle rec = new Rectangle()
                {
                    Width = width[i],
                    Height = height,
                    Fill = Brushes.Green,
                    Stroke = Brushes.LemonChiffon,
                    StrokeThickness = 2,
                };
                gameWin.Children.Add(rec);
                Canvas.SetTop(rec, topPositions[i]);
                Canvas.SetLeft(rec, leftPositions[i]);
            }
            endIndex = gameWin.Children.Count;
            startIndex = gameWin.Children.Count - number;
        }
    }

    /// <summary>
    /// "product a2" class
    /// </summary>
    class WinterPlatform : Platform
    {
        public WinterPlatform()
        {
            width = new int[] { 250, 2 };
            color = Colors.LightGray;
            width = new int[] { 229, 299, 411, 200 };
            number = 4;
            height = 32;
            topPositions = new int[] { 510, 316, 310, 201 };
            leftPositions = new int[] { 397, 46, 789, 539 };
            var gameWin = (Application.Current.MainWindow.FindName("MainWin") as Canvas).Children[2] as Canvas;
            for (int i = 0; i < number; i++)
            {
                Rectangle rec = new Rectangle()
                {
                    Width = width[i],
                    Height = height,
                    Fill = Brushes.LightBlue,
                    Stroke = Brushes.Blue,
                    StrokeThickness = 2,
                };
                gameWin.Children.Add(rec);
                Canvas.SetTop(rec, topPositions[i]);
                Canvas.SetLeft(rec, leftPositions[i]);
            }
            endIndex = gameWin.Children.Count;
            startIndex = gameWin.Children.Count - number;
        }
    }

    /// <summary>
    /// "product b1" class
    /// </summary>
    class SummerBackground : Background
    {
        public SummerBackground()
        {
            spritePath = "pack://application:,,,/Images/fonas1.png";
        }

    }

    /// <summary>
    /// "product b2" class
    /// </summary>
    class WinterBackground : Background
    {
        public WinterBackground()
        {
            spritePath = "pack://application:,,,/Images/fonas2.png";
        }
    }

    class SummerObstacle : Obstacle
    {
        public SummerObstacle()
        {
            spritePath = "pack://application:,,,/Images/obs1.png";
        }
    }
    class WinterObstacle : Obstacle
    {
        public WinterObstacle()
        {
            spritePath = "pack://application:,,,/Images/obs2.png";
        }
    }

    class SummerItem : Item
    {
        public SummerItem()
        {
            width = 50;
            height = 50;
            color = Colors.Yellow;
            width = 50;
            height = 50;
            color = Colors.Magenta;

            number = 4;
            topPositions = new int[] { 400, 200, 250, 190 };
            leftPositions = new int[] { 397, 46, 789, 539 };
            var gameWin = (Application.Current.MainWindow.FindName("MainWin") as Canvas).Children[2] as Canvas;

            for (int i = 0; i < number; i++)
            {
                Rectangle rec = new Rectangle()
                {
                    Width = width,
                    Height = height,
                    Fill = Brushes.BlueViolet,
                    Stroke = Brushes.LemonChiffon,
                    StrokeThickness = 2,
                };

                gameWin.Children.Add(rec);
                Canvas.SetTop(rec, topPositions[i]);
                Canvas.SetLeft(rec, leftPositions[i]);
            }
            endIndex = gameWin.Children.Count;
            startIndex = gameWin.Children.Count - number;
        }
    }
    class WinterItem : Item
    {
        public WinterItem()
        {
            width = 50;
            height = 50;
            color = Colors.Magenta;

            number = 4;
            //Random rnd = new Random(650, 1200);
            topPositions = new int[] { 400, 300, 300, 190 };
            leftPositions = new int[] { 397, 46, 789, 539 };
            var gameWin = (Application.Current.MainWindow.FindName("MainWin") as Canvas).Children[2] as Canvas;

            for (int i = 0; i < number; i++)
            {
                Rectangle rec = new Rectangle()
                {
                    Width = width,
                    Height = height,
                    Fill = Brushes.BlueViolet,
                    Stroke = Brushes.LemonChiffon,
                    StrokeThickness = 2,
                };

                gameWin.Children.Add(rec);
                Canvas.SetTop(rec, topPositions[i]);
                Canvas.SetLeft(rec, leftPositions[i]);
            }
            endIndex = gameWin.Children.Count;
            startIndex = gameWin.Children.Count - number;
        }


    }
}
