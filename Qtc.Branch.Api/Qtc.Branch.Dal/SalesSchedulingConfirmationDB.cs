using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class SalesSchedulingConfirmationDB
	{
		public static SalesSchedulingConfirmation GetItem(int salesschedulingconfirmationId)
		{
			SalesSchedulingConfirmation salesschedulingconfirmation = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spSalesSchedulingConfirmationSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", salesschedulingconfirmationId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						salesschedulingconfirmation = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return salesschedulingconfirmation;
		}

		public static SalesSchedulingConfirmationCollection GetList(SalesSchedulingConfirmationCriteria salesschedulingconfirmationCriteria)
		{
			SalesSchedulingConfirmationCollection tempList = new SalesSchedulingConfirmationCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spSalesSchedulingConfirmationSearchList";

                Helpers.CreateParameter(myCommand, DbType.DateTime, "@start_date", salesschedulingconfirmationCriteria.mStartDate);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@end_date", salesschedulingconfirmationCriteria.mEndDate);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", salesschedulingconfirmationCriteria.mLmmId);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new SalesSchedulingConfirmationCollection();
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

		public static int SelectCountForGetList(SalesSchedulingConfirmationCriteria salesschedulingconfirmationCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spSalesSchedulingConfirmationSearchList";

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

		public static int Save(SalesSchedulingConfirmation mySalesSchedulingConfirmation)
		{
			if (!mySalesSchedulingConfirmation.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a salesschedulingconfirmation in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spSalesSchedulingConfirmationInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.DateTime, "@start_date", mySalesSchedulingConfirmation.mStartDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@end_date", mySalesSchedulingConfirmation.mEndDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", mySalesSchedulingConfirmation.mLmmId);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", mySalesSchedulingConfirmation.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", mySalesSchedulingConfirmation.mDatestamp);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@cutoff_id", mySalesSchedulingConfirmation.mCutoffId);

				Helpers.SetSaveParameters(myCommand, mySalesSchedulingConfirmation);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update salesschedulingconfirmation as it has been updated by someone else");
				}
				//mySalesSchedulingConfirmation.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spSalesSchedulingConfirmationDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static SalesSchedulingConfirmation FillDataRecord(IDataRecord myDataRecord)
		{
			SalesSchedulingConfirmation salesschedulingconfirmation = new SalesSchedulingConfirmation();

			salesschedulingconfirmation.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			salesschedulingconfirmation.mStartDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("start_date"));
			salesschedulingconfirmation.mEndDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("end_date"));
			salesschedulingconfirmation.mLmmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_id"));
			salesschedulingconfirmation.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			salesschedulingconfirmation.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			salesschedulingconfirmation.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
            salesschedulingconfirmation.mCutoffId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("cutoff_id"));
			return salesschedulingconfirmation;
		}
	}
}