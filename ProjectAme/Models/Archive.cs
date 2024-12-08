using SharpCompress.Archives;
using System.Collections.ObjectModel;

namespace ProjectAme.Models;

public sealed class Archive : IEquatable<Archive>, IDisposable {
    private readonly IArchive realArchive;

    public Archive(string archivePath) {
        realArchive = ArchiveFactory.Open(archivePath);
        ArchivePath = archivePath;
        Entries = new(realArchive.Entries.ToArray());
    }

    public string ArchivePath { get; }

    public ReadOnlyCollection<IArchiveEntry> Entries { get; }

    public static bool operator ==(Archive left, Archive right) => left.Equals(right);

    public static bool operator !=(Archive left, Archive right) => !(left == right);

    public override string ToString() => ArchivePath;

    public override bool Equals(object? obj) => Equals(obj as Archive);

    public bool Equals(Archive? other) => other is not null && ArchivePath == other.ArchivePath;

    public override int GetHashCode() => ArchivePath.GetHashCode();

    public void Dispose() => realArchive.Dispose();
}
