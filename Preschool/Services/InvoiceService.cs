using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using Preschool.Data;
using Preschool.Models;

namespace Preschool.Services
{
    public class InvoiceService : IInvoiceService
    {

        private readonly ApplicationDbContext _db;

        public InvoiceService(ApplicationDbContext db)
        {
                _db=db;
        }
        public void AddInvoice(Invoice invoice)
        {
            _db.Invoices.Add(invoice);
            _db.SaveChanges();
        }

        public async Task<Invoice> GetInvoiceById(int? id)
        {
            return await _db.Invoices.FindAsync(id);
        }

        public async Task<IEnumerable<Invoice>> GetInvoices()
        {
            return await _db.Invoices.ToListAsync();
        }

        public bool IsExists(int? id)
        {
            return _db.Invoices.Any(a => a.Id == id);
        }

        public void RemoveInvoice(Invoice invoice)
        {
            _db.Remove(invoice);
            _db.SaveChanges();
        }

        public void UpdateInvoice(Invoice invoice)
        {
            _db.Invoices.Update(invoice);
            _db.SaveChanges();
        }
    }
}
