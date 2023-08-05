using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class TraineeDB
	{
		public static Trainee GetItem(int traineeId)
		{
			Trainee trainee = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spTraineeSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", traineeId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						trainee = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return trainee;
		}

		public static TraineeCollection GetList(TraineeCriteria traineeCriteria)
		{
			TraineeCollection tempList = new TraineeCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spTraineeSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", traineeCriteria.mLmmId);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@employee_id", traineeCriteria.mEmployeeId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new TraineeCollection();
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

		public static int SelectCountForGetList(TraineeCriteria traineeCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spTraineeSearchList";

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

		public static int Save(Trainee myTrainee)
		{
			if (!myTrainee.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a trainee in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spTraineeInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myTrainee.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@is_trainee", myTrainee.mIsTrainee);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myTrainee.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", myTrainee.mLmmId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@start_date", myTrainee.mStartDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@end_date", myTrainee.mEndDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myTrainee.mDatestamp);

				Helpers.SetSaveParameters(myCommand, myTrainee);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update trainee as it has been updated by someone else");
				}
				//myTrainee.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spTraineeDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static Trainee FillDataRecord(IDataRecord myDataRecord)
		{
			Trainee trainee = new Trainee();

			trainee.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			trainee.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			trainee.mIsTrainee = myDataRecord.GetInt32(myDataRecord.GetOrdinal("is_trainee"));
			trainee.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			trainee.mLmmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_id"));
			trainee.mStartDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("start_date"));
			trainee.mEndDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("end_date"));
			trainee.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			trainee.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));

			return trainee;
		}
	}
}