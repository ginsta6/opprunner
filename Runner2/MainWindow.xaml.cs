using Microsoft.AspNetCore.SignalR.Client;
using Runner2.Classes;
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

namespace Runner2
{

    public partial class MainWindow : Window, IObserver
    {
        SignalRService rService;

        DispatcherTimer gameTimer = new DispatcherTimer();

        Rect playerHitBox;
        Rect groundHitBox;
        Rect platformHitBox;
        Rect obstacleHitBox;
        Rect itemHitBox;

        bool jumping;

        PlayerAnimationState playerAnimationCurrentState;
        enum PlayerAnimationState
        {
            RunningLeft,
            RunningRight,
            Standing
        }

        PlayerFactory playerF;
        Player currentPlayer;

        ItemFactory itemF;

        int force = 20;
        int speed = 10;


        int currentPlayerTypeIndex = 1;
        int maxPlayerTypeIndex = 3;


        Random rnd = new Random();

        bool gameOver;

        double spriteIndex = 0;
        double spriteIndexJump = 0;

        ImageBrush playerSprite = new ImageBrush();
        ImageBrush backgroundSprite = new ImageBrush();
        ImageBrush obstacleSprite = new ImageBrush();
        ImageBrush avatarSprite = new ImageBrush();

        int[] obstaclePosition = { 320, 310, 300, 305, 315 };

        int score = 0;

        private int CurrentPlayers;


        public MainWindow()
        {
            HubConnection connection = new HubConnectionBuilder()           //Connecting to hub
                .WithUrl("http://localhost:5000/runner").Build();

            rService = new SignalRService(connection);                      //Creating service with the connection

            rService.Register(this);
            
            rService.Connect();

            InitializeComponent();
            MainWin.Focus();

            gameTimer.Tick += GameEngine;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);

            backgroundSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/background.gif"));
            avatarSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/avatarpink.png"));
            //avatarSprite.ImageSource= new BitmapImage(new Uri("pack://application:,,,/Images/newRunner_08.gif"));
            avatar.Fill = avatarSprite;

            background.Fill = backgroundSprite;
            background2.Fill = backgroundSprite;

            //StartGame();
            //CharacterTypeSelected.Content = currentPlayerTypeIndex.ToString();
        }

        // ----------------------------------------------------------------------------------------------------------
        // SignalR functions

       
        private async Task SendPlayerToServer(string name)
        {
            await rService.SendPlayerMessage(name);
        }

        private async Task SendSignalToServer(string methodName)
        {
            await rService.SendSignalToServer(methodName);
        }

        // ------------------------------------------------------------------------------------------------------------
        // Game engine functions

        private void StartGame()
        {
            CantPlayText.Visibility = Visibility.Hidden;

            Canvas.SetLeft(background, 0);
            Canvas.SetLeft(background2, 1262);

            Canvas.SetLeft(player, 110);
            Canvas.SetTop(player, 140);

            Canvas.SetLeft(obstacle, 950);
            Canvas.SetTop(obstacle, 310);

            RunSprite(1);

            obstacleSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/obstacle.png"));
            obstacle.Fill = obstacleSprite;

            jumping = false;
            gameOver = false;
            score = 0;
            playerAnimationCurrentState = PlayerAnimationState.Standing;

            scoreText.Content = "Score: " + score;

            obstacle.Visibility = Visibility.Visible;
            player.Visibility = Visibility.Visible;
            background.Visibility = Visibility.Visible;
            background2.Visibility = Visibility.Visible;
            scoreText.Visibility = Visibility.Visible;

            avatar.Visibility = Visibility.Hidden;
            //platform.Visibility = Visibility.Hidden;

            gameTimer.Start();
        }

        private void GameEngine(object sender, EventArgs e)
        {
            //Canvas.SetLeft(background, Canvas.GetLeft(background) - currentPlayer.Speed);
            //Canvas.SetLeft(background2, Canvas.GetLeft(background2) - currentPlayer.Speed);

            if (Canvas.GetLeft(background) < -1262)
                Canvas.SetLeft(background, Canvas.GetLeft(background2) + background2.Width);


            if (Canvas.GetLeft(background2) < -1262)
                Canvas.SetLeft(background2, Canvas.GetLeft(background) + background.Width);

            Canvas.SetTop(player, Canvas.GetTop(player) + speed);
            //Canvas.SetLeft(obstacle, Canvas.GetLeft(obstacle) - currentPlayer.Speed);
            //Canvas.SetLeft(item, Canvas.GetLeft(item) - currentPlayer.Speed);

            scoreText.Content = "Score: " + score;

            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width - 15, player.Height);
            obstacleHitBox = new Rect(Canvas.GetLeft(obstacle), Canvas.GetTop(obstacle), obstacle.Width, obstacle.Height);
            itemHitBox = new Rect(Canvas.GetLeft(item), Canvas.GetTop(item), item.Width, item.Height);
            groundHitBox = new Rect(Canvas.GetLeft(ground), Canvas.GetTop(ground), ground.Width, ground.Height);
            platformHitBox = new Rect(Canvas.GetLeft(platform), Canvas.GetTop(platform), platform.Width, platform.Height);

