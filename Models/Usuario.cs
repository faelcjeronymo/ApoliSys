using System;
using System.Collections.Generic;

namespace ApoliSys.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public int IdPessoa { get; set; }
        public string SenhaUsuario { get; set; } = null!;

        public virtual Pessoa IdPessoaNavigation { get; set; } = null!;
    }
}
