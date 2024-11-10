using ProjetoDePost.Models;

namespace ProjetoDePost.Data.DTOs
{
    public class CampanhaReadDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int EmpresaId { get; set; }
        public string TemaPrincipal { get; set; }
        public List<ParticipanteEmpresaReadDto> Participantes { get; set; }
    }
}
