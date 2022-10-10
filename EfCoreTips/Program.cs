using EfCoreTips.Data;
using EfCoreTips.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreTips
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("======= Dicas e Truques EfCore =======");
            //ObterSQlGerado();
            //DebugView();
        }

        static void ObterSQlGerado()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureCreated();

            var query = db.Departamentos.Where(lbda => lbda.Id > 2);

            var sql = query.ToQueryString();
            Console.WriteLine(sql);
        }

        static void DebugView()
        {
            using var db = new ApplicationContext();

            db.Departamentos.Add(new Departamento { Descricao = "teste debug view" });

            var query = db.Departamentos.Where(lbda => lbda.Id > 2);
            //dentro do query teremos a propriedade "DebugView" onde estará contida nossa 
            //query e podemos obte-lá em modo de debug!
        }
    }
}