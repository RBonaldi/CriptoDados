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

            return await mercadoBitcoinAdapter.ObterDadosNegociacoesDoDiaAsync(siglaMoeda);
        }
    }
}