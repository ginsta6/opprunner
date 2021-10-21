using Caliburn.Micro;
using Runner2.ViewModels;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Caliburn.Micro;
using System.Windows.Input;
using Runner2.Input;

namespace Runner2
{
    public class Bootstrapper : BootstrapperBase
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        private SimpleContainer _container;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);

            //Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();

            ServiceProvider = serviceCollection.BuildServiceProvider();

            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void Configure()
        {
            _container = new SimpleContainer();
            _container.PerRequest<ShellViewModel>();

            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventAggregator, EventAggregator>();

            _container.Singleton<StartViewModel>();
            _container.Singleton<GameViewModel>();
            _container.Singleton<LobbyViewModel>();

            var defaultCreateTrigger = Parser.CreateTrigger;

            Parser.CreateTrigger = (target, triggerText) =>
            {
                if (triggerText == null)
                {
                    return defaultCreateTrigger(target, null);
                }

                var triggerDetail = triggerText
                    .Replace("[", string.Empty)
                    .Replace("]", string.Empty);

                var splits = triggerDetail.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

                switch (splits[0])
                {
                    case "Key":
                        var key = (Key)Enum.Parse(typeof(Key), splits[1], true);
                        return new KeyTrigger { Key = key };

                    case "Gesture":
                        var mkg = (MultiKeyGesture)(new MultiKeyGestureConverter()).ConvertFrom(splits[1]);
                        return new KeyTrigger { Modifiers = mkg.KeySequences[0].Modifiers, Key = mkg.KeySequences[0].Keys[0] };
                }

                return defaultCreateTrigger(target, triggerText);
            };
        }
    }
}
