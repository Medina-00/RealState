

using Microsoft.AspNetCore.Http;

namespace RealState.Core.Application.Helpers
{
    public static class FileHelped
    {
        public static string UploadFile(IFormFile file, object id, bool isEditMode = false, string entidad = "", string imagePath = "")
        {
            if (file == null)
            {
                if (isEditMode) 
                {
                    return imagePath;
                }
                return null;
            }

            string basePath = $"/Images/{entidad}/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new FileInfo(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode && !string.IsNullOrEmpty(imagePath))
            {
                // Obtener el nombre del archivo de la ruta proporcionada
                string oldFileName = Path.GetFileName(imagePath);
                // Combinar la ruta de la carpeta con el nombre del archivo
                string completeImageOldPath = Path.Combine(path, oldFileName);

                if (File.Exists(completeImageOldPath))
                {
                    File.Delete(completeImageOldPath);
                }
            }

            return $"{basePath}/{fileName}";
        }

        public static void FileDelete(string entidad, object id)
        {
            string basePath = $"/Images/{entidad}/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new(path);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo folder in directory.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }

        }

    }
}
