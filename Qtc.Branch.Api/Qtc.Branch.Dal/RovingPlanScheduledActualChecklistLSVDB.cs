using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class RovingPlanScheduledActualChecklistLSVDB
	{
		public static RovingPlanScheduledActualChecklistLSV GetItem(int rovingplanscheduledactualchecklistlsvId)
		{
			RovingPlanScheduledActualChecklistLSV rovingplanscheduledactualchecklistlsv = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRovingPlanScheduledActualChecklistLSVSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", rovingplanscheduledactualchecklistlsvId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						rovingplanscheduledactualchecklistlsv = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return rovingplanscheduledactualchecklistlsv;
		}

		public static RovingPlanScheduledActualChecklistLSVCollection GetList(RovingPlanScheduledActualChecklistLSVCriteria rovingplanscheduledactualchecklistlsvCriteria)
		{
			RovingPlanScheduledActualChecklistLSVCollection tempList = new RovingPlanScheduledActualChecklistLSVCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRovingPlanScheduledActualChecklistLSVSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@rps_id", rovingplanscheduledactualchecklistlsvCriteria.mRpsId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@rps_chklist_id", rovingplanscheduledactualchecklistlsvCriteria.mRpsChklistId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new RovingPlanScheduledActualChecklistLSVCollection();
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

		public static int SelectCountForGetList(RovingPlanScheduledActualChecklistLSVCriteria rovingplanscheduledactualchecklistlsvCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRovingPlanScheduledActualChecklistLSVSearchList";

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

		public static int Save(RovingPlanScheduledActualChecklistLSV myRovingPlanScheduledActualChecklistLSV)
		{
			if (!myRovingPlanScheduledActualChecklistLSV.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a rovingplanscheduledactualchecklistlsv in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingPlanScheduledActualChecklistLSVInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myRovingPlanScheduledActualChecklistLSV.mRecordId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@rps_id", myRovingPlanScheduledActualChecklistLSV.mRpsId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@rps_chklist_id", myRovingPlanScheduledActualChecklistLSV.mRpsChklistId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@violation_id", myRovingPlanScheduledActualChecklistLSV.mViolationId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@with_violation", myRovingPlanScheduledActualChecklistLSV.mWithViolation);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@lost_sales", myRovingPlanScheduledActualChecklistLSV.mLostSales);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myRovingPlanScheduledActualChecklistLSV.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.String, "@explanation", myRovingPlanScheduledActualChecklistLSV.mExplanation);

				Helpers.SetSaveParameters(myCommand, myRovingPlanScheduledActualChecklistLSV);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update rovingplanscheduledactualchecklistlsv as it has been updated by someone else");
				}
				//myRovingPlanScheduledActualChecklistLSV.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "ptApi_spRovingPlanScheduledActualChecklistLSVDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static RovingPlanScheduledActualChecklistLSV FillDataRecord(IDataRecord myDataRecord)
		{
			RovingPlanScheduledActualChecklistLSV rovingplanscheduledactualchecklistlsv = new RovingPlanScheduledActualChecklistLSV();

			rovingplanscheduledactualchecklistlsv.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			rovingplanscheduledactualchecklistlsv.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));
			rovingplanscheduledactualchecklistlsv.mRpsId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("rps_id"));
			rovingplanscheduledactualchecklistlsv.mRpsChklistId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("rps_chklist_id"));
			rovingplanscheduledactualchecklistlsv.mViolationId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("violation_id"));
			rovingplanscheduledactualchecklistlsv.mWithViolation = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("with_violation"));
			rovingplanscheduledactualchecklistlsv.mLostSales = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("lost_sales"));
			rovingplanscheduledactualchecklistlsv.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			rovingplanscheduledactualchecklistlsv.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			rovingplanscheduledactualchecklistlsv.mExplanation = myDataRecord.GetString(myDataRecord.GetOrdinal("explanation"));
            rovingplanscheduledactualchecklistlsv.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));


            return rovingplanscheduledactualchecklistlsv;
		}
	}
}