using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class AuditMessageDetailDB
	{
		public static AuditMessageDetail GetItem(int auditmessagedetailId)
		{
			AuditMessageDetail auditmessagedetail = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAuditMessageDetailSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", auditmessagedetailId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						auditmessagedetail = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return auditmessagedetail;
		}

		public static AuditMessageDetailCollection GetList(AuditMessageDetailCriteria auditmessagedetailCriteria)
		{
			AuditMessageDetailCollection tempList = new AuditMessageDetailCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAuditMessageDetailSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "audit_message_id", auditmessagedetailCriteria.mAuditMessageId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "user_id", auditmessagedetailCriteria.mUserId);
				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new AuditMessageDetailCollection();
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

		public static int SelectCountForGetList(AuditMessageDetailCriteria auditmessagedetailCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAuditMessageDetailSearchList";

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

		public static int Save(AuditMessageDetail myAuditMessageDetail)
		{
			if (!myAuditMessageDetail.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a auditmessagedetail in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAuditMessageDetailInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@audit_message_id", myAuditMessageDetail.mAuditMessageId);
				Helpers.CreateParameter(myCommand, DbType.String, "@message", myAuditMessageDetail.mMessage);
				Helpers.CreateParameter(myCommand, DbType.String, "@status", myAuditMessageDetail.mStatus);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@user_id", myAuditMessageDetail.mUserId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@message_date", myAuditMessageDetail.mMessageDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myAuditMessageDetail.mDatestamp);

				Helpers.SetSaveParameters(myCommand, myAuditMessageDetail);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update auditmessagedetail as it has been updated by someone else");
				}
				//myAuditMessageDetail.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spAuditMessageDetailDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static AuditMessageDetail FillDataRecord(IDataRecord myDataRecord)
		{
			AuditMessageDetail auditmessagedetail = new AuditMessageDetail();

			auditmessagedetail.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			auditmessagedetail.mAuditMessageId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("audit_message_id"));
			auditmessagedetail.mMessage = myDataRecord.GetString(myDataRecord.GetOrdinal("message"));
			auditmessagedetail.mStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("status"));
			auditmessagedetail.mUserId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("user_id"));
			auditmessagedetail.mMessageDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("message_date"));
			auditmessagedetail.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
			auditmessagedetail.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return auditmessagedetail;
		}
	}
}