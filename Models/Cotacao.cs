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

        public bool Cadastrar () {
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
    }
}
