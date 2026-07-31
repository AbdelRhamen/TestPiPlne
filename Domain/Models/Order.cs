
namespace Core.Models
{
    public class Order : AuditEntity    
    {
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    }
}

