using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Petuda.ViewModels;
using Petuda.ViewModels.Events;
using Petuda.Views.Helpers;
using Petuda.Views.Resources;

namespace Petuda.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel viewModel;

        private bool scriptsPanelOpened = false;
        private bool isLoaded = false;

        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();

            this.DataContext = viewModel;

            this.viewModel = viewModel;
            
            this.viewModel.JokeAddedOrChanged += ViewModelOnJokeAddedOrChanged;
        }

        // open "joke editor window" on mouse left doubleclick
        // if we've clicked on the empty space then we can add new joke
        private void dataGridJokes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.GetPosition(dataGridJokes).Y < 50 || 
                e.GetPosition(dataGridJokes).X > dataGridJokes.ActualWidth - 20 ||
                e.ChangedButton != MouseButton.Left)
            {
                return;
            }

            var selectedDataGridRow = PetudaVisualTreeHelper.FindAnchestor<DataGridRow>((DependencyObject)e.OriginalSource);

            if (selectedDataGridRow != null && this.viewModel .EditJokeCommand.CanExecute(null))
            {
                this.viewModel.EditJokeCommand.Execute(null);
                return;
            }
            
            //if there is no selected jokes then we've clicked on the empty space in datagrid
            //then we can open add new joke dialog
            if ( this.viewModel.AddJokeCommand.CanExecute(null))
            {
                this.viewModel.AddJokeCommand.Execute(null);
            }
        }

        private void textBlockNoJokesInformationMessage_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount < 2)
            {
                return;
            }

            if (this.viewModel.AddJokeCommand.CanExecute(null))
            {
                this.viewModel.AddJokeCommand.Execute(null);
            }
        }

        #region Script panel

        private void expanderScripts_Expanded(object sender, RoutedEventArgs e)
        {
            if (!this.scriptsPanelOpened && this.viewModel.OpenScriptPanelCommand.CanExecute(null))
            {
                this.viewModel.OpenScriptPanelCommand.Execute(null);
                this.scriptsPanelOpened = true;
            }
        }

        #endregion

        #region Drag and drop

        private void pageRoot_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("DraggedJokeFormat"))
            {
                Size popupSize = new Size(popupDraggingJoke.ActualWidth, popupDraggingJoke.ActualHeight);
                var position = e.GetPosition(this);
                position.Y += 10;
                popupDraggingJoke.PlacementRectangle = new Rect(position, popupSize);
            }
        }

        private void dataGridJokes_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
                return;

            // Get the dragged Item
            DataGridRow dataGridRow = PetudaVisualTreeHelper.FindAnchestor<DataGridRow>((DependencyObject) e.OriginalSource);

            if (dataGridRow == null)
            {
                return;
            }

            // Initialize the drag & drop operation
            DataObject dragData = new DataObject("DraggedJokeFormat", "");

            this.viewModel.NotifyStartJokeDrag();

            DragDrop.DoDragDrop(dataGridRow, dragData, DragDropEffects.All);

            // Close drag popup
            e.Handled = false;
            this.viewModel.CloseDraggingPopup();
        }

        #endregion

        #region Scroll to added joke

        private void ViewModelOnJokeAddedOrChanged(object sender, IndexEventArgs e)
        {
            if (e.Index >= 0)
            {
                ScrollToSelectedJoke(e.Index);
            }
        }

        private void ScrollToSelectedJoke(int jokeIndex)
        {
            var joke = dataGridJokes.Items[jokeIndex];
            dataGridJokes.ScrollIntoView(joke);
            dataGridJokes.Focus();
        }

        #endregion

        // "Select a date" watermark fix
        private void DatePicker_OnLoaded(object sender, RoutedEventArgs e)
        {
            var dp = sender as DatePicker;
            if (dp == null)
            {
                return;
            }

            var tb = PetudaVisualTreeHelper.GetChildOfType<DatePickerTextBox>(dp);
            if (tb == null)
            {
                return;
            }

            var wm = tb.Template.FindName("PART_Watermark", tb) as ContentControl;
            if (wm == null)
            {
                return;
            }

            wm.Content = Strings.SelectADate;
        }

        #region Bug fix: Incorrect jokes column width; Manual width calculation

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            CorrectJokesColumnWidth();
            this.isLoaded = true;
        }

        private void CorrectJokesColumnWidth()
        {
            var jokesColumn = dataGridJokes.Columns[0];

            var margins = 60;
            var expanderWidth = expanderScripts.ActualWidth;
            var themeAndDateColumnsWidth = dataGridJokes.Columns[1].ActualWidth + dataGridJokes.Columns[2].ActualWidth;
            var expectedWidth = this.ActualWidth - expanderWidth - themeAndDateColumnsWidth - margins;

            if (jokesColumn.ActualWidth != expectedWidth)
            {
                jokesColumn.Width = expectedWidth;
            }
        }

        private void DataGridJokes_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.isLoaded)
            {
                CorrectJokesColumnWidth();
            }
        }
        
        #endregion

    }//window
}//namespace