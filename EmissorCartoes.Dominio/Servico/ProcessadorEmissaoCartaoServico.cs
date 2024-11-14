using EmissorCartoes.Dominio.DTOs;
using EmissorCartoes.Dominio.Interfaces;

namespace EmissorCartoes.Dominio.Servico;

public class ProcessadorEmissaoCartaoServico(IMensageria mensageria) : IProcessadorEmissaoCartaoServico
{
    public async Task Processar(PropostaCredito propostaCredito)
    {
        var quantidadeCartoes = (propostaCredito.Renda) switch
        {
            ( < 1400) => 1,
            ( >= 1400 and <= 5000) => 2,
            ( > 5000) => 3,
        };

        var emissaoCartoes = ConverterParaClienteEmissaoCartoesCredito(propostaCredito, quantidadeCartoes);

        await mensageria.EnviarEmissaoCartoes(emissaoCartoes);
    }

    public static EmissaoCartaoCreditoDto ConverterParaClienteEmissaoCartoesCredito(PropostaCredito propostaCredito, int quantidadeCartoes)
    {
        return new EmissaoCartaoCreditoDto
        {
            Id = propostaCredito.Id,
            QtdCartoesEmitidos = quantidadeCartoes
        };
    }

}
