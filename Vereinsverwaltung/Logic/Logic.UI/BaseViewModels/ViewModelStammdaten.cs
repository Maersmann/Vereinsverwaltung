using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Vereinsverwaltung.Logic.UI.BaseViewModels
{
    public class ViewModelStammdaten : ViewModelBasis
    {
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
