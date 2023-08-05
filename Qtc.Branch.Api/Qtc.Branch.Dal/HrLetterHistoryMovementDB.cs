using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class HrLetterHistoryMovementDB
	{
		public static HrLetterHistoryMovement GetItem(int hrletterhistorymovementId)
		{
			HrLetterHistoryMovement hrletterhistorymovement = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spHrLetterHistoryMovementSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", hrletterhistorymovementId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						hrletterhistorymovement = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return hrletterhistorymovement;
		}

		public static HrLetterHistoryMovementCollection GetList(HrLetterHistoryMovementCriteria hrletterhistorymovementCriteria)
		{
			HrLetterHistoryMovementCollection tempList = new HrLetterHistoryMovementCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spHrLetterHistoryMovementSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@hr_letter_id", hrletterhistorymovementCriteria.mHrLetterId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new HrLetterHistoryMovementCollection();
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

		public static int SelectCountForGetList(HrLetterHistoryMovementCriteria hrletterhistorymovementCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spHrLetterHistoryMovementSearchList";

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

		public static int Save(HrLetterHistoryMovement myHrLetterHistoryMovement)
		{
			if (!myHrLetterHistoryMovement.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a hrletterhistorymovement in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spHrLetterHistoryMovementInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@hr_letter_id", myHrLetterHistoryMovement.mHrLetterId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@sequence", myHrLetterHistoryMovement.mSequence);
				Helpers.CreateParameter(myCommand, DbType.String, "@action", myHrLetterHistoryMovement.mAction);
				Helpers.CreateParameter(myCommand, DbType.String, "@rider", myHrLetterHistoryMovement.mRider);
				Helpers.CreateParameter(myCommand, DbType.String, "@series_number", myHrLetterHistoryMovement.mSeriesNumber);
				Helpers.CreateParameter(myCommand, DbType.String, "@destination", myHrLetterHistoryMovement.mDestination);
				Helpers.CreateParameter(myCommand, DbType.String, "@date_trip", myHrLetterHistoryMovement.mDateTrip);
				Helpers.CreateParameter(myCommand, DbType.String, "@branch_name", myHrLetterHistoryMovement.mBranchName);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date", myHrLetterHistoryMovement.mDate);
				Helpers.CreateParameter(myCommand, DbType.String, "@action_by", myHrLetterHistoryMovement.mActionBy);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myHrLetterHistoryMovement.mDatestamp);

				Helpers.SetSaveParameters(myCommand, myHrLetterHistoryMovement);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update hrletterhistorymovement as it has been updated by someone else");
				}
				//myHrLetterHistoryMovement.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spHrLetterHistoryMovementDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static HrLetterHistoryMovement FillDataRecord(IDataRecord myDataRecord)
		{
			HrLetterHistoryMovement hrletterhistorymovement = new HrLetterHistoryMovement();

			hrletterhistorymovement.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			hrletterhistorymovement.mHrLetterId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("hr_letter_id"));
			hrletterhistorymovement.mSequence = myDataRecord.GetInt32(myDataRecord.GetOrdinal("sequence"));
			hrletterhistorymovement.mAction = myDataRecord.GetString(myDataRecord.GetOrdinal("action"));
			hrletterhistorymovement.mRider = myDataRecord.GetString(myDataRecord.GetOrdinal("rider"));
			hrletterhistorymovement.mSeriesNumber = myDataRecord.GetString(myDataRecord.GetOrdinal("series_number"));
			hrletterhistorymovement.mDestination = myDataRecord.GetString(myDataRecord.GetOrdinal("destination"));
			hrletterhistorymovement.mDateTrip = myDataRecord.GetString(myDataRecord.GetOrdinal("date_trip"));
			hrletterhistorymovement.mBranchName = myDataRecord.GetString(myDataRecord.GetOrdinal("branch_name"));
			hrletterhistorymovement.mDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date"));
			hrletterhistorymovement.mActionBy = myDataRecord.GetString(myDataRecord.GetOrdinal("action_by"));
			hrletterhistorymovement.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
			hrletterhistorymovement.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return hrletterhistorymovement;
		}
	}
}