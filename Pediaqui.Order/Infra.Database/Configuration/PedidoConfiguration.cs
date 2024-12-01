using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.Configuration;

public class PedidoConfiguration : BaseEntityConfiguration<Pedido>
{
    public override void Configure(EntityTypeBuilder<Pedido> builder)
    {
        base.Configure(builder);

        builder
            .Property(p => p.CPFCliente)
            .HasColumnType("varchar(255)")
            .IsRequired(false);

        builder.Property(p => p.Status)
            .HasColumnType("int")
            .IsRequired();

        builder.HasMany(e => e.Itens)
            .WithOne()
            .IsRequired();
    }
}
