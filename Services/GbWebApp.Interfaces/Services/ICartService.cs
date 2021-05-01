using GbWebApp.Domain.ViewModels;

namespace GbWebApp.Interfaces.Services
{
    public interface ICartService
    {
        CartViewModel GetViewModel();

        void Clear();

        void Increment(int id);

        void Decrement(int id);

        void Remove(int id);
    }
}