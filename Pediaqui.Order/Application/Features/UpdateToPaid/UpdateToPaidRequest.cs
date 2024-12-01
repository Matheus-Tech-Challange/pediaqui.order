namespace Application.Features.UpdateToPaid;

public class UpdateToPaidRequest : IRequest
{
    public int PedidoId { get; set; }
}
