namespace ProjetoDePost.Data.DTOs
{
    /// <summary>
    /// Define o papel do usuário na empresa, como "Administrador" ou "Membro".
    /// </summary>
    public class ParticipanteEmpresaCreateDto
    {
        public string UsuarioId { get; set; }
        public int EmpresaId { get; set; }
        public string Papel { get; set; }
        public int CampanhaId { get; set; }
    }
}
