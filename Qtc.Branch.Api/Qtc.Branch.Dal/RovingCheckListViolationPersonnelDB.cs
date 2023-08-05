using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class RovingCheckListViolationPersonnelDB
	{
		public static RovingCheckListViolationPersonnel GetItem(int rovingchecklistviolationpersonnelId)
		{
			RovingCheckListViolationPersonnel rovingchecklistviolationpersonnel = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRovingCheckListViolationPersonnelSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", rovingchecklistviolationpersonnelId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						rovingchecklistviolationpersonnel = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return rovingchecklistviolationpersonnel;
		}

		public static RovingCheckListViolationPersonnelCollection GetList(RovingCheckListViolationPersonnelCriteria rovingchecklistviolationpersonnelCriteria)
		{
			RovingCheckListViolationPersonnelCollection tempList = new RovingCheckListViolationPersonnelCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRovingCheckListViolationPersonnelSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@rps_id", rovingchecklistviolationpersonnelCriteria.mRpsId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@rps_chklist_id", rovingchecklistviolationpersonnelCriteria.mRpsChklistId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@violation_id", rovingchecklistviolationpersonnelCriteria.mViolationId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@rclvd_detail_id", rovingchecklistviolationpersonnelCriteria.mRclvdDetailId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@roving_checklist_violation_id", rovingchecklistviolationpersonnelCriteria.mRovingChecklistViolationId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", rovingchecklistviolationpersonnelCriteria.mEmployeeId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new RovingCheckListViolationPersonnelCollection();
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

		public static int SelectCountForGetList(RovingCheckListViolationPersonnelCriteria rovingchecklistviolationpersonnelCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRovingCheckListViolationPersonnelSearchList";

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

		public static int Save(RovingCheckListViolationPersonnel myRovingCheckListViolationPersonnel)
		{
			if (!myRovingCheckListViolationPersonnel.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a rovingchecklistviolationpersonnel in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRovingCheckListViolationPersonnelInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@rps_id", myRovingCheckListViolationPersonnel.mRpsId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@rps_chklist_id", myRovingCheckListViolationPersonnel.mRpsChklistId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@violation_id", myRovingCheckListViolationPersonnel.mViolationId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@rclvd_detail_id", myRovingCheckListViolationPersonnel.mRclvdDetailId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@roving_checklist_violation_id", myRovingCheckListViolationPersonnel.mRovingChecklistViolationId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myRovingCheckListViolationPersonnel.mEmployeeId);

				Helpers.SetSaveParameters(myCommand, myRovingCheckListViolationPersonnel);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update rovingchecklistviolationpersonnel as it has been updated by someone else");
				}
				//myRovingCheckListViolationPersonnel.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "ptApi_spRovingCheckListViolationPersonnelDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static RovingCheckListViolationPersonnel FillDataRecord(IDataRecord myDataRecord)
		{
			RovingCheckListViolationPersonnel rovingchecklistviolationpersonnel = new RovingCheckListViolationPersonnel();

			rovingchecklistviolationpersonnel.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			rovingchecklistviolationpersonnel.mRpsId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("rps_id"));
			rovingchecklistviolationpersonnel.mRpsChklistId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("rps_chklist_id"));
			rovingchecklistviolationpersonnel.mViolationId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("violation_id"));
			rovingchecklistviolationpersonnel.mRclvdDetailId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("rclvd_detail_id"));
			rovingchecklistviolationpersonnel.mRovingChecklistViolationId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("roving_checklist_violation_id"));
			rovingchecklistviolationpersonnel.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			rovingchecklistviolationpersonnel.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return rovingchecklistviolationpersonnel;
		}
	}
}