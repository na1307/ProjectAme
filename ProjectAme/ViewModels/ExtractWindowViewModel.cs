using SharpCompress.Archives;
using System.Collections.ObjectModel;

namespace ProjectAme.ViewModels;

public sealed partial class ExtractWindowViewModel : ViewModelBase {
    [Obsolete("This overload must only be used for Designer.", true)]
    public ExtractWindowViewModel() => Entries = [];

    public ExtractWindowViewModel(IEnumerable<IArchiveEntry> entries) => Entries = new(entries);

    public ObservableCollection<IArchiveEntry> Entries { get; }
}
