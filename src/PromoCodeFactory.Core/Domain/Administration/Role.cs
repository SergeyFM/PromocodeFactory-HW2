namespace PromoCodeFactory.Core.Domain.Administration
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public override BaseEntity Clone()
        {
            return new Role
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description
            };
        }
    }
}