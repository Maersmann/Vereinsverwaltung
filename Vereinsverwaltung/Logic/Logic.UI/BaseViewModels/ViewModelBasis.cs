using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Logic.Core;
using Logic.Messages.BaseMessages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Logic.UI.BaseViewModels
{
    public class ViewModelBasis : ViewModelBase
    {
        public ViewModelBasis()
        {
            CleanUpCommand = new RelayCommand(() => ExecuteCleanUpCommand());
            this.CloseWindowCommand = new RelayCommand<Window>(this.ExecuteCloseWindowCommand);
            SetConnection();
        }

        protected string messageToken;
        public string Title { get; protected set; }
        public string MessageToken { set { messageToken = value; } }

        protected HttpClient Client { get; set; }

        public ICommand CleanUpCommand { get; set; }
        public RelayCommand<Window> CloseWindowCommand { get; private set; }

        protected virtual void ExecuteCleanUpCommand()
        {
            Cleanup(); 
        }

        protected virtual void ExecuteCloseWindowCommand(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }



        public void SendExceptionMessage(string inException)
        {
            Messenger.Default.Send<ExceptionMessage>(new ExceptionMessage {  Message = inException });
        }

        public void SendInformationMessage(string informationMessage)
        {
            Messenger.Default.Send<InformationMessage>(new InformationMessage { Message = informationMessage });
        }


        public void OnlyNumbersCommand(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        public void OnlyBetragCommand(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Equals(","))
            {
                e.Handled = true;
            }
            else
            { 
                Regex regex = new Regex("[^0-9]+");
                e.Handled = regex.IsMatch(e.Text);
            }
        }

        private void SetConnection()
        {
            string url;
            if (GlobalVariables.BackendServer_IP == null || GlobalVariables.BackendServer_IP.Equals(""))
                url = "https://localhost:5003";
            else
                url = GlobalVariables.BackendServer_URL;
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };
            Client = new HttpClient(clientHandler)
            {
                BaseAddress = new Uri(url + "/")
            };
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/json"));
        }


    }
}
