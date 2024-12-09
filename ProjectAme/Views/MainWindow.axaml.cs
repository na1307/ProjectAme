using Avalonia.Controls;
using Avalonia.Input;
using ProjectAme.ViewModels;

namespace ProjectAme.Views;

public sealed partial class MainWindow : Window {
    public MainWindow() => InitializeComponent();

    private void FilesTable_OnDoubleTapped(object? sender, TappedEventArgs e) {
        if (e.GetPosition(FilesTable).Y > 30) {
            ((MainWindowViewModel)DataContext!).OpenFileCommand.Execute(FilesTable.SelectedIndex);
        }
    }
}
