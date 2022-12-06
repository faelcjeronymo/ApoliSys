using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ApoliSys.Models
{
    public partial class Cotacao
    {
        public Cotacao()
        {
            Apolices = new HashSet<Apolice>();
        }

        ApoliSysContext _context = new ApoliSysContext();

        public int Id { get; set; }
        public int IdVeiculo { get; set; }
        public plano_seguro PlanoSeguro { get; set; }
        public decimal ValorPremioSeguro { get; set; }
        public decimal ValorTotalPremioSeguro { get; set; }
        public forma_pagamento FormaPagamento { get; set; }
        public status_cotacao Status { get; set; }

        public virtual Veiculo IdVeiculoNavigation { get; set; } = null!;
        public virtual ICollection<Apolice> Apolices { get; set; }

        public bool Cadastrar () 
        {
            try
            {
                _context.Cotacaos.Add(this);

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

        public bool Modificar(Cotacao NovasInfoCotacao)
        {
            try
            {
                PlanoSeguro = NovasInfoCotacao.PlanoSeguro;
                ValorPremioSeguro = NovasInfoCotacao.ValorPremioSeguro;
                ValorTotalPremioSeguro = NovasInfoCotacao.ValorTotalPremioSeguro;
                FormaPagamento = NovasInfoCotacao.FormaPagamento;

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
        
        public bool Aprovar() 
        {
            try
            {
                Status = status_cotacao.aprovada;

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
        
        public bool Negar() 
        {
            try
            {
                Status = status_cotacao.negada;

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
                if (this.Id != 0)
                {
                    _context.Remove(this);

                    _context.SaveChanges();
                }
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
