
namespace aautomation_framework.Utility.Helpers.Db.Repos
{
    public class ProjectRepo
    {
        private readonly string connectionString;
        private readonly string clientName;

        public ProjectRepo(string connectionString, string clientName)
        {
            this.connectionString = connectionString;
            this.clientName = clientName;
        }

        /// <summary>
        /// Method to delete projects
        /// </summary>
        /// <param name="projectId">Project id to delete</param>
        public void DeleteProjectById(string projectId)
        {
            using (PostgreSqlDbClient dbcMcInvoices = new PostgreSqlDbClient(connectionString, $"mc_invoices_{clientName}"))
            {
                dbcMcInvoices.ExecuteModifyQuery(GetQueryToDeleteProjectFromMCInvoices(projectId));
            }

            using (PostgreSqlDbClient dbcMcSummary = new PostgreSqlDbClient(connectionString, $"mc_summary_{clientName}"))
            {
                dbcMcSummary.ExecuteModifyQuery(GetQueryToDeleteProjectFromMCSummary(projectId));
            }

            using (PostgreSqlDbClient dbcMcValidations = new PostgreSqlDbClient(connectionString, $"mc_validations_{clientName}"))
            {
                dbcMcValidations.ExecuteModifyQuery(GetQueryToDeleteProjectFromMCValidations(projectId));
            }

            using (PostgreSqlDbClient dbcMcCalculations = new PostgreSqlDbClient(connectionString, $"mc_calculations_{clientName}"))
            {
                dbcMcCalculations.ExecuteModifyQuery(GetQueryToDeleteProjectFromMCCalculations(projectId));

            }
        }

        #region Queries

        private string GetQueryToDeleteProjectFromMCInvoices(string projectId)
        {
            return $@"

            delete from rebate_summary where project_id = '{projectId}';
            delete from invoice_scripts where invoice_id = '{projectId}';
            delete from invoice_operation_log where invoice_id = '{projectId}';
            delete from invoice_load where invoice_id = '{projectId}';
            delete from submissions where invoice_id = '{projectId}';
            delete from invoice where invoice_id = '{projectId}';";
        }

        private string GetQueryToDeleteProjectFromMCSummary(string projectId)
        {
            return $@"

            delete from submission where invoice_id = '{projectId}';
            delete from invoice where invoice_id = '{projectId}';";
        }

        private string GetQueryToDeleteProjectFromMCValidations(string projectId)
        {
            return $@"

            delete from submissions where invoice_id = '{projectId}';
            delete from invoice where invoice_id = '{projectId}';";
        }

        private string GetQueryToDeleteProjectFromMCCalculations(string projectId)
        {
            return $@"

            delete from submissions where invoice_id = '{projectId}';
            delete from invoices where id = '{projectId}';";
        }

        #endregion
    }
}

