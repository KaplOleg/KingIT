using System.Windows;

namespace KingIT.View.OperationWindow
{
    public partial class PavilionsWindows : Window
    {
        public PavilionsWindows()
        {
            InitializeComponent();
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            AddPavilionWinow addPavilion = new AddPavilionWinow();
            addPavilion.Show();
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            EditPavilionWindow editPavilion = new EditPavilionWindow();
            editPavilion.Show();
        }
    }
}
