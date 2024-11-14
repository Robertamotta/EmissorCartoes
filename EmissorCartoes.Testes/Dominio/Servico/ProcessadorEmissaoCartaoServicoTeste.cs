using EmissorCartoes.Dominio.DTOs;
using EmissorCartoes.Dominio.Interfaces;
using EmissorCartoes.Dominio.Servico;
using Moq;

namespace EmissorCartoes.Testes.Dominio.Servico
{
    public class ProcessadorEmissaoCartaoServicoTeste
    {
        private readonly Mock<IMensageria> mensageriaMock = new();

        private readonly ProcessadorEmissaoCartaoServico processadorEmissaoCartaoServico ;

        public ProcessadorEmissaoCartaoServicoTeste()
        {
            processadorEmissaoCartaoServico = new ProcessadorEmissaoCartaoServico(mensageriaMock.Object);
        }

        [Theory]
        [InlineData(1300, 1)]
        [InlineData(3000, 2)]
        [InlineData(6000, 3)]
        public async Task Processar_DeveEnviarEmissaoCartoesComQuantidadeCorreta(decimal renda, int qtdEsperada)
        {
            // Arrange
            var propostaCredito = new PropostaCredito
            {
                Id = 1,
                Renda = renda
            };

            mensageriaMock.Setup(m => m.EnviarEmissaoCartoes(It.IsAny<EmissaoCartaoCreditoDto>())).Returns(Task.CompletedTask);

            // Act
            await processadorEmissaoCartaoServico.Processar(propostaCredito);

            // Assert
            mensageriaMock.Verify(m => m.EnviarEmissaoCartoes(It.Is<EmissaoCartaoCreditoDto>(x => x.Id == propostaCredito.Id && x.QtdCartoesEmitidos == qtdEsperada)), Times.Once);
        }
    }
}
