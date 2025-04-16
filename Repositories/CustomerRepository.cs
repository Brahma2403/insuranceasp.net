using insuranceApp1.Models;

namespace insuranceApp1.Repositories
{
    public class CustomerRepository
    {
        InsuranceDbContext cutx = null;

        public CustomerRepository(InsuranceDbContext cutx)
        {
            this.cutx = cutx;
        }
        public bool AddCustomer(Customer customer)
        {
            cutx.Customers.Add(customer);
            int r = cutx.SaveChanges();
            if (r > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //public bool RemoveCustomer(Customer customer)
        //{
        //    cutx.Customers.Remove(customer);
        //    int r = cutx.SaveChanges();
        //    if (r > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        public bool UpdateCustomer(Customer customer)
        {
            Customer avCustomer = cutx.Customers.Find(customer.CustomerId);
            avCustomer.Name = customer.Name;
            avCustomer.Email = customer.Email;
            avCustomer.Phone = customer.Phone;
            avCustomer.Address = customer.Address;
            int r = cutx.SaveChanges();
            if (r > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Customer SearchCustomer(int customerId)
        {
            return cutx.Customers.Find(customerId);
        }
        public List<Customer> AllCustomerDetails()
        {
            return cutx.Customers.ToList();
        }
    }
}