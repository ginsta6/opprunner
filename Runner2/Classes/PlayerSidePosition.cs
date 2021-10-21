using System.ComponentModel;

namespace Runner2.Classes
{
    public class PlayerSidePosition : INotifyPropertyChanged
    {
        public PlayerSidePosition() { }

        public PlayerSidePosition(PlayerSidePosition playerSidePosition)
        {
            PlayerLeft = playerSidePosition.PlayerLeft;
            PlayerWidth = playerSidePosition.PlayerWidth;
            PlayerBottom = playerSidePosition.PlayerBottom;
        }

        private int _playerLeft;

        public int PlayerLeft
        {
            get => _playerLeft;

            set
            {
                _playerLeft = value;
                NotifyPropertyChanged("PlayerLeft");
            }
        }

        private int _playerWidth;

        public int PlayerWidth
        {
            get => _playerWidth;

            set
            {
                _playerWidth = value;
                NotifyPropertyChanged("PlayerWidth");
            }
        }

        private int _playerBottom;

        public int PlayerBottom
        {
            get => _playerBottom;

            set
            {
                _playerBottom = value;
                NotifyPropertyChanged("PlayerBottom");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
