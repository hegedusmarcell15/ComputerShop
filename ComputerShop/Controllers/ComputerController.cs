using ComputerShop.Models;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Globalization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

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
        [HttpGet("GetComputerByOs")]
        public object GetComputerByOs(string os)
        {
            var computers = context.Computers
                .Include(c => c.Osys) 
                .Where(c => c.Osys != null && c.Osys.Name == os) 
                .ToList();

            return new { message = "Computers retrieved successfully", data = computers };
        }
        [HttpGet("GetAllComputerByOs")]
        public object GetAllComputerByOs()
        {
            var computers = context.Computers
                .Include(c => c.Osys)
                .ToList();

            return new { message = "Computers retrieved successfully", data = computers };
        }
        [HttpGet("GetOswithComputers")]
        public object GetOswithComputers()
        {
            var osystems = context.Osystems
                .Include(o => o.Computers)
                .ToList();

            return new { message = "Operating systems retrieved successfully", data = osystems };
        }
        [HttpGet("GetComputerOs/{id}")]
        public object GetComputerOs(Guid id)
        {
            var computer = context.Computers
                .Include(c => c.Osys)
                .FirstOrDefault(c => c.Id == id);

            if (computer == null)
            {
                return new { message = "Computer not found" };
            }

            return new { message = "Computer retrieved successfully", data = computer };
        }
        [HttpGet("GetOsComputerCount")]
        public object GetOsComputerCount()
        {
            var osCount = context.Osystems
                .Select(o => new
                {
                    OsName = o.Name,
                    ComputerCount = o.Computers.Count
                })
                .ToList();

            return new { message = "Operating system computer counts retrieved successfully", data = osCount };
        }
        [HttpGet("GetComputersByBrand/{brand}")]
        public object GetComputersByBrand(string brand)
        {
            var computers = context.Computers
                .Include(c => c.Osys)
                .Where(c => c.Brand == brand)
                .ToList();

            return new { message = "Computers retrieved successfully", data = computers };
        }
        [HttpGet("GetComputersSorted")]
        public object GetComputersSorted()
        {
            var computers = context.Computers
                .Include(c => c.Osys)
                .OrderBy(c => c.Osys.Name)
                .ThenBy(c => c.Brand)
                .ThenBy(c => c.Type)
                .ToList();

            return new { message = "Computers retrieved successfully", data = computers };
        }
        [HttpGet("GetComputersByMinDisplay/{size}")]
        public object GetComputersByMinDisplay(double size)
        {
            var computers = context.Computers
                .Include(c => c.Osys)
                .Where(c => c.Display >= size)
                .ToList();

            return new { message = "Computers retrieved successfully", data = computers };
        }
        [HttpGet("GetOsComputerCount2")]
        public object GetOsComputerCount2()
        {
            var osCount = context.Osystems
                .Select(o => new
                {
                    OsName = o.Name,
                    ComputerCount = o.Computers.Count
                })
                .ToList();

            return new { message = "Operating system computer counts retrieved successfully", data = osCount };
        }
        [HttpGet("GetComputersByOs/{osName}")]
        public object GetComputersByOs(string osName)
        {
            var computers = context.Computers
                .Include(c => c.Osys)
                .Where(c => c.Osys.Name == osName)
                .ToList();

            return new { message = "Computers retrieved successfully", data = computers };
        }
        [HttpGet("GetComputersWithoutOs")]
        public object GetComputersWithoutOs()
        {
            var computers = context.Computers
                .Include(c => c.Osys)
                .Where(c => c.Osys == null)
                .ToList();

            return new { message = "Computers retrieved successfully", data = computers };
        }
        [HttpGet("GetLargestMemoryComputerByOs/{osName}")]
        public object GetLargestMemoryComputerByOs(string osName)
        {
            var computer = context.Computers
                .Include(c => c.Osys)
                .Where(c => c.Osys.Name == osName)
                .OrderByDescending(c => c.Memory)
                .FirstOrDefault();

            return new { message = "Computer retrieved successfully", data = computer };
        }
        [HttpGet("GetLatestComputers")]
        public object GetLatestComputers()
        {
            var computers = context.Computers
                .Include(c => c.Osys)
                .OrderByDescending(c => c.Id)
                .Take(5)
                .ToList();

            return new { message = "Latest computers retrieved successfully", data = computers };
        }
        [HttpGet("GetBrandSummary")]
        public object GetBrandSummary()
        {
            var brandSummary = context.Computers
                .GroupBy(c => c.Brand)
                .Select(g => new
                {
                    Brand = g.Key,
                    ComputerCount = g.Count()
                })
                .ToList();

            return new { message = "Brand summary retrieved successfully", data = brandSummary };
        }
    }
}
