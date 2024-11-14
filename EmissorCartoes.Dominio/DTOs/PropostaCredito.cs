using System.Diagnostics.CodeAnalysis;

namespace EmissorCartoes.Dominio.DTOs
{
    [ExcludeFromCodeCoverage]
    public class PropostaCredito
    {
        public int Id { get; set; }
        public int ScoreCredito { get; set; }
        public bool AprovacaoCredito { get; set; }
        public decimal Renda { get; set; }
    }
}
