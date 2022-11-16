using System;
using System.Collections.Generic;
using System.Diagnostics;

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
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Celular { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime DataNascimento { get; set; }
        public string Estado { get; set; }
        public string Cnh { get; set; }

        public virtual ICollection<Segurado> Segurados { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }

        public bool validarDataNascimento () {
            //Validando se a data é maior do que o dia atual
            DateTime dataHoje = DateTime.Today;

            if (DateTime.Compare(DataNascimento, dataHoje) > 0) {
                return false;
            }

            return true;
        }

        public bool validarCpfCnpj () {
            //Removendo mascara
            CpfCnpj = CpfCnpj.Replace("-", "").Replace(".", "");
            string CpfCnpjValidado = "";

            //Verificando primeiro digito verificador
            string noveDigitos = CpfCnpj.Substring(0, 9);
            char[] vetorDigitos = noveDigitos.ToCharArray();
            int somaMultiplicacaoDigitos = 0;
            int multiplicadorDigitos = 10;
            int primeiroDigitoVerificador = 0;

            foreach (char digito in vetorDigitos)
            {
                somaMultiplicacaoDigitos = ((int)Char.GetNumericValue(digito) * multiplicadorDigitos) + somaMultiplicacaoDigitos;

                if (multiplicadorDigitos > 2) {
                    multiplicadorDigitos--;
                }
            }

            int restoDivisao = somaMultiplicacaoDigitos % 11;

            if (restoDivisao < 2) {
                primeiroDigitoVerificador = 0;
            } else if (restoDivisao >= 2) {
                primeiroDigitoVerificador = 11 - restoDivisao;
            }

            CpfCnpjValidado = noveDigitos + primeiroDigitoVerificador.ToString();

            //Verificando o segundo digito
            vetorDigitos = CpfCnpjValidado.ToCharArray();
            somaMultiplicacaoDigitos = 0;
            multiplicadorDigitos = 11;
            int segundoDigitoVerificador = 0;

            foreach (var digito in vetorDigitos)
            {
                somaMultiplicacaoDigitos = ((int)Char.GetNumericValue(digito) * multiplicadorDigitos) + somaMultiplicacaoDigitos;
                
                if (multiplicadorDigitos > 2) {
                    multiplicadorDigitos--;
                }
            }

            restoDivisao = somaMultiplicacaoDigitos % 11;

            if (restoDivisao < 2) {
                segundoDigitoVerificador = 0;
            } else if (restoDivisao >= 2) {
                segundoDigitoVerificador = 11 - restoDivisao;
            }

            CpfCnpjValidado = CpfCnpjValidado + segundoDigitoVerificador.ToString();

            //Verificando se o CPF digitado é válido
            if (CpfCnpj != CpfCnpjValidado)
            {
                return false;
            }

            return true;
        }
    }
}
