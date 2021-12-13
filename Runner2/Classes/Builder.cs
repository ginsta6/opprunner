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
    public abstract class Builder
    {
        public abstract Rect buildPlatform();
        public abstract void buildBackground();
        public abstract void buildObstacle();
        public abstract Item buildItem();

        public List<Rect> buildPlatforms(int n)
        {
            List<Rect> list = new List<Rect>();
            for (int i = 0; i < n; i++)
            {
                Rect r = buildPlatform();
                list.Add(r);
            }
            return list;
        }

        public List<Item> buildItems(int n)
        {
            List<Item> list = new List<Item>();
            for (int i = 0; i < n; i++)
            {
                Item it = buildItem();
                list.Add(it);
            }
            return list;
        }

        //Template method
        public Tuple<List<Rect>, List<Item>> buildScene(int platN, int itemN)
        {
            buildBackground();
            List<Rect> l1 = buildPlatforms(platN);
            List<Item> l2 = buildItems(itemN);
            buildObstacle();

            return Tuple.Create(l1, l2);
        }

        
    }

    public class SummerBuilder : Builder
    {
        Facade facade;
        public SummerBuilder()
        {
            facade = new Facade();
        }

        public override void buildBackground()
        {
            //return factory.CreateBackground();
        }

        public override Item buildItem()
        {
            var ite = facade.CreateItem("summer");
            //var ite = factory.CreateItem();

            var gameWin = (Application.Current.MainWindow.FindName("MainWin") as Canvas).Children[2] as Canvas;

            //Greitaveikai kaip turi buti kai blogai
            //ImageBrush brush = new ImageBrush();
            //brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/screen-1.jpg"));

            Rectangle rec = new Rectangle()
            {
                Width = ite.width,
                Height = ite.height,
                //Fill = brush,
                Fill = ite.color,
                Stroke = Brushes.LemonChiffon,
                StrokeThickness = 2,
            };

           
            //Geras budas su flaiveitu
            rec.Fill = facade.prod.GetIcon("summer").brush;

            gameWin.Children.Add(rec);
            Canvas.SetTop(rec, facade.random.Next(200, 400));
            Canvas.SetLeft(rec, facade.random.Next(50, 800));

            var item = gameWin.Children[gameWin.Children.Count - 1] as Rectangle;
            ite.hitbox = new Rect(Canvas.GetLeft(item), Canvas.GetTop(item), item.Width, item.Height);

            return ite;
        }

        public override void buildObstacle()
        {
            //return factory.CreateObstacle();
        }

        //paduodi koordinates
        public override Rect buildPlatform()
        {
            var plat = facade.CreatePlatform("summer");

            var gameWin = (Application.Current.MainWindow.FindName("MainWin") as Canvas).Children[2] as Canvas;
            
            Rectangle rec = new Rectangle()
            {
                Width = facade.random.Next(200, 300),
                Height = 32,
                Fill = plat.color,
                Stroke = Brushes.LemonChiffon,
                StrokeThickness = 2,
            };
            gameWin.Children.Add(rec);
            Canvas.SetTop(rec, facade.random.Next(200, 500));
            Canvas.SetLeft(rec, facade.random.Next(50, 800));

            var gamePlatform = gameWin.Children[gameWin.Children.Count - 1] as Rectangle;
            Rect platformHitBox = new Rect(Canvas.GetLeft(gamePlatform), Canvas.GetTop(gamePlatform), gamePlatform.Width, gamePlatform.Height);

            return platformHitBox;

        }
    }
    public class WinterBuilder : Builder
    {
        Facade facade;
        public WinterBuilder()
        {
            facade = new Facade();
        }

        public override void buildBackground()
        {
            //return factory.CreateBackground();
        }

        public override Item buildItem()
        {
            var ite = facade.CreateItem("winter");

            var gameWin = (Application.Current.MainWindow.FindName("MainWin") as Canvas).Children[2] as Canvas;


            Rectangle rec = new Rectangle()
            {
                Width = ite.width,
                Height = ite.height,
                Fill = ite.color,
                Stroke = Brushes.LemonChiffon,
                StrokeThickness = 2,
            };

            //Geras budas su flaiveitu
            rec.Fill = facade.prod.GetIcon("winter").brush;

            gameWin.Children.Add(rec);
            Canvas.SetTop(rec, facade.random.Next(200, 400));
            Canvas.SetLeft(rec, facade.random.Next(50, 800));

            var item = gameWin.Children[gameWin.Children.Count - 1] as Rectangle;
            ite.hitbox = new Rect(Canvas.GetLeft(item), Canvas.GetTop(item), item.Width, item.Height);

            return ite;
        }

        public override void buildObstacle()
        {
            //return factory.CreateObstacle();
        }

        public override Rect buildPlatform()
        {
            var plat = facade.CreatePlatform("winter");

            var gameWin = (Application.Current.MainWindow.FindName("MainWin") as Canvas).Children[2] as Canvas;

            Rectangle rec = new Rectangle()
            {
                Width = facade.random.Next(200, 300),
                Height = 32,
                Fill = plat.color,
                Stroke = Brushes.LemonChiffon,
                StrokeThickness = 2,
            };
            gameWin.Children.Add(rec);
            Canvas.SetTop(rec, facade.random.Next(200, 500));
            Canvas.SetLeft(rec, facade.random.Next(50, 800));

            var gamePlatform = gameWin.Children[gameWin.Children.Count - 1] as Rectangle;
            Rect platformHitBox = new Rect(Canvas.GetLeft(gamePlatform), Canvas.GetTop(gamePlatform), gamePlatform.Width, gamePlatform.Height);

            return platformHitBox;

        }
    }
}
