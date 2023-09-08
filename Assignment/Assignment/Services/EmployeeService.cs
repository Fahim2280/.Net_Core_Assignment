using Assignment.DTO;
using Assignment.Interfaces;
using Assignment.Models;
using Assignment.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Services
{
   
    public interface IEmployeeService
    {
        Task<EmployeeDto> UpdateEmployee(int id, EmployeeDto updateDto);
    }
    
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository; // Declare _employeeRepository

        public EmployeeService(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository; // Initialize _employeeRepository in the constructor
        }

        public async Task<EmployeeDto> UpdateEmployee(int id, EmployeeDto updateDto)
        {
            try
            {
                var employee = await _employeeRepository.GetById(id);

                if (employee == null)
                {
                    return null; // or throw an exception, depending on your error handling strategy
                }

                // Validate and update logic

                // Check for duplicate EmployeeCode
                var existingEmployeeWithCode = await _employeeRepository.GetByCode(updateDto.EmployeeCode);

                if (existingEmployeeWithCode != null && existingEmployeeWithCode.EmployeeId != id)
                {
                    throw new Exception("Employee code already exists.");
                }

                employee.EmployeeName = updateDto.EmployeeName;
                employee.EmployeeCode = updateDto.EmployeeCode;
                employee.EmployeeSalary = updateDto.EmployeeSalary;
                employee.SupervisorId = updateDto.SupervisorId;

                var updatedEmployee = await _employeeRepository.Update(employee);

                return new EmployeeDto
                {
                    EmployeeId = updatedEmployee.EmployeeId,
                    EmployeeName = updatedEmployee.EmployeeName,
                    EmployeeCode = updatedEmployee.EmployeeCode,
                    EmployeeSalary = updatedEmployee.EmployeeSalary,
                    SupervisorId = updatedEmployee.SupervisorId
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}


