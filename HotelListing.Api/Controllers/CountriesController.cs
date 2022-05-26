using AutoMapper;
using HotelListing.Api.Contracts;
using HotelListing.Api.Data;
using HotelListing.Api.Models.Country;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesRepository _countriesRepository;

        private readonly IMapper _mapper;


        public CountriesController(IMapper mapper,  ICountriesRepository countriesRepository)
        {
            _countriesRepository = countriesRepository;
            _mapper = mapper;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {
            var countries = await _countriesRepository.GetAllAsync();
            var records = _mapper.Map<List<GetCountryDto>>(countries);
            return Ok(records);
        }

        // POST: api/Countries
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto createCountry)
        {
            var country = _mapper.Map<Country>(createCountry);
            await _countriesRepository.AddAsync(country);
            return CreatedAtAction("GetCountry", new {id = country.Id}, country);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            var country = await _countriesRepository.GetDetails(id);

            if(country == null)
            {
                return NotFound();
            }

            var record = _mapper.Map<CountryDto>(country);
            return Ok(record);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto country)
        {
           if(id != country.Id)
           {
               return BadRequest("Invalid record Id");
           }

            // _context.Entry(country).State = EntityState.Modified;

            var countrySelected = await _countriesRepository.GetAsync(id);

            if(countrySelected == null)
            {
                return NotFound();
            }

           _mapper.Map(country, countrySelected);

            try
            {
                await _countriesRepository.UpdateAsync(countrySelected);
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!await CountryExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _countriesRepository.GetAsync(id);

            if(country == null)
            {
                return NotFound();
            }

            await _countriesRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            return await _countriesRepository.Exists(id);
        }
        
    }
}