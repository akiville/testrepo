using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class IbwDB
	{
		public static Ibw GetItem(int ibwId)
		{
			Ibw ibw = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spIbwSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", ibwId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						ibw = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return ibw;
		}

		public static IbwCollection GetList(IbwCriteria ibwCriteria)
		{
			IbwCollection tempList = new IbwCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spIbwSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", ibwCriteria.mBranchId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@to_branch_id", ibwCriteria.mToBranchId);
                
                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new IbwCollection();
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

		public static int SelectCountForGetList(IbwCriteria ibwCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spIbwSearchList";

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

		public static int Save(Ibw myIbw)
		{
			if (!myIbw.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a ibw in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spIbwInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@ibw_id", myIbw.mIbwId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date", myIbw.mDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@transaction_date", myIbw.mTransactionDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@number", myIbw.mNumber);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@planner_id", myIbw.mPlannerId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@mc_id", myIbw.mMcId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myIbw.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@to_branch_id", myIbw.mToBranchId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@requested_by_id", myIbw.mRequestedById);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@approved_by_id", myIbw.mApprovedById);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@reason_id", myIbw.mReasonId);
				Helpers.CreateParameter(myCommand, DbType.String, "@nov_no", myIbw.mNovNo);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myIbw.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@code_id", myIbw.mCodeId);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "@from_branch_accepted", myIbw.mFromBranchAccepted);

                Helpers.SetSaveParameters(myCommand, myIbw);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update ibw as it has been updated by someone else");
				}
				//myIbw.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spIbwDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static Ibw FillDataRecord(IDataRecord myDataRecord)
		{
			Ibw ibw = new Ibw();

			ibw.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			ibw.mIbwId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ibw_id"));
			ibw.mDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date"));
			ibw.mTransactionDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("transaction_date"));
			ibw.mNumber = myDataRecord.GetInt32(myDataRecord.GetOrdinal("number"));
			ibw.mPlannerId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("planner_id"));
			ibw.mMcId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("mc_id"));
			ibw.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			ibw.mToBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("to_branch_id"));
			ibw.mRequestedById = myDataRecord.GetInt32(myDataRecord.GetOrdinal("requested_by_id"));
			ibw.mApprovedById = myDataRecord.GetInt32(myDataRecord.GetOrdinal("approved_by_id"));
			ibw.mReasonId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("reason_id"));
			ibw.mNovNo = myDataRecord.GetString(myDataRecord.GetOrdinal("nov_no"));
			ibw.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			ibw.mCodeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("code_id"));
			ibw.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            ibw.mBranchName = myDataRecord.GetString(myDataRecord.GetOrdinal("branch_name"));
            ibw.mToBranchName = myDataRecord.GetString(myDataRecord.GetOrdinal("to_branch_name"));
            ibw.mFromBranchAccepted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("from_branch_accepted"));
            return ibw;
		}
	}
}