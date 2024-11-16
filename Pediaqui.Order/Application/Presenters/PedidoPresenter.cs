using Application.Features;
using AutoMapper;
using Domain.Entities;
using Pediaqui.Cliente.Ports;
using Pediaqui.Payment.Models;
using Pediaqui.Payment.Ports;
using Pediaqui.Produto.Ports;

namespace Application.Presenters;

public class PedidoPresenter
{
    private IProdutoRepository _produtoRepository;
    private IClienteRepository _clienteRepository;
    private IPaymentRepository _paymentRepository;
    private IMapper _mapper;

    public PedidoPresenter(
        IProdutoRepository produtoRepository, 
        IMapper mapper, IClienteRepository clienteRepository, 
        IPaymentRepository paymentRepository)
    {
        _produtoRepository = produtoRepository;
        _clienteRepository = clienteRepository;
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
            var produto = await _produtoRepository.obterPorId(item.ProdutoId);
            item.Nome = produto.Nome!;
        }

        if (pedido.CPFCliente != null)
        {
            var cliente = await _clienteRepository.buscarPorCpf(pedido.CPFCliente);
            map.ClienteNome = cliente.cpf!;
        }

        return map;
    }
}