            if (playerHitBox.IntersectsWith(groundHitBox))
            {
                speed = 0;
                Canvas.SetTop(player, Canvas.GetTop(ground) - player.Height);
                jumping = false;

            }
            if (playerHitBox.IntersectsWith(platformHitBox))
            {
                speed = 0;
                Canvas.SetTop(player, Canvas.GetTop(platform) - player.Height);
                jumping = false;
            }

            if (playerAnimationCurrentState == PlayerAnimationState.RunningLeft || playerAnimationCurrentState == PlayerAnimationState.RunningRight)
            {
                spriteIndex += .5;

                if (spriteIndex > 6)
                    spriteIndex = 1;

                //RunSprite(spriteIndex);
            }

            if (jumping == true)
            {
                speed = -9;
                force -= 1;
            }
            else
                speed = 8;

            if (force < 0)
                jumping = false;

            if (Canvas.GetLeft(obstacle) < -50)
            {
                Canvas.SetLeft(obstacle, 950);
                Canvas.SetTop(obstacle, obstaclePosition[rnd.Next(0, obstaclePosition.Length)]);
                score += 1;
            }

            if (Canvas.GetLeft(item) < -50)
            {
                Canvas.SetLeft(item, 2000);
            }

            if (playerHitBox.IntersectsWith(obstacleHitBox))
            {
                gameOver = true;
                gameTimer.Stop();
            }

            if (playerHitBox.IntersectsWith(itemHitBox))
            {
                Canvas.SetLeft(item, 2000);

                switch (rnd.Next(1, 3))
                {
                    case 1:
                        itemF = new GoodItemFactory();
                        break;
                    case 2:
                        itemF = new BadItemFactory();
                        break;
                    default:
                        break;

                }

                scoreText.Content = "labasssss";
                var potion = itemF.CreatePotion();

                //scoreText.Content = "labukas :*";
                //currentPlayer.Speed += potion.speedMod;
            }

            if (gameOver == true)
            {
                obstacle.Stroke = Brushes.Black;
                obstacle.StrokeThickness = 1;

                player.Stroke = Brushes.Red;
                player.StrokeThickness = 1;

                scoreText.Content = "Score: " + score + " Press Enter to play again!!";
            }
            else
            {
                player.StrokeThickness = 0;
                obstacle.StrokeThickness = 0;
            }

