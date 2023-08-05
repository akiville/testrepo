using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class EmployeeAttendanceDB
	{
		public static EmployeeAttendance GetItem(int employeeattendanceId)
		{
			EmployeeAttendance employeeattendance = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spEmployeeAttendanceSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", employeeattendanceId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						employeeattendance = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return employeeattendance;
		}

		public static EmployeeAttendanceCollection GetList(EmployeeAttendanceCriteria employeeattendanceCriteria)
		{
			EmployeeAttendanceCollection tempList = new EmployeeAttendanceCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spEmployeeAttendanceSearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new EmployeeAttendanceCollection();
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

		public static int SelectCountForGetList(EmployeeAttendanceCriteria employeeattendanceCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spEmployeeAttendanceSearchList";

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

		public static int Save(EmployeeAttendance myEmployeeAttendance)
		{
			if (!myEmployeeAttendance.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a employeeattendance in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spEmployeeAttendanceInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myEmployeeAttendance.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@attendance_id", myEmployeeAttendance.mAttendanceId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@attendance_tracking_no", myEmployeeAttendance.mAttendanceTrackingNo);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_code", myEmployeeAttendance.mBranchCode);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@attendance_date", myEmployeeAttendance.mAttendanceDate);

				Helpers.SetSaveParameters(myCommand, myEmployeeAttendance);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update employeeattendance as it has been updated by someone else");
				}
				//myEmployeeAttendance.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spEmployeeAttendanceDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static EmployeeAttendance FillDataRecord(IDataRecord myDataRecord)
		{
			EmployeeAttendance employeeattendance = new EmployeeAttendance();

			employeeattendance.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			employeeattendance.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			employeeattendance.mAttendanceId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("attendance_id"));
			employeeattendance.mAttendanceTrackingNo = myDataRecord.GetInt32(myDataRecord.GetOrdinal("attendance_tracking_no"));
			employeeattendance.mBranchCode = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_code"));
			employeeattendance.mAttendanceDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("attendance_date"));
			employeeattendance.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			//employeeattendance.mConcurrencyId = (byte[]) myDataRecord.GetValue(myDataRecord.GetOrdinal("concurrency_id"));

			return employeeattendance;
		}
	}
}