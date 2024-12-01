using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using ProjectAme.Services;
using SharpCompress.Readers;
using File = ProjectAme.Models.File;

namespace ProjectAme.ViewModels;

public sealed partial class MainWindowViewModel : ViewModelBase {
    [ObservableProperty]
    private string? filePath;

    [ObservableProperty]
    private IReadOnlyCollection<File>? files;

    [RelayCommand]
    private async Task Open() {
        var seleted = await Ioc.Default.GetRequiredService<FilesService>().OpenFilePickerAsync(new() {
            AllowMultiple = false,
            FileTypeFilter = [
                new("ZIP Files") {
                    Patterns = ["*.zip"]
                }
            ],
            Title = "Open File"
        });

        FilePath = seleted[0].Path.LocalPath;

        await using var stream = await seleted[0].OpenReadAsync();
        using var reader = ReaderFactory.Open(stream);

        List<File> tmpfiles = [];

        while (reader.MoveToNextEntry()) {
            tmpfiles.Add(new(reader.Entry.Key!));
        }

        Files = [.. tmpfiles];
    }

    [RelayCommand]
    private void Extract() => throw new NotImplementedException();

    [RelayCommand]
    private void NewArchive() => throw new NotImplementedException();

    [RelayCommand]
    private void AddFiles() => throw new NotImplementedException();

    [RelayCommand]
    private void DeleteFiles() => throw new NotImplementedException();
}
