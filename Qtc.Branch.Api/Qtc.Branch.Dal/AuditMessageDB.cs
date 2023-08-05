using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class AuditMessageDB
	{
		public static AuditMessage GetItem(int auditmessageId)
		{
			AuditMessage auditmessage = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAuditMessageSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", auditmessageId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						auditmessage = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return auditmessage;
		}

		public static AuditMessageCollection GetList(AuditMessageCriteria auditmessageCriteria)
		{
			AuditMessageCollection tempList = new AuditMessageCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAuditMessageSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "user_id", auditmessageCriteria.mUserId);
				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new AuditMessageCollection();
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

		public static int SelectCountForGetList(AuditMessageCriteria auditmessageCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAuditMessageSearchList";

				DbParameter idParam = myCommand.CreateParameter();
				idParam.DbType = DbType.Int32;
				idParam.Direction = ParameterDirection.InputOutput;
				idParam.ParameterName = "@record_count";
				idParam.Value = 0;
				myCommand.Parameters.Add(idParam);

				myCommand.Connection.Open();
				myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();

				return (int)myCommand.Parameters["@record_count"].Value;
			}
		}

		public static int Save(AuditMessage myAuditMessage)
		{
			if (!myAuditMessage.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a auditmessage in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAuditMessageInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.String, "@message", myAuditMessage.mMessage);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myAuditMessage.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_created", myAuditMessage.mDateCreated);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@user_id", myAuditMessage.mUserId);
				Helpers.CreateParameter(myCommand, DbType.String, "@status", myAuditMessage.mStatus);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myAuditMessage.mDatestamp);

				Helpers.SetSaveParameters(myCommand, myAuditMessage);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update auditmessage as it has been updated by someone else");
				}
				//myAuditMessage.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
				result = Helpers.GetBusinessBaseId(myCommand);
				myCommand.Connection.Close();
			}
			return result;
		}

		public static bool Delete(int id)
		{
			int result = 0;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAuditMessageDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static AuditMessage FillDataRecord(IDataRecord myDataRecord)
		{
			AuditMessage auditmessage = new AuditMessage();

			auditmessage.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			auditmessage.mMessage = myDataRecord.GetString(myDataRecord.GetOrdinal("message"));
			auditmessage.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			auditmessage.mDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_created"));
			auditmessage.mUserId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("user_id"));
			auditmessage.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			auditmessage.mStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("status"));
			auditmessage.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));

			return auditmessage;
		}
	}
}