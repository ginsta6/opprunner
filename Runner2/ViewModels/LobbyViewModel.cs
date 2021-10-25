using Caliburn.Micro;
using Runner2.Classes;
using Runner2.Services;
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
    public class LobbyViewModel : Screen, IObserver
    {
        private SignalRService rService;
        private int CurrentPlayers;

        public LobbyViewModel(SignalRService rservice)
        {
            rService = rservice;
            rService.Register(this);

            _characterTypeSelected = new Label();
            _characterTypeSelected.Content = "Pink Monster";
            _characterTypeSelected.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            _avatar = new BitmapImage(new Uri("pack://application:,,,/Images/avatarowlet1.png"));

            _playerNumberError = new Label();
        }

        private Label _playerNumberError;
        public Label PlayerNumberError
        {
            get => _playerNumberError;
            set
            {
                _playerNumberError = value;
                NotifyOfPropertyChange(() => PlayerNumberError);
            }
        }

        public void StartGame()
        {
            if (CurrentPlayers < 1)                     //----------------------------------++++++++++++++*+*+**+**+**+*+*++**+*+*+*++*+
            {
                _playerNumberError.Content = "Not enough players";
                _playerNumberError.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                NotifyOfPropertyChange(() => PlayerNumberError);
            }
            else
            {
                _playerNumberError.Content = "";
                NotifyOfPropertyChange(() => PlayerNumberError);

                SendSignalToServer("SendStartSignal");
            }
        }
        private async Task SendSignalToServer(string name)
        {
            await rService.SendSignalToServer(name);
        }
        public async void ChangeView(Screen screen)
        {
            if (Parent is IConductor conductor)
                await conductor.ActivateItemAsync(screen);
        }

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
                            ChangeView(new GameViewModel());
                            break;
                        default:
                            _players = Convert.ToString(message);      //Gal geriau butu (ateity maziau problemu gal) jeigu paduoti player-name ne kaip stringa, o kaip koki "User" klases objekta?
                            NotifyOfPropertyChange(() => Players);
                            break;
                    }
                    break;
                default:
                    break;
            }
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

        private string _players;
        public string Players
        {
            get => _players;
            set
            {
                _players = value;
                NotifyOfPropertyChange(() => Players);
            }
        }
    }
}
