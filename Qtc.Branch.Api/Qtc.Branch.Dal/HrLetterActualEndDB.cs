using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class HrLetterActualEndDB
	{
		public static HrLetterActualEnd GetItem(int hrletteractualendId)
		{
			HrLetterActualEnd hrletteractualend = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spHrLetterActualEndSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", hrletteractualendId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						hrletteractualend = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return hrletteractualend;
		}

		public static HrLetterActualEndCollection GetList(HrLetterActualEndCriteria hrletteractualendCriteria)
		{
			HrLetterActualEndCollection tempList = new HrLetterActualEndCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spHrLetterActualEndSearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new HrLetterActualEndCollection();
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

		public static int SelectCountForGetList(HrLetterActualEndCriteria hrletteractualendCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spHrLetterActualEndSearchList";

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

		public static int Save(HrLetterActualEnd myHrLetterActualEnd)
		{
			if (!myHrLetterActualEnd.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a hrletteractualend in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spHrLetterActualEndInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@letter_id", myHrLetterActualEnd.mLetterId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@actual_end", myHrLetterActualEnd.mActualEnd);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@user_id", myHrLetterActualEnd.mUserId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_added", myHrLetterActualEnd.mDateAdded);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@is_renewal_hr_letter_id", myHrLetterActualEnd.mIsRenewalHrLetterId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@type_of_letter_id", myHrLetterActualEnd.mTypeOfLetterId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myHrLetterActualEnd.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myHrLetterActualEnd.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@duration_from", myHrLetterActualEnd.mDurationFrom);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@duration_to", myHrLetterActualEnd.mDurationTo);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myHrLetterActualEnd.mRecordId);

				Helpers.SetSaveParameters(myCommand, myHrLetterActualEnd);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update hrletteractualend as it has been updated by someone else");
				}
				//myHrLetterActualEnd.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spHrLetterActualEndDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static HrLetterActualEnd FillDataRecord(IDataRecord myDataRecord)
		{
			HrLetterActualEnd hrletteractualend = new HrLetterActualEnd();

			hrletteractualend.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			hrletteractualend.mLetterId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("letter_id"));
			hrletteractualend.mActualEnd = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("actual_end"));
			hrletteractualend.mUserId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("user_id"));
			hrletteractualend.mDateAdded = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_added"));
			hrletteractualend.mIsRenewalHrLetterId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("is_renewal_hr_letter_id"));
			hrletteractualend.mTypeOfLetterId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("type_of_letter_id"));
			hrletteractualend.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			hrletteractualend.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			hrletteractualend.mDurationFrom = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("duration_from"));
			hrletteractualend.mDurationTo = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("duration_to"));
			hrletteractualend.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			hrletteractualend.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));

			return hrletteractualend;
		}
	}
}