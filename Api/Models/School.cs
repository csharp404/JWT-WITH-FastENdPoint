namespace Api.Models
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<AppUser>? Students { get; set; }
        }
}
