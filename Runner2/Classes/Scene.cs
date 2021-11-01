using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

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
        public double width;
        public Color color;
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
        public string spritePath;
    }

    /// <summary>
    /// "product a1" class
    /// </summary>
    class SummerPlatform : Platform
    {
        public SummerPlatform()
        {
            width = 300;
            color = Colors.Green;
        }
    }

    /// <summary>
    /// "product a2" class
    /// </summary>
    class WinterPlatform : Platform
    {
        public WinterPlatform()
        {
            width = 250;
            color = Colors.LightGray;
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

    class SummerItem: Item
    {
        public SummerItem()
        {
            width = 50;
            height = 50;
        }
    } 
    class WinterItem: Item
    {
        public WinterItem()
        {
            width = 50;
            height = 50;
        }
    }
}
