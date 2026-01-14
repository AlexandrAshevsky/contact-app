namespace Contact.Application.Contracts
{
    public class UpdateContactRequest : CreateContactRequest
    {
        public int Id { get; set; }
    }
}
