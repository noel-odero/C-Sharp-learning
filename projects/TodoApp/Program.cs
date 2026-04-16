class Program
{
    static void Main(string[] args)
    {
        
        string path = @"C:\\Users\\Hp\\";
        ShowLargeFilesWithoutLinq(path);
    }

    private static void ShowLargeFilesWithoutLinq(string path)
    {
        DirectoryInfo directory = new DirectoryInfo(path);
        FileInfo[] files = directory.GetFiles();

        foreach(FileInfo file in files)
        {
            Console.WriteLine($"{file.Name}: {file.Length}");
        }

    }
}


public class FileInfoComparer : IComparer<FileInfo>
{
    
}
 