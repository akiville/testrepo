using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class DisseminatedLetterDB
	{
		public static DisseminatedLetter GetItem(int disseminatedletterId)
		{
			DisseminatedLetter disseminatedletter = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDisseminatedLetterSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", disseminatedletterId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						disseminatedletter = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return disseminatedletter;
		}

		public static DisseminatedLetterCollection GetList(DisseminatedLetterCriteria disseminatedletterCriteria)
		{
			DisseminatedLetterCollection tempList = new DisseminatedLetterCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDisseminatedLetterSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", disseminatedletterCriteria.mLmmId);
                Helpers.CreateParameter(myCommand, DbType.String, "@type", disseminatedletterCriteria.mType);


				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new DisseminatedLetterCollection();
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

		public static int SelectCountForGetList(DisseminatedLetterCriteria disseminatedletterCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDisseminatedLetterSearchList";

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

		public static int Save(DisseminatedLetter myDisseminatedLetter)
		{
			if (!myDisseminatedLetter.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a disseminatedletter in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDisseminatedLetterInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@hr_letter_id", myDisseminatedLetter.mHrLetterId);
				Helpers.CreateParameter(myCommand, DbType.String, "@lmm", myDisseminatedLetter.mLmm);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_created", myDisseminatedLetter.mDateCreated);
				Helpers.CreateParameter(myCommand, DbType.String, "@area_name", myDisseminatedLetter.mAreaName);
				Helpers.CreateParameter(myCommand, DbType.String, "@name", myDisseminatedLetter.mName);
				Helpers.CreateParameter(myCommand, DbType.String, "@agency_id_name", myDisseminatedLetter.mAgencyIdName);
				Helpers.CreateParameter(myCommand, DbType.String, "@branch", myDisseminatedLetter.mBranch);
				Helpers.CreateParameter(myCommand, DbType.String, "@type", myDisseminatedLetter.mType);
				Helpers.CreateParameter(myCommand, DbType.String, "@duration", myDisseminatedLetter.mDuration);
				Helpers.CreateParameter(myCommand, DbType.String, "@control_no", myDisseminatedLetter.mControlNo);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_trip", myDisseminatedLetter.mDateTrip);
				Helpers.CreateParameter(myCommand, DbType.String, "@courier_name", myDisseminatedLetter.mCourierName);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", myDisseminatedLetter.mLmmId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myDisseminatedLetter.mBranchId);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@duration_from", myDisseminatedLetter.mDurationFrom);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@duration_to", myDisseminatedLetter.mDurationTo);
                Helpers.CreateParameter(myCommand, DbType.String, "@actual_expiration_date", myDisseminatedLetter.mActualExpirationDate);
				Helpers.CreateParameter(myCommand, DbType.String, "@response_date", myDisseminatedLetter.mResponseDate);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@updated_by_lmm", myDisseminatedLetter.mUpdatedByLmm);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_disseminated", myDisseminatedLetter.mDateDisseminated);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myDisseminatedLetter.mRecordId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myDisseminatedLetter.mEmployeeId);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_manual", myDisseminatedLetter.mIsManual);
                Helpers.SetSaveParameters(myCommand, myDisseminatedLetter);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update disseminatedletter as it has been updated by someone else");
				}
				//myDisseminatedLetter.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spDisseminatedLetterDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static DisseminatedLetter FillDataRecord(IDataRecord myDataRecord)
		{
			DisseminatedLetter disseminatedletter = new DisseminatedLetter();

			disseminatedletter.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			disseminatedletter.mHrLetterId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("hr_letter_id"));
			disseminatedletter.mLmm = myDataRecord.GetString(myDataRecord.GetOrdinal("lmm"));
			disseminatedletter.mDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_created"));
			disseminatedletter.mAreaName = myDataRecord.GetString(myDataRecord.GetOrdinal("area_name"));
			disseminatedletter.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));
			disseminatedletter.mAgencyIdName = myDataRecord.GetString(myDataRecord.GetOrdinal("agency_id_name"));
			disseminatedletter.mBranch = myDataRecord.GetString(myDataRecord.GetOrdinal("branch"));
			disseminatedletter.mType = myDataRecord.GetString(myDataRecord.GetOrdinal("type"));
			disseminatedletter.mDuration = myDataRecord.GetString(myDataRecord.GetOrdinal("duration"));
			disseminatedletter.mControlNo = myDataRecord.GetString(myDataRecord.GetOrdinal("control_no"));
			disseminatedletter.mDateTrip = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_trip"));
			disseminatedletter.mCourierName = myDataRecord.GetString(myDataRecord.GetOrdinal("courier_name"));
			disseminatedletter.mLmmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_id"));
            disseminatedletter.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
            disseminatedletter.mDurationFrom = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("duration_from"));
            disseminatedletter.mDurationTo = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("duration_to"));
            disseminatedletter.mActualExpirationDate = myDataRecord.GetString(myDataRecord.GetOrdinal("actual_expiration_date"));
			disseminatedletter.mResponseDate = myDataRecord.GetString(myDataRecord.GetOrdinal("response_date"));
			disseminatedletter.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            disseminatedletter.mUpdatedByLmm = myDataRecord.GetInt32(myDataRecord.GetOrdinal("updated_by_lmm"));
            disseminatedletter.mDateDisseminated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_disseminated"));
            disseminatedletter.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));
            disseminatedletter.mDaysBeforeExpire = myDataRecord.GetInt32(myDataRecord.GetOrdinal("days_before_expire"));
            disseminatedletter.mIsExpired = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_expired"));
            disseminatedletter.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
            disseminatedletter.mRenewalRequestStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("rr_status"));
            disseminatedletter.mRenewalRequestId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("rr_id"));
            disseminatedletter.mIsManual = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_manual"));
            disseminatedletter.mBranchCode = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_code"));
            return disseminatedletter;
		}
	}
}