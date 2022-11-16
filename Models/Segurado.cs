using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ApoliSys.Models
{
    public class Resposta {

    }
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

        public bool Cadastrar (Pessoa pessoa) {
            try
            {
                DbContext _context = new ApoliSysContext();

                //Removendo mascaras de formatacao
                pessoa.CpfCnpj = pessoa.CpfCnpj.Replace("-", "").Replace(".", "");
                pessoa.Celular = pessoa.Celular.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
                pessoa.Cep = pessoa.Cep.Replace("-", "");

                IdPessoaNavigation = pessoa;

                _context.Add(pessoa);
                _context.Add(this);

                _context.SaveChanges();

            }
            catch (DbUpdateException e)
            {
                Debug.WriteLine(e.InnerException);
                return false;
                throw;
            }

            return true;
        }
        
    }
}
