using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class GreenslipDB
	{
		public static Greenslip GetItem(int greenslipId)
		{
			Greenslip greenslip = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spGreenslipSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", greenslipId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						greenslip = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return greenslip;
		}

		public static GreenslipCollection GetList(GreenslipCriteria greenslipCriteria)
		{
			GreenslipCollection tempList = new GreenslipCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spGreenslipSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", greenslipCriteria.mBranchId);
                Helpers.CreateParameter(myCommand, DbType.String, "@start_date", greenslipCriteria.mStartDate);
                Helpers.CreateParameter(myCommand, DbType.String, "@end_date", greenslipCriteria.mEndDate);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_downloaded", greenslipCriteria.mIsDownloaded);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@id", greenslipCriteria.mId);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new GreenslipCollection();
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

		public static int SelectCountForGetList(GreenslipCriteria greenslipCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spGreenslipSearchList";

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

		public static int Save(Greenslip myGreenslip)
		{
			if (!myGreenslip.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a greenslip in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spGreenslipInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date", myGreenslip.mDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_requested", myGreenslip.mDateRequested);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myGreenslip.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@requested_by_id", myGreenslip.mRequestedById);
				Helpers.CreateParameter(myCommand, DbType.String, "@type", myGreenslip.mType);
				Helpers.CreateParameter(myCommand, DbType.String, "@type2", myGreenslip.mType2);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@greenslip_request_id", myGreenslip.mGreenslipRequestId);
				Helpers.CreateParameter(myCommand, DbType.String, "@explanation", myGreenslip.mExplanation);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myGreenslip.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.String, "@approved_by", myGreenslip.mApprovedBy);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@number", myGreenslip.mNumber);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@denied", myGreenslip.mDenied);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@cancelled", myGreenslip.mCancelled);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@delivery_date", myGreenslip.mDeliveryDate);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_downloaded", myGreenslip.mIsDownloaded);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@app_download_date", myGreenslip.mAppDownloadDate);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "@taken_action", myGreenslip.mTakenAction);

                Helpers.SetSaveParameters(myCommand, myGreenslip);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update greenslip as it has been updated by someone else");
				}
				//myGreenslip.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spGreenslipDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static Greenslip FillDataRecord(IDataRecord myDataRecord)
		{
			Greenslip greenslip = new Greenslip();

			greenslip.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			greenslip.mDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date"));
			greenslip.mDateRequested = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_requested"));
			greenslip.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			greenslip.mRequestedById = myDataRecord.GetInt32(myDataRecord.GetOrdinal("requested_by_id"));
			greenslip.mType = myDataRecord.GetString(myDataRecord.GetOrdinal("type"));
			greenslip.mType2 = myDataRecord.GetString(myDataRecord.GetOrdinal("type2"));
			greenslip.mGreenslipRequestId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("greenslip_request_id"));
			greenslip.mExplanation = myDataRecord.GetString(myDataRecord.GetOrdinal("explanation"));
			greenslip.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			greenslip.mApprovedBy = myDataRecord.GetString(myDataRecord.GetOrdinal("approved_by"));
			greenslip.mNumber = myDataRecord.GetInt32(myDataRecord.GetOrdinal("number"));
			greenslip.mDenied = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("denied"));
			greenslip.mCancelled = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("cancelled"));
			greenslip.mDeliveryDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("delivery_date"));
			greenslip.mIsDownloaded = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_downloaded"));
			greenslip.mAppDownloadDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("app_download_date"));
			greenslip.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            greenslip.mTakenAction = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("taken_action"));
            //greenslip.mConcurrencyId = (byte[]) myDataRecord.GetValue(myDataRecord.GetOrdinal("concurrency_id"));

            return greenslip;
		}
	}
}