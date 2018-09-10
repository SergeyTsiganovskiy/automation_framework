using System.Data;

namespace aautomation_framework.Utility.Helpers.Db.Repos
{
    public static class DataTableHelper
    {
        public static string ReadData(this DataTable table, int rowNumber, string columnName)
        {
            return table.Rows[rowNumber][columnName].ToString();
        }
    }
}
