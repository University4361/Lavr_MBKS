using System.Windows;
using System.Windows.Controls;

namespace Lavrov_lab2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _currentCode;
        private AccessUser _currentAccessUser;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            FirstTB.Text = string.Empty;
            SecondTB.Text = string.Empty;
            ThridTB.Text = string.Empty;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (FirstRB.IsChecked ?? false)
                _currentCode = FirstTB.Text;
            if (SecondRB.IsChecked ?? false)
                _currentCode = SecondTB.Text;
            if (ThridRB.IsChecked ?? false)
                _currentCode = ThridTB.Text;

            _currentAccessUser = new AccessUser(_currentCode);
        }

        private void InputTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(_currentCode) || _currentAccessUser == null)
                return;

            OutputTB.Text = string.Empty;

            foreach (var ch in InputTB.Text)
            {
                if (_currentAccessUser.AccessDictionary.ContainsKey(ch) && _currentAccessUser.AccessDictionary[ch] == 1)
                    OutputTB.Text += ch;
            }
        }
    }
}
