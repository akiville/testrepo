using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class ShiftDB
	{
		public static Shift GetItem(int shiftId)
		{
			Shift shift = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spShiftSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", shiftId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						shift = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return shift;
		}

		public static ShiftCollection GetList(ShiftCriteria shiftCriteria)
		{
			ShiftCollection tempList = new ShiftCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spShiftSearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new ShiftCollection();
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

		public static int SelectCountForGetList(ShiftCriteria shiftCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spShiftSearchList";

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

		public static int Save(Shift myShift)
		{
			if (!myShift.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a shift in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spShiftInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.String, "@code", myShift.mCode);
				Helpers.CreateParameter(myCommand, DbType.String, "@in", myShift.mIn);
				Helpers.CreateParameter(myCommand, DbType.String, "@break_out", myShift.mBreakOut);
				Helpers.CreateParameter(myCommand, DbType.String, "@break_in", myShift.mBreakIn);
				Helpers.CreateParameter(myCommand, DbType.String, "@out", myShift.mOut);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@n_shift", myShift.mNShift);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@minutes_need", myShift.mMinutesNeed);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@in_schedule", myShift.mInSchedule);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@break_out_schedule", myShift.mBreakOutSchedule);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@break_in_schedule", myShift.mBreakInSchedule);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@out_schedule", myShift.mOutSchedule);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@flexi_time", myShift.mFlexiTime);

				Helpers.SetSaveParameters(myCommand, myShift);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update shift as it has been updated by someone else");
				}
				//myShift.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spShiftDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static Shift FillDataRecord(IDataRecord myDataRecord)
		{
			Shift shift = new Shift();

			shift.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			shift.mCode = myDataRecord.GetString(myDataRecord.GetOrdinal("code"));
			shift.mIn = myDataRecord.GetString(myDataRecord.GetOrdinal("in"));
			shift.mBreakOut = myDataRecord.GetString(myDataRecord.GetOrdinal("break_out"));
			shift.mBreakIn = myDataRecord.GetString(myDataRecord.GetOrdinal("break_in"));
			shift.mOut = myDataRecord.GetString(myDataRecord.GetOrdinal("out"));
			shift.mNShift = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("n_shift"));
			shift.mMinutesNeed = myDataRecord.GetInt32(myDataRecord.GetOrdinal("minutes_need"));
			shift.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			shift.mInSchedule = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("in_schedule"));
			shift.mBreakOutSchedule = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("break_out_schedule"));
			shift.mBreakInSchedule = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("break_in_schedule"));
			shift.mOutSchedule = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("out_schedule"));
			shift.mFlexiTime = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("flexi_time"));

			return shift;
		}
	}
}