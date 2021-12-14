using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner2.Classes
{
    public interface Controller
    {
        void run(ICommand cmd);
        void undo();
        
    }

    class ControllerProxy : Controller
    {
        private PlayerStatsController controller;
        public string msg = "";

        public ControllerProxy(PlayerStatsController controller)
        {
            this.controller = controller;
        }
        public void run(ICommand cmd)
        {
            msg = "You do not have access to run!";
        }

        public void undo()
        {
            msg = "You do not have access to undo!";
        }

        public void GetContent()
        {
            foreach (var item in controller.GetCommands())
            {
                msg += "\n" + item.ToString();
            }
        }

    }
}
