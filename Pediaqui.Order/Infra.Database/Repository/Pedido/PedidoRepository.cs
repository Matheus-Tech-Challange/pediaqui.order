using Domain.Pedido.Ports;
using Microsoft.EntityFrameworkCore;

namespace Infra.Database.Repository.Pedido;

public class PedidoRepository : IPedidoRepository
{
    public PedidoRepository(DatabaseContext databaseContext)
    {
        _context = databaseContext;
    }

    public DatabaseContext _context { get; set; }

    public void Atualiza(Domain.Entities.Pedido pedido)
    {
        _context.Pedidos.Update(pedido);
        _context.SaveChanges();
    }

    public async Task<int> Cria(Domain.Entities.Pedido pedido)
    {
        var p = await _context.Pedidos.AddAsync(pedido);
        await _context.PedidoItems.AddRangeAsync(pedido.Itens);

        _context.SaveChanges();
        return p.Entity.Id;
    }

    public async Task<List<Domain.Entities.Pedido>> ListaTodos()
    {
        return await _context.Pedidos.ToListAsync();
    }

    public async Task<Domain.Entities.Pedido?> ObterPorId(int id)
    {
        return await _context.Pedidos
            .Where(p => p.Id == id)
            .Include(p => p.Itens)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Domain.Entities.Pedido>> ObterTodos()
    {
        return await _context.Pedidos
            .Include(p => p.Itens).ToListAsync();
    }
}
