using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Runner2.ViewModels
{

    public class GameViewModel : Screen
    {
        PlayerAnimationState playerAnimationCurrentState;
        enum PlayerAnimationState
        {
            RunningLeft,
            RunningRight,
            Standing
        }
        int currentPlayerTypeIndex = 3;

        double spriteIndex = 1;
        double spriteIndexJump = 0;

        private ImageSource _player;
        private ImageSource _player1;
        private ImageSource _obstacle;
        private ImageSource _item;
        private ImageSource _background;
        private Label _scoreText;
        private Label _otherScoreText;
        private int _playerLeft = 110;
        int _playerSpeed;
        bool finish;
        DispatcherTimer gameTimer = new DispatcherTimer();

        public GameViewModel()
        {
            _scoreText = new Label();
            _player = new BitmapImage(new Uri("pack://application:,,,/Images/dude/dude5.png"));
            _player1 = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pink5.png"));
            _obstacle = new BitmapImage(new Uri("pack://application:,,,/Images/obstacle.png"));
            _item = new BitmapImage(new Uri("pack://application:,,,/Images/item.png"));
            _background = new BitmapImage(new Uri("pack://application:,,,/Images/background.gif"));
            gameTimer.Tick += Update;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            _playerSpeed = 5;
            finish = false;
            // This code line must be the last in the constructor!
            gameTimer.Start();
        }

        public ImageSource Player
        {
            get => _player;
            set
            {
                _player = value;
                NotifyOfPropertyChange(() => Player);
            }
        }
        public ImageSource Player1
        {
            get => _player1;
            set
            {
                _player1 = value;
                NotifyOfPropertyChange(() => Player1);
            }
        }
        public ImageSource Obstacle
        {
            get => _obstacle;
            set
            {
                _obstacle = value;
                NotifyOfPropertyChange(() => Obstacle);
            }
        }
        public ImageSource Item
        {
            get => _item;
            set
            {
                _item = value;
                NotifyOfPropertyChange(() => Item);
            }
        }
        public ImageSource Background
        {
            get => _background;

            set
            {
                _background = value;
                NotifyOfPropertyChange(() => Background);
            }
        }
        public Label ScoreText
        {
            get => _scoreText;
            set
            {
                _scoreText = value;
                NotifyOfPropertyChange(() => ScoreText);
            }
        }

        public int PlayerLeft
        {
            get => _playerLeft;
            set
            {
                _playerLeft = value;
                NotifyOfPropertyChange(() => PlayerLeft);
            }
        }


        public void MoveLeft()
        {
            playerAnimationCurrentState = PlayerAnimationState.RunningLeft;
            //EnterMessage = "Enter has been pressed " + count.ToString() + " " + PlayerLeft.ToString();
        }

        public void MoveRight()
        {
            playerAnimationCurrentState = PlayerAnimationState.RunningRight;

        }
        public void Jump()
        {

        }
        private void Update(object sender, EventArgs e)
        {
            if (playerAnimationCurrentState == PlayerAnimationState.RunningLeft || playerAnimationCurrentState == PlayerAnimationState.RunningRight)
            {
                spriteIndex += .5;

                if (spriteIndex > 6)
                    spriteIndex = 1;
                RunSprite(spriteIndex);

            }
            else IdleSprite();
            HandleInputs();
            playerAnimationCurrentState = PlayerAnimationState.Standing;
        }

        private void HandleInputs()
        {
            //Player.RenderTransformOrigin = new Point(0.5, 0.5);
            //ScaleTransform flipTrans = new ScaleTransform();
            if (playerAnimationCurrentState == PlayerAnimationState.RunningLeft)
            {
                //flipTrans.ScaleX = -1;
                _playerLeft -= _playerSpeed;
            }
            else if (playerAnimationCurrentState == PlayerAnimationState.RunningRight)
            {
                //flipTrans.ScaleX = 1;
                _playerLeft += _playerSpeed;
            }
            NotifyOfPropertyChange(() => PlayerLeft);
            //player.RenderTransform = flipTrans;
        }
        private void IdleSprite()
        {
            if (currentPlayerTypeIndex == 1)
            {
                _player = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pinkjump4.png"));

            }
            else if (currentPlayerTypeIndex == 2)
            {
                _player = new BitmapImage(new Uri("pack://application:,,,/Images/avatarowlet1.png"));

            }
            else if (currentPlayerTypeIndex == 3)
            {
                _player = new BitmapImage(new Uri("pack://application:,,,/Images/avatardude.png"));
            }
        }
        private void RunSprite(double i)
        {
            if (currentPlayerTypeIndex == 1)
            {
                switch (i)
                {
                    case 1:
                        _player = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pink1.png"));
                        break;
                    case 2:
                        _player = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pink2.png"));
                        break;
                    case 3:
                        _player = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pink3.png"));
                        break;
                    case 4:
                        _player = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pink4.png"));
                        break;
                    case 5:
                        _player = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pink5.png"));
                        break;
                    case 6:
                        _player = new BitmapImage(new Uri("pack://application:,,,/Images/pink/pink6.png"));
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
                        _player = new BitmapImage(new Uri("pack://application:,,,/Images/owlet/owlet1.png"));
                        break;
                    case 2:
                        _player = new BitmapImage(new Uri("pack://application:,,,/Images/owlet/owlet2.png"));
                        break;
                    case 3:
                        _player = new BitmapImage(new Uri("pack://application:,,,/Images/owlet/owlet3.png"));
                        break;
                    case 4:
                        _player = new BitmapImage(new Uri("pack://application:,,,/Images/owlet/owlet4.png"));
                        break;
                    case 5:
                        _player = new BitmapImage(new Uri("pack://application:,,,/Images/owlet/owlet5.png"));
                        break;
                    case 6:
                        _player = new BitmapImage(new Uri("pack://application:,,,/Images/owlet/owlet6.png"));
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
                        _player = new BitmapImage(new Uri("pack://application:,,,/Images/dude/dude1.png"));
                        break;
                    case 2:
                        _player = new BitmapImage(new Uri("pack://application:,,,/Images/dude/dude2.png"));
                        break;
                    case 3:
                        _player = new BitmapImage(new Uri("pack://application:,,,/Images/dude/dude3.png"));
                        break;
                    case 4:
                        _player = new BitmapImage(new Uri("pack://application:,,,/Images/dude/dude4.png"));
                        break;
                    case 5:
                        _player = new BitmapImage(new Uri("pack://application:,,,/Images/dude/dude5.png"));
                        break;
                    case 6:
                        _player = new BitmapImage(new Uri("pack://application:,,,/Images/dude/dude6.png"));
                        break;
                    default:
                        break;
                }
            }
            NotifyOfPropertyChange(() => Player);
        }

        //public void AltEnterPressed()
        //{
        //    EnterMessage = "Alt+Enter has been pressed";
        //}
        public void KeyIsDown(ActionExecutionContext context)
        {
            var keyArgs = context.EventArgs as KeyEventArgs;

            if (keyArgs != null && keyArgs.Key == Key.Enter)
            {
                ScoreText.Content = "labas";
                NotifyOfPropertyChange(() => ScoreText);
            }
            //var e = (System.Windows.Input.KeyEventArgs)context.EventArgs;
            //if (e.Key == System.Windows.Input.Key.Left)
            //{
            //    //flipTrans.ScaleX = -1;
            //    PlayerLeft += 10;
            //    //Canvas.SetLeft(Player, pos + 5);
            //}
            //else if (e.Key == System.Windows.Input.Key.Right)
            //{
            //    PlayerLeft -= 10;
            //    //flipTrans.ScaleX = 1;
            //    //Canvas.SetLeft(Player, Canvas.GetLeft(Player) + 5);
            //}
        }

    }
}
