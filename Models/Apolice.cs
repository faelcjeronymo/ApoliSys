using System;
using System.Collections.Generic;

namespace ApoliSys.Models
{
    public partial class Apolice
    {
        public int Id { get; set; }
        public int IdCotacao { get; set; }
        public string? Produto { get; set; }
        public string ProcessoSusep { get; set; } = null!;
        public string CodigoRamo { get; set; } = null!;
        public string NumeroApolice { get; set; } = null!;
        public DateOnly DataEmissao { get; set; }
        public DateOnly DataTermino { get; set; }
        public status_apolice Status { get; set; }

        public virtual Cotacao IdCotacaoNavigation { get; set; } = null!;
    }
}
