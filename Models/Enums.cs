namespace ApoliSys.Models
{
    public enum genero {
        masculino,
        feminino,
        outro
    }

    public enum faixa_salarial {
        mil_dois_mil,
        dois_mil_tres_mil,
        tres_mil_quatro_mill,
        quatro_mill_cinco_mill,
        cinco_mil_mais
    }

    public enum marca {
        caoa_chery,
        chevrolet,
        fiat,
        honda,
        hyundai,
        jeep,
        nissan,
        renault,
        citroen,
        peugeot,
        ford,
        mitsubishi,
        toyota,
        volkswagen,
        bmw,
        audi
    }

    public enum categoria_veiculo {
        hatch,
        sedan,
        suv,
        picape,
        utilitario
    }

    public enum combustivel {
        etanol,
        flex,
        gasolina_comum,
        gasolina_aditivada,
        gasolina_premium,
        diesel,
        gas_veicular
    }

    public enum plano_seguro {
        cobertura_roubo_furto,
        cobertura_basica,
        cobertura_compreensiva,
        cobertura_acidentes_passageiros,
        cobertura_terceiros
    }

    public enum forma_pagamento {
        a_vista,
        duas_parcelas,
        tres_parcelas,
        quatro_parcelas,
        cinco_parcelas,
        seis_parcelas,
        sete_parcelas,
        oito_parcelas,
        nove_parcelas,
        dez_parcelas,
        onze_parcelas,
        doze_parcelas
    }

    public enum status_cotacao {
        em_analise,
        aprovada,
        negada
    }

    public enum status_apolice {
        cancelada,
        emitida
    }
}