using Refit;

namespace Pediaqui.Produto.Ports;

public interface IProdutoRepository
{
    [Get("api/produtos/{id}")]
    Task<Models.Produto> obterPorId([AliasAs("id")] int produtoId);
}
