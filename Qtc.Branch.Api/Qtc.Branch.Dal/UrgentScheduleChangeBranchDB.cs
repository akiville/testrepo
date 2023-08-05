using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class UrgentScheduleChangeBranchDB
	{
		public static UrgentScheduleChangeBranch GetItem(int urgentschedulechangebranchId)
		{
			UrgentScheduleChangeBranch urgentschedulechangebranch = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spUrgentScheduleChangeBranchSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", urgentschedulechangebranchId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						urgentschedulechangebranch = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return urgentschedulechangebranch;
		}

		public static UrgentScheduleChangeBranchCollection GetList(UrgentScheduleChangeBranchCriteria urgentschedulechangebranchCriteria)
		{
			UrgentScheduleChangeBranchCollection tempList = new UrgentScheduleChangeBranchCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spUrgentScheduleChangeBranchSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@urgent_schedule_change_id", urgentschedulechangebranchCriteria.mUrgentScheduleChangeId);
				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new UrgentScheduleChangeBranchCollection();
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

		public static int SelectCountForGetList(UrgentScheduleChangeBranchCriteria urgentschedulechangebranchCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spUrgentScheduleChangeBranchSearchList";

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

		public static int Save(UrgentScheduleChangeBranch myUrgentScheduleChangeBranch)
		{
			if (!myUrgentScheduleChangeBranch.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a urgentschedulechangebranch in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spUrgentScheduleChangeBranchInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@urgent_schedule_change_id", myUrgentScheduleChangeBranch.mUrgentScheduleChangeId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myUrgentScheduleChangeBranch.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@user_id", myUrgentScheduleChangeBranch.mUserId);

				Helpers.SetSaveParameters(myCommand, myUrgentScheduleChangeBranch);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update urgentschedulechangebranch as it has been updated by someone else");
				}
				//myUrgentScheduleChangeBranch.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spUrgentScheduleChangeBranchDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static UrgentScheduleChangeBranch FillDataRecord(IDataRecord myDataRecord)
		{
			UrgentScheduleChangeBranch urgentschedulechangebranch = new UrgentScheduleChangeBranch();

			urgentschedulechangebranch.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			urgentschedulechangebranch.mUrgentScheduleChangeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("urgent_schedule_change_id"));
			urgentschedulechangebranch.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			urgentschedulechangebranch.mUserId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("user_id"));
			urgentschedulechangebranch.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            urgentschedulechangebranch.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));

			return urgentschedulechangebranch;
		}
	}
}