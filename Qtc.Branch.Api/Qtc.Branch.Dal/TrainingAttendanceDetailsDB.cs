using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class TrainingAttendanceDetailsDB
	{
		public static TrainingAttendanceDetails GetItem(int trainingattendancedetailsId)
		{
			TrainingAttendanceDetails trainingattendancedetails = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spTrainingAttendanceDetailsSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", trainingattendancedetailsId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						trainingattendancedetails = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return trainingattendancedetails;
		}

		public static TrainingAttendanceDetailsCollection GetList(TrainingAttendanceDetailsCriteria trainingattendancedetailsCriteria)
		{
			TrainingAttendanceDetailsCollection tempList = new TrainingAttendanceDetailsCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spTrainingAttendanceDetailsSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@training_attendance_id", trainingattendancedetailsCriteria.mTrainingAttendanceId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", trainingattendancedetailsCriteria.mLmmId);
                Helpers.CreateParameter(myCommand, DbType.String, "@date_created", trainingattendancedetailsCriteria.mDateCreated);


				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new TrainingAttendanceDetailsCollection();
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

		public static int SelectCountForGetList(TrainingAttendanceDetailsCriteria trainingattendancedetailsCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spTrainingAttendanceDetailsSearchList";

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

		public static int Save(TrainingAttendanceDetails myTrainingAttendanceDetails)
		{
			if (!myTrainingAttendanceDetails.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a trainingattendancedetails in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spTrainingAttendanceDetailsInsertUpdateSingleItem";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myTrainingAttendanceDetails.mRecordId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@training_attendance_id", myTrainingAttendanceDetails.mTrainingAttendanceId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_created", myTrainingAttendanceDetails.mDateCreated);
				Helpers.CreateParameter(myCommand, DbType.String, "@type_status", myTrainingAttendanceDetails.mTypeStatus);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@attendance_id", myTrainingAttendanceDetails.mAttendanceId);

				Helpers.SetSaveParameters(myCommand, myTrainingAttendanceDetails);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update trainingattendancedetails as it has been updated by someone else");
				}
				//myTrainingAttendanceDetails.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spTrainingAttendanceDetailsDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static TrainingAttendanceDetails FillDataRecord(IDataRecord myDataRecord)
		{
			TrainingAttendanceDetails trainingattendancedetails = new TrainingAttendanceDetails();

			trainingattendancedetails.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
            trainingattendancedetails.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));
            trainingattendancedetails.mTrainingAttendanceId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("training_attendance_id"));
			trainingattendancedetails.mDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_created"));
			trainingattendancedetails.mTypeStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("type_status"));
			trainingattendancedetails.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			trainingattendancedetails.mAttendanceId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("attendance_id"));

			return trainingattendancedetails;
		}
	}
}