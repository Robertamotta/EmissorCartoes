using EmissorCartoes.Dominio.DTOs;

namespace EmissorCartoes.Dominio.Interfaces;

public interface IProcessadorEmissaoCartaoServico
{
    Task Processar(PropostaCredito propostaCredito);
}
