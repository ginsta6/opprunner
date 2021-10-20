using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Runner2.ViewModels
{
    public class GameViewModel: Screen
    {
        private ImageSource _player;
        private ImageSource _player1;
        private ImageSource _obstacle;
        private ImageSource _item;
        private ImageSource _background;
        private Label _scoreText;
        private Label _otherScoreText;
        
        public GameViewModel()
        {
            _player= new BitmapImage(new Uri("pack://application:,,,/Images/dude/dude5.png"));
            _player1= new BitmapImage(new Uri("pack://application:,,,/Images/pink/pink5.png"));
            _obstacle= new BitmapImage(new Uri("pack://application:,,,/Images/obstacle.png"));
            _item = new BitmapImage(new Uri("pack://application:,,,/Images/item.png"));
            _background= new BitmapImage(new Uri("pack://application:,,,/Images/background.gif"));
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

    }
}
