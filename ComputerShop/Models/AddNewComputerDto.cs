using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerShop.Models
{
    public class AddNewComputerDto
    {
        public Guid Id { get; set; }
        public string? Brand { get; set; }
        public string? Type { get; set; }
        public double Display { get; set; }
        public int Memory { get; set; }
        public DateTime CreatedTime { get; set; }
        public Osystem Osys { get; set; }
    }
}
