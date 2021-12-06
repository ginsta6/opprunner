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
    public class Facade
    {
        Player player;
        Creator playerF;
        AbstractSceneFactory sceneF;
        Builder builder;

        SummerFactory SF;
        WinterFactory WF;

        ImageBrush backgroundSprite;
        ImageBrush obstacleSprite;

        int itemStartInd;
        int itemEndInd;
        int platStartInd;
        int platEndInd;

        public Facade()
        {
            SF = new SummerFactory();
            WF = new WinterFactory();
        }

        public Player CreatePlayer(int typeToCreate)
        {
            playerF = new ConcreteCreator();
            switch (typeToCreate)
            {
                case 1:
                    player = playerF.FactoryMethod("Pink");
                    break;
                case 2:
                    player = playerF.FactoryMethod("Owlet");
                    break;
                case 3:
                    player = playerF.FactoryMethod("Dude");
                    break;
                default:
                    break;
            }
            
            return player;
        }

        public void UsePotion(Player player, string potionType)
        {
            Potion pot;
            switch (potionType)
            {
                case "speedUp":
                    //Create potion effect
                    pot = new SpeedUpPotion();
                    //Use potion effect
                    pot.algorithm.giveEffect(player);
                    break;
                case "speedDown":
                    pot = new SpeedDownPotion();
                    pot.algorithm.giveEffect(player);
                    break;
                
            }
            
        }

        public Item CreateItem(string type)
        {
            Item ite; 
            switch (type)
            {
                case "summer":
                    ite = SF.CreateItem();
                    break;
                case "winter":
                    ite = WF.CreateItem();
                    break;
                default:
                    ite = WF.CreateItem();
                    break;
            }

            return ite;
        }
        public Platform CreatePlatform(string type)
        {
            Platform ite; 
            switch (type)
            {
                case "summer":
                    ite = SF.CreatePlatform();
                    break;
                case "winter":
                    ite = WF.CreatePlatform();
                    break;
                default:
                    ite = WF.CreatePlatform();
                    break;
            }

            return ite;
        }


        //private void CreateScene(int level, ref List<FrameworkElement> gamePlatforms, ref List<Rect> platformHitBoxes, ref List<Rect> itemHitBoxes,
        //    ref List<FrameworkElement> canvasItems, ref Obstacle obs, ref Rect obstacleHitBox, )
        //{
        //    var GameWin = (Application.Current.MainWindow.FindName("MainWin") as Canvas).Children[2] as Canvas;

        //    switch (level)
        //    {
        //        case 1:
        //            sceneF = new SummerFactory();
        //            builder = new SummerBuilder();
        //            break;
        //        case 2:
        //            for (int i = itemStartInd; i < itemEndInd; i++)
        //            {
        //                GameWin.Children.Remove(GameWin.Children[itemStartInd]);
        //            }
        //            for (int i = platStartInd; i < platEndInd; i++)
        //            {
        //                GameWin.Children.Remove(GameWin.Children[platStartInd]);
        //            }

        //            gamePlatforms = new List<FrameworkElement>();
        //            platformHitBoxes = new List<Rect>();
        //            itemHitBoxes = new List<Rect>();
        //            canvasItems = new List<FrameworkElement>();

        //            sceneF = new WinterFactory();
        //            builder = new WinterBuilder();
        //            break;
        //    }



        //    //------------Background------------------------------------
        //    var bg = sceneF.CreateBackground();
        //    backgroundSprite.ImageSource = new BitmapImage(new Uri(bg.spritePath));

        //    //------------Ground-Platform------------------------------
        //    var plat = sceneF.CreatePlatform();
        //    (GameWin.Children[2] as Rectangle).Fill = plat.color;

        //    //-----------------Obstacle-----------------------------

        //    obs = sceneF.CreateObstacle();
        //    obstacleSprite.ImageSource = new BitmapImage(new Uri(obs.spritePath));

        //    var obstacle = GameWin.Children[5] as Rectangle;
        //    Canvas.SetLeft(obstacle, 947);
        //    obstacleHitBox = new Rect(Canvas.GetLeft(obstacle), Canvas.GetTop(obstacle), obstacle.Width, obstacle.Height);
        //    obstacle.Fill = obstacleSprite;
        //    obstacle.Visibility = Visibility.Visible;

        //    //--------Player-copy--------------------------------------
        //    currentPlayerDeepCopy = (Player)currentPlayer.deepCopy();
        //    opposingPlayerDeepCopy = (Player)opposingPlayer.deepCopy();

        //    currentPlayerShallowCopy = (Player)currentPlayer.shallowCopy();
        //    opposingPlayerShallowCopy = (Player)opposingPlayer.shallowCopy();

        //    //--------------------------platforms---------------------
        //    int[] width = new int[] { 229, 299, 411, 200 };
        //    int[] topPositions = new int[] { 510, 316, 310, 201 };
        //    int[] leftPositions = new int[] { 397, 46, 789, 539 };

        //    platStartInd = GameWin.Children.Count;


        //    for (int i = 0; i < 4; i++)
        //    {
        //        var platHitBox = builder.buildPlatform(width[i], 32, topPositions[i], leftPositions[i]);
        //        gamePlatforms.Add(GameWin.Children[GameWin.Children.Count - 1] as Rectangle);
        //        platformHitBoxes.Add(platHitBox);
        //    }

        //    platEndInd = GameWin.Children.Count;

        //    //-------------items----------------------------

        //    int[] itemTopPositions = new int[] { 400, 200, 250, 190 };
        //    int[] itemLeftPositions = new int[] { 397, 46, 789, 539 };

        //    itemStartInd = GameWin.Children.Count;

        //    for (int i = 0; i < 4; i++)
        //    {
        //        var ite = builder.buildItem(itemTopPositions[i], itemLeftPositions[i]);
        //        canvasItems.Add(GameWin.Children[GameWin.Children.Count - 1] as Rectangle);
        //        items.Add(ite);
        //        itemHitBoxes.Add(ite.hitbox);
        //    }

        //    itemEndInd = GameWin.Children.Count;

        //}

    }
}
