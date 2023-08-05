using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class EmployeeDB
	{
		public static Employee GetItem(int employeeId)
		{
			Employee employee = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spEmployeeSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", employeeId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						employee = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return employee;
		}

        public static EmployeeCollection GetListWithDate(EmployeeCriteria employeeCriteria)
        {
            EmployeeCollection tempList = new EmployeeCollection();
            using (DbCommand myCommand = AppConfiguration.CreateCommand())
            {
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Qt_spEmployeeSearchLisyBySalesScheduleDate";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@tracking_no", employeeCriteria.mTrackingNo);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_code", employeeCriteria.mBranchCode);
                Helpers.CreateParameter(myCommand, DbType.String, "@password", employeeCriteria.mPassword);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@sales_Schedule_date", employeeCriteria.mSalesScheduleDate);

                myCommand.Connection.Open();
                using (DbDataReader myReader = myCommand.ExecuteReader())
                {
                    if (myReader.HasRows)
                    {
                        tempList = new EmployeeCollection();
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

        public static EmployeeCollection GetList(EmployeeCriteria employeeCriteria)
		{
			EmployeeCollection tempList = new EmployeeCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spEmployeeSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@tracking_no", employeeCriteria.mTrackingNo);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_code", employeeCriteria.mBranchCode);
                Helpers.CreateParameter(myCommand, DbType.String, "@password", employeeCriteria.mPassword);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new EmployeeCollection();
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

		public static int SelectCountForGetList(EmployeeCriteria employeeCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spEmployeeSearchList";

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

		public static int Save(Employee myEmployee)
		{
			if (!myEmployee.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a employee in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spEmployeeInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myEmployee.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.String, "@lastname", myEmployee.mLastname);
				Helpers.CreateParameter(myCommand, DbType.String, "@firstname", myEmployee.mFirstname);
				Helpers.CreateParameter(myCommand, DbType.String, "@middlename", myEmployee.mMiddlename);
				Helpers.CreateParameter(myCommand, DbType.String, "@position_name", myEmployee.mPositionName);
				Helpers.CreateParameter(myCommand, DbType.String, "@cellphone_no", myEmployee.mCellphoneNo);
				Helpers.CreateParameter(myCommand, DbType.String, "@residential_address", myEmployee.mResidentialAddress);
				Helpers.CreateParameter(myCommand, DbType.String, "@password", myEmployee.mPassword);

				Helpers.SetSaveParameters(myCommand, myEmployee);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update employee as it has been updated by someone else");
				}
				//myEmployee.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spEmployeeDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();  
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static Employee FillDataRecord(IDataRecord myDataRecord)
		{
			Employee employee = new Employee();

			employee.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			employee.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			employee.mLastname = myDataRecord.GetString(myDataRecord.GetOrdinal("lastname"));
			employee.mFirstname = myDataRecord.GetString(myDataRecord.GetOrdinal("firstname"));
			employee.mMiddlename = myDataRecord.GetString(myDataRecord.GetOrdinal("middlename"));
			employee.mPositionName = myDataRecord.GetString(myDataRecord.GetOrdinal("position_name"));
			employee.mCellphoneNo = myDataRecord.GetString(myDataRecord.GetOrdinal("cellphone_no"));
			employee.mResidentialAddress = myDataRecord.GetString(myDataRecord.GetOrdinal("residential_address"));
			employee.mPassword = myDataRecord.GetString(myDataRecord.GetOrdinal("password"));
			employee.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            employee.mBranchCode = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_code"));
            employee.mIsLoggedIn = myDataRecord.GetInt32(myDataRecord.GetOrdinal("is_logged_in"));
            employee.mLastLogin = myDataRecord.GetString(myDataRecord.GetOrdinal("last_login"));
            employee.mBranchName = myDataRecord.GetString(myDataRecord.GetOrdinal("branch_name"));
            employee.mAgency = myDataRecord.GetString(myDataRecord.GetOrdinal("agency"));
            employee.mAreaName = myDataRecord.GetString(myDataRecord.GetOrdinal("area_name"));
            employee.mLmmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_id"));
            employee.mTimeIn = myDataRecord.GetString(myDataRecord.GetOrdinal("time_in"));
            employee.mTimeOut = myDataRecord.GetString(myDataRecord.GetOrdinal("time_out"));
            employee.mAgencyId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("agency_id"));

            //employee.mConcurrencyId = (byte[]) myDataRecord.GetValue(myDataRecord.GetOrdinal("concurrency_id"));

            return employee;
		}
	}
}