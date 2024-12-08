using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using ProjectAme.Services;
using SharpCompress.Archives;
using System.Diagnostics;
using File = ProjectAme.Models.File;

namespace ProjectAme.ViewModels;

public sealed partial class MainWindowViewModel : ViewModelBase {
    private static readonly string cache = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/.cache/";

    [ObservableProperty]
    private string? filePath;

    [ObservableProperty]
    private IReadOnlyList<File>? files;

    [RelayCommand]
    private async Task Open() {
        var seleted = await Ioc.Default.GetRequiredService<FilesService>().OpenFilePickerAsync(new() {
            AllowMultiple = false,
            FileTypeFilter = [
                new("ZIP Files") {
                    Patterns = ["*.zip"],
                    MimeTypes = ["application/zip"]
                }
            ],
            Title = "Open File"
        });

        if (seleted.Count >= 1) {
            FilePath = seleted[0].Path.LocalPath;
            Files = [.. ArchiveFactory.Open(FilePath).Entries.Select(e => new File(e.Key!, e))];
        }
    }

    [RelayCommand]
    private void Extract() => throw new NotImplementedException();

    [RelayCommand]
    private void NewArchive() => throw new NotImplementedException();

    [RelayCommand]
    private void AddFiles() => throw new NotImplementedException();

    [RelayCommand]
    private void DeleteFiles() => throw new NotImplementedException();

    [RelayCommand]
    private void OpenFile(int index) {
        var file = Files![index];

        file.Entry.WriteToDirectory(cache, new() {
            ExtractFullPath = false,
            Overwrite = true
        });

        Process.Start(new ProcessStartInfo(cache + Path.GetFileName(file.Entry.Key)) {
            UseShellExecute = true
        });
    }
}
