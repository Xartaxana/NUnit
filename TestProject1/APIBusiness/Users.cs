using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.APIBusiness
{
    internal class Users
    {
        public class Address
        {
            public string? Street { get; set; }
            public string? Suite { get; set; }
            public string? City { get; set; }
            public string? Zipcode { get; set; }
            public Geo? Geo { get; set; }
        }

        public class Company
        {
            public string? Name { get; set; }
            public string? CatchPhrase { get; set; }
            public string? Bs { get; set; }
        }

        public class Geo
        {
            public string? Lat { get; set; }
            public string? Lng { get; set; }
        }

        public class User
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? Username { get; set; }
            public string? Email { get; set; }
            public Address? Address { get; set; }
            public string? Phone { get; set; }
            public string? Website { get; set; }
            public Company? Company { get; set; }
        }
        public class UserBuilder
        {
            private readonly User _user;

            public UserBuilder()
            {
                _user = new User();
            }

            public UserBuilder WithName(string name)
            {
                _user.Name = name;
                return this;
            }

            public UserBuilder WithUsername(string username)
            {
                _user.Username = username;
                return this;
            }

            public UserBuilder WithEmail(string email)
            {
                _user.Email = email;
                return this;
            }

            public User Build()
            {
                return _user;
            }
        }
    }
}
