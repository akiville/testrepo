using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class LmmDB
	{
		public static Lmm GetItem(int lmmId)
		{
			Lmm lmm = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLmmSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", lmmId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						lmm = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return lmm;
		}

		public static LmmCollection GetList(LmmCriteria lmmCriteria)
		{
			LmmCollection tempList = new LmmCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLmmSearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new LmmCollection();
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

		public static int SelectCountForGetList(LmmCriteria lmmCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLmmSearchList";

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

		public static int Save(Lmm myLmm)
		{
			if (!myLmm.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a lmm in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLmmInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myLmm.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", myLmm.mLmmId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@sales_date", myLmm.mSalesDate);

				Helpers.SetSaveParameters(myCommand, myLmm);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update lmm as it has been updated by someone else");
				}
				//myLmm.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spLmmDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static Lmm FillDataRecord(IDataRecord myDataRecord)
		{
			Lmm lmm = new Lmm();

			lmm.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			lmm.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			lmm.mLmmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_id"));
			lmm.mSalesDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("sales_date"));
			lmm.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return lmm;
		}
	}
}