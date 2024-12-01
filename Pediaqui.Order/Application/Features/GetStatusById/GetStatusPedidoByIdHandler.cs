using Application.Presenters;
using Domain.Pedido.Ports;

namespace Application.Features.GetStatusById
{
    public class GetStatusPedidoByIdHandler : IRequestHandler<GetStatusPedidoByIdRequest, PedidoResponse>
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly PedidoPresenter _presenter;
        private readonly NotificationContext _notificationContext;

        public GetStatusPedidoByIdHandler(
            IPedidoRepository pedidoRepository,
            PedidoPresenter presenter,
            NotificationContext notificationContext)
        {
            _pedidoRepository = pedidoRepository;
            _presenter = presenter;
            _notificationContext = notificationContext;
        }

        public async Task<PedidoResponse> Handle(GetStatusPedidoByIdRequest request, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepository.ObterPorId(request.PedidoId);

            if (pedido == null)
            {
                _notificationContext.AddNotification("NullReference", "Pedido não encontrado");
                return null!;
            }

            return await _presenter.ToPedidoResponse(pedido);
        }
    }
}
