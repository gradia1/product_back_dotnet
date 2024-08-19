using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyApiProject.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public Stock Stock { get; set; }
    }

    public class Stock
    {
        public string Id { get; set; }
        public int Amount { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }

    }



}