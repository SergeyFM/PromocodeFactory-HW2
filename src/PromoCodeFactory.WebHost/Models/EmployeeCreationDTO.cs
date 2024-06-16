using System;
using System.Collections.Generic;

namespace PromoCodeFactory.WebHost.Models;

/// <summary>
/// Модель для создания нового сотрудника
/// </summary>
public class EmployeeCreationDTO 
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}

