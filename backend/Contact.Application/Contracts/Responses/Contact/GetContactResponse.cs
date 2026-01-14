namespace Contact.Application.Contracts
{
    public class GetContactResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string MobilePhone { get; set; }

        public string JobTitle { get; set; }

        public DateOnly BirthDate { get; set; }
    }
}
