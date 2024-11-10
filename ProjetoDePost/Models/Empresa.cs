using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoDePost.Models
{
    /// <summary>
    /// Representa uma empresa no sistema.
    /// Cada empresa tem um administrador e pode ter vários participantes.
    /// </summary>
    public class Empresa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string SetorAtuacao { get; set; }
        public string LinksRedesSociais { get; set; }


        /// <summary>
        /// Identificador do administrador da empresa.
        /// Refere-se ao usuário que possui permissões de administrar à empresa.
        /// </summary>
        [ForeignKey("Administrador")]
        public string AdministradorId {  get; set; }
        public Usuario Administrador { get; set; } // Referência para o Usuário que é o administrador dessa empresa!


        /// <summary>
        /// Lista de usuários que participam da Empresa.
        /// </summary>
        public ICollection<ParticipanteEmpresa> Participantes { get; set; } = new List<ParticipanteEmpresa>();
        
        /// <summary>
        /// Lista de Projetos da Empresa.
        /// </summary>
        public ICollection<Campanha> Campanhas { get; set; } = new List<Campanha>();
    }
}
