namespace cvti.data.Files
{
    using cvti.data.Core;
    using cvti.data.Output;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public class HlavickyManagerJson : FileManagerJson<Hlavicka>
    {
        public static readonly string FileName = "hlavicky.json";

        internal HlavickyManagerJson(LogManager log, string directory, string dataDirectory)
            : base(log, directory, FileName)
        {
            DataDirectory = dataDirectory;
        }

        public string DataDirectory { get; set; }

        public override bool AddValue(Hlavicka value)
        {
            if (base.AddValue(value))
            {
                var newPath = System.IO.Path.Combine(DataDirectory, value.Name + ".xlsx");
                if (newPath.ToLower() != value.FilePath.ToLower())
                {
                    System.IO.File.Copy(value.FilePath, newPath);
                    value.FilePath = newPath;
                }

                return true;
            }

            return false;
        }

        protected override IEnumerable<Hlavicka> GetDefault()
            => GenerateDefaultHlavicky(GetTempPath());


        private string GetTempPath()
            => System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TempRocenka");

        public static void GenerateDefaultFile(IEnumerable<Hlavicka> hlavicky, string directory)
        {
            var filePath = System.IO.Path.Combine(directory, FileName);
            var data = hlavicky.ToArray();
            System.IO.File.WriteAllText(filePath, JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            }));
        }

        /// <summary>
        /// Na vybranom mieste vygeneruje defaultne hlavickove XLSX subory 
        /// </summary>
        /// <param name="hlavickyPath">Cesta kde sa vygeneruju defaultne XLSX hlavickove subory</param>
        /// <returns>Vygenerovane hlavickove subory ako <see cref="IEnumerable{T}"/> kde T je <see cref="Hlavicka"/></returns>
        public static IEnumerable<Hlavicka> GenerateDefaultHlavicky(string hlavickyPath)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var hlavickoveSubory = executingAssembly
                .GetManifestResourceNames()
                .Where(r => r.StartsWith("cvti.data.Hlavicky") && r.EndsWith(".xlsx"))
                .ToArray();

            var hlavicky = new List<Hlavicka>();
            foreach (var h in hlavickoveSubory)
            {
                var splitted = h.Split('.');
                var fileName = splitted[splitted.Length - 2] + "." + splitted[splitted.Length - 1];
                var path = Path.Combine(hlavickyPath, fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    executingAssembly.GetManifestResourceStream(h).CopyTo(fileStream);
                }
                hlavicky.Add(new Hlavicka(path));
            }
            return hlavicky;
        }
    }
}
