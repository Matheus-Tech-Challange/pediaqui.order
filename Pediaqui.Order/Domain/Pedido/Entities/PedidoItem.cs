using Domain.Common.Entities;
using Domain.Pedido.Factories;

namespace Domain.Entities
{
    public class PedidoItem : Entity
    {
        public PedidoItem(int produtoId, int quantidade, decimal preco, string observacao)
        {
            ProdutoId = produtoId;
            Quantidade = quantidade;
            Preco = preco;
            Observacao = observacao;

            Validar();
        }

        public int PedidoId { get; private set; }
        public int ProdutoId { get; private set; }
        public int Quantidade { get; private set; }
        public decimal Preco { get; private set; }
        public string? Observacao { get; private set; }

        public void AdicionarQuantidade(int qtd)
        {
            Quantidade += qtd;
            Validar();
        }

        public void RemoverQuantidade(int qtd)
        {
            Quantidade -= qtd;
            Validar();
        }

        public void EditaObservacao(string observacao)
        {
            Observacao = observacao;
        }

        public void Validar()
        {
            Validar<PedidoItem>(this, PedidoItemValidatorFactory.Create());
        }
    }
}
