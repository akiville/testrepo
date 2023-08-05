using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class EmployeeStatusEmploymentDB
	{
		public static EmployeeStatusEmployment GetItem(int employeestatusemploymentId)
		{
			EmployeeStatusEmployment employeestatusemployment = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spEmployeeStatusEmploymentSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", employeestatusemploymentId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						employeestatusemployment = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return employeestatusemployment;
		}

		public static EmployeeStatusEmploymentCollection GetList(EmployeeStatusEmploymentCriteria employeestatusemploymentCriteria)
		{
			EmployeeStatusEmploymentCollection tempList = new EmployeeStatusEmploymentCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spEmployeeStatusEmploymentSearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new EmployeeStatusEmploymentCollection();
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

		public static int SelectCountForGetList(EmployeeStatusEmploymentCriteria employeestatusemploymentCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spEmployeeStatusEmploymentSearchList";

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

		public static int Save(EmployeeStatusEmployment myEmployeeStatusEmployment)
		{
			if (!myEmployeeStatusEmployment.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a employeestatusemployment in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spEmployeeStatusEmploymentInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_created", myEmployeeStatusEmployment.mDateCreated);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myEmployeeStatusEmployment.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@status_id", myEmployeeStatusEmployment.mStatusId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@position_id", myEmployeeStatusEmployment.mPositionId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@department_id", myEmployeeStatusEmployment.mDepartmentId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@section_id", myEmployeeStatusEmployment.mSectionId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@lines_id", myEmployeeStatusEmployment.mLinesId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@group_head_count_id", myEmployeeStatusEmployment.mGroupHeadCountId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@group_payroll_id", myEmployeeStatusEmployment.mGroupPayrollId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@batch_no_id", myEmployeeStatusEmployment.mBatchNoId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myEmployeeStatusEmployment.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@tax_id", myEmployeeStatusEmployment.mTaxId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_applied", myEmployeeStatusEmployment.mDateApplied);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_hired", myEmployeeStatusEmployment.mDateHired);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_regularized", myEmployeeStatusEmployment.mDateRegularized);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_orientation", myEmployeeStatusEmployment.mDateOrientation);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_orientation_to", myEmployeeStatusEmployment.mDateOrientationTo);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_extention", myEmployeeStatusEmployment.mDateExtention);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_last_attendance", myEmployeeStatusEmployment.mDateLastAttendance);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_clearance", myEmployeeStatusEmployment.mDateClearance);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_separated", myEmployeeStatusEmployment.mDateSeparated);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_type_id", myEmployeeStatusEmployment.mEmployeeTypeId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@agency_id", myEmployeeStatusEmployment.mAgencyId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@process_type_id", myEmployeeStatusEmployment.mProcessTypeId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@shuttle_id", myEmployeeStatusEmployment.mShuttleId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@separated_type_id", myEmployeeStatusEmployment.mSeparatedTypeId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@applicant_category_id", myEmployeeStatusEmployment.mApplicantCategoryId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_current", myEmployeeStatusEmployment.mIsCurrent);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@reason_code_id", myEmployeeStatusEmployment.mReasonCodeId);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myEmployeeStatusEmployment.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@reason_leaving_id", myEmployeeStatusEmployment.mReasonLeavingId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@agency_id_ref", myEmployeeStatusEmployment.mAgencyIdRef);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myEmployeeStatusEmployment.mRecordId);

				Helpers.SetSaveParameters(myCommand, myEmployeeStatusEmployment);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update employeestatusemployment as it has been updated by someone else");
				}
				//myEmployeeStatusEmployment.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spEmployeeStatusEmploymentDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static EmployeeStatusEmployment FillDataRecord(IDataRecord myDataRecord)
		{
			EmployeeStatusEmployment employeestatusemployment = new EmployeeStatusEmployment();

			employeestatusemployment.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			employeestatusemployment.mDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_created"));
			employeestatusemployment.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			employeestatusemployment.mStatusId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("status_id"));
			employeestatusemployment.mPositionId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("position_id"));
			employeestatusemployment.mDepartmentId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("department_id"));
			employeestatusemployment.mSectionId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("section_id"));
			employeestatusemployment.mLinesId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lines_id"));
			employeestatusemployment.mGroupHeadCountId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("group_head_count_id"));
			employeestatusemployment.mGroupPayrollId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("group_payroll_id"));
			employeestatusemployment.mBatchNoId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("batch_no_id"));
			employeestatusemployment.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			employeestatusemployment.mTaxId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("tax_id"));
			employeestatusemployment.mDateApplied = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_applied"));
			employeestatusemployment.mDateHired = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_hired"));
			employeestatusemployment.mDateRegularized = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_regularized"));
			employeestatusemployment.mDateOrientation = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_orientation"));
			employeestatusemployment.mDateOrientationTo = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_orientation_to"));
			employeestatusemployment.mDateExtention = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_extention"));
			employeestatusemployment.mDateLastAttendance = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_last_attendance"));
			employeestatusemployment.mDateClearance = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_clearance"));
			employeestatusemployment.mDateSeparated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_separated"));
			employeestatusemployment.mEmployeeTypeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_type_id"));
			employeestatusemployment.mAgencyId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("agency_id"));
			employeestatusemployment.mProcessTypeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("process_type_id"));
			employeestatusemployment.mShuttleId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("shuttle_id"));
			employeestatusemployment.mSeparatedTypeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("separated_type_id"));
			employeestatusemployment.mApplicantCategoryId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("applicant_category_id"));
			employeestatusemployment.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			employeestatusemployment.mIsCurrent = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_current"));
			employeestatusemployment.mReasonCodeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("reason_code_id"));
			employeestatusemployment.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			employeestatusemployment.mReasonLeavingId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("reason_leaving_id"));
			employeestatusemployment.mAgencyIdRef = myDataRecord.GetInt32(myDataRecord.GetOrdinal("agency_id_ref"));
			employeestatusemployment.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));

			return employeestatusemployment;
		}
	}
}