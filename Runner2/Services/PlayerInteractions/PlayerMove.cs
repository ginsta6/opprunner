using Runner2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner2.Services.PlayerInteractions
{
    public class PlayerMove
    {
        private readonly CanvasPosition _playersPositions;

        private readonly CanvasPosition _playersStartingPositions;
        private bool _isLeft;
    
        public PlayerMove(CanvasPosition playersPositions)
        {
            _playersPositions = playersPositions;
            _playersStartingPositions = new CanvasPosition(playersPositions);
        }
        private void SetNextCoordinate(bool left)
        {
            if(left)
            {
                _playersPositions.PlayerCharacter.PlayerLeft += 5;
            }
        }
    }
}
