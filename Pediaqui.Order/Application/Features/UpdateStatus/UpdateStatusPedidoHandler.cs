using Application.Presenters;
using Domain.Pedido.Ports;

namespace Application.Features.UpdateStatus;

public class UpdateStatusPedidoHandler : IRequestHandler<UpdateStatusPedidoRequest, PedidoResponse>
{
    private readonly NotificationContext _notificationContext;
    private readonly IPedidoRepository _pedidoRepository;
    private readonly PedidoPresenter _presenter;

    public UpdateStatusPedidoHandler(
        NotificationContext notificationContext,
        IPedidoRepository pedidoRepository,
        PedidoPresenter presenter)
    {
        _pedidoRepository = pedidoRepository;
        _notificationContext = notificationContext;
        _presenter = presenter;
    }

    public async Task<PedidoResponse> Handle(UpdateStatusPedidoRequest request, CancellationToken cancellationToken)
    {
        var pedido = await _pedidoRepository.ObterPorId(request.PedidoId);

        if (pedido == null)
        {
            _notificationContext.AddNotification("NullReference", $"Pedido com identificador {request.PedidoId} não encontrado. ");
            return null!;
        }

        pedido.AtualizaProximoStatus();

        _pedidoRepository.Atualiza(pedido);

        return await _presenter.ToPedidoResponse(pedido);
    }
}
