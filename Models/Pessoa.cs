using System;
using System.Collections.Generic;

namespace ApoliSys.Models
{
    public partial class Pessoa
    {
        public Pessoa()
        {
            Segurados = new HashSet<Segurado>();
            Usuarios = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string CpfCnpj { get; set; } = null!;
        public genero Genero { get; set; }
        public string Cep { get; set; } = null!;
        public string Cidade { get; set; } = null!;
        public string Bairro { get; set; } = null!;
        public string Rua { get; set; } = null!;
        public int Numero { get; set; }
        public string Complemento { get; set; } = null!;
        public string Celular { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateOnly DataNascimento { get; set; }
        public string Estado { get; set; } = null!;
        public string Cnh { get; set; } = null!;

        public virtual ICollection<Segurado> Segurados { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
