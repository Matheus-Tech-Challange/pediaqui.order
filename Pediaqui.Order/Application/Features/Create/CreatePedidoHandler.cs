using Application.Presenters;
using Domain.Entities;
using Domain.Pedido.Ports;
using Pediaqui.Catalog.Ports;
using Pediaqui.Payment.Models;
using Pediaqui.Payment.Ports;

namespace Application.Features.Create
{
    public class CreatePedidoHandler : IRequestHandler<CreatePedidoRequest, CheckoutPedidoResponse>
    {
        private readonly NotificationContext _notificationContext;
        private readonly PedidoPresenter _presenter;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly ICatalogRepository _catalogRepository;
        private readonly IPaymentRepository _pagamentoExternoGateway;

        public CreatePedidoHandler(
            NotificationContext notificationContext,
            PedidoPresenter presenter,
            IPedidoRepository pedidoRepository,
            ICatalogRepository clienteRepository,
            IPaymentRepository pagamentoExternoGateway)
        {
            _notificationContext = notificationContext;
            _pedidoRepository = pedidoRepository;
            _catalogRepository = clienteRepository;
            _presenter = presenter;
            _pagamentoExternoGateway = pagamentoExternoGateway;
        }

        public async Task<CheckoutPedidoResponse> Handle(CreatePedidoRequest request, CancellationToken cancellationToken)
        {
            List<PedidoItem> itens = new List<PedidoItem>();

            foreach (var i in request.Itens)
            {
                var produto = await _catalogRepository.buscarProdutoPorId(i.Id);

                if (produto is null)
                {
                    _notificationContext.AddNotification("NullReference",
                        $"Produto com identificador '{i.Id}' não encontrado");
                    return null!;
                }

                var item = new PedidoItem(produto.Id, i.Quantidade, produto.Preco, i.Observacao!);

                if (item.Invalid)
                {
                    _notificationContext.AddNotifications(item.GetErrors());
                    return null!;
                }

                itens.Add(item);
            }

            Pedido pedido = new();
            pedido.AdicionarItens(itens);

            if (request.CPFCliente != null)
            {
                var cliente = await _catalogRepository.buscarClientePorCpf(request.CPFCliente);

                if (cliente is null)
                {
                    _notificationContext.AddNotification("NullReference",
                        $"Cliente com CPF '{request.CPFCliente}' não encontrado");

                    return null!;
                }

                pedido.ReferenciarCliente(cliente.cpf);
            }


            if (pedido.Invalid)
            {
                _notificationContext.AddNotifications(pedido.GetErrors());
                return null!;
            }

            decimal valorPedido = pedido.CalculaValorTotal();

            int idPedido = await _pedidoRepository.Cria(pedido);

            CreatePaymentRequest paymentRequest = new(idPedido, valorPedido);
            string Idpagamento = await _pagamentoExternoGateway.CreatePayment(paymentRequest);

            return await _presenter.ToCheckoutPedidoResponse(pedido);
        }
    }
}
