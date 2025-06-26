using System;
using System.Collections.Generic;

namespace LiseWoreEmp.Models;

public partial class Employee
{
    public short EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Department { get; set; }

    public string? Position { get; set; }

    public DateOnly? HireDate { get; set; }

    public short AddedBy { get; set; }

    public virtual UserList AddedByNavigation { get; set; } = null!;
}
