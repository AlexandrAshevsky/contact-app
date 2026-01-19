using Contact.Application.Contracts;

namespace Contact.Application.Interfaces
{
    public interface IContactService
    {
        public Task<GetContactResponse> GetById(int id);

        public Task<List<GetContactResponse>> GetAll();

        public Task<int> Create(CreateContactRequest request);

        public Task<int> Update(UpdateContactRequest request);

        public Task Delete(int id);
    }
}
