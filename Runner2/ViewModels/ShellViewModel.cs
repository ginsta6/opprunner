using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner2.ViewModels
{
    public class ShellViewModel: Conductor<object>
    {
        public ShellViewModel() { }

        public ShellViewModel(StartViewModel menuViewModel)
        {
            ChangeView(menuViewModel);
        }

        public async void ChangeView(Screen screen)
        {
            await ActivateItemAsync(screen);
        }
    }
}
