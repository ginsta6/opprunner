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
    public class LobbyViewModel:Screen
    {
        public LobbyViewModel()
        {
            _characterTypeSelected = new Label();
            _characterTypeSelected.Content = "Pink Monster";
            _characterTypeSelected.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            _avatar = new BitmapImage(new Uri("pack://application:,,,/Images/avatarowlet1.png"));
        }

        public void StartGame()
        {
            ChangeView(new GameViewModel());
        }
        public async void ChangeView(Screen screen)
        {
            if (Parent is IConductor conductor)
                await conductor.ActivateItemAsync(screen);
        }

        private Label _characterTypeSelected;
        public Label CharacterTypeSelected
        {
            get => _characterTypeSelected;
            set
            {
                _characterTypeSelected = value;
                NotifyOfPropertyChange(() => CharacterTypeSelected);
            }
        }



        private ImageSource _avatar;
        public ImageSource Avatar
        {
            get => _avatar;
            set
            {
                _avatar = value;
                NotifyOfPropertyChange(() => Avatar);
            }
        }
    }
}
