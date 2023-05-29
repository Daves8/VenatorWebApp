
namespace VenatorWebApp.Models.Abstracts.Base
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public User? Owner { get; set; }
        public DateTime CreationDate { get; set; }

        protected BaseEntity()
        {
        }

        protected BaseEntity(int id)
        {
            Id = id;
        }

        public abstract bool IsValid();
    }
}
