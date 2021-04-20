using GbWebApp.ViewModels;

namespace GbWebApp.Infrastructure.Interfaces
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