            AnimatePlayer(spriteIndex);
            MovePlayer();
        }

        // ------------------------------------------------------------------------------------------------------------
        // Key input functions

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && gameOver == true)
            {
                StartGame();
            }

            if (e.Key == Key.Left)
            {
                playerAnimationCurrentState = PlayerAnimationState.RunningLeft;
            }
            else if (e.Key == Key.Right)
            {
                playerAnimationCurrentState = PlayerAnimationState.RunningRight;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && gameOver == false && jumping == false)
            {
                //renameLater();
                jumping = true;
                force = 15;
                speed = -3;

                //spriteIndexJump += .5;

                //if (spriteIndexJump > 6)
                //    spriteIndexJump = 1;

                //RunSpriteJump(spriteIndexJump);

                //playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/newRunner_02.gif"));
            }
            if (e.Key == Key.Left || e.Key == Key.Right)
            {
                playerAnimationCurrentState = PlayerAnimationState.Standing;
            }
        }

        // -------------------------------------------------------------------------------------------------------------
        // Button press functions

        private void cycleCharacterTypeLeftBtn_Click(object sender, RoutedEventArgs e)
        {
            currentPlayerTypeIndex--;
            if (currentPlayerTypeIndex == 0)
                currentPlayerTypeIndex = maxPlayerTypeIndex;
            //CharacterTypeSelected.Content = currentPlayerTypeIndex.ToString();
            changeAvatar(currentPlayerTypeIndex);
        }
        private void cycleCharacterTypeRightBtn_Click(object sender, RoutedEventArgs e)
        {
            currentPlayerTypeIndex++;
            if (currentPlayerTypeIndex > maxPlayerTypeIndex)
                currentPlayerTypeIndex = 1;
            //CharacterTypeSelected.Content = currentPlayerTypeIndex.ToString();
            changeAvatar(currentPlayerTypeIndex);
        }

        private void joinLobbyBtnClick(object sender, RoutedEventArgs e)
        {
            if (nameInput.Text.Length > 0)
            {
                CantJoinLobbyText.Visibility = Visibility.Hidden;
                setActiveLobbyObjs();
                //players.Content = nameInput.Text;
                CreatePlayer();
                SendPlayerToServer(nameInput.Text);
            }
            else
            {
                CantJoinLobbyText.Visibility = Visibility.Visible;
            }
        }

        private void startBtnClick(object sender, RoutedEventArgs e)
        {
            //uzkomentinau kad tikrint changus butu lengviau 
            if (CurrentPlayers < 2)
            {
                CantPlayText.Visibility = Visibility.Visible;
            }
            else
            {
                MainBackground.Visibility = Visibility.Hidden;
                startGameBtn.Visibility = Visibility.Hidden;
                players.Visibility = Visibility.Hidden;
                titlePlayers.Visibility = Visibility.Hidden;
                avatar.Visibility = Visibility.Hidden;
                //platform.Visibility = Visibility.Hidden;


                obstacle.Visibility = Visibility.Visible;
                item.Visibility = Visibility.Visible;
                player.Visibility = Visibility.Visible;
                background.Visibility = Visibility.Visible;
                background2.Visibility = Visibility.Visible;
                scoreText.Visibility = Visibility.Visible;


                SendSignalToServer("SendStartSignal");
                //StartGame();
            }

        }

        // -------------------------------------------------------------------------------------------------------------
        // Logic functions

        private void MovePlayer()
        {
            player.RenderTransformOrigin = new Point(0.5, 0.5);
            ScaleTransform flipTrans = new ScaleTransform();
            if (playerAnimationCurrentState == PlayerAnimationState.RunningLeft)
            {
                flipTrans.ScaleX = -1;
                Canvas.SetLeft(player, Canvas.GetLeft(player) - currentPlayer.Speed);
            }
            else if (playerAnimationCurrentState == PlayerAnimationState.RunningRight)
            {
                flipTrans.ScaleX = 1;
                Canvas.SetLeft(player, Canvas.GetLeft(player) + currentPlayer.Speed);
            }

            player.RenderTransform = flipTrans;
        }

        private void CreatePlayer()
        {
            switch (currentPlayerTypeIndex)
            {
                case 1:
                    playerF = new PinkMonsterFactory(10);
                    break;
                case 2:
                    playerF = new OwlMonsterFactory(12);
                    break;
                case 3:
                    playerF = new DudeMonsterFactory(8);
                    break;
                default:
                    break;
            }

            currentPlayer = playerF.GetPlayer();
        }

        private void setActiveLobbyObjs()
        {
            title.Visibility = Visibility.Hidden;
            startGameBtn.Visibility = Visibility.Hidden;
            nameInput.Visibility = Visibility.Hidden;
            joinLobbyBtn.Visibility = Visibility.Hidden;
            cycleCharacterTypeLeftBtn.Visibility = Visibility.Hidden;
            cycleCharacterTypeRightBtn.Visibility = Visibility.Hidden;
            CharacterTypeSelected.Visibility = Visibility.Hidden;

            titlePlayers.Visibility = Visibility.Visible;
            startGameBtn.Visibility = Visibility.Visible;
            players.Visibility = Visibility.Visible;
        }

        private void changeAvatar(int index)
        {
            switch (index)
            {
                case 1:
                    avatarSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/avatarpink.png"));
                    CharacterTypeSelected.Content = "Pink monster";
                    break;
                case 2:
                    avatarSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/avatarowlet1.png"));
                    CharacterTypeSelected.Content = "Owlet monster";
                    break;
                case 3:
                    avatarSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/avatardude.png"));
                    CharacterTypeSelected.Content = "Dude monster";
                    break;
            }
            avatar.Fill = avatarSprite;
        }

        private void StartCountDown()
        {
            StartGame();
        }

        // -------------------------------------------------------------------------------------------------------------------
        // Animation functions

        private void RunSprite(double i)
        {
            if (currentPlayerTypeIndex == 1)
            {
                switch (i)
                {
                    case 1:
                        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pink1.png"));
                        break;
                    case 2:
                        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pink2.png"));
                        break;
                    case 3:
                        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pink3.png"));
                        break;
                    case 4:
                        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pink4.png"));
                        break;
                    case 5:
                        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pink5.png"));
                        break;
                    case 6:
                        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pink6.png"));
                        break;
                    default:
                        break;
                }
            }
            else if (currentPlayerTypeIndex == 2)
            {
                switch (i)
                {
                    case 1:
                        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/owlet/owlet1.png"));
                        break;
                    case 2:
                        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/owlet/owlet2.png"));
                        break;
                    case 3:
                        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/owlet/owlet3.png"));
                        break;
                    case 4:
                        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/owlet/owlet4.png"));
                        break;
                    case 5:
                        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/owlet/owlet5.png"));
                        break;
                    case 6:
                        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/owlet/owlet6.png"));
                        break;
                    default:
                        break;
                }
            }
            else if (currentPlayerTypeIndex == 3)
            {
                switch (i)
                {
                    case 1:
                        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/dude/dude1.png"));
                        break;
                    case 2:
                        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/dude/dude2.png"));
                        break;
                    case 3:
                        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/dude/dude3.png"));
                        break;
                    case 4:
                        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/dude/dude4.png"));
                        break;
                    case 5:
                        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/dude/dude5.png"));
                        break;
                    case 6:
                        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/dude/dude6.png"));
                        break;
                    default:
                        break;
                }
            }
            player.Fill = playerSprite;
        }

        // ++++++++++++++++++++++++++++++++++++++++++++
        private void IdleSprite()
        {
            if (currentPlayerTypeIndex == 1)
            {
                playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pinkjump4.png"));

            }
            else if (currentPlayerTypeIndex == 2)
            {
                playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/avatarowlet1.png"));

            }
            else if (currentPlayerTypeIndex == 3)
            {
                playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/avatardude.png"));
            }

            player.Fill = playerSprite;
        }

        private void AnimatePlayer(double i)
        {
            if (playerAnimationCurrentState == PlayerAnimationState.RunningLeft || playerAnimationCurrentState == PlayerAnimationState.RunningRight)
            {
                RunSprite(i);
            }
            else
            {
                IdleSprite();
            }
        }


        //------------Observer Update Method---------------

        public void Update<T>(T message)
        {
            switch (Type.GetTypeCode(message.GetType()))
            {
                case TypeCode.Int32:
                    CurrentPlayers = Convert.ToInt32(message);
                    break;
                case TypeCode.String:
                    switch (Convert.ToString(message))
                    {
                        case "StartSignal":
                            StartCountDown();
                            break;
                        default:
                            players.Content = message;      //Gal geriau butu (ateity maziau problemu gal) jeigu paduoti player-name ne kaip stringa, o kaip koki "User" klases objekta?
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        

        //private void RunSpriteJump(double i)
        //{
        //    if (currentPlayerTypeIndex == 1)
        //    {
        //        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pinkjump1.png"));
        //        //switch (i)
        //        //{
        //        //    case 1:
        //        //        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pinkjump1.png"));
        //        //        break;
        //        //    case 2:
        //        //        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pinkjump2.png"));
        //        //        break;
        //        //    case 3:
        //        //        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pinkjump3.png"));
        //        //        break;
        //        //    case 4:
        //        //        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pinkjump4.png"));
        //        //        break;
        //        //    case 5:
        //        //        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pinkjump5.png"));
        //        //        break;
        //        //    default:
        //        //        break;
        //        //}
        //    }
        //    else if (currentPlayerTypeIndex == 2)
        //    {
        //        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/owlet/owlet1.png"));

        //        //    switch (i)
        //        //    {
        //        //        case 1:
        //        //            playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pinkjump1.png"));
        //        //            break;
        //        //        case 2:
        //        //            playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pinkjump2.png"));
        //        //            break;
        //        //        case 3:
        //        //            playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pinkjump3.png"));
        //        //            break;
        //        //        case 4:
        //        //            playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pinkjump4.png"));
        //        //            break;
        //        //        case 5:
        //        //            playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pinkjump5.png"));
        //        //            break;
        //        //        default:
        //        //            break;
        //        //    }
        //    }
        //    else if (currentPlayerTypeIndex == 3)
        //    {
        //        playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/dude/dude1.png"));
        //    }
        //    player.Fill = playerSprite;
        //}
    }
}
