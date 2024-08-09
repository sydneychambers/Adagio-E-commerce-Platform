using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace EC1ContinuousAssessment_2005734
{
    public class User
    {
        public string Username { get; set; }
        public string Email { get; set; }
        [Column(TypeName = "Date")]
        public DateTime DateOfBirth { get; set; }
        public string UserType { get; set; }

        public User(string username, string email, DateTime dateOfBirth, string userType)
        {
            Username = username;
            Email = email;
            DateOfBirth = dateOfBirth;
            UserType = userType;
        }
    }
}