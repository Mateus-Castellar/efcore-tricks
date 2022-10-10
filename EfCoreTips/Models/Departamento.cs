namespace EfCoreTips.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = default!;

        //EfCore Relational
        public List<Colaborador>? Colaboradores { get; set; }
    }
}