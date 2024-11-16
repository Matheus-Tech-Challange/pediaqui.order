using Application.Presenters;
using Domain.Enuns;
using Domain.Pedido.Ports;
using Pediaqui.Produto.Ports;

namespace Application.Features.ListAll;

public class ListAllPedidoHandler : IRequestHandler<ListAllPedidosRequest, ListPedidosResponse>
{
    private readonly NotificationContext _notificationContext;
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly PedidoPresenter _presenter;

    public ListAllPedidoHandler(
        NotificationContext notificationContext,
        IProdutoRepository produtoRepository,
        IPedidoRepository pedidoRepository,
        PedidoPresenter presenter)
    {
        _notificationContext = notificationContext;
        _pedidoRepository = pedidoRepository;
        _produtoRepository = produtoRepository;
        _presenter = presenter;
    }

    public async Task<ListPedidosResponse> Handle(ListAllPedidosRequest request, CancellationToken cancellationToken)
    {
        var pedidos = await _pedidoRepository.ObterTodos();

        var filter = pedidos
            .Where(p =>
               p.Status == StatusPedido.PRONTO
            || p.Status == StatusPedido.EM_PREPARACAO
            || p.Status == StatusPedido.RECEBIDO)
            .OrderBy(p =>
                p.Status == StatusPedido.PRONTO ? 0 : p.Status == StatusPedido.EM_PREPARACAO ? 1 : 2)
            .ThenBy(p => p.Id)
            .ToList();

        return await _presenter.ToListPedidoResponse(filter);
    }
}
