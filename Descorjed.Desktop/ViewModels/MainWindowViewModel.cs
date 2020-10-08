using Descorjed.Client.Http;
using Descorjed.Client.Http.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Descorjed.Desktop.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string _greeting;

        private string _state;

        private DescorjedClient client;
        public string State
        {
            get => _state;


            set
            {
                _state = value;

                OnPropertyChanged(nameof(State));
            }
        }

        public string Greeting {
            get => _greeting;
            
            set {
                _greeting = value;
                OnPropertyChanged(nameof(Greeting));
            } 
        
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindowViewModel()
        {
            State = "Connecting...";

            client = new DescorjedClient();

            State = "Connected, Getting servers";
            try
            {
                loadServers();
            }catch(Exception e)
            {
                State = $"Error while getting servers, details: {e.StackTrace}";
            }
            




        }

        public async void loadServers()
        {
            List<Server> servers = await client.GetServers();
            var _newState = $"Servers({servers.Count}):\n";
            servers.ForEach(server =>
            {
                _newState += $"{server.name}\n";
            });

            State = _newState;
            
        }

        
    }
}
