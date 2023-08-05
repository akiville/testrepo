using Qtc.Branch.BusinessEntities;
using System.Data;
using System.Data.Common;
using System;
namespace Qtc.Branch.Dal
{
    public class AuditDB
    {
        public static string GetNewNumber(string tableName)
        {
            string value = "";
            using (DbCommand myCommand = AppConfiguration.CreateCommand())
            {
                myCommand.CommandType = CommandType.Text;
                myCommand.CommandText = "SELECT ISNULL(MAX(CAST(number AS INT)), 0) + 1 FROM " + tableName + " WHERE disable = 0";

                myCommand.Connection.Open();
                value = Convert.ToString(myCommand.ExecuteScalar());
                myCommand.Connection.Close();
            }
            return value;
        }

        public static DateTime GetDateToday()
        {
            DateTime value = DateTime.Now.Date;
            using (DbCommand myCommand = AppConfiguration.CreateCommand())
            {
                myCommand.CommandType = CommandType.Text;
                myCommand.CommandText = "SELECT GETDATE()";

                myCommand.Connection.Open();
                value = Convert.ToDateTime(myCommand.ExecuteScalar());
                myCommand.Connection.Close();
            }
            return value;
        }

        public static AuditCollection GetList(AuditCriteria auditCriteria)
        {
            AuditCollection tempList = new AuditCollection();

            using (DbCommand myCommand = AppConfiguration.CreateCommandLog())
            {
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "ptApi_spAuditSearchList";

                if (auditCriteria.mStartDate != DateTime.MinValue)
                    Helpers.CreateParameter(myCommand, DbType.DateTime, "@start_date", auditCriteria.mStartDate);

                if (auditCriteria.mEndDate != DateTime.MinValue)
                    Helpers.CreateParameter(myCommand, DbType.DateTime, "@end_date", auditCriteria.mEndDate);

                myCommand.Connection.Open();
                using (DbDataReader myReader = myCommand.ExecuteReader())
                {
                    if (myReader.HasRows)
                    {
                        tempList = new AuditCollection();
                        while (myReader.Read())
                        {
                            tempList.Add(FillDataRecord(myReader));
                        }

                        myReader.Close();
                    }

                }
                myCommand.Connection.Close();
            }

            return tempList;
        }

       
        public static void Save(BusinessEntities.Audit audit)
        {
            using (DbCommand myCommand = AppConfiguration.CreateCommandLog())
            {
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "ptApi_spLogInsertSingleItem";

                Helpers.CreateParameter(myCommand, DbType.String, "@user_name", "");
                Helpers.CreateParameter(myCommand, DbType.Int16, "@table_id", audit.mTableId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@row_id", audit.mRowId);
                Helpers.CreateParameter(myCommand, DbType.Byte, "@action", audit.mAction);
                Helpers.CreateParameter(myCommand, DbType.String, "@field", audit.mField);
                Helpers.CreateParameter(myCommand, DbType.String, "@old_value", String.IsNullOrEmpty(audit.mOldValue) ? "" : audit.mOldValue);
                Helpers.CreateParameter(myCommand, DbType.String, "@new_value", String.IsNullOrEmpty(audit.mNewValue) ? "" : audit.mNewValue);
                /*Helpers.CreateParameter(myCommand, DbType.Int32, "@user_id", audit.mUserId);*/
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_time", DateTime.Now);
                Helpers.CreateParameter(myCommand, DbType.String, "@description", audit.mDescription);
                Helpers.CreateParameter(myCommand, DbType.Int16, "@is_sub_item", audit.mIsSubItem);
                
                //Helpers.CreateParameter(myCommand, DbType.String, )
                myCommand.Connection.Open();

                myCommand.ExecuteNonQuery();

                myCommand.Connection.Close();
            }

        }

        private static BusinessEntities.Audit FillDataRecord(IDataRecord myDataRecord)
        {
            BusinessEntities.Audit audit = new BusinessEntities.Audit();

            audit.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
            audit.mUserFullName = myDataRecord.GetString(myDataRecord.GetOrdinal("user_name"));
            audit.mTableId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("table_id"));
            audit.mRowId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("row_id"));
            audit.mAction = myDataRecord.GetInt32(myDataRecord.GetOrdinal("action"));
            audit.mField = myDataRecord.GetString(myDataRecord.GetOrdinal("field"));
            audit.mOldValue = myDataRecord.GetString(myDataRecord.GetOrdinal("old_value"));
            audit.mNewValue = myDataRecord.GetString(myDataRecord.GetOrdinal("new_value"));
            audit.mDateTime = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_time"));
            if(myDataRecord["description"] != DBNull.Value)
                audit.mDescription = myDataRecord.GetString(myDataRecord.GetOrdinal("description"));

            audit.mIsSubItem = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_sub_item")); 

            audit.mModule = myDataRecord.GetString(myDataRecord.GetOrdinal("module"));
            audit.mActionDescription = myDataRecord.GetString(myDataRecord.GetOrdinal("action_description"));
            audit.mDateString = audit.mDateTime.ToLongDateString() + " " + audit.mDateTime.ToLongTimeString();

            return audit;
        }

        public static void BackUpDatabase(string script, string path)
        {

            using (DbCommand myCommand = AppConfiguration.CreateCommand())
            {
                myCommand.CommandType = CommandType.Text;
                myCommand.CommandText = script;

                Helpers.CreateParameter(myCommand, DbType.String, "@path", path);
                Helpers.CreateParameter(myCommand, DbType.String, "@path2", path.Replace(".bak","log.bak"));

                myCommand.Connection.Open();

                myCommand.ExecuteNonQuery();

                myCommand.Connection.Close();
            }

        }
    }
}
