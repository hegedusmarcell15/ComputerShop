using ComputerShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OsystemController : ControllerBase
    {
        public CmpShopDBContext context = new CmpShopDBContext();
        [HttpGet("getall")]
        public object GetAllOsystems()
        {
            var osystems = context.Osystems.ToList();
            return new { message = "Osystems retrieved successfully", data = osystems };
        }
        [HttpPost]

        public object AddOsystem(AddNewOsystemDto addNewOsystemDto)
        {
            var osystem = new Osystem
            {
                Id = Guid.NewGuid(),
                Name = addNewOsystemDto.Name,
                Version = addNewOsystemDto.Version,
                RegisterTime = DateTime.UtcNow,
                UpdateTime = DateTime.UtcNow
            };

            context.Osystems.Add(osystem);
            context.SaveChanges();

            return new { message = "Osystem added successfully", data = osystem };

        }
        [HttpPut]
        public object UpdateOsystem(UpdateOsystemDto updateOsystemDto)
        {
            var osystem = context.Osystems.FirstOrDefault(o => o.Id == updateOsystemDto.Id);
            if (osystem == null)
            {
                return new { message = "Osystem not found" };
            }
            osystem.Name = updateOsystemDto.Name;
            osystem.Version = updateOsystemDto.Version;
            osystem.UpdateTime = DateTime.Now;
            context.SaveChanges();
            return new { message = "Osystem updated successfully", data = osystem };
        }

        [HttpDelete]
        public object DeleteOsystem(Guid id)
        {
            var osystem = context.Osystems.FirstOrDefault(o => o.Id == id);
            if (osystem == null)
            {
                return new { message = "Osystem not found" };
            }
            context.Osystems.Remove(osystem);
            context.SaveChanges();
            return new { message = "Osystem deleted successfully" };
        }
    }
}
