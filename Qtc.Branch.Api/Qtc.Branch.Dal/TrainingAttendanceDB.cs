using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class TrainingAttendanceDB
	{
		public static TrainingAttendance GetItem(int trainingattendanceId)
		{
			TrainingAttendance trainingattendance = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spTrainingAttendanceSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", trainingattendanceId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						trainingattendance = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return trainingattendance;
		}

		public static TrainingAttendanceCollection GetList(TrainingAttendanceCriteria trainingattendanceCriteria)
		{
			TrainingAttendanceCollection tempList = new TrainingAttendanceCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spTrainingAttendanceSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", trainingattendanceCriteria.mLmmId);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_created", trainingattendanceCriteria.mDateCreated);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new TrainingAttendanceCollection();
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

		public static int SelectCountForGetList(TrainingAttendanceCriteria trainingattendanceCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spTrainingAttendanceSearchList";

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

		public static int Save(TrainingAttendance myTrainingAttendance)
		{
			if (!myTrainingAttendance.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a trainingattendance in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spTrainingAttendanceInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myTrainingAttendance.mRecordId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_created", myTrainingAttendance.mDateCreated);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myTrainingAttendance.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", myTrainingAttendance.mLmmId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myTrainingAttendance.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.String, "@orientation_date", myTrainingAttendance.mOrientationDate);

				Helpers.SetSaveParameters(myCommand, myTrainingAttendance);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update trainingattendance as it has been updated by someone else");
				}
				//myTrainingAttendance.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spTrainingAttendanceDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static TrainingAttendance FillDataRecord(IDataRecord myDataRecord)
		{
			TrainingAttendance trainingattendance = new TrainingAttendance();

			trainingattendance.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			trainingattendance.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));
			trainingattendance.mDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_created"));
			trainingattendance.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			trainingattendance.mLmmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_id"));
			trainingattendance.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			trainingattendance.mOrientationDate = myDataRecord.GetString(myDataRecord.GetOrdinal("orientation_date"));
			trainingattendance.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            trainingattendance.mDay1 = myDataRecord.GetString(myDataRecord.GetOrdinal("day1"));
            trainingattendance.mDay2 = myDataRecord.GetString(myDataRecord.GetOrdinal("day2"));
            trainingattendance.mDay3 = myDataRecord.GetString(myDataRecord.GetOrdinal("day3"));
            trainingattendance.mDay4 = myDataRecord.GetString(myDataRecord.GetOrdinal("day4"));
            trainingattendance.mDay5 = myDataRecord.GetString(myDataRecord.GetOrdinal("day5"));
            trainingattendance.mDay6 = myDataRecord.GetString(myDataRecord.GetOrdinal("day6"));
            trainingattendance.mDay7 = myDataRecord.GetString(myDataRecord.GetOrdinal("day7"));
            trainingattendance.mDay1Id = myDataRecord.GetInt32(myDataRecord.GetOrdinal("day1_id"));
            trainingattendance.mDay2Id = myDataRecord.GetInt32(myDataRecord.GetOrdinal("day2_id"));
            trainingattendance.mDay3Id = myDataRecord.GetInt32(myDataRecord.GetOrdinal("day3_id"));
            trainingattendance.mDay4Id = myDataRecord.GetInt32(myDataRecord.GetOrdinal("day4_id"));
            trainingattendance.mDay5Id = myDataRecord.GetInt32(myDataRecord.GetOrdinal("day5_id"));
            trainingattendance.mDay6Id = myDataRecord.GetInt32(myDataRecord.GetOrdinal("day6_id"));
            trainingattendance.mDay7Id = myDataRecord.GetInt32(myDataRecord.GetOrdinal("day7_id"));
            trainingattendance.mBranchName = myDataRecord.GetString(myDataRecord.GetOrdinal("branch_name"));
            trainingattendance.mEmployeeName = myDataRecord.GetString(myDataRecord.GetOrdinal("employee_name"));
            trainingattendance.mCellphoneNo = myDataRecord.GetString(myDataRecord.GetOrdinal("cellphone_no"));
            trainingattendance.mAgency = myDataRecord.GetString(myDataRecord.GetOrdinal("agency"));
            trainingattendance.mDay1Date = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("day1_date"));
            trainingattendance.mDay2Date = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("day2_date"));
            trainingattendance.mDay3Date = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("day3_date"));
            trainingattendance.mDay4Date = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("day4_date"));
            trainingattendance.mDay5Date = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("day5_date"));
            trainingattendance.mDay6Date = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("day6_date"));
            trainingattendance.mDay7Date = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("day7_date"));
            trainingattendance.mMotherBranch = myDataRecord.GetString(myDataRecord.GetOrdinal("mother_branch"));
            trainingattendance.mMotherBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("mother_branch_id"));
            return trainingattendance;
		}
	}
}