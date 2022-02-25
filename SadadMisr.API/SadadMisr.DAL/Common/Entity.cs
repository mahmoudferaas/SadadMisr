namespace SadadMisr.DAL.Common
{
    public class Entity : Entity<int>
    { }

    public class Entity<T>
    {
        public virtual T Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}