using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class LmmAttendanceDB
	{
		public static LmmAttendance GetItem(int lmmattendanceId)
		{
			LmmAttendance lmmattendance = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLmmAttendanceSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", lmmattendanceId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						lmmattendance = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return lmmattendance;
		}

		public static LmmAttendanceCollection GetList(LmmAttendanceCriteria lmmattendanceCriteria)
		{
			LmmAttendanceCollection tempList = new LmmAttendanceCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLmmAttendanceSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", lmmattendanceCriteria.mLmmId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", lmmattendanceCriteria.mEmployeeId);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@date", lmmattendanceCriteria.mAttendanceDate);


				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new LmmAttendanceCollection();
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

        public static LmmAttendanceCollection GetListCount(LmmAttendanceCriteria lmmattendanceCriteria)
        {
            LmmAttendanceCollection tempList = new LmmAttendanceCollection();
            using (DbCommand myCommand = AppConfiguration.CreateCommand())
            {
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "syPa_LmmAttendanceCount";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", lmmattendanceCriteria.mLmmId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", lmmattendanceCriteria.mEmployeeId);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@start_date", lmmattendanceCriteria.mStartDate);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@end_date", lmmattendanceCriteria.mEndDate);

                myCommand.Connection.Open();
                using (DbDataReader myReader = myCommand.ExecuteReader())
                {
                    if (myReader.HasRows)
                    {
                        tempList = new LmmAttendanceCollection();
                        while (myReader.Read())
                        {
                            tempList.Add(FillDataRecordCount(myReader));
                        }
                        myReader.Close();
                    }
                }
                myCommand.Connection.Close();
            }

            return tempList;
        }

        public static int SelectCountForGetList(LmmAttendanceCriteria lmmattendanceCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLmmAttendanceSearchList";

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

		public static int Save(LmmAttendance myLmmAttendance)
		{
			if (!myLmmAttendance.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a lmmattendance in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLmmAttendanceInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", myLmmAttendance.mLmmId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myLmmAttendance.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@cutoff_id", myLmmAttendance.mCutoffId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@attendance_type_id", myLmmAttendance.mAttendanceTypeId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myLmmAttendance.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@attendance_date", myLmmAttendance.mAttendanceDate);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myLmmAttendance.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myLmmAttendance.mDatestamp);

				Helpers.SetSaveParameters(myCommand, myLmmAttendance);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update lmmattendance as it has been updated by someone else");
				}
				//myLmmAttendance.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spLmmAttendanceDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}
        private static LmmAttendance FillDataRecordCount(IDataRecord myDataRecord)
        {
            LmmAttendance lmmattendance = new LmmAttendance();

            lmmattendance.mTotalItem = myDataRecord.GetInt32(myDataRecord.GetOrdinal("total_item"));
            lmmattendance.mWithEntry = myDataRecord.GetInt32(myDataRecord.GetOrdinal("with_entry"));
            lmmattendance.mLmmName = myDataRecord.GetString(myDataRecord.GetOrdinal("lmm_name"));
            lmmattendance.mLmmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_id"));
            lmmattendance.mAttendanceDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("sales_date"));

            return lmmattendance;
        }
		private static LmmAttendance FillDataRecord(IDataRecord myDataRecord)
		{
			LmmAttendance lmmattendance = new LmmAttendance();

			lmmattendance.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			lmmattendance.mLmmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_id"));
			lmmattendance.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			lmmattendance.mCutoffId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("cutoff_id"));
			lmmattendance.mAttendanceTypeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("attendance_type_id"));
			lmmattendance.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			lmmattendance.mAttendanceDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("attendance_date"));
			lmmattendance.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			lmmattendance.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
			lmmattendance.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            lmmattendance.mEmployeeName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));
            lmmattendance.mBranchName = myDataRecord.GetString(myDataRecord.GetOrdinal("branch_name"));
            lmmattendance.mAgencyName = myDataRecord.GetString(myDataRecord.GetOrdinal("agency_id_name"));
            lmmattendance.mCode = myDataRecord.GetInt32(myDataRecord.GetOrdinal("code"));
            lmmattendance.mAttendanceTypeName = myDataRecord.GetString(myDataRecord.GetOrdinal("attendance_type_name"));
            lmmattendance.mLmmName = myDataRecord.GetString(myDataRecord.GetOrdinal("lmm_name"));

            return lmmattendance;
		}
	}
}