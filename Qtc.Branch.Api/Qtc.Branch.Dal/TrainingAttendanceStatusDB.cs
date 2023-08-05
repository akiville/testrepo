using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class TrainingAttendanceStatusDB
	{
		public static TrainingAttendanceStatus GetItem(int trainingattendancestatusId)
		{
			TrainingAttendanceStatus trainingattendancestatus = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spTrainingAttendanceStatusSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", trainingattendancestatusId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						trainingattendancestatus = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return trainingattendancestatus;
		}

		public static TrainingAttendanceStatusCollection GetList(TrainingAttendanceStatusCriteria trainingattendancestatusCriteria)
		{
			TrainingAttendanceStatusCollection tempList = new TrainingAttendanceStatusCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spTrainingAttendanceStatusSearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new TrainingAttendanceStatusCollection();
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

		public static int SelectCountForGetList(TrainingAttendanceStatusCriteria trainingattendancestatusCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spTrainingAttendanceStatusSearchList";

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

		public static int Save(TrainingAttendanceStatus myTrainingAttendanceStatus)
		{
			if (!myTrainingAttendanceStatus.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a trainingattendancestatus in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spTrainingAttendanceStatusInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.String, "@status", myTrainingAttendanceStatus.mStatus);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myTrainingAttendanceStatus.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@user_id", myTrainingAttendanceStatus.mUserId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myTrainingAttendanceStatus.mDatestamp);
				Helpers.CreateParameter(myCommand, DbType.String, "@text_color", myTrainingAttendanceStatus.mTextColor);

				Helpers.SetSaveParameters(myCommand, myTrainingAttendanceStatus);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update trainingattendancestatus as it has been updated by someone else");
				}
				//myTrainingAttendanceStatus.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spTrainingAttendanceStatusDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static TrainingAttendanceStatus FillDataRecord(IDataRecord myDataRecord)
		{
			TrainingAttendanceStatus trainingattendancestatus = new TrainingAttendanceStatus();

			trainingattendancestatus.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			trainingattendancestatus.mStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("status"));
			trainingattendancestatus.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			trainingattendancestatus.mUserId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("user_id"));
			trainingattendancestatus.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			trainingattendancestatus.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
			trainingattendancestatus.mTextColor = myDataRecord.GetString(myDataRecord.GetOrdinal("text_color"));

			return trainingattendancestatus;
		}
	}
}