using Assignment.Interfaces;
using Assignment.Models;
using Assignment.Repositories;

namespace Assignment.DataAccess
{
    public class DataAccessFactory
    {
        private readonly AppDbContext _context;

        public DataAccessFactory(AppDbContext context)
        {
            _context = context;
        }

        public IRepository<Employee> GetEmployeeRepository()
        {
            return new EmployeeRepository(_context);
        }
    }
}
