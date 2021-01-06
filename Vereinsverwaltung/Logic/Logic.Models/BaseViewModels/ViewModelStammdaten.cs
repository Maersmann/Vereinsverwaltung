﻿using Data.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Logic.UI.BaseViewModels
{
    public class ViewModelStammdaten : ViewModelBasis
    {
        protected State state;
        public ICommand SaveCommand { get; protected set; }

        protected bool CanExecuteSaveCommand()
        {
            return ValidationErrors.Count == 0;
        }

        protected virtual void ExecuteSaveCommand()
        {
            throw new NotImplementedException();
        }
    }
}
