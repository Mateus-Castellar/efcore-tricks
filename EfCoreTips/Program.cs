using EfCoreTips.Data;
using Microsoft.EntityFrameworkCore;

namespace EfCoreTips
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("======= Dicas e Truques EfCore");
            ObterSQlGerado();
        }

        static void ObterSQlGerado()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureCreated();

            var query = db.Departamentos.Where(lbda => lbda.Id > 2);

            var sql = query.ToQueryString();
            Console.WriteLine(sql);
        }
    }
}