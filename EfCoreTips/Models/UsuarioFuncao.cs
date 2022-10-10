using Microsoft.EntityFrameworkCore;

namespace EfCoreTips.Models
{
    [Keyless]
    public class UsuarioFuncao
    {
        public Guid UsuarioId { get; set; }
        public Guid FuncaoId { get; set; }
    }
}
