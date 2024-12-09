using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using ProjectAme.Models;
using ProjectAme.Services;
using ProjectAme.Views;
using SharpCompress.Archives;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ProjectAme.ViewModels;

public sealed partial class MainWindowViewModel : ViewModelBase {
    private static readonly string cache = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/.cache/";

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ExtractCommand), nameof(AddFilesCommand), nameof(DeleteFilesCommand))]
    private Archive? archive;

    private bool ArchiveLoaded => Archive is not null;

    [RelayCommand]
    private async Task Open() {
        var seleted = await Ioc.Default.GetRequiredService<FilesService>().OpenFilePickerAsync(new() {
            AllowMultiple = false,
            FileTypeFilter = [
                new("ZIP File") {
                    Patterns = ["*.zip"],
                    MimeTypes = ["application/zip"]
                }
            ],
            Title = "Open File"
        });

        if (seleted.Count >= 1) {
            Archive?.Dispose();
            Archive = new(seleted[0].Path.LocalPath);
        }
    }

    [RelayCommand(CanExecute = nameof(ArchiveLoaded))]
    private async Task Extract(ReadOnlyCollection<object> parameter) => await new ExtractWindow {
        DataContext = new ExtractWindowViewModel(((IList)parameter[1]).Cast<IArchiveEntry>())
    }.ShowDialog((Window)parameter[0]);

    [RelayCommand]
    private void NewArchive() => throw new NotImplementedException();

    [RelayCommand(CanExecute = nameof(ArchiveLoaded))]
    private void AddFiles() => throw new NotImplementedException();

    [RelayCommand(CanExecute = nameof(ArchiveLoaded))]
    private void DeleteFiles() => throw new NotImplementedException();

    [RelayCommand]
    private void OpenFile(int index) {
        var entry = Archive!.Entries[index];

        if (entry.IsDirectory) {
            return;
        }

        entry.WriteToDirectory(cache, new() {
            ExtractFullPath = false,
            Overwrite = true
        });

        Process.Start(new ProcessStartInfo(cache + Path.GetFileName(entry.Key)) {
            UseShellExecute = true
        });
    }
}
