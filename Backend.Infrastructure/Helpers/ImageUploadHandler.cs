namespace Backend.Infrastructure.Helpers;

public static class ImageUploadHandler
{
    public static async Task<string> SaveFile(IFormFile file)
    {
        if (file.Length < 1)
        {
            throw new Exception("No file submited in the form.");
        }

        string extension = file.FileName.Reverse().Split(".")[0].Reverse();
        string fileName = $"{GenerateName()}.{extension}";

        // This will get the current WORKING directory (i.e. \bin\Debug)
        string workingDirectory = Environment.CurrentDirectory;
        // or: Directory.GetCurrentDirectory() gives the same result

        // This will get the current PROJECT bin directory (ie ../bin/)
        string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;

        string path = $"{projectDirectory}/frontend/public/assets/images/{fileName}";
        
        using (Stream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            await file.CopyToAsync(fileStream);
        }

        return fileName;
    }

    public static async Task DeleteFile(string file)
    {
        if (file.Length < 1)
        {
            return;
        }

        // This will get the current WORKING directory (i.e. \bin\Debug)
        string workingDirectory = Environment.CurrentDirectory;
        // or: Directory.GetCurrentDirectory() gives the same result

        // This will get the current PROJECT bin directory (ie ../bin/)
        string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;

        string path = $"{projectDirectory}/frontend/public/assets/images/{file}";

        using (FileStream stream = new FileStream(path, FileMode.Truncate, FileAccess.Write, FileShare.Delete, 4096, true))
        {
            await stream.FlushAsync();
            File.Delete(path);
        }
    }

    private static string GenerateName()
    {
        string[] data = Guid.NewGuid().ToString().Split("-");

        for (int i = 1; i < 4; i++)
        {
            data[i] += Generate(4);
        }

        string result = string.Join('-', data);

        return result;
    }

    private static string Generate(int lenght)
    {
        StringBuilder builder = new StringBuilder();
        Enumerable.Range(65, 26)
                  .Select(e => ((char)e).ToString())
                  .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                  .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                  .OrderBy(e => Guid.NewGuid())
                  .Take(lenght)
                  .ToList().ForEach(e => builder.Append(e));

        return builder.ToString();
    }
}
