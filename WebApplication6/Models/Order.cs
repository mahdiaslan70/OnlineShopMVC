namespace WebApplication6.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime OrderedAt { get; set; } = DateTime.Now;
        public bool IsPaid { get; set; }

        public bool IsProcessed { get; set; } = false;

        public ICollection<OrderItem> OrderItems { get; set; }

    }
}
