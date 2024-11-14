using EmissorCartoes.Dominio.DTOs;

namespace EmissorCartoes.Dominio.Interfaces
{
    public interface IMensageria
    {
        Task EnviarEmissaoCartoes(EmissaoCartaoCreditoDto emissaoCartao);
    }
}
