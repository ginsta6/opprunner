using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner2.Classes
{
    public class CanvasPosition: INotifyPropertyChanged
    {
        public CanvasPosition()
        {
            // Characters
            PlayerCharacter = new PlayerSidePosition();
            //EnemyCharacter = new EnemySidePosition();         
           
        }

        public CanvasPosition(CanvasPosition canvasPosition)
        {
            PlayerCharacter = new PlayerSidePosition(canvasPosition.PlayerCharacter);
           // EnemyCharacter = new EnemySidePosition(canvasPosition.EnemyCharacter);
        }

        private PlayerSidePosition _playerCharacter;

        public PlayerSidePosition PlayerCharacter
        {
            get => _playerCharacter;

            set
            {
                _playerCharacter = value;
                NotifyPropertyChanged("PlayerCharacter");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
