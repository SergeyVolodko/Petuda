using System.Windows;
using System.Windows.Input;

namespace Petuda.Views
{
    /// <summary>
    /// Interaction logic for JokeEditor.xaml
    /// </summary>
    public partial class JokeEditor : Window
    {
        public JokeEditor()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void JokeEditorWindow_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }

            DragMove();
        }
    }
}