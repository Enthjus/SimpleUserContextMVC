using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleUser.API.DTOs;
using SimpleUser.Domain.Entities;
using SimpleUser.Persistence.Data;

namespace SimpleUser.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CustomerService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task DeleteAsync(int id)
        {
            Customer Customer = await FindByIdAsync(id);
            _context.Customers.Remove(Customer);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer> FindByIdAsync(int id)
        {
            Customer Customer = await _context.Customers.Include(x => x.CustomerDetail).FirstOrDefaultAsync(u => u.Id == id);
            return Customer;
        }

        public async Task<CustomerDto> FindCustomerDtoByIdAsync(int id)
        {
            Customer Customer = await FindByIdAsync(id);
            CustomerDto CustomerDto = _mapper.Map<CustomerDto>(Customer);
            return CustomerDto;
        }

        public async Task<int> InsertAsync(CustomerCreateDto CustomerCreateDto)
        {
            CustomerCreateDto.Password = BCrypt.Net.BCrypt.HashPassword(CustomerCreateDto.Password);
            Customer Customer = _mapper.Map<Customer>(CustomerCreateDto);
            var entry = _context.Customers.Add(Customer);
            await _context.SaveChangesAsync();
            return entry.Entity.Id;
        }

        public async Task<int> UpdateAsync(CustomerUpdateDto CustomerUpdateDto)
        {
            var oldCustomer = await FindByIdAsync(CustomerUpdateDto.Id);
            _mapper.Map(CustomerUpdateDto, oldCustomer);
            var entry = _context.Customers.Update(oldCustomer);
            await _context.SaveChangesAsync();
            return entry.Entity.Id;
        }

        public bool IsCustomerAlreadyExistsByEmail(string email, int id = 0)
        {
            if(id != 0)
            {
                var oldCustomer = _context.Customers.Find(id);
                if(oldCustomer.Email != email)
                {
                    return _context.Customers.Any(x => x.Email == email);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return _context.Customers.Any(x => x.Email == email);
            }
        }

        public async Task<PaginatedList<CustomerDto>> FindAllByPageAsync(int pageSize, int pageIndex, string filter)
        {
            IQueryable<Customer> Customers = from u in _context.Customers.Include(x => x.CustomerDetail)
                                             select u;
            IQueryable<CustomerDto> CustomerDtos = _mapper.ProjectTo<CustomerDto>(Customers);
            PaginatedList<CustomerDto> pageList;
            if (!string.IsNullOrEmpty(filter))
            {
                CustomerDtos = CustomerDtos
                    .Where(u => u.CustomerDetailDto.LastName.ToUpper().Contains(filter.ToUpper()) ||
                    u.CustomerDetailDto.FirstName.ToUpper().Contains(filter.ToUpper()) ||
                    u.CustomerDetailDto.PhoneNumber.Contains(filter) ||
                    u.Email.ToUpper().Contains(filter.ToUpper()));
            }
            if(IsZeroOrNull(pageSize))
            {
                pageSize = 4;
            }
            if(IsZeroOrNull(pageIndex))
            {
                pageIndex = 1;
            }
            pageList = await PaginatedList<CustomerDto>.CreateAsync(CustomerDtos, pageIndex, pageSize);
            return pageList;
        }

        private bool IsZeroOrNull(int? num)
        {
            if(num == 0 || num == null) return true;
            return false;
        }

        public Customer FindById(int id)
        {
            Customer Customer = _context.Customers.Include(x => x.CustomerDetail).FirstOrDefault(u => u.Id == id);
            return Customer;
        }

        public async Task<Customer> Login(string email, string password)
        {
            Customer Customer = await _context.Customers.FirstOrDefaultAsync(u => u.Email == email);
            if(Customer != null && BCrypt.Net.BCrypt.Verify(password, Customer.Password))
            {
                return Customer;
            }
            return null;
        }
    }
}
