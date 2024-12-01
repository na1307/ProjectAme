using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace ProjectAme.Services;

public sealed class FilesService(Window target) {
    public Task<IReadOnlyList<IStorageFile>> OpenFilePickerAsync(FilePickerOpenOptions options) =>
        TopLevel.GetTopLevel(target)!.StorageProvider.OpenFilePickerAsync(options);

    public Task<IStorageFile?> SaveFilePickerAsync(FilePickerSaveOptions options) =>
        TopLevel.GetTopLevel(target)!.StorageProvider.SaveFilePickerAsync(options);
}
