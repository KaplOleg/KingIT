using KingIT.View.OperationWindow;
using System.Windows;

namespace KingIT.View
{
    public partial class MessengerC : Window
    {
        public MessengerC()
        {
            InitializeComponent();
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            EditWindow window = new EditWindow();
            window.Show();
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            AddWindow window = new AddWindow();
            window.Show();
        }

        private void Button_Click_Read_Pavilions(object sender, RoutedEventArgs e)
        {
            PavilionsWindows window = new PavilionsWindows();
            window.Show();
        }
    }
}
