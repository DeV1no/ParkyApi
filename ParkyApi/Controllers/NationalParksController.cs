using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parky.DTO;
using Parky.Entity;
using Parky.Repository.IRepository;

namespace Parky.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalParksController : ControllerBase
    {
        private INationalParkRepository _nationalPark;
        private readonly IMapper _mapper;

        public NationalParksController(INationalParkRepository nationalPark, IMapper mapper)
        {
            _nationalPark = nationalPark;
            _mapper = mapper;
        }

        /// <summary>
        /// Get List Of national Parks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetNationalParks()
        {
            var parkList = _nationalPark.GetNationalParks();
            var parkListDTO = new List<NationalParkDTO>();
            foreach (var item in parkList)
            {
                parkListDTO.Add(_mapper.Map<NationalParkDTO>(item));
            }

            return Ok(parkListDTO);
        }

        /// <summary>
        ///  Get individual national park 
        /// </summary>
        /// <param name="nationalParkId"></param>
        /// <returns></returns>
        [HttpGet("{nationalParkId:int}", Name = "GetNationalPark")]
        [Authorize]
        public IActionResult GetNationalPark(int nationalParkId)
        {
            var parkDb = _nationalPark.GetNationalPark(nationalParkId);
            if (parkDb != null)
            {
                var parkDTO = _mapper.Map<NationalParkDTO>(parkDb);
                return Ok(parkDTO);
            }
            else
                return NotFound();
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public IActionResult CreateNationalPark([FromBody] NationalParkDTO nationalParkDto)
        {
            if (nationalParkDto == null)
                return BadRequest(ModelState);
            if (_nationalPark.NationalParkExists(nationalParkDto.Name))
            {
                ModelState.AddModelError("", "This National Park is Exsisted!");
                return StatusCode(404, ModelState);
            }

            /*if (!ModelState.IsValid)
                return BadRequest(ModelState);*/

            var nationalParkObj = _mapper.Map<NationalPark>(nationalParkDto);
            if (!_nationalPark.CreateNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Something went Wrong while Saving record {nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }
            else
                return CreatedAtRoute("GetNationalPark", new {nationalParkId = nationalParkObj.Id}, nationalParkObj);
        }

        [HttpPatch("{nationalParkId:int}", Name = "UpdateNationalPark")]
        public IActionResult UpdateNationalPark(int nationalParkId, [FromBody] NationalParkDTO nationalParkDto)
        {
            if (nationalParkDto == null || nationalParkId != nationalParkDto.Id)
                return BadRequest(ModelState);
            var nationalParkObj = _mapper.Map<NationalPark>(nationalParkDto);
            if (!_nationalPark.UpdateNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Something went Wrong while Updateing record {nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }
            else
                return CreatedAtRoute("GetNationalPark", new {nationalParkId = nationalParkObj.Id}, nationalParkObj);
        }

        [HttpDelete("{nationalParkId:int}", Name = "DeleteNationalPark")]
        public IActionResult DeleteNationalPark(int nationalParkId)
        {
            if (!_nationalPark.NationalParkExists(nationalParkId))
                return NotFound();

            var nationalParkObj = _nationalPark.GetNationalPark(nationalParkId);
            if (!_nationalPark.DeleteNationalPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Something went Wrong while Deleting record {nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }
            else
                return NoContent();
        }
    }
}