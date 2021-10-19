using Caliburn.Micro;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using Runner2.Classes;

namespace Runner2.ViewModels
{
    public class StartViewModel : Screen
    {
       public int currentPlayerTypeIndex = 1;
        int maxPlayerTypeIndex = 3;

        public StartViewModel()
        {
            _characterTypeSelected = new Label();  
            _characterTypeSelected.Content = "Pink Monster";
            _characterTypeSelected.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            _avatar = new BitmapImage(new Uri("pack://application:,,,/Images/avatarowlet1.png"));
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
        public void JoinLobby()
        {
            //susikurt player-----------------------------------------------------------------------------------------
            //Player player= new
            ChangeView(new LobbyViewModel());
        }
        public async void ChangeView(Screen screen)
        {
            if (Parent is IConductor conductor)
                await conductor.ActivateItemAsync(screen);
        }
        public void RightBtn()
        {
            currentPlayerTypeIndex--;
            if (currentPlayerTypeIndex == 0)
                currentPlayerTypeIndex = maxPlayerTypeIndex;
            changeAvatar(currentPlayerTypeIndex);
        }
        public void LeftBtn()
        {
            currentPlayerTypeIndex++;
            if (currentPlayerTypeIndex > maxPlayerTypeIndex)
                currentPlayerTypeIndex = 1;
            changeAvatar(currentPlayerTypeIndex);
        }
        private void changeAvatar(int index)
        {
            ImageSource belekas = new BitmapImage(new Uri("pack://application:,,,/Images/avatarowlet1.png"));
            switch (index)
            {
                case 1:
                    belekas = new BitmapImage(new Uri("pack://application:,,,/Images/avatarowlet1.png"));
                    _characterTypeSelected.Content = "Owlet monster";
                    break;
                case 2:
                    belekas = new BitmapImage(new Uri("pack://application:,,,/Images/avatarpink.png"));
                    _characterTypeSelected.Content = "Pink monster";
                    break;
                case 3:
                    belekas = new BitmapImage(new Uri("pack://application:,,,/Images/avatardude.png"));
                    _characterTypeSelected.Content = "Dude monster";
                    break;
            }
            _avatar = belekas;

            NotifyOfPropertyChange(() => Avatar);
        }

    }
}
