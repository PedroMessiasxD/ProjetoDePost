namespace ProjetoDePost.Data.DTOs
{
    public class ParticipanteEmpresaReadDto
    {
        public int Id { get; set; }

       
        public string UsuarioId { get; set; }

       
        public string UsuarioNome { get; set; }

        public int EmpresaId { get; set; }

        
        public string Papel { get; set; }

        public DateTime DataEntrada { get; set; }

        public int? CampanhaId { get; set; }

        public string Email { get; set; }
    }
}
