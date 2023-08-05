using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class EmployeeHrLetterDB
	{
		public static EmployeeHrLetter GetItem(int employeehrletterId)
		{
			EmployeeHrLetter employeehrletter = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spEmployeeHrLetterSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", employeehrletterId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						employeehrletter = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return employeehrletter;
		}

		public static EmployeeHrLetterCollection GetList(EmployeeHrLetterCriteria employeehrletterCriteria)
		{
			EmployeeHrLetterCollection tempList = new EmployeeHrLetterCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spEmployeeHrLetterSearchList";

                Helpers.CreateParameter(myCommand, DbType.DateTime, "@duration_from", employeehrletterCriteria.mDurationFrom);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@duration_to", employeehrletterCriteria.mDurationTo);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@request_by", employeehrletterCriteria.mRequestBy);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", employeehrletterCriteria.mEmployeeId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", employeehrletterCriteria.mBranchId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_to", employeehrletterCriteria.mBranchTo);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new EmployeeHrLetterCollection();
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

		public static int SelectCountForGetList(EmployeeHrLetterCriteria employeehrletterCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spEmployeeHrLetterSearchList";

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

		public static int Save(EmployeeHrLetter myEmployeeHrLetter)
		{
			if (!myEmployeeHrLetter.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a employeehrletter in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spEmployeeHrLetterInsertUpdateSingleItem";
                Helpers.CreateParameter(myCommand, DbType.Int32, "@type_of_letter_id", myEmployeeHrLetter.mTypeOfLetterId);
				Helpers.CreateParameter(myCommand, DbType.String, "@control_no", myEmployeeHrLetter.mControlNo);
				Helpers.CreateParameter(myCommand, DbType.String, "@form_no", myEmployeeHrLetter.mFormNo);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myEmployeeHrLetter.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_created", myEmployeeHrLetter.mDateCreated);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@duration_from", myEmployeeHrLetter.mDurationFrom);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@duration_to", myEmployeeHrLetter.mDurationTo);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myEmployeeHrLetter.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_to", myEmployeeHrLetter.mBranchTo);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myEmployeeHrLetter.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@no_copies", myEmployeeHrLetter.mNoCopies);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@cancelled", myEmployeeHrLetter.mCancelled);
				Helpers.CreateParameter(myCommand, DbType.String, "@cancelled_remark", myEmployeeHrLetter.mCancelledRemark);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@approved", myEmployeeHrLetter.mApproved);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@approved_date", myEmployeeHrLetter.mApprovedDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@approved_by", myEmployeeHrLetter.mApprovedBy);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@print_no", myEmployeeHrLetter.mPrintNo);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@print_date", myEmployeeHrLetter.mPrintDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@released_date", myEmployeeHrLetter.mReleasedDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@released_by_id", myEmployeeHrLetter.mReleasedById);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@returned_date", myEmployeeHrLetter.mReturnedDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@returned_to", myEmployeeHrLetter.mReturnedTo);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@request_by", myEmployeeHrLetter.mRequestBy);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@released_copies", myEmployeeHrLetter.mReleasedCopies);
				Helpers.CreateParameter(myCommand, DbType.String, "@released_no", myEmployeeHrLetter.mReleasedNo);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@released_to", myEmployeeHrLetter.mReleasedTo);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@duration_from_actual", myEmployeeHrLetter.mDurationFromActual);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@duration_to_actual", myEmployeeHrLetter.mDurationToActual);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@intro_letter_id", myEmployeeHrLetter.mIntroLetterId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@print_release_to", myEmployeeHrLetter.mPrintReleaseTo);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@request_rtw", myEmployeeHrLetter.mRequestRtw);
				Helpers.CreateParameter(myCommand, DbType.String, "@lmm_name", myEmployeeHrLetter.mLmmName);
				Helpers.CreateParameter(myCommand, DbType.String, "@oic_name", myEmployeeHrLetter.mOicName);
				Helpers.CreateParameter(myCommand, DbType.String, "@reason", myEmployeeHrLetter.mReason);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@weekend_letter", myEmployeeHrLetter.mWeekendLetter);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@opening_scheduled", myEmployeeHrLetter.mOpeningScheduled);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@released_date_request", myEmployeeHrLetter.mReleasedDateRequest);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@encoded_by_id", myEmployeeHrLetter.mEncodedById);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@hr_letter_category_id", myEmployeeHrLetter.mHrLetterCategoryId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@notify_mc_bag", myEmployeeHrLetter.mNotifyMcBag);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@notify_key", myEmployeeHrLetter.mNotifyKey);
				Helpers.CreateParameter(myCommand, DbType.String, "@courier_name", myEmployeeHrLetter.mCourierName);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@other_type", myEmployeeHrLetter.mOtherType);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_generated", myEmployeeHrLetter.mDateGenerated);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@cancelled_date", myEmployeeHrLetter.mCancelledDate);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@first_reliever", myEmployeeHrLetter.mFirstReliever);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@need_to_release", myEmployeeHrLetter.mNeedToRelease);

				Helpers.SetSaveParameters(myCommand, myEmployeeHrLetter);

                myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update employeehrletter as it has been updated by someone else");
				}
				//myEmployeeHrLetter.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spEmployeeHrLetterDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static EmployeeHrLetter FillDataRecord(IDataRecord myDataRecord)
		{
			EmployeeHrLetter employeehrletter = new EmployeeHrLetter();

			employeehrletter.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			employeehrletter.mTypeOfLetterId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("type_of_letter_id"));
			employeehrletter.mControlNo = myDataRecord.GetString(myDataRecord.GetOrdinal("control_no"));
			employeehrletter.mFormNo = myDataRecord.GetString(myDataRecord.GetOrdinal("form_no"));
			employeehrletter.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			employeehrletter.mDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_created"));
			employeehrletter.mDurationFrom = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("duration_from"));
			employeehrletter.mDurationTo = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("duration_to"));
			employeehrletter.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			employeehrletter.mBranchTo = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_to"));
			employeehrletter.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			employeehrletter.mNoCopies = myDataRecord.GetInt32(myDataRecord.GetOrdinal("no_copies"));
			employeehrletter.mCancelled = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("cancelled"));
			employeehrletter.mCancelledRemark = myDataRecord.GetString(myDataRecord.GetOrdinal("cancelled_remark"));
			employeehrletter.mApproved = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("approved"));
			employeehrletter.mApprovedDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("approved_date"));
			employeehrletter.mApprovedBy = myDataRecord.GetInt32(myDataRecord.GetOrdinal("approved_by"));
			employeehrletter.mPrintNo = myDataRecord.GetInt32(myDataRecord.GetOrdinal("print_no"));
			employeehrletter.mPrintDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("print_date"));
			//employeehrletter.mReleasedSign = myDataRecord.GetBinary(myDataRecord.GetOrdinal("released_sign"));
			employeehrletter.mReleasedDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("released_date"));
			employeehrletter.mReleasedById = myDataRecord.GetInt32(myDataRecord.GetOrdinal("released_by_id"));
			employeehrletter.mReturnedDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("returned_date"));
			employeehrletter.mReturnedTo = myDataRecord.GetInt32(myDataRecord.GetOrdinal("returned_to"));
			employeehrletter.mRequestBy = myDataRecord.GetInt32(myDataRecord.GetOrdinal("request_by"));
			employeehrletter.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			employeehrletter.mReleasedCopies = myDataRecord.GetInt32(myDataRecord.GetOrdinal("released_copies"));
			//employeehrletter.mReturnedFile = myDataRecord.GetBinary(myDataRecord.GetOrdinal("returned_file"));
			employeehrletter.mReleasedNo = myDataRecord.GetString(myDataRecord.GetOrdinal("released_no"));
			employeehrletter.mReleasedTo = myDataRecord.GetInt32(myDataRecord.GetOrdinal("released_to"));
			employeehrletter.mDurationFromActual = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("duration_from_actual"));
			employeehrletter.mDurationToActual = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("duration_to_actual"));
			employeehrletter.mIntroLetterId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("intro_letter_id"));
			employeehrletter.mPrintReleaseTo = myDataRecord.GetInt32(myDataRecord.GetOrdinal("print_release_to"));
			//employeehrletter.mPrintReleaseSign = myDataRecord.GetBinary(myDataRecord.GetOrdinal("print_release_sign"));
			employeehrletter.mRequestRtw = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("request_rtw"));
			employeehrletter.mLmmName = myDataRecord.GetString(myDataRecord.GetOrdinal("lmm_name"));
			employeehrletter.mOicName = myDataRecord.GetString(myDataRecord.GetOrdinal("oic_name"));
			employeehrletter.mReason = myDataRecord.GetString(myDataRecord.GetOrdinal("reason"));
			employeehrletter.mWeekendLetter = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("weekend_letter"));
			employeehrletter.mOpeningScheduled = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("opening_scheduled"));
			employeehrletter.mReleasedDateRequest = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("released_date_request"));
			employeehrletter.mEncodedById = myDataRecord.GetInt32(myDataRecord.GetOrdinal("encoded_by_id"));
			employeehrletter.mHrLetterCategoryId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("hr_letter_category_id"));
			employeehrletter.mNotifyMcBag = myDataRecord.GetInt32(myDataRecord.GetOrdinal("notify_mc_bag"));
			employeehrletter.mNotifyKey = myDataRecord.GetInt32(myDataRecord.GetOrdinal("notify_key"));
			employeehrletter.mCourierName = myDataRecord.GetString(myDataRecord.GetOrdinal("courier_name"));
			employeehrletter.mOtherType = myDataRecord.GetInt32(myDataRecord.GetOrdinal("other_type"));
			employeehrletter.mDateGenerated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_generated"));
			employeehrletter.mCancelledDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("cancelled_date"));
			employeehrletter.mFirstReliever = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("first_reliever"));
			employeehrletter.mNeedToRelease = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("need_to_release"));
            employeehrletter.mEmployeeName = myDataRecord.GetString(myDataRecord.GetOrdinal("employee_name"));
            employeehrletter.mBranchName = myDataRecord.GetString(myDataRecord.GetOrdinal("branch_name"));
            employeehrletter.mBranchToName = myDataRecord.GetString(myDataRecord.GetOrdinal("branch_name_to"));
            employeehrletter.mRequestedByName = myDataRecord.GetString(myDataRecord.GetOrdinal("requested_by_name"));
            employeehrletter.mTypeOfLetter = myDataRecord.GetString(myDataRecord.GetOrdinal("type_of_letter"));
            return employeehrletter;
		}
	}
}