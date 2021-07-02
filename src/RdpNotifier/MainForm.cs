using System.ComponentModel;
using System.Windows.Forms;

namespace RdpNotifier
{
    public partial class MainForm : Form
    {
        public MainForm(MainViewModel vm)
        {
            InitializeComponent();
            bindingSource1.DataSource = vm;

            vm.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            var vm = (MainViewModel)sender!;

            if (e.PropertyName == nameof(vm.IsLoading))
                textBox1.Cursor = Cursor = vm.IsLoading ? Cursors.AppStarting : null;
        }
    }
}
