﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Vereinsverwaltung.Logic.UI.BaseViewModels
{
    public class ViewModelBasis : ViewModelBase
    {
        public ViewModelBasis()
        {
            CloseCommand = new RelayCommand(() => ExecuteCloseCommand());
        }

        public string Title { get; protected set; }

        public ICommand CloseCommand { get; set; }

        protected virtual void ExecuteCloseCommand()
        {
            throw new NotImplementedException();   
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




    }
}
