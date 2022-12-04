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

        ApoliSysContext _context = new ApoliSysContext();

        public int Id { get; set; }
        public int IdPessoa { get; set; }
        public faixa_salarial FaixaSalarial { get; set; }
        public string Profissao { get; set; } = null!;

        public virtual Pessoa IdPessoaNavigation { get; set; } = null!;
        public virtual ICollection<Veiculo> Veiculos { get; set; }

        public bool Cadastrar (Pessoa pessoa) {
            try
            {
                //Removendo mascaras de formatacao
                pessoa.CpfCnpj = pessoa.CpfCnpj.Replace("-", "").Replace(".", "");
                pessoa.Celular = pessoa.Celular.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
                
                if (pessoa.Cep != null) {
                    pessoa.Cep = pessoa.Cep.Replace("-", "");
                }

                IdPessoaNavigation = pessoa;

                _context.Add(pessoa);
                _context.Add(this);

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.InnerException);
                return false;
                throw;
            }

            return true;
        }

        public bool Modificar (Segurado NovasInfoSegurado) {
            try
            {
                IdPessoaNavigation.Nome = NovasInfoSegurado.IdPessoaNavigation.Nome;
                IdPessoaNavigation.Genero = NovasInfoSegurado.IdPessoaNavigation.Genero;
                IdPessoaNavigation.DataNascimento = NovasInfoSegurado.IdPessoaNavigation.DataNascimento;
                IdPessoaNavigation.CpfCnpj = NovasInfoSegurado.IdPessoaNavigation.CpfCnpj.Replace("-", "").Replace(".", "");
                IdPessoaNavigation.Celular = NovasInfoSegurado.IdPessoaNavigation.Celular.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");

                Profissao = NovasInfoSegurado.Profissao;
                FaixaSalarial = NovasInfoSegurado.FaixaSalarial;

                if (NovasInfoSegurado.IdPessoaNavigation.Cep != null) {
                    IdPessoaNavigation.Cep = NovasInfoSegurado.IdPessoaNavigation.Cep.Replace("-", "");
                }
                
                IdPessoaNavigation.Cidade = NovasInfoSegurado.IdPessoaNavigation.Cidade;
                IdPessoaNavigation.Estado = NovasInfoSegurado.IdPessoaNavigation.Estado;
                IdPessoaNavigation.Bairro = NovasInfoSegurado.IdPessoaNavigation.Bairro;
                IdPessoaNavigation.Rua = NovasInfoSegurado.IdPessoaNavigation.Rua;
                IdPessoaNavigation.Numero = NovasInfoSegurado.IdPessoaNavigation.Numero;
                IdPessoaNavigation.Complemento = NovasInfoSegurado.IdPessoaNavigation.Complemento;

                _context.Update(IdPessoaNavigation);
                _context.Update(this);

                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.InnerException);
                return false;
                throw;
            }

            return true;
        }
        
    }
}
