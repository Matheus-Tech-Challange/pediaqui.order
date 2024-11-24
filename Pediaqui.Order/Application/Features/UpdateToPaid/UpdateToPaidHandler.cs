using Domain.Pedido.Ports;

namespace Application.Features.UpdateToPaid;

public class UpdateToPaidHandler : IRequestHandler<UpdateToPaidRequest>
{
    private readonly IPedidoRepository pedidoRepository;
    private readonly NotificationContext notificationContext;

    public UpdateToPaidHandler(IPedidoRepository pedidoRepository, NotificationContext notificationContext)
    {
        this.pedidoRepository = pedidoRepository;
        this.notificationContext = notificationContext;
    }

    public async Task Handle(UpdateToPaidRequest request, CancellationToken cancellationToken)
    {
        var p = await pedidoRepository.ObterPorId(request.PedidoId);

        if (p == null)
        {
            notificationContext.AddNotification("Pedido não encontrado",
                $"Pedido com o id {request.PedidoId} não econtrado.");

            return;
        }

        p.Registrar();
        pedidoRepository.Atualiza(p);
        return;
    }
}
