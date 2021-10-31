using Microsoft.AspNetCore.SignalR.Client;
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

namespace Runner2
{
    public partial class MainWindow : Window
    {

        SignalRService rService;

        DispatcherTimer gameTimer = new DispatcherTimer();

        Rect playerHitBox;
        Rect player2HitBox;
        Rect groundHitBox;
        Rect platformHitBox;
        Rect platform2HitBox;
        Rect platform3HitBox;
        List<FrameworkElement> gamePlatforms = new List<FrameworkElement>();
        List<Rect> platformHitBoxes = new List<Rect>();
        Rect obstacleHitBox;
        Rect itemHitBox;
        Rect finishHitBox;

        bool jumping;
        bool opposingJumping;

        PlayerAnimationState playerAnimationCurrentState;
        PlayerAnimationState player2AnimationCurrentState;
        enum PlayerAnimationState
        {
            RunningLeft,
            RunningRight,
            Standing
        }

        PlayerFactory playerF;
        Player currentPlayer;
        Player opposingPlayer;

        //ItemFactory itemF;
        AbstractSceneFactory sceneF;

        int force = 20;
        int speed = 10;
        int opposingSpeed = 10;


        int currentPlayerTypeIndex = 1;
        int maxPlayerTypeIndex = 3;


        Random rnd = new Random();

        bool gameOver;

        double spriteIndex = 0;
        double spriteIndexJump = 0;

        ImageBrush playerSprite = new ImageBrush();
        ImageBrush player2Sprite = new ImageBrush();
        ImageBrush backgroundSprite = new ImageBrush();
        ImageBrush obstacleSprite = new ImageBrush();
        ImageBrush avatarSprite = new ImageBrush();
        ImageBrush doorSprite = new ImageBrush();

        int[] obstaclePosition = { 320, 310, 300, 305, 315 };

        int score = 0;

        private int CurrentPlayers;


        public MainWindow()
        {
            HubConnection connection = new HubConnectionBuilder()           //Connecting to hub
                .WithUrl("http://localhost:5000/runner").Build();

            rService = new SignalRService(connection);                      //Creating service with the connection

            rService.TauntMessageReceived += SignalRService_TauntMessageReceived;
            rService.PlayerCountReceived += SignalRService_PlayerCountReceived;
            rService.StartSignalReceived += SignalRService_StartSignalReceived;
            rService.PlayerTypeReceived += SignalRService_PlayerTypeReceived;
            rService.PlayerStateReceived += SignalRService_PlayerStateReceived;
            rService.PlayerJumpReceived += SignalRService_PlayerJumpReceived;
            rService.ChangeLevelSignalReceived += SignalRService_ChangeLevelSignalReceived;

            rService.Connect();

            InitializeComponent();
            MainWin.Focus();

            gameTimer.Tick += GameEngine;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);

            //backgroundSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/fonas1.png"));
            avatarSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/avatarpink.png"));
            doorSprite.ImageSource= new BitmapImage(new Uri("pack://application:,,,/Images/door.png"));
            gameEndPoint.Fill = doorSprite;
            avatar.Fill = avatarSprite;


            background.Fill = backgroundSprite;
            //background2.Fill = backgroundSprite;

            //StartGame();
            //CharacterTypeSelected.Content = currentPlayerTypeIndex.ToString();
        }

        // ----------------------------------------------------------------------------------------------------------
        // SignalR receiving funcions

        private void SignalRService_StartSignalReceived()
        {
            StartCountDown();
        }

        private void SignalRService_PlayerCountReceived(int count)
        {
            CurrentPlayers = count;
        }

        private void SignalRService_TauntMessageReceived(string message)
        {
            //TauntMessage.Content = message;
            players.Content = message;
        }
        private void SignalRService_PlayerTypeReceived(List<string> message)
        {
            //Could've done it with "Clients.Others" on the server side instead but oh well. I thought of it too late.
            if (nameInput.Text == players.Content.ToString().Split('\n')[0])        //Player in given instance is the first one who connected
            {
                //give player2 second type from list
                CreatePlayer(Convert.ToInt32(message[1]), 2);
            }
            else
            {
                //give player2 first type from list
                CreatePlayer(Convert.ToInt32(message[0]), 2);
            }
            IdleSprite(opposingPlayer, player2, player2Sprite);
        }

        private void SignalRService_PlayerStateReceived(int state)
        {
            player2AnimationCurrentState = (PlayerAnimationState)state;
            MoveOtherPlayer();
        }

        private void SignalRService_PlayerJumpReceived(bool jump)
        {
            opposingJumping = jump;
        }

