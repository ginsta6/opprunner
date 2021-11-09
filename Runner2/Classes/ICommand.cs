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
    public class GivePointsCommand : ICommand
    {
        public int targetPoints;
        public GivePointsCommand(Player target, int points)
        {
            targetPoints = points;
            this.player = target;
        }
        public override void execute()
        {
            player.Points.AddPoints(targetPoints);
        }

        public override void undo()
        {
            player.Points.SubtractPoints(targetPoints);
        }
    }
    public class RemovePointsCommand : ICommand
    {
        public int targetPoints;
        public RemovePointsCommand(Player target, int points)
        {
            targetPoints = points;
            this.player = target;
        }
        public override void execute()
        {
            player.Points.SubtractPoints(targetPoints);
        }

        public override void undo()
        {
            
            player.Points.AddPoints(targetPoints);
        }
    }
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
