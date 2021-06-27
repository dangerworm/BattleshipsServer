using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BattleshipsServer.Helpers;
using BattleshipsServer.Interfaces;
using BattleshipsServer.Models;
using Microsoft.Extensions.Options;

namespace BattleshipsServer.Data
{
    public class LocalDataStore : IFileDataStore
    {
        private readonly FileStoreOptions _fileStoreOptions;

        public LocalDataStore(IOptions<FileStoreOptions> fileStoreOptions)
        {
            Verify.NotNull(fileStoreOptions, nameof(fileStoreOptions));

            _fileStoreOptions = fileStoreOptions.Value;
        }

        public Task<string> Load(string fileName)
        {
            return File.ReadAllTextAsync(GetPath(fileName));
        }

        public Task Save(string fileName, string content)
        {
            return File.WriteAllTextAsync(GetPath(fileName), content);
        }

        private string GetPath(string fileName)
        {
            return Path.Combine(_fileStoreOptions.Path, ReplaceSpecialCharacters(fileName));
        }

        private string ReplaceSpecialCharacters(string fileName)
        {
            var replacements = new Dictionary<string, string>
            {
                {":", "-"}
            };

            foreach (var replacement in replacements)
            {
                fileName = fileName.Replace(replacement.Key, replacement.Value);
            }

            return fileName;
        }
    }
}
