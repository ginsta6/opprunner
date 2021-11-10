using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Runner2.Classes
{
    public class ObstacleAdapter : Item
    {
        Obstacle obstacle;

        public ObstacleAdapter(Obstacle obs)
        {
            isGood = false;
            pointsModifier = obs.pointsModifier ;
            obstacle = obs;
        }        

        public override void modifyPoints(Player player)
        {
            obstacle.removePoints(player);
        }        
    }
}
