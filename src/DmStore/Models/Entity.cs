namespace DmStore.Models
{
    public abstract class Entity
    {
        protected Entity()
        {
            ID = Guid.NewGuid().ToString();
        }
        public string ID { get; set; }
    }
}
