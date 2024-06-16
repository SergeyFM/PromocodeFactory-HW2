using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.WebHost.Models;

namespace PromoCodeFactory.WebHost.Controllers;

/// <summary>
/// Сотрудники
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController(
    IRepository<Employee> _employeeRepository
    ) : ControllerBase
{

    /// <summary>
    /// Получить данные всех сотрудников
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<EmployeeShortResponse>> GetEmployeesAsync()
    {
        var employees = await _employeeRepository.GetAllAsync();

        var employeesModelList = employees.Select(x =>
            new EmployeeShortResponse()
            {
                Id = x.Id,
                Email = x.Email,
                FullName = x.FullName,
            }).ToList();

        return employeesModelList;
    }

    /// <summary>
    /// Получить данные сотрудника по Id
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EmployeeResponse>> GetEmployeeByIdAsync(Guid id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);

        if (employee == null)
            return NotFound();

        var employeeModel = new EmployeeResponse()
        {
            Id = employee.Id,
            Email = employee.Email,
            Roles = employee.Roles.Select(x => new RoleItemResponse()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToList(),
            FullName = employee.FullName,
            AppliedPromocodesCount = employee.AppliedPromocodesCount
        };

        return employeeModel;
    }

    /// <summary>
    /// Добавить нового сотрудника
    /// </summary>
    /// <param name="employeeCreationDto">Данные нового сотрудника</param>
    /// <returns>Гуид созданного сотрудника</returns>
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateNewEmployee([FromBody] EmployeeCreationDTO employeeCreationDto)
    {
        if (employeeCreationDto is null)
        {
            return BadRequest("Employee is null.");
        }

        var employee = new Employee
        {
            FirstName = employeeCreationDto.FirstName,
            LastName = employeeCreationDto.LastName,
            Email = employeeCreationDto.Email,
        };

        Guid resGuid = await _employeeRepository.CreateAsync(employee);
        return Ok(resGuid);
    }

    /// <summary>
    /// Удалить сотрудника
    /// </summary>
    /// <param name="id">Его гуид</param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEmployeeByIdAsync(Guid id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee is null)
        {
            return NotFound();
        }

        await _employeeRepository.DeleteByIdAsync(id);
        return Ok( new { message = $"Сотрудник {id} удалён" });
    }

    /// <summary>
    /// Обновить данные сотрудника по его гуид
    /// </summary>
    /// <param name="employeeUpdateDto"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateEmployeeAsync([FromBody] EmployeeUpdateDTO employeeUpdateDto)
    {
        if (employeeUpdateDto is null)
        {
            return BadRequest("Employee is null.");
        }

        var existingEmployee = await _employeeRepository.GetByIdAsync(employeeUpdateDto.Id);
        if (existingEmployee is null)
        {
            return NotFound();
        }

        existingEmployee.FirstName = employeeUpdateDto.FirstName;
        existingEmployee.LastName = employeeUpdateDto.LastName;
        existingEmployee.Email = employeeUpdateDto.Email;

        await _employeeRepository.UpdateAsync(existingEmployee);
        return Ok(new { message = $"Сотрудник {employeeUpdateDto.Id} обновлён успешно" });
    }



}