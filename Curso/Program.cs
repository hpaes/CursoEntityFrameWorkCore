using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new Data.ApplicationContext();

            // Indicado para utilizar somente em produção
            // db.Database.Migrate();

            var migrationExists = db.Database.GetPendingMigrations().Any();
            if (migrationExists)
            {
                Console.WriteLine("Pending migration exists");
            }
        }
    }
}
