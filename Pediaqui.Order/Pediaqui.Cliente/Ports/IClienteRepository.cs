using Refit;

namespace Pediaqui.Cliente.Ports;

public interface IClienteRepository
{
    [Get("api/clientes/{cpf}")]
    Task<Models.Cliente> buscarPorCpf([AliasAs("cpf")] string cpfCliente);
}
