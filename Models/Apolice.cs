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

        public bool ValidarProcessoSusep() 
        {
            if (ProcessoSusep.Length != 20) 
            {
                return false;
            }

            return true;
        }

        public bool ValidarNumeroApolice() 
        {
            if (NumeroApolice.Length != 9) 
            {
                return false;
            }

            return true;
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

        public bool Modificar(Apolice NovasInfoApolice)
        {
            try
            {
                ProcessoSusep = NovasInfoApolice.ProcessoSusep;
                DataTermino = NovasInfoApolice.DataTermino;

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
        
        public bool Cancelar()
        {
            try
            {
                this.Status = status_apolice.cancelada;

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

        public bool Remover()
        {
            try
            {
                _context.Remove(this);

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
