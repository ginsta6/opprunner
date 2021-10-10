using Microsoft.AspNetCore.SignalR.Client;
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
    //abstract class AbstractPlayerFactory
    //{
    //    public abstract AbstractPlayerA CreatePlayerA();
    //    public abstract AbstractPlayerB CreatePlayerB();
    //}
    //abstract class AbstractPlayerA
    //{

    //}
    //abstract class AbstractPlayerB
    //{

    //}
    //class  PlayerFactory : AbstractPlayerFactory
    //{
    //    public override AbstractPlayerA CreatePlayerA()
    //    {
    //        return 
    //    }
    //    public override AbstractPlayerA CreatePlayerB()
    //    {
    //        return 
    //    }
    //}
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SignalRService rService;

        DispatcherTimer gameTimer = new DispatcherTimer();

        Rect playerHitBox;
        Rect groundHitBox;
        Rect obstacleHitBox;

        bool jumping;

        int force = 20;
        int speed = 5;

        Random rnd = new Random();

        bool gameOver;

        double spriteIndex = 0;

        ImageBrush playerSprite = new ImageBrush();
        ImageBrush backgroundSprite = new ImageBrush();
        ImageBrush obstacleSprite = new ImageBrush();

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

            rService.Connect();

            InitializeComponent();
            MainWin.Focus();

            gameTimer.Tick += GameEngine;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);

            backgroundSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/background.gif"));

            background.Fill = backgroundSprite;
            background2.Fill = backgroundSprite;

            //StartGame();
        }

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
            players.Content += message + '\n';
        }

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

            scoreText.Content = "Score: " + score;

            obstacle.Visibility = Visibility.Visible;
            player.Visibility = Visibility.Visible;
            background.Visibility = Visibility.Visible;
            background2.Visibility = Visibility.Visible;
            scoreText.Visibility = Visibility.Visible;

            gameTimer.Start();
        }

        private void GameEngine(object sender, EventArgs e)
        {
            Canvas.SetLeft(background, Canvas.GetLeft(background) - 3);
            Canvas.SetLeft(background2, Canvas.GetLeft(background2) - 3);

            if (Canvas.GetLeft(background) < -1262)
                Canvas.SetLeft(background, Canvas.GetLeft(background2) + background2.Width);


            if (Canvas.GetLeft(background2) < -1262)
                Canvas.SetLeft(background2, Canvas.GetLeft(background) + background.Width);

            Canvas.SetTop(player, Canvas.GetTop(player) + speed);
            Canvas.SetLeft(obstacle, Canvas.GetLeft(obstacle) - 12);

            scoreText.Content = "Score: " + score;

            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width - 15, player.Height);
            obstacleHitBox = new Rect(Canvas.GetLeft(obstacle), Canvas.GetTop(obstacle), obstacle.Width, obstacle.Height);
            groundHitBox = new Rect(Canvas.GetLeft(ground), Canvas.GetTop(ground), ground.Width, ground.Height);

            if (playerHitBox.IntersectsWith(groundHitBox))
            {
                speed = 0;
                Canvas.SetTop(player, Canvas.GetTop(ground) - player.Height);
                jumping = false;
                spriteIndex += .5;

                if (spriteIndex > 8)
                    spriteIndex = 1;

                RunSprite(spriteIndex);
            }

            if (jumping == true)
            {
                speed = -9;
                force -= 1;
            }
            else
                speed = 12;

            if (force < 0)
                jumping = false;

            if (Canvas.GetLeft(obstacle) < -50)
            {
                Canvas.SetLeft(obstacle, 950);
                Canvas.SetTop(obstacle, obstaclePosition[rnd.Next(0, obstaclePosition.Length)]);
                score += 1;
            }

            if (playerHitBox.IntersectsWith(obstacleHitBox))
            {
                gameOver = true;
                gameTimer.Stop();
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
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && gameOver == true)
            {
                StartGame();
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && gameOver == false && Canvas.GetTop(player) > 260)
            {
                //renameLater();
                jumping = true;
                force = 15;
                speed = -12;
                playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/newRunner_02.gif"));
            }
        }

        private async Task renameLater(string name)
        {
            await rService.SendTauntMessage(name);
        }

        private async Task SendStartSignalOthers()
        {
            await rService.SendStartSignal();
        }




        private void RunSprite(double i)
        {
            switch (i)
            {
                case 1:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/newRunner_01.gif"));
                    break;
                case 2:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/newRunner_02.gif"));
                    break;
                case 3:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/newRunner_03.gif"));
                    break;
                case 4:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/newRunner_04.gif"));
                    break;
                case 5:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/newRunner_05.gif"));
                    break;
                case 6:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/newRunner_06.gif"));
                    break;
                case 7:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/newRunner_07.gif"));
                    break;
                case 8:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/newRunner_08.gif"));
                    break;
            }
            player.Fill = playerSprite;
        }
        private void joinLobbyBtnClick(object sender, RoutedEventArgs e)
        {
            titlePlayers.Visibility = Visibility.Visible;
            title.Visibility = Visibility.Hidden;
            startGameBtn.Visibility = Visibility.Hidden;
            nameInput.Visibility = Visibility.Hidden;
            joinLobbyBtn.Visibility = Visibility.Hidden;
            startGameBtn.Visibility = Visibility.Visible;
            players.Visibility = Visibility.Visible;
            //players.Content = nameInput.Text;
            renameLater(nameInput.Text);
        }

        private void startBtnClick(object sender, RoutedEventArgs e)
        {
            if (CurrentPlayers != 2)
            {
                CantPlayText.Visibility = Visibility.Visible;
            }
            else 
            {
                MainBackground.Visibility = Visibility.Hidden;
                startGameBtn.Visibility = Visibility.Hidden;
                players.Visibility = Visibility.Hidden;
                players.Visibility = Visibility.Hidden;
                SendStartSignalOthers();
                //StartGame();
            }

        }

        private void StartCountDown()
        {
            StartGame();
        }


    }
}
