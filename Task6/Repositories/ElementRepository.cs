using Task6.Data;
using Task6.Models;

namespace Task6.Repositories
{
    public class ElementRepository : EFGenericRepository<Element>
    {
        public ElementRepository(ApplicationContext context) : base(context) {}
    }
}
