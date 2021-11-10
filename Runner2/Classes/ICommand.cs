using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner2.Classes
{
    public abstract class ICommand
    {
        public Player player;
        public abstract void execute();
        public abstract void undo();

    }


    public class PlayerMovementController
    {
        private List<ICommand> commands;

        public PlayerMovementController()
        {
            commands = new List<ICommand>();
        }

        public void run(ICommand cmd)
        {
            commands.Add(cmd);
            cmd.execute();

        }

    }
    public class PlayerStatsController
    {
        private List<ICommand> commands;
        public PlayerStatsController()
        {
            commands = new List<ICommand>();
        }

        public void run(ICommand cmd)
        {
            commands.Add(cmd);
            cmd.execute();

        }

        public void undo()
        {
            if(commands.Count != 0)
            {
                ICommand cmd = commands.Last();
                cmd.undo();
                commands.Remove(cmd);
            }

        }
    }

    public class MoveLeftCommand : ICommand
    {
        public override void execute()
        {

        }

        public override void undo()
        {
        }
    }

    public class MoveRightCommand : ICommand
    {
        public override void execute()
        {
            
        }

        public override void undo()
        {
        }
    }
    public class ModifyPointsCommand : ICommand
    {
        public Item item;
        public ModifyPointsCommand(Player target, Item item)
        {
            this.item = item;
            this.player = target;
        }
        public override void execute()
        {
            item.modifyPoints(player);
        }

        public override void undo()
        {
            switch (item.isGood)
            {
                case true:
                    player.Points.SubtractPoints(item.pointsModifier);
                    return;
                case false:
                    player.Points.AddPoints(item.pointsModifier);
                    return;
            }
            
        }
    }
    //public class RemovePointsCommand : ICommand
    //{
    //    public Item item;
    //    public int targetPoints;
    //    public RemovePointsCommand(Player target, Item item)
    //    {
    //        this.item = item;
    //        this.player = target;
    //    }
    //    public override void execute()
    //    {
    //        item.modifyPoints(player);
    //    }

    //    public override void undo()
    //    {
    //        switch (item.isGood)
    //        {
    //            case true:
    //                player.Points.SubtractPoints(item.pointsModifier);
    //                return;
    //            case false:
    //                player.Points.AddPoints(item.pointsModifier);
    //                return;
    //        }

    //    }
    //}
    public class GiveSpeedCommand : ICommand
    {
        public float targetSpeed;
        public GiveSpeedCommand(Player target, int speed)
        {
            targetSpeed = speed;
            this.player = target;
        }
        public override void execute()
        {
            player.Speed += targetSpeed;
        }
        public override void undo()
        {
            player.Speed -= targetSpeed;
            
        }
    }
    public class RemoveSpeedCommand : ICommand
    {
        public float targetSpeed;
        public RemoveSpeedCommand(Player target, int speed)
        {
            targetSpeed = speed;
            this.player = target;
        }
        public override void execute()
        {
            player.Speed -= targetSpeed;
        }

        public override void undo()
        {

            player.Speed += targetSpeed;
        }
    }
}
