using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class OperationReportDB
	{
		public static OperationReport GetItem(int operationreportId)
		{
			OperationReport operationreport = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spOperationReportSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", operationreportId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						operationreport = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return operationreport;
		}

		public static OperationReportCollection GetList(OperationReportCriteria operationreportCriteria)
		{
			OperationReportCollection tempList = new OperationReportCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spOperationReportSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", operationreportCriteria.mLmId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", operationreportCriteria.mBranchId);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@start_date", operationreportCriteria.mStartDate);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@end_date", operationreportCriteria.mEndDate);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new OperationReportCollection();
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

		public static int SelectCountForGetList(OperationReportCriteria operationreportCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spOperationReportSearchList";

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

		public static int Save(OperationReport myOperationReport)
		{
			if (!myOperationReport.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a operationreport in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spOperationReportInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.DateTime, "@concern_date", myOperationReport.mConcernDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myOperationReport.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@lm_id", myOperationReport.mLmId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myOperationReport.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.String, "@concern", myOperationReport.mConcern);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_filed", myOperationReport.mDateFiled);
				Helpers.CreateParameter(myCommand, DbType.String, "@image_path", myOperationReport.mImagePath);
				Helpers.CreateParameter(myCommand, DbType.String, "@image_name", myOperationReport.mImageName);

				Helpers.SetSaveParameters(myCommand, myOperationReport);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update operationreport as it has been updated by someone else");
				}
				//myOperationReport.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spOperationReportDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static OperationReport FillDataRecord(IDataRecord myDataRecord)
		{
			OperationReport operationreport = new OperationReport();

			operationreport.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			operationreport.mConcernDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("concern_date"));
			operationreport.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			operationreport.mLmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lm_id"));
			operationreport.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			operationreport.mConcern = myDataRecord.GetString(myDataRecord.GetOrdinal("concern"));
			operationreport.mDateFiled = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_filed"));
			operationreport.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			operationreport.mImagePath = myDataRecord.GetString(myDataRecord.GetOrdinal("image_path"));
			operationreport.mImageName = myDataRecord.GetString(myDataRecord.GetOrdinal("image_name"));

			return operationreport;
		}
	}
}