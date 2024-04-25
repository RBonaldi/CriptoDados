using Domain.Adapters;
using Domain.Models;
using Domain.Services;

namespace Application
{
    public class CriptoMoedaService : ICriptoMoedaService
    {
        private readonly IMercadoBitcoinAdapter mercadoBitcoinAdapter;
        public CriptoMoedaService(IMercadoBitcoinAdapter mercadoBitcoinAdapter)
        {
            this.mercadoBitcoinAdapter = mercadoBitcoinAdapter ??
                throw new ArgumentNullException(nameof(mercadoBitcoinAdapter));
        }
        public async Task<NegociacoesDoDia> ObterDadosNegociacoesDoDiaAsync(string siglaMoeda)
        {
            if (string.IsNullOrWhiteSpace(siglaMoeda))
            {
                throw new Exception("Sigla escolhida não é valida.");
            }

            var retorno = await mercadoBitcoinAdapter.ObterDadosNegociacoesDoDiaAsync(siglaMoeda);

            if(retorno.MenorPrecoOfertado > retorno.MaiorPrecoOfertado)
            {
                // Correção: Verificar se PrecoDivisao é diferente de zero antes de dividir
                if (retorno.PrecoDivisao != 0)
                {
                    retorno.PrecoFinal = retorno.MaiorPreco / retorno.PrecoDivisao;
                }
                else
                {
                    retorno.PrecoFinal = retorno.MaiorPreco; // Ou aplicar outra lógica conforme regra de negócio
                }
            }

            return retorno;
        }
    }
}