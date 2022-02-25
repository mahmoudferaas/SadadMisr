namespace SadadMisr.DAL.Common
{
    public class LookupEntity<T> : Entity<T>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}