using System;

namespace aautomation_framework.Utility.Helpers.Db.Repos
{
    public class CustomerRepo
    {
        private readonly string connectionString;
        private readonly string dataBaseName;

        public CustomerRepo(string connectionString, string dataBaseName)
        {
            this.connectionString = connectionString;
            this.dataBaseName = dataBaseName;
        }

        /// <summary>
        /// Method for deleting customers in MC or MC Amgen consists of two parts: deleting from customers and 'mc_user' database
        /// After deleting customers from already connected (customer) DB - reconnecting to the 'mc_user' DB and customers deletion takes place from there
        /// </summary>
        /// <param name="customerName"></param>
        /// <returns>Number of affected rows from mc_customer DB</returns>
        public int DeleteCustomersByName(string customerName)
        {
            int rowsAffected;
            using (PostgreSqlDbClient dbc = new PostgreSqlDbClient(connectionString, this.dataBaseName))
            {
                rowsAffected = dbc.ExecuteModifyQuery(CustomerDeleteQueryFromCustomerTable(customerName));
            }

            using (PostgreSqlDbClient dbc = new PostgreSqlDbClient(connectionString, "mc_user"))
            {
                dbc.ExecuteModifyQuery(CustomerDeleteQueryFromUserTable(customerName));
            }

            return rowsAffected;
        }

        /// <summary>
        /// Method to retrieve customer id by customer's name
        /// </summary>
        /// <param name="customerName"></param>
        /// <returns>Customer id</returns>
        public string FindIdByName(string customerName)
        {
            string id = null;
            using (PostgreSqlDbClient dbc = new PostgreSqlDbClient(connectionString, this.dataBaseName))
            {
                var result = dbc.ExecuteSelectQuery($"SELECT * FROM CUSTOMERS WHERE NAME = '{customerName}'");
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    if (DataTableHelper.ReadData(result, i, "name").Contains(customerName))
                    {
                        id = DataTableHelper.ReadData(result, i, "id");
                        break;
                    }
                }
            }

            return id ?? throw new ArgumentNullException("Id is null");
        }

        #region Queries

        /// <summary>
        /// Method to get transaction query for connected customer database
        /// </summary>
        /// <param name="customerName"></param>
        /// <returns>Transaction query concatenated with customer name user would like to delete</returns>
        private string CustomerDeleteQueryFromCustomerTable(string customerName)
        {
            return $@"

            delete from payment_methods where customer_id in (select id from customers where name like '{customerName}%');

            delete from staging_plans where customer_id in (select id from customers where name like '{customerName}%');

            delete from staging_plans_uploads where customer_id in (select id from customers where name like '{customerName}%');

            delete from plans where customer_id in (select id from customers where name like '{customerName}%');

            delete from customers where name like '{customerName}%';";
        }

        /// <summary>
        /// Method to get transaction query for users database
        /// </summary>
        /// <param name="customerName"></param>
        /// <returns>Transaction query concatenated with customer name user would like to delete</returns>
        private string CustomerDeleteQueryFromUserTable(string customerName)
        {
            return $@"

            delete from assignments 
            where client_customer_id in (
            select client_customer_id 
            from assignments as a 
            left join client_customer as cc on a.client_customer_id = cc.id
            join customers as c on cc.customer_id  = c.id
            where c.name like '{customerName}%'
            );
    
            delete from availability
            where client_customer_id in (
            select client_customer_id 
            from availability as a 
            left join client_customer as cc on a.client_customer_id = cc.id
            join customers as c on cc.customer_id  = c.id
            where c.name like '{customerName}%'
            );

            delete from client_customer 
            where customer_id in (
            select id from customers where name like '{customerName}%'
            );

            delete from customers where name like '{customerName}%';";
        }

        #endregion
    }
}

