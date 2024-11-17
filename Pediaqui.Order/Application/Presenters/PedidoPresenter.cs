using Application.Features;
using AutoMapper;
using Domain.Entities;
using Pediaqui.Catalog.Ports;
using Pediaqui.Payment.Ports;

namespace Application.Presenters;

public class PedidoPresenter
{
    private ICatalogRepository _catalogRepository;
    private IPaymentRepository _paymentRepository;
    private IMapper _mapper;

    public PedidoPresenter(
        IMapper mapper, ICatalogRepository clienteRepository,
        IPaymentRepository paymentRepository)
    {
        _catalogRepository = clienteRepository;
        _mapper = mapper;
        _paymentRepository = paymentRepository;
    }

    public async Task<CheckoutPedidoResponse> ToCheckoutPedidoResponse(Pedido pedido)
    {
        var map = _mapper.Map<CheckoutPedidoResponse>(pedido);
        map.StatusPagamento = await _paymentRepository.GetPaymentStatus(pedido.Id);

        return await Task.FromResult(map);
    }

    public async Task<ListPedidosResponse> ToListPedidoResponse(List<Pedido> pedidos)
    {
        ListPedidosResponse result = new();

        foreach (var item in pedidos)
        {
            var p = await ToPedidoResponse(item);
            result.Pedidos.Add(p);
        }

        return result;
    }

    public async Task<PedidoResponse> ToPedidoResponse(Pedido pedido)
    {
        var map = _mapper.Map<PedidoResponse>(pedido);

        foreach (var item in map.Itens)
        {
            var produto = await _catalogRepository.buscarProdutoPorId(item.ProdutoId);
            item.Nome = produto.Nome!;
        }

        if (pedido.CPFCliente != null)
        {
            var cliente = await _catalogRepository.buscarClientePorCpf(pedido.CPFCliente);
            map.ClienteNome = cliente.nome!;
        }

        return map;
    }
}
