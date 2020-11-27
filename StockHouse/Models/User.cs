namespace StockHouse.Models
{
    public class User : IModel
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Role { get; set; }
    }
}