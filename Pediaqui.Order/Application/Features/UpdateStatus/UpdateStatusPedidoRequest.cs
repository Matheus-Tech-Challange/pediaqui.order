namespace Application.Features.UpdateStatus;

public class UpdateStatusPedidoRequest : IRequest<PedidoResponse>
{
    public int PedidoId { get; set; }
}
