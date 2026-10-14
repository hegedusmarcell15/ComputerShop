using ComputerShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComputerShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComputerController : ControllerBase
    {
        public CmpShopDBContext context = new CmpShopDBContext();
        [HttpGet("GetAllComputers")]
        public object GetAllComputers()
        {
            var computers = context.Computers.ToList();
            return new { message = "Computers retrieved successfully", data = computers };
        }
        [HttpPost]
        public object AddComputer(AddNewComputerDto addNewComputerDto)
        {
            var computer = new Computer
            {
                Id = Guid.NewGuid(),
                Brand = addNewComputerDto.Brand,
                Type = addNewComputerDto.Type,
                Display = addNewComputerDto.Display,
                Memory = addNewComputerDto.Memory,
                CreatedTime = DateTime.UtcNow
            };

            context.Computers.Add(computer);
            context.SaveChanges();

            return new { message = "Computer added successfully", data = computer };
        }
        [HttpPut]
        public object UpdateComputer(UpdateComputerDto updateComputerDto)
        {
            var computer = context.Computers.FirstOrDefault(c => c.Id == updateComputerDto.Id);
            if (computer == null)
            {
                return new { message = "Computer not found" };
            }
            computer.Brand = updateComputerDto.Brand;
            computer.Type = updateComputerDto.Type;
            computer.Display = updateComputerDto.Display;
            computer.Memory = updateComputerDto.Memory;
            computer.CreatedTime = DateTime.UtcNow;
            context.SaveChanges();
            return new { message = "Computer updated successfully", data = computer };
        }
        [HttpDelete]
        public object DeleteComputer(Guid id)
        {
            var computer = context.Computers.FirstOrDefault(c => c.Id == id);
            if (computer == null)
            {
                return new { message = "Computer not found" };
            }
            context.Computers.Remove(computer);
            context.SaveChanges();
            return new { message = "Computer deleted successfully" };
        }
    }
}