        private void SignalRService_ChangeLevelSignalReceived()
        {
            GoToNextLevel();
        }

        //-----------------Functions to send to server----------------

        private async Task renameLater(string name, string playerType)
        {
            await rService.SendTauntMessage(name, playerType);
        }

        private async Task SendStartSignalOthers()
        {
            await rService.SendStartSignal();
        }

        private async Task SendPlayerState()
        {
            await rService.SendPlayerState((int)playerAnimationCurrentState);
        }

        private async Task SendPlayerJump()
        {
            await rService.SendPlayerJump(jumping);
        }

        private async Task SendChangeLevelSignal()
        {
            await rService.SendChangeLevelSignal();
        }

        // ------------------------------------------------------------------------------------------------------------
        // Game engine functions

        private void StartGame()
        {
            CantPlayText.Visibility = Visibility.Hidden;

            CreateScene(1);

            Canvas.SetLeft(background, 0);
            //Canvas.SetLeft(background2, 1262);


            //Canvas.SetLeft(obstacle, 950);
            //Canvas.SetTop(obstacle, 310);

            RunSprite(1);

            jumping = false;
            gameOver = false;
            score = 0;
            playerAnimationCurrentState = PlayerAnimationState.Standing;

            scoreText.Content = "Score: " + score;

            obstacle.Visibility = Visibility.Visible;
            player.Visibility = Visibility.Visible;
            player2.Visibility = Visibility.Visible;
            background.Visibility = Visibility.Visible;
            //background2.Visibility = Visibility.Visible;
            scoreText.Visibility = Visibility.Visible;

            avatar.Visibility = Visibility.Hidden;
            //platform.Visibility = Visibility.Hidden;

            gameTimer.Start();
        }

        private void GameEngine(object sender, EventArgs e)
        {
            //Canvas.SetLeft(background, Canvas.GetLeft(background) - currentPlayer.Speed);
            //Canvas.SetLeft(background2, Canvas.GetLeft(background2) - currentPlayer.Speed);

            //if (Canvas.GetLeft(background) < -1262)
            //    Canvas.SetLeft(background, Canvas.GetLeft(background2) + background2.Width);


            //if (Canvas.GetLeft(background2) < -1262)
            //    Canvas.SetLeft(background2, Canvas.GetLeft(background) + background.Width);

            Canvas.SetTop(player, Canvas.GetTop(player) + speed);
            Canvas.SetTop(player2, Canvas.GetTop(player2) + opposingSpeed);
            //Canvas.SetLeft(obstacle, Canvas.GetLeft(obstacle) - currentPlayer.Speed);
            //Canvas.SetLeft(item, Canvas.GetLeft(item) - currentPlayer.Speed);

            scoreText.Content = "Score: " + score;

            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width - 15, player.Height);
            player2HitBox = new Rect(Canvas.GetLeft(player2), Canvas.GetTop(player2), player2.Width - 15, player2.Height);
            obstacleHitBox = new Rect(Canvas.GetLeft(obstacle), Canvas.GetTop(obstacle), obstacle.Width, obstacle.Height);
            itemHitBox = new Rect(Canvas.GetLeft(item), Canvas.GetTop(item), item.Width, item.Height);
            groundHitBox = new Rect(Canvas.GetLeft(ground), Canvas.GetTop(ground), ground.Width, ground.Height);

            for (int i = GameWin.Children.Count - 4; i < GameWin.Children.Count; i++)
            {
                var gamePlatform = GameWin.Children[i] as FrameworkElement;
                gamePlatforms.Add(gamePlatform);
                platformHitBox = new Rect(Canvas.GetLeft(gamePlatform), Canvas.GetTop(gamePlatform), gamePlatform.ActualWidth, gamePlatform.ActualHeight);
                platformHitBoxes.Add(platformHitBox);
            }
            
            finishHitBox = new Rect(Canvas.GetLeft(gameEndPoint), Canvas.GetTop(gameEndPoint), gameEndPoint.Width, gameEndPoint.Height);

            //-------Hitbox platform interaction----
            HandleHitBoxCollisions();


            //--------------------------------------
            if (playerAnimationCurrentState == PlayerAnimationState.RunningLeft || playerAnimationCurrentState == PlayerAnimationState.RunningRight)
            {
                spriteIndex += .5;

                if (spriteIndex > 6)
                    spriteIndex = 1;

                //RunSprite(spriteIndex);
            }

