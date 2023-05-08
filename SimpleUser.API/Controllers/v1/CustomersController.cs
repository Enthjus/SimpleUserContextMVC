using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleUser.API.DTOs;
using SimpleUser.API.Services;

namespace SimpleUser.API.Controllers.v1
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[ModelValidator]
    public class CustomersController : ControllerBase
    {
        private ICustomerService _CustomerService;
        private readonly ILogger _logger;

        public CustomersController(ICustomerService CustomerService, ILogger<CustomersController> logger)
        {
            _CustomerService = CustomerService;
            _logger = logger;
        }

        [Authorize(Policy = "RequireUser")]
        [HttpGet]
        public async Task<ActionResult<PaginatedList<CustomerDto>>> Index([FromQuery] PageInfoDto pageInfo)
        {
            PaginatedList<CustomerDto> Customers = await _CustomerService.FindAllByPageAsync(pageInfo.PageSize, pageInfo.PageIndex, pageInfo.Filter);
            if (Customers == null)
            {
                _logger.LogInformation("Can not find any Customer");
                return NotFound();
            }
            _logger.LogInformation($"Find {Customers.Customers.Count} Customers");
            return Customers;
        }

        [Authorize(Policy = "RequireUser")]
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerInfoDto>> Details(int id)
        {
            CustomerInfoDto customerInfoDto = await _CustomerService.FindCustomerDtoByIdAsync(id);

            if (customerInfoDto == null)
            {
                _logger.LogInformation($"Can not find Customer have id {id}");
                return NotFound();
            }

            return customerInfoDto;
        }

        [Authorize(Policy = "RequireManager")]
        [HttpPost]
        public async Task<IActionResult> Create(CustomerCreateDto CustomerCreateDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("An error has occurred: " + ModelState);
                return Unauthorized(ModelState);
            }
            _logger.LogInformation($"Successful create Customer {CustomerCreateDto.Customername}");
            return Ok(await _CustomerService.InsertAsync(CustomerCreateDto));
        }

        [Authorize(Policy = "RequireManager")]
        [HttpPut]
        public async Task<ActionResult<int>> Edit(CustomerUpdateDto CustomerUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("An error has occurred: " + ModelState);
                return Unauthorized(ModelState);
            }
            _logger.LogInformation($"Successful update Customer {CustomerUpdateDto.Customername}");
            return Ok(await _CustomerService.UpdateAsync(CustomerUpdateDto));
        }

        [Authorize(Policy = "RequireAdministrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _CustomerService.DeleteAsync(id);
                _logger.LogInformation($"Successful delete Customer have id {id}");
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError("An error has occurred: " + ex);
                return BadRequest();
            }
        }
    }
}
