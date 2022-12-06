using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace ApoliSys.Models
{
    public partial class Veiculo
    {
        public Veiculo()
        {
            Cotacaos = new HashSet<Cotacao>();
        }

        ApoliSysContext _context = new ApoliSysContext();

        public int Id { get; set; }
        public int IdSegurado { get; set; }
        public string CodigoFipe { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public int Ano { get; set; }
        public marca Marca { get; set; }
        public categoria_veiculo Categoria { get; set; }
        public string Placa { get; set; } = null!;
        public string Renavam { get; set; } = null!;
        public bool ZeroKm { get; set; }
        public int Km { get; set; }
        public combustivel Combustivel { get; set; }
        public string Utilizacao { get; set; } = null!;

        public virtual Segurado IdSeguradoNavigation { get; set; } = null!;
        public virtual ICollection<Cotacao> Cotacaos { get; set; }

        public bool Cadastrar() 
        {
            try
            {
                CodigoFipe = CodigoFipe.Replace("-", "");

                TextInfo cultInfo = new CultureInfo("pt-BR", false).TextInfo;

                Modelo = cultInfo.ToTitleCase(Modelo);

                Placa = Placa.Replace("-", "");

                _context.Add(this);

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

        public bool Modificar(Veiculo NovasInfoVeiculo)
        {
            try
            {
                CodigoFipe = NovasInfoVeiculo.CodigoFipe.Replace("-", "");
                TextInfo cultInfo = new CultureInfo("pt-BR", false).TextInfo;
                Modelo = cultInfo.ToTitleCase(NovasInfoVeiculo.Modelo);
                Ano = NovasInfoVeiculo.Ano;
                Marca = NovasInfoVeiculo.Marca;
                Categoria = NovasInfoVeiculo.Categoria;
                Placa = NovasInfoVeiculo.Placa.Replace("-", "");
                Renavam = NovasInfoVeiculo.Renavam;
                Km = NovasInfoVeiculo.Km;
                ZeroKm = NovasInfoVeiculo.ZeroKm;
                Combustivel = NovasInfoVeiculo.Combustivel;
                Utilizacao = NovasInfoVeiculo.Utilizacao;

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

        public bool Remover() {
            try
            {
                if (this.Id != 0)
                {
                    _context.Veiculos.Remove(this);
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
