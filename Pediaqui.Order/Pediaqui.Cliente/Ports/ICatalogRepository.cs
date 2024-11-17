using Pediaqui.Catalog.Models;
using Refit;

namespace Pediaqui.Catalog.Ports;

public interface ICatalogRepository
{
    [Get("/api/clientes/{cpf}")]
    Task<Cliente> buscarClientePorCpf([AliasAs("cpf")] string cpfCliente);
    
    [Get("/api/produtos/{id}")]
    Task<Produto> buscarProdutoPorId([AliasAs("id")] int produtoId);
}
