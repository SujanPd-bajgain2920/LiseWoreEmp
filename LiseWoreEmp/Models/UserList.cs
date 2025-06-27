using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiseWoreEmp.Models;

public partial class UserList
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public short UserId { get; set; }

    public string EmailAddress { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
