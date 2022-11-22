using System.ComponentModel.DataAnnotations;

namespace ApoliSys.Models
{
    public enum genero {
        [Display(Name = "Masculino")]
        masculino,
        [Display(Name = "Feminino")]
        feminino,
        [Display(Name = "Outro")]
        outro
    }

    public enum faixa_salarial {
        [Display(Name = "R$ 1000,00 - R$ 2000,00")]
        mil_dois_mil,
        [Display(Name = "R$ 2000,00 - R$ 3000,00")]
        dois_mil_tres_mil,
        [Display(Name = "R$ 3000,00 - R$ 4000,00")]
        tres_mil_quatro_mill,
        [Display(Name = "R$ 4000,00 - R$ 5000,00")]
        quatro_mill_cinco_mill,
        [Display(Name = "R$ 5000,00+")]
        cinco_mil_mais
    }

    public enum marca {
        [Display(Name = "Caoa Chery")]
        caoa_chery,
        [Display(Name = "Chevrolet")]
        chevrolet,
        [Display(Name = "Fiat")]
        fiat,
        [Display(Name = "Honda")]
        honda,
        [Display(Name = "Hyundai")]
        hyundai,
        [Display(Name = "Jeep")]
        jeep,
        [Display(Name = "Nissan")]
        nissan,
        [Display(Name = "Renault")]
        renault,
        [Display(Name = "Citröen")]
        citroen,
        [Display(Name = "Peugeot")]
        peugeot,
        [Display(Name = "Ford")]
        ford,
        [Display(Name = "Mitsubishi")]
        mitsubishi,
        [Display(Name = "Toyota")]
        toyota,
        [Display(Name = "Volkswagen")]
        volkswagen,
        [Display(Name = "BMW")]
        bmw,
        [Display(Name = "Audi")]
        audi
    }

    public enum categoria_veiculo {
        [Display(Name = "Hatch")]
        hatch,
        [Display(Name = "Sedan")]
        sedan,
        [Display(Name = "SUV")]
        suv,
        [Display(Name = "Picape")]
        picape,
        [Display(Name = "Utilitário")]
        utilitario
    }

    public enum combustivel {
        [Display(Name = "Etanol")]
        etanol,
        [Display(Name = "Flex")]
        flex,
        [Display(Name = "Gasolina Comum")]
        gasolina_comum,
        [Display(Name = "Gasolina Aditivada")]
        gasolina_aditivada,
        [Display(Name = "Gasolina Premium")]
        gasolina_premium,
        [Display(Name = "Diesel")]
        diesel,
        [Display(Name = "Gás Veicular")]
        gas_veicular
    }

    public enum plano_seguro {
        [Display(Name = "Cobertura Contra Roubo e Furto")]
        cobertura_roubo_furto,
        [Display(Name = "Cobertura Básica")]
        cobertura_basica,
        [Display(Name = "Cobertura Compreensiva")]
        cobertura_compreensiva,
        [Display(Name = "Cobertura de Acidentes à Passageiros")]
        cobertura_acidentes_passageiros,
        [Display(Name = "Cobertura de Acidentes à Terceiros")]
        cobertura_terceiros
    }

    public enum forma_pagamento {
        [Display(Name = "A Vista")]
        a_vista,
        [Display(Name = "2X")]
        duas_parcelas,
        [Display(Name = "3X")]
        tres_parcelas,
        [Display(Name = "4X")]
        quatro_parcelas,
        [Display(Name = "5X")]
        cinco_parcelas,
        [Display(Name = "6X")]
        seis_parcelas,
        [Display(Name = "7X")]
        sete_parcelas,
        [Display(Name = "8X")]
        oito_parcelas,
        [Display(Name = "9X")]
        nove_parcelas,
        [Display(Name = "10X")]
        dez_parcelas,
        [Display(Name = "11X")]
        onze_parcelas,
        [Display(Name = "12X")]
        doze_parcelas
    }

    public enum status_cotacao {
        [Display(Name = "Em Análise")]
        em_analise,
        [Display(Name = "Aprovada")]
        aprovada,
        [Display(Name = "Negada")]
        negada
    }

    public enum status_apolice {
        [Display(Name = "Cancelada")]
        cancelada,
        [Display(Name = "Emitida")]
        emitida
    }
}