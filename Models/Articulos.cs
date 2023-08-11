namespace API_STRIPE.Models
{
    public class Articulos
    {
        public int ID { get; set; }

        public string UserKey { get; set; }
        public string Nombre { get; set; }
        public int Costo { get; set; }
        public int Quantity { get; set; }

        public string Email { get; set; }
    }
}
