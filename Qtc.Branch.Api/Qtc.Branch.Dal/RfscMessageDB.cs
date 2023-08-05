using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class RfscMessageDB
	{
		public static RfscMessage GetItem(int rfscmessageId)
		{
			RfscMessage rfscmessage = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRfscMessageSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", rfscmessageId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						rfscmessage = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return rfscmessage;
		}

		public static RfscMessageCollection GetList(RfscMessageCriteria rfscmessageCriteria)
		{
			RfscMessageCollection tempList = new RfscMessageCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRfscMessageSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "to_lmm_id", rfscmessageCriteria.mToLmmId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "lmm_id", rfscmessageCriteria.mLmmId);
                Helpers.CreateParameter(myCommand, DbType.String, "status", rfscmessageCriteria.mStatus);
                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new RfscMessageCollection();
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

		public static int SelectCountForGetList(RfscMessageCriteria rfscmessageCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRfscMessageSearchList";

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

		public static int Save(RfscMessage myRfscMessage)
		{
			if (!myRfscMessage.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a rfscmessage in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRfscMessageInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@rfsc_id", myRfscMessage.mRfscId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", myRfscMessage.mLmmId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myRfscMessage.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@start_date", myRfscMessage.mStartDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@end_date", myRfscMessage.mEndDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@to_lmm_id", myRfscMessage.mToLmmId);
				Helpers.CreateParameter(myCommand, DbType.String, "@status", myRfscMessage.mStatus);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@personnel_id", myRfscMessage.mPersonnelId);
				Helpers.CreateParameter(myCommand, DbType.String, "@personnel_name", myRfscMessage.mPersonnelName);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datetime", myRfscMessage.mDatetime);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@affected_branch_id", myRfscMessage.mAffectedBranchId);
				Helpers.SetSaveParameters(myCommand, myRfscMessage);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update rfscmessage as it has been updated by someone else");
				}
				//myRfscMessage.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spRfscMessageDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static RfscMessage FillDataRecord(IDataRecord myDataRecord)
		{
			RfscMessage rfscmessage = new RfscMessage();

			rfscmessage.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			rfscmessage.mRfscId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("rfsc_id"));
			rfscmessage.mLmmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_id"));
			rfscmessage.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			rfscmessage.mStartDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("start_date"));
			rfscmessage.mEndDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("end_date"));
			rfscmessage.mToLmmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("to_lmm_id"));
			rfscmessage.mStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("status"));
			rfscmessage.mPersonnelId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("personnel_id"));
			rfscmessage.mPersonnelName = myDataRecord.GetString(myDataRecord.GetOrdinal("personnel_name"));
			rfscmessage.mDatetime = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datetime"));
			rfscmessage.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            rfscmessage.mToLmmName = myDataRecord.GetString(myDataRecord.GetOrdinal("to_lmm_name"));
            rfscmessage.mLmmName = myDataRecord.GetString(myDataRecord.GetOrdinal("lmm_name"));
            rfscmessage.mBranchName = myDataRecord.GetString(myDataRecord.GetOrdinal("branch_name"));
            rfscmessage.mAffectedBranchName = myDataRecord.GetString(myDataRecord.GetOrdinal("affected_branch_name"));
            rfscmessage.mDateFiled = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_filed"));

            return rfscmessage;
		}
	}
}