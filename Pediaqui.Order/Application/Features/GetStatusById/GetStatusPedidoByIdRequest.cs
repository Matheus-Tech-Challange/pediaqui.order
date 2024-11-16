namespace Application.Features.GetStatusById;

public record GetStatusPedidoByIdRequest : IRequest<PedidoResponse>
{
    public int PedidoId { get; set; }
}
