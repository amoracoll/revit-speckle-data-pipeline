#region System Namespaces
using System.Windows;
using System.Windows.Controls;
#endregion

namespace RevitSpeckleExporter.Views
{
    internal class SpeckleExportDialog : Window
    {
        private readonly TextBox _serverUrlBox;
        private readonly TextBox _streamIdBox;
        private readonly PasswordBox _tokenBox;
        private bool _confirmed;

        internal string ServerUrl => _serverUrlBox.Text.TrimEnd('/');
        internal string StreamId => _streamIdBox.Text.Trim();
        internal string Token => _tokenBox.Password.Trim();

        internal SpeckleExportDialog()
        {
            Title = "Export to Speckle";
            Width = 420;
            Height = 240;
            ResizeMode = ResizeMode.NoResize;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Grid grid = new Grid { Margin = new Thickness(16) };
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(110) });
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            _serverUrlBox = AddRow(grid, 0, "Server URL", "https://speckle.xyz");
            _streamIdBox = AddRow(grid, 1, "Stream ID", string.Empty);
            _tokenBox = AddPasswordRow(grid, 2, "API Token");

            StackPanel buttons = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 8, 0, 0)
            };

            Button btnExport = new Button { Content = "Export", Width = 80, Margin = new Thickness(0, 0, 8, 0), IsDefault = true };
            Button btnCancel = new Button { Content = "Cancel", Width = 80, IsCancel = true };

            btnExport.Click += (s, e) => { _confirmed = true; Close(); };
            btnCancel.Click += (s, e) => Close();

            buttons.Children.Add(btnExport);
            buttons.Children.Add(btnCancel);

            Grid.SetRow(buttons, 3);
            Grid.SetColumnSpan(buttons, 2);
            grid.Children.Add(buttons);

            Content = grid;
        }

        internal bool ShowDialog(System.Windows.Interop.WindowInteropHelper owner)
        {
            new System.Windows.Interop.WindowInteropHelper(this).Owner = owner.Handle;
            ShowDialog();

            return _confirmed;
        }

        private TextBox AddRow(Grid grid, int row, string label, string placeholder)
        {
            TextBlock lbl = new TextBlock
            {
                Text = label,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 8, 8)
            };

            Grid.SetRow(lbl, row);
            Grid.SetColumn(lbl, 0);
            grid.Children.Add(lbl);

            TextBox box = new TextBox
            {
                Text = placeholder,
                Margin = new Thickness(0, 0, 0, 8),
                VerticalContentAlignment = VerticalAlignment.Center
            };

            Grid.SetRow(box, row);
            Grid.SetColumn(box, 1);
            grid.Children.Add(box);

            return box;
        }

        private PasswordBox AddPasswordRow(Grid grid, int row, string label)
        {
            TextBlock lbl = new TextBlock
            {
                Text = label,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 0, 8, 8)
            };

            Grid.SetRow(lbl, row);
            Grid.SetColumn(lbl, 0);
            grid.Children.Add(lbl);

            PasswordBox box = new PasswordBox { Margin = new Thickness(0, 0, 0, 8) };
            Grid.SetRow(box, row);
            Grid.SetColumn(box, 1);
            grid.Children.Add(box);

            return box;
        }
    }
}
