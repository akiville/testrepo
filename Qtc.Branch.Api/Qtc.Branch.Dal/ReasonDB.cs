using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class ReasonDB
	{
		public static Reason GetItem(int reasonId)
		{
			Reason reason = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spReasonSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", reasonId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						reason = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return reason;
		}

		public static ReasonCollection GetList(ReasonCriteria reasonCriteria)
		{
			ReasonCollection tempList = new ReasonCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spReasonSearchList";

                Helpers.CreateParameter(myCommand, DbType.Boolean, "repair", reasonCriteria.mRepair);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "logistics", reasonCriteria.mLogistics);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new ReasonCollection();
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

		public static int SelectCountForGetList(ReasonCriteria reasonCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spReasonSearchList";

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

		public static int Save(Reason myReason)
		{
			if (!myReason.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a reason in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spReasonInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.String, "@name", myReason.mName);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@form_id", myReason.mFormId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@rfd", myReason.mRfd);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@ncs", myReason.mNcs);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@inventory", myReason.mInventory);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@logistics", myReason.mLogistics);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@repair", myReason.mRepair);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@activity", myReason.mActivity);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@ddir", myReason.mDdir);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@ordering", myReason.mOrdering);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@ddir_no", myReason.mDdirNo);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@rp_no", myReason.mRpNo);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@mc_bag", myReason.mMcBag);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@loe", myReason.mLoe);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@rma", myReason.mRma);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@ods", myReason.mOds);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@ddd", myReason.mDdd);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@ddd_required_incident", myReason.mDddRequiredIncident);

				Helpers.SetSaveParameters(myCommand, myReason);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update reason as it has been updated by someone else");
				}
				//myReason.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "ptApi_spReasonDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static Reason FillDataRecord(IDataRecord myDataRecord)
		{
			Reason reason = new Reason();

			reason.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			reason.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));
			reason.mFormId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("form_id"));
			reason.mRfd = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("rfd"));
			reason.mNcs = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("ncs"));
			reason.mInventory = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("inventory"));
			reason.mLogistics = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("logistics"));
			reason.mRepair = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("repair"));
			reason.mActivity = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("activity"));
			reason.mDdir = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("ddir"));
			reason.mOrdering = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ordering"));
			reason.mDdirNo = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("ddir_no"));
			reason.mRpNo = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("rp_no"));
			reason.mMcBag = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("mc_bag"));
			reason.mLoe = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("loe"));
			reason.mRma = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("rma"));
			reason.mOds = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("ods"));
			reason.mDdd = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("ddd"));
			reason.mDddRequiredIncident = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("ddd_required_incident"));
			reason.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return reason;
		}
	}
}