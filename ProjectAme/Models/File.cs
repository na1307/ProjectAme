using SharpCompress.Archives;

namespace ProjectAme.Models;

public sealed record class File(string FullPath, IArchiveEntry Entry);
