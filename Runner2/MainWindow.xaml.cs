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
        Facade facade;

        #region Variables (a lot less needs to stay (if any) )
        Background bg;
        Platform plat;
        Obstacle obs;
        Item itm;

        SignalRService rService;

        Creator playerF = new ConcreteCreator();
        Player currentPlayer;
        Player opposingPlayer;

        Player currentPlayerDeepCopy;
        Player currentPlayerShallowCopy;

        Player opposingPlayerDeepCopy;
        Player opposingPlayerShallowCopy;

        PlayerStatsController statsController = new PlayerStatsController();

        #region Memento
        Caretaker caretaker = new Caretaker();
        #endregion

        Composite root = new Composite("root");

        //ItemFactory itemF;
        AbstractSceneFactory sceneF;
        Builder builder;

        DispatcherTimer gameTimer = new DispatcherTimer();

        Rect playerHitBox;
        Rect player2HitBox;
        Rect groundHitBox;
        Rect finishGroundHitBox;
        Rect platformHitBox;
        Rect ItemHitBox;
        List<FrameworkElement> gamePlatforms = new List<FrameworkElement>();
        List<Rect> platformHitBoxes = new List<Rect>();
        Rect obstacleHitBox;
        List<Rect> itemHitBoxes = new List<Rect>();
        List<Rect> stuffHitBoxes = new List<Rect>();
        List<FrameworkElement> canvasItems = new List<FrameworkElement>();
        List<Item> items = new List<Item>();
        Rect magicHatHitBox;
        Rect baseballHatHitBox;
        Rect cowboyHatHitBox;
        Rect finishHitBox;
        Rect potionHitBox;

        Rect backpackHitBox;
        Rect trinketHitBox;
        Rect pelianHitBox;
        Rect pencilHitBox;
        Rect rubberHitBox;

        bool jumping;
        bool opposingJumping;
        bool hasMagicHat;
        bool hasBaseballHat;
        bool hasCowboyHat;

        public PlayerAnimationState playerAnimationCurrentState;
        public PlayerAnimationState player2AnimationCurrentState;
        public enum PlayerAnimationState
        {
            RunningLeft,
            RunningRight,
            Standing
        }


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
        ImageBrush symbolSprite = new ImageBrush();
        ImageBrush potionSprite = new ImageBrush();

        int[] obstaclePosition = { 320, 310, 300, 305, 315 };


        int platStartInd;
        int platEndInd;
        int itemStartInd;
        int itemEndInd;

        ExclamationPoint exclamationPoint = new ExclamationPoint();
        QuestionMark questionMark = new QuestionMark();



        private int CurrentPlayers;
        #endregion

        #region This stays but with less stuff
        public MainWindow()
        {
            facade = new Facade();

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
            rService.EndGameSignalReceived += SingalRService_EndGameSignalReceived;
            rService.UndoSignalReceived += SignalRService_UndoSignalReceived;
            rService.AddPointsReceived += SignalRService_AddPointsReceived;
            rService.RemovePointsReceived += SignalRService_RemovePointsReceived;
            rService.Connect();

            InitializeComponent();
            MainWin.Focus();

            gameTimer.Tick += GameEngine;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);

            //backgroundSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/fonas1.png"));
            avatarSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/avatarpink.png"));
            doorSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/door.png"));
            potionSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/potion.png"));

            //jhlkjhlkjhlkj
           
            backpack.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Stuff/backpack.png"))}; 
            backpackR.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Stuff/backpack.png")) };
            trinket.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Stuff/trinket.png")) };
            trinketR.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Stuff/trinket.png")) };
            pelian.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Stuff/pelian.png")) };
            pelianR.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Stuff/pelian.png")) };
            pencil.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Stuff/pencil.png")) };
            pencilR.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Stuff/pencil.png")) };
            rubber.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Stuff/rubber.png")) };
            rubberR.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Stuff/rubber.png")) };


            //ajhdgfjalshdfjas

            gameEndPoint.Fill = doorSprite;
            avatar.Fill = avatarSprite;
            testpotion.Fill = potionSprite;

            background.Fill = backgroundSprite;

            createHats();

            //background2.Fill = backgroundSprite;

            //StartGame();
            //CharacterTypeSelected.Content = currentPlayerTypeIndex.ToString();
        }
        #endregion
        // ----------------------------------------------------------------------------------------------------------
        // SignalR receiving funcions

        #region These all go
        private void SignalRService_StartSignalReceived()
        {
            StartCountDown();
        }

        private void SignalRService_PlayerCountReceived(int count)
        {
            CurrentPlayers = count;
        }

        //modify slightly
        private void SignalRService_TauntMessageReceived(string message)
        {
            //TauntMessage.Content = message;
            players.Content = message;
        }

        //modify slightly
        private void SignalRService_PlayerTypeReceived(List<string> message)
        {
            //Could've done it with "Clients.Others" on the server side instead but oh well. I thought of it too late.
            if (nameInput.Text == players.Content.ToString().Split('\n')[0])        //Player in given instance is the first one who connected
            {
                //give player2 second type from list
                opposingPlayer = facade.CreatePlayer(Convert.ToInt32(message[1]));
            }
            else
            {
                //give player2 first type from list
                opposingPlayer = facade.CreatePlayer(Convert.ToInt32(message[0]));
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
        private void SignalRService_UndoSignalReceived()
        {
            statsController.undo();
        }

        private void SignalRService_ChangeLevelSignalReceived()
        {
            GoToNextLevel();
        }

        private void SingalRService_EndGameSignalReceived()
        {
            EndGameWin();
        }

        private void SignalRService_AddPointsReceived(int number)
        {
            currentPlayer.Points.AddPoints(number);
            opposingPlayer.Points.AddPoints(number);
        }

        private void SignalRService_RemovePointsReceived(int number)
        {
            currentPlayer.Points.AddPoints(-number);
            opposingPlayer.Points.AddPoints(-number);
        }
        #endregion

        //-----------------Functions to send to server----------------

        #region These all go
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

        private async Task SendUndoSignal()
        {
            await rService.SendUndoSignal();
        }

        private async Task SendChangeLevelSignal()
        {
            await rService.SendChangeLevelSignal();
        }

        private async Task SendEndGameSignal()
        {
            await rService.SendEndGameSignal();
        }

        private async Task AllowConsoleCommand()
        {
            await rService.SendAllowConsoleSignal();
        }
        #endregion

        // ------------------------------------------------------------------------------------------------------------
        // Game engine functions

        #region This goes
        private void StartGame()
        {
            CantPlayText.Visibility = Visibility.Hidden;

            LobbyWin.Visibility = Visibility.Hidden;
            GameWin.Visibility = Visibility.Visible;

            CreateScene(1);

            Canvas.SetLeft(background, 0);
            //Canvas.SetLeft(background2, 1262);

            groundHitBox = new Rect(Canvas.GetLeft(ground), Canvas.GetTop(ground), ground.Width, ground.Height);
            finishGroundHitBox = new Rect(Canvas.GetLeft(finishGround), Canvas.GetTop(finishGround), finishGround.Width, finishGround.Height);
            potionHitBox = new Rect(Canvas.GetLeft(testpotion), Canvas.GetTop(testpotion), testpotion.Width, testpotion.Height);

            backpackHitBox = new Rect(Canvas.GetLeft(backpackR), Canvas.GetTop(backpackR), backpackR.Width, backpackR.Height);
            trinketHitBox = new Rect(Canvas.GetLeft(trinketR), Canvas.GetTop(trinketR), trinketR.Width, trinketR.Height);
            pelianHitBox = new Rect(Canvas.GetLeft(pelianR), Canvas.GetTop(pelianR), pelianR.Width, pelianR.Height);
            pencilHitBox = new Rect(Canvas.GetLeft(pencilR), Canvas.GetTop(pencilR), pencilR.Width, pencilR.Height);
            rubberHitBox = new Rect(Canvas.GetLeft(rubberR), Canvas.GetTop(rubberR), rubberR.Width, rubberR.Height);

            //Canvas.SetLeft(obstacle, 950);
            //Canvas.SetTop(obstacle, 310);

            RunSprite(1);

            jumping = false;
            gameOver = false;
            hasMagicHat = false;
            hasBaseballHat = false;
            hasCowboyHat = false;
            playerAnimationCurrentState = PlayerAnimationState.Standing;
            //currentPlayer.state = new Standing();

            LobbyWin.Visibility = Visibility.Hidden;
            GameWin.Visibility = Visibility.Visible;



            gameTimer.Start();
        }
        #endregion

        #region This definately stays but modified
        private void GameEngine(object sender, EventArgs e)
        {
            //-------------Gravity----------
            Canvas.SetTop(player, Canvas.GetTop(player) + speed);
            Canvas.SetTop(player2, Canvas.GetTop(player2) + opposingSpeed);
            //------------------------------
            Canvas.SetTop(symbolObject, Canvas.GetTop(player) + speed - 80);
            Canvas.SetLeft(symbolObject, Canvas.GetLeft(player));

            scoreText.Content = "Your Score: " + currentPlayer.Points.points;
            OtherScoreText.Content = "Oponent Score: " + opposingPlayer.Points.points;

            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width - 15, player.Height);
            player2HitBox = new Rect(Canvas.GetLeft(player2), Canvas.GetTop(player2), player2.Width - 15, player2.Height);
            
            //----------------------------------------------------------------------------------------------------------------------------------

            finishHitBox = new Rect(Canvas.GetLeft(gameEndPoint), Canvas.GetTop(gameEndPoint), gameEndPoint.Width, gameEndPoint.Height);

            //-------Hitbox platform interaction----
            HandleHitBoxCollisions();



            //--------------------------------------
            if (playerAnimationCurrentState == PlayerAnimationState.RunningLeft || playerAnimationCurrentState == PlayerAnimationState.RunningRight)
           // if ( currentPlayer.state is RunningLeft|| currentPlayer.state is RunningRight)
            {
                spriteIndex += .5;

                if (spriteIndex > 6)
                    spriteIndex = 1;

            }

            //-------------Player 1 jumping----------
            if (jumping == true)
            //if (currentPlayer.state is Jumping)
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
                //currentPlayer.state = new Standing();
            
            //------------------------------------------
            if (gameOver == true)
            {
                obstacle.Stroke = Brushes.Black;
                obstacle.StrokeThickness = 1;

                player.Stroke = Brushes.Red;
                player.StrokeThickness = 1;

                scoreText.Content = "Score: " + currentPlayer.Points + " Press Enter to play again!!";
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
            currentPlayer.Update();
            opposingPlayer.Update();
        }
        #endregion

        // ------------------------------------------------------------------------------------------------------------
        // Key input functions
        #region These stay but modified
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && gameOver == true)
            {
                StartGame();
            }

            if (e.Key == Key.Left)
            {
                playerAnimationCurrentState = PlayerAnimationState.RunningLeft;
                //currentPlayer.state = new RunningLeft();

            }
            else if (e.Key == Key.Right)
            {
                playerAnimationCurrentState = PlayerAnimationState.RunningRight;
                //currentPlayer.state = new RunningRight();
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.U && gameOver == false)
            {
                statsController.undo();
                SendUndoSignal();
            }

            if (e.Key == Key.B)
            {
                AllowConsoleCommand();
            }

            //PADARYK KAD VEIKTU IR KITAM SCRYNE GINTAI
            if (e.Key == Key.Q)
            {
                caretaker.Memento = currentPlayer.CreateMemento();
            }

            if (e.Key == Key.W)
            {
                currentPlayer.SetMemento(caretaker.Memento);
            }

            if (e.Key == Key.Space && gameOver == false && jumping == false)
            {
                //renameLater();
                jumping = true;
                //currentPlayer.state = new Jumping();
                //currentPlayer.Request(ref force, ref speed);
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
                //currentPlayer.state = new Standing();
            }
            if (e.Key == Key.Z)
            {
                currentPlayer.symbol = questionMark;
                symbolSprite.ImageSource = new BitmapImage(new Uri(currentPlayer.symbol.getPictureString()));
                symbolObject.Fill = symbolSprite;
                symbolObject.Visibility = Visibility.Visible;
            }
            if (e.Key == Key.X)
            {
                currentPlayer.symbol = exclamationPoint;
                symbolSprite.ImageSource = new BitmapImage(new Uri(currentPlayer.symbol.getPictureString()));
                symbolObject.Fill = symbolSprite;
                symbolObject.Visibility = Visibility.Visible;
            }
        }
        #endregion

        // -------------------------------------------------------------------------------------------------------------
        // Button press functions

        #region These stay but modified
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

                StartWin.Visibility = Visibility.Hidden;
                LobbyWin.Visibility = Visibility.Visible;

                //setActiveLobbyObjs();
                //players.Content = nameInput.Text;
                currentPlayer = facade.CreatePlayer(currentPlayerTypeIndex);
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
                //LobbyWin.Visibility = Visibility.Hidden;
                //GameWin.Visibility = Visibility.Visible;

                SendStartSignalOthers();
                //StartGame();
            }

        }

        private void EndGameBtnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        // -------------------------------------------------------------------------------------------------------------
        // Logic functions
        #region These all go. Modify everything with static indexes in canvas
        private void HandleHitBoxCollisions()
        {
            //Ground platform
            if (playerHitBox.IntersectsWith(groundHitBox))
            {
                speed = 0;
                Canvas.SetTop(player, Canvas.GetTop(ground) - player.Height);
                jumping = false;
                //currentPlayer.state = new Standing();

            }
            if (player2HitBox.IntersectsWith(groundHitBox))
            {
                opposingSpeed = 0;
                Canvas.SetTop(player2, Canvas.GetTop(ground) - player2.Height);

            }
            //Platforms (1-4)
            for (int i = 0; i < 4; i++)
            {
                if (playerHitBox.IntersectsWith(platformHitBoxes[i]) && !(currentPlayer.state is SmallSizeState))
                {
                    speed = 0;
                    Canvas.SetTop(player, Canvas.GetTop(gamePlatforms[i]) - player.Height);
                    jumping = false;
                    //currentPlayer.state = new Standing();
                }
                if (player2HitBox.IntersectsWith(platformHitBoxes[i]) && !(opposingPlayer.state is SmallSizeState))
                {
                    opposingSpeed = 0;
                    Canvas.SetTop(player2, Canvas.GetTop(gamePlatforms[i]) - player2.Height);
                }
            }
            //HATS-----------------------------------------------------------------------------------------------------------
            if (playerHitBox.IntersectsWith(magicHatHitBox) && !hasMagicHat)
            {
                hasMagicHat = true;
                currentPlayer = new MagicHat(currentPlayer, "player");
            }
            if (playerHitBox.IntersectsWith(baseballHatHitBox) && !hasBaseballHat)
            {
                hasBaseballHat = true;
                currentPlayer = new BaseballHat(currentPlayer, "player");
            }
            if (playerHitBox.IntersectsWith(cowboyHatHitBox) && !hasCowboyHat)
            {
                hasCowboyHat = true;
                currentPlayer = new CowboyHat(currentPlayer, "player");
            }
            if (player2HitBox.IntersectsWith(magicHatHitBox) && !hasMagicHat)
            {
                hasMagicHat = true;
                opposingPlayer = new MagicHat(opposingPlayer, "player2");
            }
            if (player2HitBox.IntersectsWith(baseballHatHitBox) && !hasBaseballHat)
            {
                hasBaseballHat = true;
                opposingPlayer = new BaseballHat(opposingPlayer, "player2");
            }
            if (player2HitBox.IntersectsWith(cowboyHatHitBox) && !hasCowboyHat)
            {
                hasCowboyHat = true;
                opposingPlayer = new CowboyHat(opposingPlayer, "player2");
            }
            //items
            for (int i = 0; i < itemHitBoxes.Count; i++)
            {
                if (playerHitBox.IntersectsWith(itemHitBoxes[i]) && !(currentPlayer.state is LargeSizeState) )
                {
                    itemHitBoxes[i] = new Rect(2000, 2000, 2, 2);
                    Canvas.SetLeft(canvasItems[i], 2000);
                    canvasItems[i].Visibility = Visibility.Hidden;
                    //Canvas.SetLeft(items[i], 2000);

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

                    statsController.run(new ModifyPointsCommand(currentPlayer, items[i]));
                    currentPlayer.Request(4);
                }
                if (player2HitBox.IntersectsWith(itemHitBoxes[i]) && !(opposingPlayer.state is LargeSizeState))
                {
                    itemHitBoxes[i] = new Rect(2000, 2000, 2, 2);
                    Canvas.SetLeft(canvasItems[i], 2000);
                    canvasItems[i].Visibility = Visibility.Hidden;

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
                    statsController.run(new ModifyPointsCommand(opposingPlayer, items[i]));
                    opposingPlayer.Request(3);
                }
            }
            //gameEndPoint
            if (playerHitBox.IntersectsWith(finishHitBox) && player2HitBox.IntersectsWith(finishHitBox))
            {
                if (sceneF is WinterFactory)
                {
                    SendEndGameSignal();
                }
                else
                    SendChangeLevelSignal();
            }
            //Obstacle
            if (playerHitBox.IntersectsWith(obstacleHitBox) && !obs.exploded)
            {
                currentPlayer.RemoveHats();
                (root.elements.Single(x => x.name == "backpack") as Composite).Remove("pelian");

                pelian.Visibility = Visibility.Hidden;
                Canvas.SetLeft(pelianR, 861);
                pelianHitBox = new Rect(Canvas.GetLeft(pelianR), Canvas.GetTop(pelianR), pelianR.Width, pelianR.Height);

                pencil.Visibility = Visibility.Hidden;
                Canvas.SetLeft(pencilR, 496);
                pencilHitBox = new Rect(Canvas.GetLeft(pencilR), Canvas.GetTop(pencilR), pencilR.Width, pencilR.Height);

                rubber.Visibility = Visibility.Hidden;
                Canvas.SetLeft(rubberR, 539);
                rubberHitBox = new Rect(Canvas.GetLeft(rubberR), Canvas.GetTop(rubberR), rubberR.Width, rubberR.Height);

                if (sceneF is WinterFactory)
                {
                    // DEEP COPY
                    currentPlayer = currentPlayerDeepCopy;
                    currentPlayerDeepCopy = (Player)currentPlayer.deepCopy();
                }
                else
                {
                    // SHALLOW COPY
                    currentPlayer = currentPlayerShallowCopy;
                    currentPlayerShallowCopy = (Player)currentPlayer.shallowCopy();
                }
                if (!obs.exploded)
                    obs.exploded = true;

                ObstacleAdapter explodedItem = new ObstacleAdapter(obs);
                items.Add(explodedItem);
                itemHitBoxes.Add(obstacleHitBox);
                canvasItems.Add(obstacle);
                obstacle.Fill = Brushes.Red;
                obs.resetPlayerPosition(4);
            }
            if (player2HitBox.IntersectsWith(obstacleHitBox) && !obs.exploded)
            {
                opposingPlayer.RemoveHats();
                if (sceneF is WinterFactory)
                {
                    // DEEP COPY
                    opposingPlayer = opposingPlayerDeepCopy;
                    opposingPlayerDeepCopy = (Player)opposingPlayer.deepCopy();
                }
                else
                {
                    // SHALLOW COPY
                    opposingPlayer = opposingPlayerShallowCopy;
                    opposingPlayerShallowCopy = (Player)opposingPlayer.shallowCopy();
                }

                if (!obs.exploded)
                    obs.exploded = true;
                ObstacleAdapter explodedItem = new ObstacleAdapter(obs);
                items.Add(explodedItem);
                itemHitBoxes.Add(obstacleHitBox);
                canvasItems.Add(obstacle);
                obstacle.Fill = Brushes.Red;
                obs.resetPlayerPosition(3);
            }

            //-----Potion------
            if (playerHitBox.IntersectsWith(potionHitBox) && !(currentPlayer.state is MediumSizeState))
            {
                Canvas.SetLeft(testpotion, 2000);
                potionHitBox = new Rect(2000, 2000, 2, 2);
                facade.UsePotion(currentPlayer, "speedUp");
            }
            if (player2HitBox.IntersectsWith(potionHitBox) && !(opposingPlayer.state is MediumSizeState))
            {
                Canvas.SetLeft(testpotion, 2000);
                potionHitBox = new Rect(2000, 2000, 2, 2);
                facade.UsePotion(opposingPlayer, "speedUp");
            }
            //------Stuff--------
            if (playerHitBox.IntersectsWith(backpackHitBox))
            {
                Composite backpackS = new Composite("backpack");
                root.Add(backpackS);

                backpackHitBox = new Rect(2000, 2000, 2, 2);
                Canvas.SetLeft(backpackR, 2000);
                backpack.Visibility = Visibility.Visible;
            }
            if (playerHitBox.IntersectsWith(trinketHitBox) && (root.elements.Any(x => x.name == "backpack")) )
            {
                Trinket trinketS = new Trinket("trinket");
                var temp = root.elements.Single(x => x.name == "backpack") as Composite;
                temp.elements.Add(trinketS);

                trinketHitBox = new Rect(2000, 2000, 2, 2);
                Canvas.SetLeft(trinketR, 2000);
                trinket.Visibility = Visibility.Visible;
            }
            if (playerHitBox.IntersectsWith(pelianHitBox) && (root.elements.Any(x => x.name == "backpack")) )
            {
                Composite pelianS = new Composite("pelian");
                var temp = root.elements.Single(x => x.name == "backpack") as Composite;
                temp.elements.Add(pelianS);

                pelianHitBox = new Rect(2000, 2000, 2, 2);
                Canvas.SetLeft(pelianR, 2000);
                pelian.Visibility = Visibility.Visible;
            }
            if (playerHitBox.IntersectsWith(pencilHitBox) && (root.elements.Any(x => x.name == "backpack"))
                && ((root.elements.Single(x => x.name == "backpack") as Composite).elements.Any(x => x.name == "pelian")))
            {
                Pensil pensilS = new Pensil("pensil");
                var temp = (root.elements.Single(x => x.name == "backpack") as Composite).elements.Single(x => x.name == "pelian") as Composite;
                temp.elements.Add(pensilS);

                pencilHitBox = new Rect(2000, 2000, 2, 2);
                Canvas.SetLeft(pencilR, 2000);
                pencil.Visibility = Visibility.Visible;
            }
            if (playerHitBox.IntersectsWith(rubberHitBox) && (root.elements.Any(x => x.name == "backpack"))
                && ((root.elements.Single(x => x.name == "backpack") as Composite).elements.Any(x => x.name == "pelian")))
            {
                Rubber rubberS = new Rubber("rubber");
                var temp = (root.elements.Single(x => x.name == "backpack") as Composite).elements.Single(x => x.name == "pelian") as Composite;
                temp.elements.Add(rubberS);

                rubberHitBox = new Rect(2000, 2000, 2, 2);
                Canvas.SetLeft(rubberR, 2000);
                rubber.Visibility = Visibility.Visible;
            }

        }

        private void GoToNextLevel()
        {
            CreateScene(2);//Canvas.Top="703" Canvas.Left="80"
            Canvas.SetTop(player, 510 + speed);
            Canvas.SetLeft(player, 80);
            Canvas.SetTop(player2, 510 + opposingSpeed);
            Canvas.SetLeft(player2, 80);

            //Change canvas visibility
        }

        private void EndGameWin()
        {
            GameWin.Visibility = Visibility.Hidden;
            EndWin.Visibility = Visibility.Visible;

            var names = players.Content.ToString().Split('\n');

            if (nameInput.Text == names[0])        //Player in given instance is the first one who connected
            {
                //give player2 second type from list
                endScore1.Content = currentPlayer.Points.points;
                endScore2.Content = opposingPlayer.Points.points;
            }
            else
            {
                //give player2 first type from list
                endScore2.Content = currentPlayer.Points.points;
                endScore1.Content = opposingPlayer.Points.points;
            }
            endPlayer1.Content = names[0];
            endPlayer2.Content = names[1];
        }

        private void CreateScene(int level)
        {
            int[] topPositions = new int[4];
            int[] leftPositions = new int[4];

            switch (level)
            {
                case 1:
                    topPositions = new int[] { 510, 315, 310, 200 };
                    leftPositions = new int[] { 400, 50, 790, 540 };
                    sceneF = new SummerFactory();
                    builder = new SummerBuilder();
                    break;
                case 2:
                    topPositions = new int[] { 400, 270, 310, 210 };
                    leftPositions = new int[] { 300, 60, 790, 340 };
                    for (int i = itemStartInd; i < itemEndInd; i++)
                    {
                        GameWin.Children.Remove(GameWin.Children[itemStartInd]);
                    }
                    for (int i = platStartInd; i < platEndInd; i++)
                    {
                        GameWin.Children.Remove(GameWin.Children[platStartInd]);
                    }

                    gamePlatforms = new List<FrameworkElement>();
                    platformHitBoxes = new List<Rect>();
                    itemHitBoxes = new List<Rect>();
                    canvasItems = new List<FrameworkElement>();

                    sceneF = new WinterFactory();
                    builder = new WinterBuilder();
                    break;
            }



            //------------Background------------------------------------
            bg = sceneF.CreateBackground();
            backgroundSprite.ImageSource = new BitmapImage(new Uri(bg.spritePath));

            //------------Ground-Platform------------------------------
            plat = sceneF.CreatePlatform();
            ground.Fill = plat.color;

            //-----------------Obstacle-----------------------------

            obs = sceneF.CreateObstacle();
            obstacleSprite.ImageSource = new BitmapImage(new Uri(obs.spritePath));
            //if (itemHitBoxes.Count == 5)
            //{
            //    itemHitBoxes.Remove(itemHitBoxes.Last());
            //    items.Remove(items.Last());
            //    canvasItems.Remove(canvasItems.Last());
            //}
            Canvas.SetLeft(obstacle, 947);
            obstacleHitBox = new Rect(Canvas.GetLeft(obstacle), Canvas.GetTop(obstacle), obstacle.Width, obstacle.Height);
            obstacle.Fill = obstacleSprite;
            obstacle.Visibility = Visibility.Visible;

            //--------Player-copy--------------------------------------
            currentPlayerDeepCopy = (Player)currentPlayer.deepCopy();
            opposingPlayerDeepCopy = (Player)opposingPlayer.deepCopy();

            currentPlayerShallowCopy = (Player)currentPlayer.shallowCopy();
            opposingPlayerShallowCopy = (Player)opposingPlayer.shallowCopy();

            //--------------------------platforms---------------------

            //new
            int platn = 4;
            int itemn = 6;
            platStartInd = GameWin.Children.Count;

            var tuple = builder.buildScene(platn, itemn);

            platEndInd = GameWin.Children.Count - itemn;
            itemStartInd = GameWin.Children.Count - itemn;
            itemEndInd = GameWin.Children.Count;

            for (int i = platStartInd; i != platEndInd; i++)
            {
                gamePlatforms.Add(GameWin.Children[i] as Rectangle);
            }
            foreach (var hitbox in tuple.Item1)
            {
                platformHitBoxes.Add(hitbox);
            }

            for (int i = itemStartInd; i != itemEndInd; i++)
            {
                canvasItems.Add(GameWin.Children[i] as Rectangle);
            }
            foreach (var item in tuple.Item2)
            {
                items.Add(item);
                itemHitBoxes.Add(item.hitbox);
            }

            //OLd
            //int[] width = new int[] { 229, 299, 411, 200 };


            //platStartInd = GameWin.Children.Count;


            //for (int i = 0; i < 4; i++)
            //{
            //    var platHitBox = builder.buildPlatform(width[i], 32, topPositions[i], leftPositions[i]);
            //    gamePlatforms.Add(GameWin.Children[GameWin.Children.Count - 1] as Rectangle);
            //    platformHitBoxes.Add(platHitBox);
            //}

            //platEndInd = GameWin.Children.Count;

            //-------------items----------------------------

            //int[] itemTopPositions = new int[] { 400, 200, 250, 190 };
            //int[] itemLeftPositions = new int[] { 397, 46, 789, 539 };

            //itemStartInd = GameWin.Children.Count;

            //for (int i = 0; i < 4; i++)
            //{
            //    var ite = builder.buildItem(itemTopPositions[i], itemLeftPositions[i]);
            //    canvasItems.Add(GameWin.Children[GameWin.Children.Count - 1] as Rectangle);
            //    items.Add(ite);
            //    itemHitBoxes.Add(ite.hitbox);
            //}

            //itemEndInd = GameWin.Children.Count;

        }

        //private void setActiveLobbyObjs()
        //{
        //    title.Visibility = Visibility.Hidden;
        //    startGameBtn.Visibility = Visibility.Hidden;
        //    nameInput.Visibility = Visibility.Hidden;
        //    joinLobbyBtn.Visibility = Visibility.Hidden;
        //    cycleCharacterTypeLeftBtn.Visibility = Visibility.Hidden;
        //    cycleCharacterTypeRightBtn.Visibility = Visibility.Hidden;
        //    CharacterTypeSelected.Visibility = Visibility.Hidden;

        //    titlePlayers.Visibility = Visibility.Visible;
        //    startGameBtn.Visibility = Visibility.Visible;
        //    players.Visibility = Visibility.Visible;
        //}

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
        #endregion

        // -------------------------------------------------------------------------------------------------------------------
        // Animation functions

        #region Sprites
        public void RunSprite(double i)
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
            //if (currentPlayer.state is  RunningLeft || currentPlayer.state is RunningRight)
            {
                RunSprite(i);
            }
            else //if(currentPlayer.state is Standing)
            {
                IdleSprite(currentPlayer, player, playerSprite);
            }
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
        private void createHats()
        {
            //Magic HAT
            magicHatItem.Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/goodItems/magicHat.png")));
            magicHatHitBox = new Rect(Canvas.GetLeft(magicHatItem), Canvas.GetTop(magicHatItem), magicHatItem.Width, magicHatItem.Height);

            //Baseball HAT
            baseballhatItem.Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/goodItems/baseballHat.png")));
            baseballHatHitBox = new Rect(Canvas.GetLeft(baseballhatItem), Canvas.GetTop(baseballhatItem), baseballhatItem.Width, baseballhatItem.Height);

            //Cowboy HAT
            cowboyHatItem.Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/goodItems/cowboyHat.png")));
            cowboyHatHitBox = new Rect(Canvas.GetLeft(cowboyHatItem), Canvas.GetTop(cowboyHatItem), cowboyHatItem.Width, cowboyHatItem.Height);
        }
        #endregion
    }
}
