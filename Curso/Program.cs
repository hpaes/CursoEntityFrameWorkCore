using System;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new Data.ApplicationContext();

            // Indicado para utilizar somente em produção
            db.Database.Migrate();
        }
    }
}
