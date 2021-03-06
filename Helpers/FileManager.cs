using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FinalLaboratorio4.Helpers
{
    public static class FileManager
    {
        public async static Task<string> Create(IFormFile file, string destinationFolderPath)
        {
            string outputFileName = Path.GetRandomFileName() + (Path.GetExtension(file.FileName)),
                outputPath = Path.Combine(destinationFolderPath, outputFileName);

            using (FileStream fs = new(outputPath, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }

            return outputFileName;
        }

        public static void Delete(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
