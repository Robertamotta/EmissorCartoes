using System.Diagnostics.CodeAnalysis;

namespace EmissorCartoes.Dominio.Entidades
{
    [ExcludeFromCodeCoverage]
    public class EmissaoCartaoCredito
    {
        public int Id { get; set; }
        public int QtdCartoesEmitidos {  get; set; }
    }
}
