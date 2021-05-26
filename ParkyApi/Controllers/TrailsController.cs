using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parky.Data;
using Parky.DTO;
using Parky.Entity;
using Parky.Repository;
using Parky.Repository.IRepository;

namespace Parky.Controllers
{
    [Route("api/Trails")]
    [ApiController]
    public class TrailsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private ITrailRepository _trailRepository;

        public TrailsController(IMapper mapper, ITrailRepository trailRepository)
        {
            _mapper = mapper;
            _trailRepository = trailRepository;
        }

        [HttpGet(Name = "GetTrails")]
        public List<TrailDTO> GetTrails()
        {
            var trailsList = _trailRepository.GetTrails();
            var trailListDTO = new List<TrailDTO>();
            foreach (var item in trailsList)
            {
                trailListDTO.Add(_mapper.Map<TrailDTO>(item));
            }

            return trailListDTO;
        }

        [HttpGet("{trailId:int}", Name = "GetTrail")]
        public IActionResult GetTrail(int trailId)
        {
            var trailDb = _trailRepository.GetTrail(trailId);
            if (trailDb != null)
            {
                var trailDTO = _mapper.Map<TrailDTO>(trailDb);
                return Ok(trailDTO);
            }
            else
                return NotFound();
        }

        [HttpGet("{nationalParkId:int}", Name = "GetTrailsInNationalPark")]
        public IActionResult GetTrailsInNationalPark(int nationalParkId)
        {
            var trailsDb = _trailRepository.GetTrailsInNationalPark(nationalParkId);
            if (trailsDb != null)
            {
                var trailsDto = new List<TrailDTO>();
                foreach (var item in trailsDb)
                {
                    trailsDto.Add(_mapper.Map<TrailDTO>(item));
                }

                return Ok(trailsDto);
            }
            else
                return NotFound();
        }

        [HttpPost(Name = "CreateTrail")]
        public IActionResult CreateTrail([FromBody] TrailCreatetDTO trailCreatetDto)
        {
            if (trailCreatetDto == null)
                return BadRequest(ModelState);
            bool isExist = _trailRepository.TrailExisted(trailCreatetDto.name);
            if (isExist)
            {
                ModelState.AddModelError("", "This Trail is Exsisted!");
                return StatusCode(404, ModelState);
            }

            var trailObj = _mapper.Map<Trails>(trailCreatetDto);
            if (!_trailRepository.CreateTrail(trailObj))
            {
                ModelState.AddModelError("", $"Something went Wrong while Saving record {trailObj.name}");
                return StatusCode(500, ModelState);
            }
            else
                return CreatedAtRoute("GetTrail", new {trailId = trailObj.Id}, trailObj);
        }

        [HttpPut(Name = "UpdateTrail")]
        public IActionResult UpdateTrail([FromBody] TrailUpdateDTO trailUpdateDto)
        {
            if (trailUpdateDto == null)
                return BadRequest(ModelState);


            var trailObj = _mapper.Map<Trails>(trailUpdateDto);
            if (!_trailRepository.UpdateTrail(trailObj))
            {
                ModelState.AddModelError("", $"Something went Wrong while Saving record {trailObj.name}");
                return StatusCode(500, ModelState);
            }
            else
                return CreatedAtRoute("GetTrail", new {trailId = trailObj.Id}, trailObj);
        }

        [HttpDelete("{trailId:int}", Name = "RemoveTrail")]
        public IActionResult RemoveTrail(int trailId)
        {
            if (!_trailRepository.TrailExisted(trailId))
                return NotFound();

            var trailObj = _trailRepository.GetTrail(trailId);
            if (!_trailRepository.DeleteTrail(trailObj))
            {
                ModelState.AddModelError("", $"Something went Wrong while Deleting record {trailObj.name}");
                return StatusCode(500, ModelState);
            }
            else
                return NoContent();
        }
    }
}