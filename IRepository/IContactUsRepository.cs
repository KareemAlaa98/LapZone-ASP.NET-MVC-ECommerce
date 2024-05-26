using E_Commerce_GP.Models;
using E_Commerce_GP.ViewModels;

namespace E_Commerce_GP.IRepository
{
    public interface IContactUsRepository
    {
        List<ContactUs> ReadAll();
        ContactUs ReadById(int id);

        void Create(ContactUsViewModel contactUsViewModel);
        void Delete(int id);

    }
}
