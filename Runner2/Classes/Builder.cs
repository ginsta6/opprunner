﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Runner2.Classes
{
    public abstract class Builder
    {
        public abstract Rect buildPlatform(int w, int h, int top, int left);
        public abstract void buildBackground();
        public abstract void buildObstacle();
        public abstract void buildItem();

        
    }

    public class SummerBuilder : Builder
    {
        AbstractSceneFactory factory;
        public SummerBuilder()
        {
            factory = new SummerFactory();
        }

        public override void buildBackground()
        {
            //return factory.CreateBackground();
        }

        public override void buildItem()
        {
            //return factory.CreateItem();
        }

        public override void buildObstacle()
        {
            //return factory.CreateObstacle();
        }

        //paduodi koordinates
        public override Rect buildPlatform(int w, int h, int top, int left)
        {
            var plat = factory.CreatePlatform();

            var gameWin = (Application.Current.MainWindow.FindName("MainWin") as Canvas).Children[2] as Canvas;
            
            Rectangle rec = new Rectangle()
            {
                Width = w,
                Height = h,
                Fill = Brushes.Green,
                Stroke = Brushes.LemonChiffon,
                StrokeThickness = 2,
            };
            gameWin.Children.Add(rec);
            Canvas.SetTop(rec, top);
            Canvas.SetLeft(rec, left);

            var gamePlatform = gameWin.Children[gameWin.Children.Count - 1] as Rectangle;
            Rect platformHitBox = new Rect(Canvas.GetLeft(gamePlatform), Canvas.GetTop(gamePlatform), gamePlatform.Width, gamePlatform.Height);

            return platformHitBox;

        }
    }
    public class WinterBuilder : Builder
    {
        AbstractSceneFactory factory;
        public WinterBuilder()
        {
            factory = new WinterFactory();
        }

        public override void buildBackground()
        {
            //return factory.CreateBackground();
        }

        public override void buildItem()
        {
            //return factory.CreateItem();
        }

        public override void buildObstacle()
        {
            //return factory.CreateObstacle();
        }

        public override Rect buildPlatform(int w, int h, int top, int left)
        {
            var plat = factory.CreatePlatform();

            var gameWin = (Application.Current.MainWindow.FindName("MainWin") as Canvas).Children[2] as Canvas;

            Rectangle rec = new Rectangle()
            {
                Width = w,
                Height = h,
                Fill = Brushes.Green,
                Stroke = Brushes.LemonChiffon,
                StrokeThickness = 2,
            };
            gameWin.Children.Add(rec);
            Canvas.SetTop(rec, top);
            Canvas.SetLeft(rec, left);

            var gamePlatform = gameWin.Children[gameWin.Children.Count] as Rectangle;
            Rect platformHitBox = new Rect(Canvas.GetLeft(gamePlatform), Canvas.GetTop(gamePlatform), gamePlatform.Width, gamePlatform.Height);

            return platformHitBox;

        }
    }
}