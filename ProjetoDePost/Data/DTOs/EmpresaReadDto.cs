namespace ProjetoDePost.Data.DTOs
{
    public class EmpresaReadDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string SetorAtuacao { get; set; }
        public string LinksRedesSociais { get; set; }
        public string AdministradorId { get; set; }
        public List<ParticipanteEmpresaReadDto> Participantes { get; set; }
    }
}
