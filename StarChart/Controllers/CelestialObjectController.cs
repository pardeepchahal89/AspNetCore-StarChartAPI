using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var CelestialObject = _context.CelestialObjects.FirstOrDefault(c => c.Id == id);
            if (CelestialObject == null)
                return NotFound();

            var celestialObject = _context.CelestialObjects.FirstOrDefault(c => c.OrbitedObjectId == CelestialObject.OrbitedObjectId);
            celestialObject.Satellites = new List<Models.CelestialObject>();

            return Ok(CelestialObject);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var CelestialObject = _context.CelestialObjects.FirstOrDefault(c => c.Name == name);
            if (CelestialObject == null)
                return NotFound();

            var celestialObject = _context.CelestialObjects.FirstOrDefault(c => c.OrbitedObjectId == CelestialObject.OrbitedObjectId);
            celestialObject.Satellites = new List<Models.CelestialObject>();

            return Ok(CelestialObject);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var all = _context.CelestialObjects.ToList();
            all.ForEach(a =>
            {
                a.Satellites = new List<Models.CelestialObject>();
            });

            return Ok(all);

        }
    }
}
