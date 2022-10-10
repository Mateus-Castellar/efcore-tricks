namespace EfCoreTips.Models
{
    public class Colaborador
    {
        public int Id { get; set; }
        public string Nome { get; set; } = default!;
        public int DepartamentoId { get; set; }

        //EfCore Relational
        public Departamento? Departamento { get; set; }
    }
}
