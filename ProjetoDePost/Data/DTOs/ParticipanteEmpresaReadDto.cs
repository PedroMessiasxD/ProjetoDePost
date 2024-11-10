namespace ProjetoDePost.Data.DTOs
{
    public class ParticipanteEmpresaReadDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Identificador do usuário que participa da empresa.
        /// </summary>
        public string UsuarioId { get; set; }

        /// <summary>
        /// Primeiro nome do usuário que participa da empresa.
        /// </summary>
        public string UsuarioNome { get; set; }

        /// <summary>
        /// Identificador da empresa da qual o usuário participa.
        /// </summary>
        public int EmpresaId { get; set; }

        /// <summary>
        /// Papel do usuário dentro da empresa.
        /// </summary>
        public string Papel { get; set; }

        /// <summary>
        /// Data em que o usuário entrou na empresa.
        /// </summary>
        public DateTime DataEntrada { get; set; }

        public int? CampanhaId { get; set; }
    }
}
