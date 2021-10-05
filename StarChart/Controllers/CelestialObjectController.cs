﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;
using StarChart.Models;

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
            CelestialObject celestialObject = _context.CelestialObjects.Find(id);
            if(celestialObject == null)
            {
                return NotFound(id);
            } 
            celestialObject.Satellites = _context.CelestialObjects.Where(x => x.OrbitedObjectId == id).ToList();
            return Ok(celestialObject);
        }

        [HttpGet("{name:string}", Name = "GetByName")]
        public IActionResult GetByName(string name) 
        {
            CelestialObject celestialObject = _context.CelestialObjects.First(x => x.Name == name);
            if(celestialObject == null)
            {
                return NotFound(name);
            }
            celestialObject.Satellites = _context.CelestialObjects.Where(x => x.OrbitedObjectId == celestialObject.Id).ToList();
            return Ok(celestialObject);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<CelestialObject> celestialObjects = _context.CelestialObjects.ToList();
            foreach(CelestialObject celestialObject in celestialObjects)
            {
                celestialObject.Satellites = _context.CelestialObjects.Where(x => x.OrbitedObjectId == celestialObject.Id).ToList();
            }
            return Ok(celestialObjects);

        }



    }
}
