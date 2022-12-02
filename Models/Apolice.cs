using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ApoliSys.Models
{
    public partial class Apolice
    {
        ApoliSysContext _context = new ApoliSysContext();

        public int Id { get; set; }
        public int IdCotacao { get; set; }
        public string? Produto { get; set; }
        public string ProcessoSusep { get; set; } = null!;
        public string CodigoRamo { get; set; } = null!;
        public string NumeroApolice { get; set; } = null!;
        public DateTime DataEmissao { get; set; }
        public DateTime DataTermino { get; set; }
        public status_apolice Status { get; set; }

        public virtual Cotacao IdCotacaoNavigation { get; set; } = null!;

        public string ValidarApolice() {
            string mensagemValidacao = "Ok";

            if (ProcessoSusep.Length != 20) {
                mensagemValidacao = "Por favor, digite um número de processo SUSEP com até 20 caracteres.";
                return mensagemValidacao;
            }

            if (NumeroApolice.Length != 9) {
                mensagemValidacao = "Por favor, digite um número de apólice com até 9 caracteres.";
                return mensagemValidacao;
            }

            return mensagemValidacao;
        }

        public bool Cadastrar() {
            try
            {
                CodigoRamo = "034";

                Status = status_apolice.emitida;

                DataEmissao = DateTime.Now;

                _context.Apolices.Add(this);

                _context.SaveChanges();
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e.InnerException);
                return false;
                throw;
            }

            return true;
        }
    }
}
