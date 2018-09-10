using System;
using System.Data;
using System.Data.Common;

using Npgsql;

namespace aautomation_framework.Utility.Helpers.Db.Repos
{
    public class PostgreSqlDbClient : IDisposable
    {
        private NpgsqlConnection connection;

        /// <summary>
        /// Connection string used to connect to the database in case user wants to reconnect to different DB with connection parameters used previously
        /// </summary>
        public string ServerOnlyСonnectionString { get; }
        public string FullСonnectionString { get; }

        /// <summary>
        /// Constructor for creation and opening connection to the specified database. Has to be put in 'using' block.
        /// </summary>
        /// <param name="connString">Full connection string with parameters: Server, Port, User ID, Password</param>
        /// <param name="dbName">Data base name user wants to connect to</param>
        public PostgreSqlDbClient(string connString, string dbName)
        {
            DbConnectionStringBuilder builder = new
                DbConnectionStringBuilder();
            this.ServerOnlyСonnectionString = connString;
            builder.ConnectionString = ServerOnlyСonnectionString;
            builder["Database"] = dbName;
            this.FullСonnectionString = builder.ConnectionString;
            connection = new NpgsqlConnection(FullСonnectionString);
            connection.Open();
            LogUtil.WriteDebug("Connection is open");
            LogUtil.WriteDebug($"Connection string is: {FullСonnectionString}");
        }

        /// <summary>
        /// General method for executing queries to modify database content
        /// </summary>
        /// <param name="query">SQL query that will be executed</param>
        /// <returns>Number of affected rows after execution of the query</returns>
        public int ExecuteModifyQuery(string query)
        {
            using (var command = new NpgsqlCommand(query, connection))
            {
                int affectedRows = 0;
                NpgsqlTransaction transaction;
                transaction = connection.BeginTransaction();
                try
                {
                    affectedRows = command.ExecuteNonQuery();
                    transaction.Commit();
                    return affectedRows;
                }
                catch (Exception ex)
                {
                    LogUtil.WriteDebug(ex.GetType().FullName + ":" + ex.Message);
                    try
                    {
                        transaction.Rollback();
                        return affectedRows;
                    }
                    catch (Exception e)
                    {
                        LogUtil.WriteDebug($"Commit Exception Type: {e.GetType()} : {e.Message}");
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// General method for execution queries to select database content
        /// </summary>
        /// <param name="query">SQL query that will be executed</param>
        /// <returns>Results after execution of the query in DataTable type</returns>
        public DataTable ExecuteSelectQuery(string query)
        {
            DataTable results = new DataTable();
            using (var command = new NpgsqlCommand(query, connection))
            {
                try
                {
                    results.Load(command.ExecuteReader());
                    return results;
                }
                catch (Exception e)
                {
                    LogUtil.WriteDebug($"Commit Exception Type: {e.GetType()} : {e.Message}");
                    throw;
                }
            }
        }

        /// <summary>
        /// Method to close database connection. Will be executed in 'using' block.
        /// </summary>
        public void Dispose()
        {
            connection.Close();
            LogUtil.WriteDebug("Connection is closed");
        }
    }
}
