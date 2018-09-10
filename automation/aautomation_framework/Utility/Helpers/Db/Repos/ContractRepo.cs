
namespace aautomation_framework.Utility.Helpers.Db.Repos
{
    public class ContractRepo
    {
        private readonly string connectionString;
        private readonly string dataBaseName;

        public ContractRepo(string connectionString, string dataBaseName)
        {
            this.connectionString = connectionString;
            this.dataBaseName = dataBaseName;
        }

        /// <summary>
        /// Method to delete active contracts
        /// </summary>
        /// <param name="contractName">Contract name to delete</param>
        /// <returns>Number of affected rows</returns>
        public int DeleteActiveContractByName(string contractName)
        {
            using (PostgreSqlDbClient dbc = new PostgreSqlDbClient(connectionString, this.dataBaseName))
            {
                return dbc.ExecuteModifyQuery(ActiveContractDeleteQuery(contractName));
            }
        }

        /// <summary>
        /// Method to delete draft contracts
        /// </summary>
        /// <param name="contractName">Contract name to delete</param>
        /// <returns>Number of affected rows</returns>
        public int DeleteDraftContractByName(string contractName)
        {
            using (PostgreSqlDbClient dbc = new PostgreSqlDbClient(connectionString, this.dataBaseName))
            {
                return dbc.ExecuteModifyQuery(DraftContractDeleteQuery(contractName));
            }
        }

        #region Queries

        private string ActiveContractDeleteQuery(string contractName)
        {
            return $@"

            delete from contract_bundles where contract_id in (select contract_id from contracts where contract_name like '{contractName}%');
            delete from contract_notes where contract_id in (select contract_id from contracts where contract_name like '{contractName}%');
            delete from contract_products where contract_id in (select contract_id from contracts where contract_name like '{contractName}%');
            delete from contract_rebates where contract_id in (select contract_id from contracts where contract_name like '{contractName}%');
            delete from contract_tiers where contract_id in (select contract_id from contracts where contract_name like '{contractName}%');
            delete from contracts where contract_name like '{contractName}%';";
        }

        private string DraftContractDeleteQuery(string contractName)
        {
            return $@"

            delete from staging_contract_products where contract_id in (select contract_id from contracts where contract_name like '{contractName}%');
            delete from staging_contract_rebates where contract_id in (select contract_id from contracts where contract_name like '{contractName}%');
            delete from staging_contract_tiers where contract_id in (select contract_id from contracts where contract_name like '{contractName}%');
            delete from staging_contracts where contract_name like '{contractName}%';";
        }

        #endregion
    }
}

