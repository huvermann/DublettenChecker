namespace DublettenChecker
{
    public interface IFileCandidate
    {
        string FileName { get; }
        string FilePath { get; }
        long FileSize { get; }
        string FileHash { get; }
    }
}