            //-------------Player 1 jumping----------
            if (jumping == true)
            {
                speed = -9;
                force -= 1;
            }
            else
                speed = 8;
            //----------Player 2 jumping-------------
            if (opposingJumping == true)
            {
                opposingSpeed = -9;
                force -= 1;
            }
            else
                opposingSpeed = 8;
            //-----Random piece of code that is useless?
            if (force < 0)
                jumping = false;
            //------------------------------------------
            //if (Canvas.GetLeft(obstacle) < -50)
            //{
            //    Canvas.SetLeft(obstacle, 950);
            //    Canvas.SetTop(obstacle, obstaclePosition[rnd.Next(0, obstaclePosition.Length)]);
            //    score += 1;
            //}

            if (Canvas.GetLeft(item) < -50)
            {
                Canvas.SetLeft(item, 2000);
            }

            //-----------------Item---------------------
            if (playerHitBox.IntersectsWith(itemHitBox))
            {
                Canvas.SetLeft(item, 2000);

                switch (rnd.Next(1, 3))
                {
                    case 1:
                        //itemF = new GoodItemFactory();
                        break;
                    case 2:
                        //itemF = new BadItemFactory();
                        break;
                    default:
                        break;

                }

                score += 1;
                //var potion = itemF.CreatePotion();

            }
            //Made two different 'if's to make logic of applying item effects easier later maybe
            if (player2HitBox.IntersectsWith(itemHitBox))
            {
                Canvas.SetLeft(item, 2000);

                switch (rnd.Next(1, 3))
                {
                    case 1:
                        //itemF = new GoodItemFactory();
                        break;
                    case 2:
                        //itemF = new BadItemFactory();
                        break;
                    default:
                        break;

                }

                score += 1;
                //var potion = itemF.CreatePotion();

            }

