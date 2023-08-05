using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class DailySalesSummaryDB
	{
		public static DailySalesSummary GetItem(int dailysalessummaryId)
		{
			DailySalesSummary dailysalessummary = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDailySalesSummarySelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", dailysalessummaryId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						dailysalessummary = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return dailysalessummary;
		}

		public static DailySalesSummaryCollection GetList(DailySalesSummaryCriteria dailysalessummaryCriteria)
		{
			DailySalesSummaryCollection tempList = new DailySalesSummaryCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDailySalesSummarySearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", dailysalessummaryCriteria.mBranchId);
                Helpers.CreateParameter(myCommand, DbType.Date, "@inventory_date", dailysalessummaryCriteria.mInventoryDate);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@id", dailysalessummaryCriteria.mId);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new DailySalesSummaryCollection();
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

		public static int SelectCountForGetList(DailySalesSummaryCriteria dailysalessummaryCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDailySalesSummarySearchList";

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

		public static int Save(DailySalesSummary myDailySalesSummary)
		{
			if (!myDailySalesSummary.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a dailysalessummary in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDailySalesSummaryInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.DateTime, "@inventory_date", myDailySalesSummary.mInventoryDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myDailySalesSummary.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@user_id", myDailySalesSummary.mUserId);
				Helpers.CreateParameter(myCommand, DbType.String, "@cash_explanation", myDailySalesSummary.mCashExplanation);
				Helpers.CreateParameter(myCommand, DbType.String, "@inventory_explanation", myDailySalesSummary.mInventoryExplanation);
                //Helpers.CreateParameter(myCommand, DbType.Binary, "@signature", myDailySalesSummary.mSignature);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myDailySalesSummary.mDatestamp);

				Helpers.SetSaveParameters(myCommand, myDailySalesSummary);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update dailysalessummary as it has been updated by someone else");
				}
				//myDailySalesSummary.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spDailySalesSummaryDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static DailySalesSummary FillDataRecord(IDataRecord myDataRecord)
		{
			DailySalesSummary dailysalessummary = new DailySalesSummary();

			dailysalessummary.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			dailysalessummary.mInventoryDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("inventory_date"));
			dailysalessummary.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			dailysalessummary.mUserId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("user_id"));
			dailysalessummary.mCashExplanation = myDataRecord.GetString(myDataRecord.GetOrdinal("cash_explanation"));
			dailysalessummary.mInventoryExplanation = myDataRecord.GetString(myDataRecord.GetOrdinal("inventory_explanation"));
            //if (myDataRecord["signature"] != DBNull.Value)
            //    dailysalessummary.mSignature = (byte[])myDataRecord.GetValue(myDataRecord.GetOrdinal("signature"));
            dailysalessummary.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
			dailysalessummary.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            dailysalessummary.mStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("status"));
			//dailysalessummary.mConcurrencyId = (byte[]) myDataRecord.GetValue(myDataRecord.GetOrdinal("concurrency_id"));

			return dailysalessummary;
		}
	}
}