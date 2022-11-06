using System;
using System.Collections.Generic;

namespace ApoliSys.Models
{
    public partial class Segurado
    {
        public Segurado()
        {
            Veiculos = new HashSet<Veiculo>();
        }

        public int Id { get; set; }
        public int IdPessoa { get; set; }
        public faixa_salarial FaixaSalarial { get; set; }
        public string Profissao { get; set; } = null!;

        public virtual Pessoa IdPessoaNavigation { get; set; } = null!;
        public virtual ICollection<Veiculo> Veiculos { get; set; }
    }
}
