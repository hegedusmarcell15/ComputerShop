using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerShop.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddNewOsystemDto : ControllerBase
    {
        public string Name { get; set; }
        public int Version { get; set; }
    }
}
