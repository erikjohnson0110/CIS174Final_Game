using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserViewModel
{
    public Guid PersonId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateCreated { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Gender { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}
