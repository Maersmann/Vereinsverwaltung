using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vereinsverwaltung.Data.Types;

namespace Vereinsverwaltung.Logic.UI.BaseViewModels
{
    public class ViewModelStammdaten<T> : ViewModelValidate
    {
        protected T data;
        protected State state;
        protected bool LoadAktie;
        public ICommand SaveCommand { get; protected set; }

        public ViewModelStammdaten()
        {
            LoadAktie = false;
            SaveCommand = new DelegateCommand(this.ExecuteSaveCommand, this.CanExecuteSaveCommand);
            Cleanup();
        }

        protected virtual bool CanExecuteSaveCommand()
        {
            return ValidationErrors.Count == 0;
        }

        protected virtual void ExecuteSaveCommand()
        {
            throw new NotImplementedException();
        }
    }
}
