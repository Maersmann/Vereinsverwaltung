using UI.Desktop.BaseViews;

namespace UI.Desktop.Vereinsmeisterschaft
{
    /// <summary>
    /// Interaktionslogik für VereinsmeisterschaftNeueErstellenView.xaml
    /// </summary>
    public partial class VereinsmeisterschaftNeueErstellenView : StammdatenView
    {
        public VereinsmeisterschaftNeueErstellenView()
        {
            InitializeComponent();
            RegisterStammdatenGespeichertMessage(Data.Types.StammdatenTypes.vereinsmeisterschaft);
        }
    }
}
