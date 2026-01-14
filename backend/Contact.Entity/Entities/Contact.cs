namespace Contact.Entity
{
    public class Contact : BaseEntity
    {
        public string Name { get; set; }

        public string MobilePhone { get; set; }

        public string JobTitle { get; set; }

        public DateOnly BirthDate { get; set; }
    }
}
