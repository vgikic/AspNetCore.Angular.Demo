using System;
using System.Text;

namespace Angular.Core.Services
{
    public class FileNameResolver
    {
        public static string RemoveGuidFromFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return fileName;

            var sb = new StringBuilder();
            var parts = fileName.Split('.');
            var suffix = parts[parts.Length - 1];
            var index = fileName.LastIndexOf('_');
            bool append = false;
            for (int i = index - 1; i >= 0; --i)
            {
                if (append && fileName[i] != ' ') sb.Insert(0, fileName[i]);
                if (fileName[i] == '_') append = true;
            }
            return $"{sb.ToString()}.{suffix}";
        }
        public static string AppendGuidToFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return fileName;

            var parts = fileName.Split('.');
            var sb = new StringBuilder();
            for (int i = 0; i < parts.Length - 1; i++)
                sb.Append(parts[i]);

            return $"{sb.ToString()}_{Guid.NewGuid()}_.{parts[parts.Length - 1]}";
        }
    }
}
