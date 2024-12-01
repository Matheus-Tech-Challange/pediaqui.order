namespace Pediaqui.Produto.Models;

public record Produto(
    int Id,
    string? Nome,
    string? Descricao,
    string? Categoria,
    decimal Preco
);