            //------------------------------------------
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
            SendPlayerState();
            SendPlayerJump();
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
                StartWin.Visibility = Visibility.Hidden;
                LobbyWin.Visibility = Visibility.Visible;
                CantJoinLobbyText.Visibility = Visibility.Hidden;
                avatarLobby.Fill = avatarSprite;
                setActiveLobbyObjs();
                //players.Content = nameInput.Text;
                CreatePlayer(currentPlayerTypeIndex, 1);
                renameLater(nameInput.Text, currentPlayer.SkinType.ToString());
            }
            else
            {
                CantJoinLobbyText.Visibility = Visibility.Visible;
            }
        }

        private void startBtnClick(object sender, RoutedEventArgs e)
        {
            //uzkomentinau kad tikrint changus butu lengviau 
            //if (CurrentPlayers < 2)
            //{
            //    CantPlayText.Visibility = Visibility.Visible;
            //}
            //else
            {
                LobbyWin.Visibility = Visibility.Hidden;
                GameWin.Visibility = Visibility.Visible;

                SendStartSignalOthers();
                //StartGame();
            }

        }

        // -------------------------------------------------------------------------------------------------------------
        // Logic functions

        private void HandleHitBoxCollisions()
        {
            //Ground platform
            if (playerHitBox.IntersectsWith(groundHitBox))
            {
                speed = 0;
                Canvas.SetTop(player, Canvas.GetTop(ground) - player.Height);
                jumping = false;

            }
            if (player2HitBox.IntersectsWith(groundHitBox))
            {
                opposingSpeed = 0;
                Canvas.SetTop(player2, Canvas.GetTop(ground) - player2.Height);

            }
            //Platforms (1-4)
            for (int i = 0; i < 4; i++)
            {
                if (playerHitBox.IntersectsWith(platformHitBoxes[i]))
                {
                    speed = 0;
                    Canvas.SetTop(player, Canvas.GetTop(gamePlatforms[i]) - player.Height);
                    jumping = false;
                }
                if (player2HitBox.IntersectsWith(platformHitBoxes[i]))
                {
                    opposingSpeed = 0;
                    Canvas.SetTop(player2, Canvas.GetTop(gamePlatforms[i]) - player2.Height);
                }
            }
            //gameEndPoint
            if (playerHitBox.IntersectsWith(finishHitBox) && player2HitBox.IntersectsWith(finishHitBox))
            {
                SendChangeLevelSignal();
            }
            //Obstacle
            if (playerHitBox.IntersectsWith(obstacleHitBox))
            {
                Canvas.SetTop(player, 703 + speed);
                Canvas.SetLeft(player, 80);
            }
            if (player2HitBox.IntersectsWith(obstacleHitBox))
            {
                Canvas.SetTop(player2, 703 + opposingSpeed);
                Canvas.SetLeft(player2, 80);
            }
        }

        private void GoToNextLevel()
        {
            CreateScene(2);//Canvas.Top="703" Canvas.Left="80"
            Canvas.SetTop(player, 703 + speed);
            Canvas.SetLeft(player, 80);
            Canvas.SetTop(player2, 703 + opposingSpeed);
            Canvas.SetLeft(player2, 80);

            //Change canvas visibility
        }

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

        private void MoveOtherPlayer()
        {
            player2.RenderTransformOrigin = new Point(0.5, 0.5);
            ScaleTransform flipTrans = new ScaleTransform();
            if (player2AnimationCurrentState == PlayerAnimationState.RunningLeft)
            {
                flipTrans.ScaleX = -1;
                Canvas.SetLeft(player2, Canvas.GetLeft(player2) - opposingPlayer.Speed);
            }
            else if (player2AnimationCurrentState == PlayerAnimationState.RunningRight)
            {
                flipTrans.ScaleX = 1;
                Canvas.SetLeft(player2, Canvas.GetLeft(player2) + opposingPlayer.Speed);
            }

            player2.RenderTransform = flipTrans;
        }

        private void CreatePlayer(int typeToCreate, int playernum)
        {
            if (playernum == 1)
            {
                switch (typeToCreate)
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
            else
            {
                switch (typeToCreate)
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

                opposingPlayer = playerF.GetPlayer();
            }
        }

        private void CreateScene(int level)
        {
            Background bg;
            Platform plat;
            Obstacle obs;
            switch (level)
            {
                case 1:
                    sceneF = new SummerFactory();
                    break;
                case 2:
                    sceneF = new WinterFactory();
                    break;
            }

            bg = sceneF.CreateBackground();
            backgroundSprite.ImageSource = new BitmapImage(new Uri(bg.spritePath));

            plat = sceneF.CreatePlatform();
            //obstaclePosition

            //gamePlatform.Fill = new SolidColorBrush(plat.color);
            //gamePlatform2.Fill = new SolidColorBrush(plat.color);
            //gamePlatform3.Fill = new SolidColorBrush(plat.color);


            int number = 4;
            int[] width = { 229, 299, 411, 200 };
            int height = 32;
            int[] topPositions = { 510, 316, 310, 201 };
            int[] leftPositions = { 397, 46, 789, 539 };

            for (int i = 0; i < number; i++)
            {
                // Create the rectangle
                Rectangle rec = new Rectangle()
                {
                    Width = width[i],
                    Height = height,
                    Fill = Brushes.Green,
                    Stroke = Brushes.Red,
                    StrokeThickness = 2,
                };

                GameWin.Children.Add(rec);
                Canvas.SetTop(rec, topPositions[i]);
                Canvas.SetLeft(rec, leftPositions[i]);
            }


            ground.Fill = new SolidColorBrush(plat.color);

            obs = sceneF.CreateObstacle();
            obstacleSprite.ImageSource = new BitmapImage(new Uri(obs.spritePath));
            obstacle.Fill = obstacleSprite;
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
                    CharacterTypeSelected.Content = "Pink monster";//startui
                    CharacterTypeSelectedLobby.Content = "Pink monster";//lobui
                    break;
                case 2:
                    avatarSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/avatarowlet1.png"));
                    CharacterTypeSelected.Content = "Owlet monster";
                    CharacterTypeSelectedLobby.Content = "Owlet monster";
                    break;
                case 3:
                    avatarSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/avatardude.png"));
                    CharacterTypeSelected.Content = "Dude monster";
                    CharacterTypeSelectedLobby.Content = "Dude monster";
                    break;
            }
            avatar.Fill = avatarSprite;
            avatarLobby.Fill = avatarSprite;
        }

        private void StartCountDown()
        {
            StartGame();
        }

        // -------------------------------------------------------------------------------------------------------------------
        // Animation functions

        private void RunSprite(double i)
        {
            if (currentPlayer.SkinType == 1)
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
            else if (currentPlayer.SkinType == 2)
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
            else if (currentPlayer.SkinType == 3)
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

        private void IdleSprite(Player player, Rectangle playerToChange, ImageBrush imageBrush)
        {
            if (player.SkinType == 1)
            {
                imageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pinkjump4.png"));

            }
            else if (player.SkinType == 2)
            {
                imageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/avatarowlet1.png"));

            }
            else if (player.SkinType == 3)
            {
                imageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/avatardude.png"));
            }

            playerToChange.Fill = imageBrush;
        }

        private void AnimatePlayer(double i)
        {
            if (playerAnimationCurrentState == PlayerAnimationState.RunningLeft || playerAnimationCurrentState == PlayerAnimationState.RunningRight)
            {
                RunSprite(i);
            }
            else
            {
                IdleSprite(currentPlayer, player, playerSprite);
            }
        }

    }
}
