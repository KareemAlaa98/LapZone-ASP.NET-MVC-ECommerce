using E_Commerce_GP.Data;
using E_Commerce_GP.IRepository;
using E_Commerce_GP.Models;
using E_Commerce_GP.ViewModels;
using Newtonsoft.Json.Serialization;

namespace E_Commerce_GP.Repository
{
    public class ContactUsRepository : IContactUsRepository
    {
        ApplicationDbContext context;
        public ContactUsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(ContactUsViewModel contactUsViewModel)
        {
            ContactUs contactUs = new ContactUs();
            contactUs.Id = contactUsViewModel.Id;
            contactUs.Name = contactUsViewModel.Name;
            contactUs.Email = contactUsViewModel.Email;
            contactUs.Subject = contactUsViewModel.Subject;
            contactUs.Message = contactUsViewModel.Message;
            contactUs.ModifiedAt = contactUsViewModel.ModifiedAt;
            context.ContactUs.Add(contactUs);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var contactUsRemove = context.ContactUs.Find(id);
            if (contactUsRemove != null)
            {
                context.ContactUs.Remove(contactUsRemove);
                context.SaveChanges();
            }
        }

        public List<ContactUs> ReadAll()
        {
            return context.ContactUs.ToList();
        }

        public ContactUs ReadById(int id)
        {
            return context.ContactUs.Find(id);

        }
    }
}
