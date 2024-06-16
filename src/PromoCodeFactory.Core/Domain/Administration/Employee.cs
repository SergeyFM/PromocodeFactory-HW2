using System.Collections.Generic;
using System.Linq;

namespace PromoCodeFactory.Core.Domain.Administration
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string Email { get; set; }

        public List<Role> Roles { get; set; }

        public int AppliedPromocodesCount { get; set; }

         public override BaseEntity Clone()
        {
            return new Employee
            {
                Id = this.Id,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                Roles = this.Roles is not null ? this.Roles.Select(role => (Role)role.Clone()).ToList() : null,
                AppliedPromocodesCount = this.AppliedPromocodesCount
            };
        }
    }
}