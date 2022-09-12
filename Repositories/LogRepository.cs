using giovani_d3_avaliacao.Interfaces;
using giovani_d3_avaliacao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace giovani_d3_avaliacao.Repositories
{
    internal class LogRepository : ILog
    {
        private readonly string path;

        public LogRepository(string path)
        {
            CreateFolderFile(path);
            this.path = path;
        }

        private static string PrepareLine(User user)
        {
            return $"O usuário {user.Name} - {user.IdUser} acessou o banco de dados {DateTimeOffset.Now}.";
        }

        public static void CreateFolderFile(string path)
        {
            string folder = path.Split("/")[0];

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
        }

        public void RegisterAccess(User user)
        {
            string line = PrepareLine(user);
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(line);
            }

        }
    }
}