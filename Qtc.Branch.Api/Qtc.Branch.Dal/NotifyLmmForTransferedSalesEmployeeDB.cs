using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class NotifyLmmForTransferedSalesEmployeeDB
	{
		public static NotifyLmmForTransferedSalesEmployee GetItem(int notifylmmfortransferedsalesemployeeId)
		{
			NotifyLmmForTransferedSalesEmployee notifylmmfortransferedsalesemployee = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spNotifyLmmForTransferedSalesEmployeeSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", notifylmmfortransferedsalesemployeeId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						notifylmmfortransferedsalesemployee = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return notifylmmfortransferedsalesemployee;
		}

		public static NotifyLmmForTransferedSalesEmployeeCollection GetList(NotifyLmmForTransferedSalesEmployeeCriteria notifylmmfortransferedsalesemployeeCriteria)
		{
			NotifyLmmForTransferedSalesEmployeeCollection tempList = new NotifyLmmForTransferedSalesEmployeeCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spNotifyLmmForTransferedSalesEmployeeSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_from_id", notifylmmfortransferedsalesemployeeCriteria.mLmmFromId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_to_id", notifylmmfortransferedsalesemployeeCriteria.mLmmToId);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@start_date", notifylmmfortransferedsalesemployeeCriteria.mStartDate);
                Helpers.CreateParameter(myCommand, DbType.Date, "@end_date", notifylmmfortransferedsalesemployeeCriteria.mEndDate);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new NotifyLmmForTransferedSalesEmployeeCollection();
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

		public static int SelectCountForGetList(NotifyLmmForTransferedSalesEmployeeCriteria notifylmmfortransferedsalesemployeeCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spNotifyLmmForTransferedSalesEmployeeSearchList";

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

		public static int Save(NotifyLmmForTransferedSalesEmployee myNotifyLmmForTransferedSalesEmployee)
		{
			if (!myNotifyLmmForTransferedSalesEmployee.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a notifylmmfortransferedsalesemployee in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spNotifyLmmForTransferedSalesEmployeeInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_from_id", myNotifyLmmForTransferedSalesEmployee.mLmmFromId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_to_id", myNotifyLmmForTransferedSalesEmployee.mLmmToId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myNotifyLmmForTransferedSalesEmployee.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date", myNotifyLmmForTransferedSalesEmployee.mDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myNotifyLmmForTransferedSalesEmployee.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id_to", myNotifyLmmForTransferedSalesEmployee.mBranchIdTo);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myNotifyLmmForTransferedSalesEmployee.mRecordId);

				Helpers.SetSaveParameters(myCommand, myNotifyLmmForTransferedSalesEmployee);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update notifylmmfortransferedsalesemployee as it has been updated by someone else");
				}
				//myNotifyLmmForTransferedSalesEmployee.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spNotifyLmmForTransferedSalesEmployeeDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static NotifyLmmForTransferedSalesEmployee FillDataRecord(IDataRecord myDataRecord)
		{
			NotifyLmmForTransferedSalesEmployee notifylmmfortransferedsalesemployee = new NotifyLmmForTransferedSalesEmployee();

			notifylmmfortransferedsalesemployee.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			notifylmmfortransferedsalesemployee.mLmmFromId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_from_id"));
			notifylmmfortransferedsalesemployee.mLmmToId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_to_id"));
			notifylmmfortransferedsalesemployee.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			notifylmmfortransferedsalesemployee.mDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date"));
			notifylmmfortransferedsalesemployee.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			notifylmmfortransferedsalesemployee.mBranchIdTo = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id_to"));
			notifylmmfortransferedsalesemployee.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			notifylmmfortransferedsalesemployee.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));
            notifylmmfortransferedsalesemployee.mBranch = myDataRecord.GetString(myDataRecord.GetOrdinal("branch"));
            notifylmmfortransferedsalesemployee.mBranchTo = myDataRecord.GetString(myDataRecord.GetOrdinal("branch_to"));
            notifylmmfortransferedsalesemployee.mLmmFrom = myDataRecord.GetString(myDataRecord.GetOrdinal("lmm_from"));
            notifylmmfortransferedsalesemployee.mLmmTo = myDataRecord.GetString(myDataRecord.GetOrdinal("lmm_to"));
            notifylmmfortransferedsalesemployee.mEmployeeName = myDataRecord.GetString(myDataRecord.GetOrdinal("employee_name"));
			return notifylmmfortransferedsalesemployee;
		}
	}
}