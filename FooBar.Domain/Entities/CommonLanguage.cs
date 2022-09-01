using System.ComponentModel.DataAnnotations;

namespace FooBar.Domain.Entities
{
    public class CommonLanguage : DomainEntity
    {
        public Guid Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; } = default!;
        [StringLength(255)]
        public string Description { get; set; } = default!;
        public DateTime ReleasedIn { get; set; }
        public CommonLanguage(Guid id, string name, string description, DateTime releasedIn)
        {
            Id = id;
            Name = name;
            Description = description;
            ReleasedIn = releasedIn;
        }
        public CommonLanguage() { }
    }
}
