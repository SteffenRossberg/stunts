using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Stunts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            for (var row=0; row < Settings.RowCount; row++)
            {
                Workspace.RowDefinitions.Add(new RowDefinition{Height = GridLength.Auto});
                for (var column = 0; column < Settings.ColumnCount; column++)
                {
                    if (row == 0)
                    {
                        Workspace.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                    }

                    var button = new Button();
                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, column);

                    button.SetBinding(TileButtonExtensions.TerrainProperty, new MultiBinding
                    {
                        Converter = new BackgroundConverter<TerrainItem>(),
                        Bindings =
                        {
                            new Binding(nameof(MainViewModel.Workspace)),
                            new Binding
                            {
                                RelativeSource = RelativeSource.Self, 
                                Path = new PropertyPath(Grid.RowProperty),
                            },
                            new Binding
                            {
                                RelativeSource = RelativeSource.Self, 
                                Path = new PropertyPath(Grid.ColumnProperty),
                            },
                            new Binding($"{nameof(MainViewModel.Workspace)}[{row}]"),
                            new Binding($"{nameof(MainViewModel.Workspace)}[{row}][{column}]"),
                        }
                    });

                    button.SetBinding(TileButtonExtensions.ItemProperty, new MultiBinding
                    {
                        Converter = new BackgroundConverter<StreetItem>(),
                        Bindings =
                        {
                            new Binding(nameof(MainViewModel.Track)),
                            new Binding
                            {
                                RelativeSource = RelativeSource.Self, 
                                Path = new PropertyPath(Grid.RowProperty),
                            },
                            new Binding
                            {
                                RelativeSource = RelativeSource.Self, 
                                Path = new PropertyPath(Grid.ColumnProperty),
                            },
                            new Binding($"{nameof(MainViewModel.Track)}[{row}]"),
                            new Binding($"{nameof(MainViewModel.Track)}[{row}][{column}]"),
                        }
                    });

                    button.SetBinding(ButtonBase.CommandProperty, new Binding
                    {
                        Path = new PropertyPath(nameof(MainViewModel.ApplyItemCommand))
                    });

                    button.CommandParameter = new []{row, column};

                    Workspace.Children.Add(button);
                }
            }
        }
    }
}
