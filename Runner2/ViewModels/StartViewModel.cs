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
using Microsoft.AspNetCore.SignalR.Client;
using Runner2.Services;

namespace Runner2.ViewModels
{
    public class StartViewModel : Screen
    {
        //HubConnection connection;
        SignalRService rService;

        public int currentPlayerTypeIndex = 1;
        int maxPlayerTypeIndex = 3;

        public int[] SpeedArray;
        public int[] JumpArray;


        public StartViewModel()
        {

            rService = new SignalRService();

            rService.Connect();

            _characterTypeSelected = new Label();
            _characterTypeSelected.Content = "Pink Monster";
            _characterTypeSelected.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            _avatar = new BitmapImage(new Uri("pack://application:,,,/Images/avatarowlet1.png"));
            SpeedArray = new int[] { 10, 5, 20 };
            JumpArray = new int[] { 12, 18, 7 };
            _characterNameError = new Label();

            _characterSpeed = new Label();
            _characterSpeed.Content = "Speed: " + SpeedArray[0];
            _characterSpeed.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            _characterSpeed.Height = 49.0d;
            _characterSpeed.FontSize = 14;
            _characterJump = new Label();
            _characterJump.Content = "Jump: " + JumpArray[0];
            _characterJump.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            _characterJump.Height = 49.0d;
            _characterJump.FontSize = 14;
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
        private Label _characterSpeed;
        public Label CharacterSpeed
        {
            get => _characterSpeed;
            set
            {
                _characterSpeed = value;
                NotifyOfPropertyChange(() => CharacterSpeed);
            }
        }
        private Label _characterJump;
        public Label CharacterJump
        {
            get => _characterJump;
            set
            {
                _characterJump = value;
                NotifyOfPropertyChange(() => CharacterJump);
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

        private string _nameInput;
        public string NameInput
        {
            get => _nameInput;
            set
            {
                _nameInput = value;
                NotifyOfPropertyChange(() => NameInput);
            }
        }

        private Label _characterNameError;
        public Label CharacterNameError
        {
            get => _characterNameError;
            set
            {
                _characterNameError = value;
                NotifyOfPropertyChange(() => CharacterNameError);
            }
        }

        public void JoinLobby()
        {
            if (NameInput != null)
            {
                _characterNameError.Content = "";
                NotifyOfPropertyChange(() => CharacterNameError);

                //susikurt player-----------------------------------------------------------------------------------------
                //Player player= new
                SendPlayerToServer(NameInput);
                ChangeView(new LobbyViewModel(rService));
            }
            else
            {
                _characterNameError.Content = "Name can not be empty";
                _characterNameError.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                NotifyOfPropertyChange(() => CharacterNameError);
            }
        }
        private async Task SendPlayerToServer(string name)
        {
            await rService.SendPlayerMessage(name);
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
            _characterSpeed.Content = "Speed: " + SpeedArray[index - 1];
            _characterJump.Content = "Jump: " + JumpArray[index - 1];

            NotifyOfPropertyChange(() => CharacterSpeed);
            NotifyOfPropertyChange(() => CharacterJump);
            NotifyOfPropertyChange(() => Avatar);
        }

    }
}
