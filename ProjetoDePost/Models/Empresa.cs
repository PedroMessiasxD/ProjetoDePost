using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoDePost.Models
{
    
    public class Empresa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string SetorAtuacao { get; set; }
        public string LinksRedesSociais { get; set; }

        [ForeignKey("Administrador")]
        public string AdministradorId {  get; set; }
        public Usuario Administrador { get; set; } 

        public ICollection<ParticipanteEmpresa> Participantes { get; set; } = new List<ParticipanteEmpresa>();
        
        public ICollection<Campanha> Campanhas { get; set; } = new List<Campanha>();
    }
}
