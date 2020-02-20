using IndiviualLabA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndiviualLabA.Repositories
{
    public class InvoiceRepo
    {
        ApplicationDbContext _context;

        public InvoiceRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Invoice> All()
        {
            var invoices = _context.Invoices.Select(inv => new Invoice()
            {
                InvoiceID = inv.InvoiceID,
                Created = inv.Created,
                Total = inv.Total,
                UserName = inv.UserName
            });

            return invoices;
        }

        public List<Invoice> GetAllUsers()
        {
            var users = _context.CustomUsers;
            List<Invoice> usersList = new List<Invoice>();

            foreach (var user in users)
            {
                usersList.Add(new Invoice() { UserName = user.UserName });
            }
            return usersList;
        }

        public CustomUser GetUserName(string username)
        {
            var userFound = _context.CustomUsers.Where(cu => cu.UserName == username).FirstOrDefault();

            if (userFound != null)
            {
                return userFound;
            }

            return null;
        }

        public bool Create(Invoice invoice)
        {
            try
            {
                var userName = GetUserName(invoice.UserName);

                _context.Invoices.Add(new Invoice
                {
                    UserName = userName.UserName,
                    Created = DateTime.Now,
                    Total = invoice.Total
                });

                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool Delete(int id)
        {
            try
            {
                Invoice removeInvoice = _context.Invoices.Where(inv => inv.InvoiceID == id).FirstOrDefault();
                _context.Invoices.Remove(removeInvoice);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}