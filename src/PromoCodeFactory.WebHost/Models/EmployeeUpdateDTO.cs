using System;
using System.Collections.Generic;

namespace PromoCodeFactory.WebHost.Models;

/// <summary>
/// Модель для редактирования сотрудника с данным Id
/// </summary>
public class EmployeeUpdateDTO 
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}

