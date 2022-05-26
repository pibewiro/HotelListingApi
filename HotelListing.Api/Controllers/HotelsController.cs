using AutoMapper;
using HotelListing.Api.Contracts;
using HotelListing.Api.Data;
using HotelListing.Api.Models.Hotel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Api.Controllers
{
    [Route("api/[controller]")]
    
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelsRepository _hotelsRepository;
        private readonly IMapper _mapper;

        public HotelsController(IMapper mapper, IHotelsRepository hotelsRepository)
        {
            _hotelsRepository = hotelsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDto>>> GetHotels()
        {
            var hotels = await _hotelsRepository.GetAllAsync();
            var records = _mapper.Map<List<HotelDto>>(hotels);
            return Ok(records);
        }
 


                    // POST: api/Countries
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(CreateHotelDto createHotel)
        {
            var hotel = _mapper.Map<Hotel>(createHotel);
            await _hotelsRepository.AddAsync(hotel);
            return CreatedAtAction("GetHotel", new {id = hotel.Id}, hotel);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetHotel(int id)
        {
            var hotel = await _hotelsRepository.GetAsync(id);

            if(hotel == null)
            {
                return NotFound();
            }

            return Ok(hotel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
           if(id != hotel.Id)
           {
               return BadRequest("Invalid record Id");
           }

            var hotelSelected = await _hotelsRepository.GetAsync(id);

            if(hotelSelected == null)
            {
                return NotFound();
            }

           _mapper.Map(hotel, hotelSelected);

            try
            {
                await _hotelsRepository.UpdateAsync(hotelSelected);
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!await HotelExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _hotelsRepository.GetAsync(id);

            if(hotel == null)
            {
                return NotFound();
            }

            await _hotelsRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _hotelsRepository.Exists(id);
        }
    }
}