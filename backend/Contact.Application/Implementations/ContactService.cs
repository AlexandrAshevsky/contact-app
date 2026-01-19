using Contact.Application.Contracts;
using Contact.Application.Interfaces;
using Contact.Database;
using Microsoft.EntityFrameworkCore;

using ContactEntity = Contact.Entity.Contact;

namespace Contact.Application.Implementations
{
    internal class ContactService : IContactService
    {
        private readonly ContactDbContext _context;

        public ContactService(ContactDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(CreateContactRequest request)
        {
            var entity = new ContactEntity
            {
                Name = request.Name,
                MobilePhone = request.MobilePhone,
                JobTitle = request.JobTitle,
                BirthDate = request.BirthDate,
            };

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public Task Delete(int id)
        {
            _context.Remove(new ContactEntity { Id = id });

            return _context.SaveChangesAsync();
        }

        public Task<List<GetContactResponse>> GetAll() =>
            _context.Contacts
            .Select(x => new GetContactResponse
            {
                Id = x.Id,
                Name = x.Name,
                MobilePhone = x.MobilePhone,
                JobTitle = x.JobTitle,
                BirthDate = x.BirthDate,
            })
            .ToListAsync();

        public Task<GetContactResponse> GetById(int id) =>
            _context.Contacts
            .Select(x => new GetContactResponse
            {
                Id = x.Id,
                Name = x.Name,
                MobilePhone = x.MobilePhone,
                JobTitle = x.JobTitle,
                BirthDate = x.BirthDate,
            })
            .FirstAsync(x => x.Id == id);

        public async Task<int> Update(UpdateContactRequest request)
        {
            var entity = await _context.Contacts.FirstAsync(x => x.Id == request.Id);

            entity.Name = request.Name;
            entity.MobilePhone = request.MobilePhone;
            entity.JobTitle = request.JobTitle;
            entity.BirthDate = request.BirthDate;

            await _context.SaveChangesAsync();

            return entity.Id;
        }
    }
}
