using System;
using System.Collections.Generic;

namespace ApoliSys.Models
{
    public partial class Cotacao
    {
        public Cotacao()
        {
            Apolices = new HashSet<Apolice>();
        }

        public int Id { get; set; }
        public int IdVeiculo { get; set; }
        public plano_seguro PlanoSeguro { get; set; }
        public decimal ValorPremioSeguro { get; set; }
        public decimal ValorTotalPremioSeguro { get; set; }
        public forma_pagamento FormaPagamento { get; set; }
        public status_cotacao Status { get; set; }

        public virtual Veiculo IdVeiculoNavigation { get; set; } = null!;
        public virtual ICollection<Apolice> Apolices { get; set; }
    }
}
