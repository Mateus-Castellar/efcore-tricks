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
            //RedefinirEstadoContexto();
            //FiltroNoIncluide();
            //SemChavePrimaria();
            //ToView();
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

        static void RedefinirEstadoContexto()
        {
            using var db = new ApplicationContext();

            db.Departamentos.Add(new Departamento { Descricao = "teste debug view" });

            db.ChangeTracker.Clear();
        }

        static void FiltroNoIncluide()
        {
            using var db = new ApplicationContext();

            db.Departamentos
                .Include(lbda => lbda.Colaboradores ?? new List<Colaborador>()
                    .Where(c => c.Nome.Contains("teste")))
                .ToQueryString();

            db.ChangeTracker.Clear();
        }

        static void SemChavePrimaria()
        {
            //O EfCore é capaz de gerar e consultar dados em uma tabela sem PK,
            //mas não realizar insercoes, remocoes ou updates!

            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var usuarioFuncoes = db.UsuarioFuncoes.Where(lbda =>
                lbda.UsuarioId == Guid.NewGuid()).ToArray();
        }

        static void ToView()
        {
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            db.Database.ExecuteSqlRaw(
                @"CREATE VIEW vw_departamento_relatorio AS
                SELECT
                    d.Descricao, count(c.Id) as Colaboradores
                FROM Departamentos d 
                LEFT JOIN Colaboradores c ON c.DepartamentoId=d.Id
                GROUP BY d.Descricao");

            var departamentos = Enumerable.Range(1, 10)
                .Select(p => new Departamento
                {
                    Descricao = $"Departamento {p}",
                    Colaboradores = Enumerable.Range(1, p)
                        .Select(c => new Colaborador
                        {
                            Nome = $"Colaborador {p}-{c}"
                        }).ToList()
                });

            var departamento = new Departamento
            {
                Descricao = $"Departamento Sem Colaborador"
            };

            db.Departamentos.Add(departamento);
            db.Departamentos.AddRange(departamentos);
            db.SaveChanges();

            var relatorio = db.DepartamentoRelatorio
                .Where(p => p.Colaboradores < 20)
                .OrderBy(p => p.Departamento)
                .ToList();

            foreach (var dep in relatorio)
                Console.WriteLine($"{dep.Departamento} [ Colaboradores: {dep.Colaboradores}]");
        }
    }
}