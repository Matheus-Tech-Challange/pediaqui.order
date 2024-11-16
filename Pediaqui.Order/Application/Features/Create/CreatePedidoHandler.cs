using Application.Presenters;
using Domain.Entities;
using Domain.Pedido.Ports;
using Pediaqui.Cliente.Ports;
using Pediaqui.Payment.Models;
using Pediaqui.Payment.Ports;
using Pediaqui.Produto.Ports;

namespace Application.Features.Create
{
    public class CreatePedidoHandler : IRequestHandler<CreatePedidoRequest, CheckoutPedidoResponse>
    {
        private readonly NotificationContext _notificationContext;
        private readonly PedidoPresenter _presenter;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IPaymentRepository _pagamentoExternoGateway;

        public CreatePedidoHandler(
            NotificationContext notificationContext,
            PedidoPresenter presenter,
            IProdutoRepository produtoRepository,
            IPedidoRepository pedidoRepository,
            IClienteRepository clienteRepository,
            IPaymentRepository pagamentoExternoGateway)
        {
            _notificationContext = notificationContext;
            _produtoRepository = produtoRepository;
            _pedidoRepository = pedidoRepository;
            _clienteRepository = clienteRepository;
            _presenter = presenter;
            _pagamentoExternoGateway = pagamentoExternoGateway;
        }

        public async Task<CheckoutPedidoResponse> Handle(CreatePedidoRequest request, CancellationToken cancellationToken)
        {
            List<PedidoItem> itens = new List<PedidoItem>();

            foreach (var i in request.Itens)
            {
                var produto = await _produtoRepository.obterPorId(i.Id);

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
                var cliente = await _clienteRepository.buscarPorCpf(request.CPFCliente);

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
