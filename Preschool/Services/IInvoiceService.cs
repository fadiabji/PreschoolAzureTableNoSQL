using Preschool.Models;

namespace Preschool.Services
{
    public interface IInvoiceService
    {
        Task<IEnumerable<Invoice>> GetInvoices();

        Task<Invoice> GetInvoiceById(int? id);

        void AddInvoice(Invoice invoice);

        void UpdateInvoice(Invoice invoice);

        public void RemoveInvoice(Invoice invoice);

        bool IsExists(int? id);
    }
}
