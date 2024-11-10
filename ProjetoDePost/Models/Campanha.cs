using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoDePost.Models
{
    /// <summary>
    /// Representa um projeto associado a uma empresa, onde os participantes podem gerenciar posts.
    /// </summary>
    public class Campanha
    {
        public int Id { get; set; }
        public string Nome { get; set; }


        /// <summary>
        /// Identificador da empresa a qual o projeto pertence.
        /// </summary>
        [ForeignKey("Empresa")]
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        public int FrequenciaMaxima { get; set; } = 10;
        
        /// <summary>
        /// Coleção de posts gerenciados pelo projeto.
        /// </summary>
        public ICollection<Postagem> Postagens { get; set; } = new List<Postagem>();
        public ICollection<ParticipanteEmpresa> Participantes { get; set; } = new List<ParticipanteEmpresa>();

        public string TemaPrincipal { get;set; } // Tema da campanha
        public int Frequencia { get; set; } // Quantidade de post

        public string Descricao { get; set; } // Descrição do prompt para o OpenAPI

        public bool Aprovada { get; set; }
        public bool Ativa { get; set; }
    }
}
