using DP_Shop.DTOs.Address;
using DP_Shop.DTOs.Users;
using DP_Shop.Helpers.Query;
using DP_Shop.Interface;
using DP_Shop.Respository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DP_Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _addressRepository;
        public AddressController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userID = User.FindFirst("userId");
            if (userID == null)
            {
                return BadRequest("User id is null");
            }

            var result = await _addressRepository.CreateAsync(request.Address, userID.Value, request.IsDefault);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddesss([FromRoute] int id, [FromBody] DeleteAddressRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _addressRepository.AddressExists(id))
            {
                return NotFound("Address not found");
            }

            var result = await _addressRepository.DeleteAsync(id, request.UserId);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        /*[Authorize]
        [HttpDelete("unlink/{id}")]
        public async Task<IActionResult> UnlinkToAddress([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _addressRepository.AddressExists(id))
            {
                return NotFound("Address not found");
            }


            var userID = User.FindFirst("userId");
            if (userID == null)
            {
                return BadRequest("User id is null");
            }
            var result = await _addressRepository.UnlinkToAddressAsync(id, userID.Value);

            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);

        }*/

        [Authorize(Roles = "Admin")]
        [HttpGet("admin/")]
        public async Task<IActionResult> GetAllAddress([FromQuery] QueryAddress query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _addressRepository.GetAllAddress(query);

            if (result == null)
            {
                return BadRequest(new { message = "List addresses is null" });
            }
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _addressRepository.AddressExists(id))
            {
                return NotFound("Address not found");
            }

            var result = await _addressRepository.GetAddressByIdAsyncNoTracking(id);

            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

      

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAddressesByUserId([FromQuery] QueryAddress query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userID = User.FindFirst("userId");
            if (userID == null)
            {
                return BadRequest("User id is null");
            }
            var result = await _addressRepository.GetAddressesByUserId(query, userID.Value);

            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress([FromRoute] int id, [FromBody] UpdateAddressRequest updateAddressRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userID = User.FindFirst("userId");
            if (userID == null)
            {
                return BadRequest("User id is null");
            }

            if (!await _addressRepository.AddressExists(id))
            {
                return NotFound("Address not found");
            }

            var result = await _addressRepository.UpdateAsync(id, updateAddressRequest, userID.Value);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        //[Authorize]
        [HttpGet("/provinces")]
        public async Task<IActionResult> GetProvinces()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _addressRepository.GetProvinces();
            if (result.Succeeded) 
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("/province/{code}")]
        public async Task<IActionResult> GetProvinceByCode([FromRoute] string code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _addressRepository.GetProvinceByCode(code);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("/district/{parentCode}")]
        public async Task<IActionResult> GetDicstrictByCode([FromRoute] string parentCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _addressRepository.GetDistricstByParentCode(parentCode);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }
        [HttpGet("/ward/{parentCode}")]
        public async Task<IActionResult> GetWardByCode([FromRoute] string parentCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _addressRepository.GetWardsByParentCode(parentCode);
            if (result.Succeeded)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }
    }
}
