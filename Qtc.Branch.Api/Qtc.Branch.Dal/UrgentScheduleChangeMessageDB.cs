using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class UrgentScheduleChangeMessageDB
	{
		public static UrgentScheduleChangeMessage GetItem(int urgentschedulechangemessageId)
		{
			UrgentScheduleChangeMessage urgentschedulechangemessage = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spUrgentScheduleChangeMessageSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", urgentschedulechangemessageId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						urgentschedulechangemessage = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return urgentschedulechangemessage;
		}

        public static UrgentScheduleChangeMessageCollection GetListByScheduleId(UrgentScheduleChangeMessageCriteria urgentschedulechangemessageCriteria)
        {
            UrgentScheduleChangeMessageCollection tempList = new UrgentScheduleChangeMessageCollection();
            using (DbCommand myCommand = AppConfiguration.CreateCommand())
            {
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Qt_spUrgentScheduleChangeMessageSearchListByURSCId";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@urgent_schedule_change_id", urgentschedulechangemessageCriteria.mToLmmId);

                myCommand.Connection.Open();
                using (DbDataReader myReader = myCommand.ExecuteReader())
                {
                    if (myReader.HasRows)
                    {
                        tempList = new UrgentScheduleChangeMessageCollection();
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

        public static UrgentScheduleChangeMessageCollection GetList(UrgentScheduleChangeMessageCriteria urgentschedulechangemessageCriteria)
		{
			UrgentScheduleChangeMessageCollection tempList = new UrgentScheduleChangeMessageCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spUrgentScheduleChangeMessageSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@to_lmm_id", urgentschedulechangemessageCriteria.mToLmmId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", urgentschedulechangemessageCriteria.mLmmId);
                Helpers.CreateParameter(myCommand, DbType.String, "@status", urgentschedulechangemessageCriteria.mStatus);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new UrgentScheduleChangeMessageCollection();
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

		public static int SelectCountForGetList(UrgentScheduleChangeMessageCriteria urgentschedulechangemessageCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spUrgentScheduleChangeMessageSearchList";

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

		public static int Save(UrgentScheduleChangeMessage myUrgentScheduleChangeMessage)
		{
			if (!myUrgentScheduleChangeMessage.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a urgentschedulechangemessage in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spUrgentScheduleChangeMessageInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@urgent_schedule_change_id", myUrgentScheduleChangeMessage.mUrgentScheduleChangeId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", myUrgentScheduleChangeMessage.mLmmId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myUrgentScheduleChangeMessage.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@start_date", myUrgentScheduleChangeMessage.mStartDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@end_date", myUrgentScheduleChangeMessage.mEndDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@to_lmm_id", myUrgentScheduleChangeMessage.mToLmmId);
				Helpers.CreateParameter(myCommand, DbType.String, "@status", myUrgentScheduleChangeMessage.mStatus);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@personnel_id", myUrgentScheduleChangeMessage.mPersonnelId);
				Helpers.CreateParameter(myCommand, DbType.String, "@personnel_name", myUrgentScheduleChangeMessage.mPersonnelName);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datetime", myUrgentScheduleChangeMessage.mDatetime);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@affected_branch_id", myUrgentScheduleChangeMessage.mAffectedBranchId);
              
				Helpers.SetSaveParameters(myCommand, myUrgentScheduleChangeMessage);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update urgentschedulechangemessage as it has been updated by someone else");
				}
				//myUrgentScheduleChangeMessage.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spUrgentScheduleChangeMessageDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static UrgentScheduleChangeMessage FillDataRecord(IDataRecord myDataRecord)
		{
			UrgentScheduleChangeMessage urgentschedulechangemessage = new UrgentScheduleChangeMessage();

			urgentschedulechangemessage.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			urgentschedulechangemessage.mUrgentScheduleChangeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("urgent_schedule_change_id"));
			urgentschedulechangemessage.mLmmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_id"));
			urgentschedulechangemessage.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			urgentschedulechangemessage.mStartDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("start_date"));
			urgentschedulechangemessage.mEndDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("end_date"));
			urgentschedulechangemessage.mToLmmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("to_lmm_id"));
			urgentschedulechangemessage.mStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("status"));
			urgentschedulechangemessage.mPersonnelId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("personnel_id"));
			urgentschedulechangemessage.mPersonnelName = myDataRecord.GetString(myDataRecord.GetOrdinal("personnel_name"));
			urgentschedulechangemessage.mDatetime = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datetime"));
			urgentschedulechangemessage.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            urgentschedulechangemessage.mToLmmName = myDataRecord.GetString(myDataRecord.GetOrdinal("to_lmm_name"));
            urgentschedulechangemessage.mLmmName = myDataRecord.GetString(myDataRecord.GetOrdinal("lmm_name"));
            urgentschedulechangemessage.mAffectedBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("affected_branch_id"));
            urgentschedulechangemessage.mAffectedBranchName = myDataRecord.GetString(myDataRecord.GetOrdinal("affected_branch_name"));
            urgentschedulechangemessage.mBranchName = myDataRecord.GetString(myDataRecord.GetOrdinal("branch_name"));
            urgentschedulechangemessage.mDateFiled = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_filed"));
            urgentschedulechangemessage.mConcernType = myDataRecord.GetString(myDataRecord.GetOrdinal("concern_type"));
            return urgentschedulechangemessage;
		}
	}
}