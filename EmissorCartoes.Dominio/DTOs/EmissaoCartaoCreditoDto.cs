using System.Diagnostics.CodeAnalysis;

namespace EmissorCartoes.Dominio.DTOs
{
    [ExcludeFromCodeCoverage]
    public class EmissaoCartaoCreditoDto
    {
        public int Id { get; set; }
        public int QtdCartoesEmitidos { get; set; }
    }
}
