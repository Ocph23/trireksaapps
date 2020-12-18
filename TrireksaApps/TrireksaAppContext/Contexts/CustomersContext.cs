using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrireksaAppContext.Models;
using MySql.Data.MySqlClient;

namespace TrireksaAppContext
{


    public class CustomersContext
    {

        //SignalR
        private readonly ApplicationDbContext db;

        public CustomersContext(ApplicationDbContext _db)
        {
            db = _db;
        }

        public Task<IEnumerable<Customer>> Get()
        {
            return Task.FromResult(db.Customer.AsEnumerable());
        }
        public Task<Customer> Get(int id)
        {
            return Task.FromResult(db.Customer.Where(x => x.Id == id).FirstOrDefault());
        }

        // POST: api/customers
        public async Task<Customer> Post(Customer value)
        {

            try
            {
                db.Customer.Add(value);
                var result = await db.SaveChangesAsync();
                if(result<=0)
                    throw new SystemException(MessageCollection.Message(MessageType.SaveFail));
                return value;
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }



        }

        // PUT: api/customers/5

        public async Task<Customer> Put(int id, Customer value)
        {
            try
            {
                var existsData = db.Customer.Where(x => x.Id == id).FirstOrDefault();
                if (existsData == null)
                    throw new SystemException("Data Not Found !");

                db.Entry(value).CurrentValues.SetValues(value);
               var result = await db.SaveChangesAsync();

                if (result <= 0)
                    throw new SystemException("Data Not Saved !");

                return value;

            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var existsData =await Get(id);
                if (existsData == null)
                    throw new SystemException("Data Not Found !");
                db.Customer.Remove(existsData);
                var result = await db.SaveChangesAsync();

                if (result <= 0)
                    throw new SystemException("Data Not Saved !");
                return true;
            }
            catch(Exception ex){

                if(ex.InnerException!=null && ex.GetType()==typeof(MySqlException)){
                    MySqlException mysqlEx = (MySqlException)ex.InnerException;
                    if(mysqlEx.Number==1451)
                        throw new SystemException("Sorry, Data Have  Transaction !");
                    else
                        throw new SystemException("Sorry, Try Againt or Contact Your Administrator !");
                }
                throw new SystemException(ex.Message);
            }
        }
    }
}
