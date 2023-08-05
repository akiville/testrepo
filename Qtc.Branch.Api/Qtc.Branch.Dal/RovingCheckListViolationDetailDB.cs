using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class RovingCheckListViolationDetailDB
	{
		public static RovingCheckListViolationDetail GetItem(int rovingchecklistviolationdetailId)
		{
			RovingCheckListViolationDetail rovingchecklistviolationdetail = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRovingCheckListViolationDetailSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", rovingchecklistviolationdetailId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						rovingchecklistviolationdetail = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return rovingchecklistviolationdetail;
		}

		public static RovingCheckListViolationDetailCollection GetList(RovingCheckListViolationDetailCriteria rovingchecklistviolationdetailCriteria)
		{
			RovingCheckListViolationDetailCollection tempList = new RovingCheckListViolationDetailCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRovingCheckListViolationDetailSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@rps_id", rovingchecklistviolationdetailCriteria.mRpsId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@rps_chklist_id", rovingchecklistviolationdetailCriteria.mRpsChklistId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@violation_id", rovingchecklistviolationdetailCriteria.mViolationId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new RovingCheckListViolationDetailCollection();
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

		public static int SelectCountForGetList(RovingCheckListViolationDetailCriteria rovingchecklistviolationdetailCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRovingCheckListViolationDetailSearchList";

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

		public static int Save(RovingCheckListViolationDetail myRovingCheckListViolationDetail)
		{
			if (!myRovingCheckListViolationDetail.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a rovingchecklistviolationdetail in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingCheckListViolationDetailInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@roving_checklist_violation_id", myRovingCheckListViolationDetail.mRovingChecklistViolationId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@rps_id", myRovingCheckListViolationDetail.mRpsId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@rps_chklist_id", myRovingCheckListViolationDetail.mRpsChklistId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@violation_id", myRovingCheckListViolationDetail.mViolationId);
				Helpers.CreateParameter(myCommand, DbType.String, "@image_file_name", myRovingCheckListViolationDetail.mImageFileName);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myRovingCheckListViolationDetail.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myRovingCheckListViolationDetail.mRecordId);

				Helpers.SetSaveParameters(myCommand, myRovingCheckListViolationDetail);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update rovingchecklistviolationdetail as it has been updated by someone else");
				}
				//myRovingCheckListViolationDetail.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "ptApi_spRovingCheckListViolationDetailDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static RovingCheckListViolationDetail FillDataRecord(IDataRecord myDataRecord)
		{
			RovingCheckListViolationDetail rovingchecklistviolationdetail = new RovingCheckListViolationDetail();

			rovingchecklistviolationdetail.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			rovingchecklistviolationdetail.mRovingChecklistViolationId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("roving_checklist_violation_id"));
			rovingchecklistviolationdetail.mRpsId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("rps_id"));
			rovingchecklistviolationdetail.mRpsChklistId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("rps_chklist_id"));
			rovingchecklistviolationdetail.mViolationId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("violation_id"));
			rovingchecklistviolationdetail.mImageFileName = myDataRecord.GetString(myDataRecord.GetOrdinal("image_file_name"));
			rovingchecklistviolationdetail.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			rovingchecklistviolationdetail.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));
			rovingchecklistviolationdetail.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return rovingchecklistviolationdetail;
		}
	}
}