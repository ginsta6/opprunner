using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner2.Classes
{
    public abstract class Potion
    {
        public PotionEffectAlgorithm algorithm;
    }

    public class SpeedUpPotion : Potion
    {
        public SpeedUpPotion()
        {
            algorithm = new IncreaseSpeedPotion();
        }
    }

    public class SpeedDownPotion : Potion
    {
        public SpeedDownPotion()
        {
            algorithm = new DecreaseSpeedPotion();
        }
    }

    public abstract class PotionEffectAlgorithm
    {
        public PotionEffectAlgorithm()
        {

        }
        public abstract void giveEffect(Player player);
    }

    public class IncreaseSpeedPotion : PotionEffectAlgorithm
    {
        public override void giveEffect(Player player)
        {
            player.Speed += 6;
        }
    }
    public class DecreaseSpeedPotion : PotionEffectAlgorithm
    {
        public override void giveEffect(Player player)
        {
            player.Speed -= 6;
        }
    }
}
