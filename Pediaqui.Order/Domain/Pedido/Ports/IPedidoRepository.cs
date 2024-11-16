namespace Domain.Pedido.Ports;

public interface IPedidoRepository
{
    public Task<Entities.Pedido?> ObterPorId(int id);
    public Task<List<Entities.Pedido>> ObterTodos();
    public Task<int> Cria(Entities.Pedido pedido);
    public void Atualiza(Entities.Pedido pedido);
    public Task<List<Entities.Pedido>> ListaTodos();
}
