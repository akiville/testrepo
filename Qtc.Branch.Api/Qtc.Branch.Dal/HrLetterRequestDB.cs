using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class HrLetterRequestDB
	{
		public static HrLetterRequest GetItem(int hrletterrequestId)
		{
			HrLetterRequest hrletterrequest = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spHrLetterRequestSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", hrletterrequestId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						hrletterrequest = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return hrletterrequest;
		}

		public static HrLetterRequestCollection GetList(HrLetterRequestCriteria hrletterrequestCriteria)
		{
			HrLetterRequestCollection tempList = new HrLetterRequestCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spHrLetterRequestSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@id", hrletterrequestCriteria.mId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new HrLetterRequestCollection();
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

		public static int SelectCountForGetList(HrLetterRequestCriteria hrletterrequestCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spHrLetterRequestSearchList";

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

		public static int Save(HrLetterRequest myHrLetterRequest)
		{
			if (!myHrLetterRequest.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a hrletterrequest in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spHrLetterRequestInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myHrLetterRequest.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myHrLetterRequest.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@duration_from", myHrLetterRequest.mDurationFrom);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@duration_to", myHrLetterRequest.mDurationTo);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@start_date", myHrLetterRequest.mStartDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@end_date", myHrLetterRequest.mEndDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@type_of_letter_id", myHrLetterRequest.mTypeOfLetterId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@hr_letter_category_id", myHrLetterRequest.mHrLetterCategoryId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@user_id", myHrLetterRequest.mUserId);
                Helpers.CreateParameter(myCommand, DbType.String, "@status", myHrLetterRequest.mStatus);
                Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myHrLetterRequest.mRemarks);
                Helpers.CreateParameter(myCommand, DbType.String, "@has_existing_hr_letter", myHrLetterRequest.mHasExistingHrLetter);
                Helpers.CreateParameter(myCommand, DbType.String, "@has_valid_intro_letter", myHrLetterRequest.mHasValidIntroLetter);
                Helpers.CreateParameter(myCommand, DbType.String, "@has_valid_intro_letter_for_additional_outlet", myHrLetterRequest.mHasValidIntroLetterForAdditionalOutlet);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id_to", myHrLetterRequest.mBranchIdTo);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@copies_count", myHrLetterRequest.mCopiesCount);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@requested_by_id", myHrLetterRequest.mRequestedBy);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@request_released_date", myHrLetterRequest.mRequestReleasedDate);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@agency_id", myHrLetterRequest.mAgencyId);
                Helpers.CreateParameter(myCommand, DbType.String, "@file_name", myHrLetterRequest.mFileName);

                Helpers.SetSaveParameters(myCommand, myHrLetterRequest);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update hrletterrequest as it has been updated by someone else");
				}
				//myHrLetterRequest.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "ptApi_spHrLetterRequestDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static HrLetterRequest FillDataRecord(IDataRecord myDataRecord)
		{
			HrLetterRequest hrletterrequest = new HrLetterRequest();

			hrletterrequest.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			hrletterrequest.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			hrletterrequest.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			hrletterrequest.mDurationFrom = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("duration_from"));
			hrletterrequest.mDurationTo = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("duration_to"));
			hrletterrequest.mStartDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("start_date"));
			hrletterrequest.mEndDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("end_date"));
			hrletterrequest.mTypeOfLetterId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("type_of_letter_id"));
			hrletterrequest.mHrLetterCategoryId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("hr_letter_category_id"));
			hrletterrequest.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			hrletterrequest.mUserId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("user_id"));
            hrletterrequest.mStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("status"));
            hrletterrequest.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
            hrletterrequest.mHasExistingHrLetter = myDataRecord.GetString(myDataRecord.GetOrdinal("has_existing_hr_letter"));
            hrletterrequest.mHasValidIntroLetter = myDataRecord.GetString(myDataRecord.GetOrdinal("has_valid_intro_letter"));
            hrletterrequest.mHasValidIntroLetterForAdditionalOutlet = myDataRecord.GetString(myDataRecord.GetOrdinal("has_valid_intro_letter_for_additional_outlet"));
            hrletterrequest.mBranchIdTo = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id_to"));
            hrletterrequest.mCopiesCount = myDataRecord.GetInt32(myDataRecord.GetOrdinal("copies_count"));
            hrletterrequest.mRequestedBy = myDataRecord.GetInt32(myDataRecord.GetOrdinal("requested_by_id"));
            hrletterrequest.mRequestReleasedDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("request_released_date"));
            hrletterrequest.mAgencyId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("agency_id"));
            hrletterrequest.mFileName = myDataRecord.GetString(myDataRecord.GetOrdinal("file_name"));
            hrletterrequest.mLastName = myDataRecord.GetString(myDataRecord.GetOrdinal("lastname"));
            hrletterrequest.mFirstName = myDataRecord.GetString(myDataRecord.GetOrdinal("firstname"));
            hrletterrequest.mTypeOfLetter = myDataRecord.GetString(myDataRecord.GetOrdinal("type_of_letter"));
            hrletterrequest.mBranchName = myDataRecord.GetString(myDataRecord.GetOrdinal("branch_name"));
            hrletterrequest.mBranchNameTo = myDataRecord.GetString(myDataRecord.GetOrdinal("branch_name_to"));

            return hrletterrequest;
		}
	}
}