using System;
using System.Collections.Generic;

namespace LiseWoreEmp.Models;

public partial class UserList
{
    public short UserId { get; set; }

    public string EmailAddress { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
