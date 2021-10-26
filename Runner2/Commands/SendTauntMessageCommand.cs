using Runner2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Runner2.Commands
{
    //class SendTauntMessageCommand : ICommand
    //{
    //    private readonly SignalRService _rService;
        
    //    public SendTauntMessageCommand(SignalRService rService)
    //    {
    //        _rService = rService;
    //    }

    //    public event EventHandler CanExecuteChanged;
        

    //    public bool CanExecute(object parameter)
    //    {
    //        return true;
    //    }

    //    public async void Execute(object parameter)
    //    {
    //        try
    //        {
    //            await _rService.SendTauntMessage("zinute");
    //        }
    //        catch (Exception)
    //        {

    //            throw;
    //        }
    //    }
    //}
}
