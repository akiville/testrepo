using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class RovingPlanScheduledActualChecklistImageDB
	{
		public static RovingPlanScheduledActualChecklistImage GetItem(int rovingplanscheduledactualchecklistimageId)
		{
			RovingPlanScheduledActualChecklistImage rovingplanscheduledactualchecklistimage = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingPlanScheduledActualChecklistImageSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", rovingplanscheduledactualchecklistimageId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						rovingplanscheduledactualchecklistimage = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return rovingplanscheduledactualchecklistimage;
		}

		public static RovingPlanScheduledActualChecklistImageCollection GetList(RovingPlanScheduledActualChecklistImageCriteria rovingplanscheduledactualchecklistimageCriteria)
		{
			RovingPlanScheduledActualChecklistImageCollection tempList = new RovingPlanScheduledActualChecklistImageCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingPlanScheduledActualChecklistImageSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@roving_plan_schedule_actual_checklist", rovingplanscheduledactualchecklistimageCriteria.mRovingChecklistActualId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new RovingPlanScheduledActualChecklistImageCollection();
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

		public static int SelectCountForGetList(RovingPlanScheduledActualChecklistImageCriteria rovingplanscheduledactualchecklistimageCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingPlanScheduledActualChecklistImageSearchList";

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

		public static int Save(RovingPlanScheduledActualChecklistImage myRovingPlanScheduledActualChecklistImage)
		{
			if (!myRovingPlanScheduledActualChecklistImage.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a rovingplanscheduledactualchecklistimage in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingPlanScheduledActualChecklistImageInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@roving_checklist_actual_id", myRovingPlanScheduledActualChecklistImage.mRovingChecklistActualId);
				Helpers.CreateParameter(myCommand, DbType.String, "@image_url", myRovingPlanScheduledActualChecklistImage.mImageUrl);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myRovingPlanScheduledActualChecklistImage.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myRovingPlanScheduledActualChecklistImage.mDatestamp);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myRovingPlanScheduledActualChecklistImage.mRecordId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@rps_id", myRovingPlanScheduledActualChecklistImage.mRpsId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@rc_id", myRovingPlanScheduledActualChecklistImage.mRcId);

				Helpers.SetSaveParameters(myCommand, myRovingPlanScheduledActualChecklistImage);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update rovingplanscheduledactualchecklistimage as it has been updated by someone else");
				}
				//myRovingPlanScheduledActualChecklistImage.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spRovingPlanScheduledActualChecklistImageDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static RovingPlanScheduledActualChecklistImage FillDataRecord(IDataRecord myDataRecord)
		{
			RovingPlanScheduledActualChecklistImage rovingplanscheduledactualchecklistimage = new RovingPlanScheduledActualChecklistImage();

			rovingplanscheduledactualchecklistimage.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			rovingplanscheduledactualchecklistimage.mRovingChecklistActualId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("roving_checklist_actual_id"));
			rovingplanscheduledactualchecklistimage.mImageUrl = myDataRecord.GetString(myDataRecord.GetOrdinal("image_url"));
			rovingplanscheduledactualchecklistimage.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			rovingplanscheduledactualchecklistimage.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			rovingplanscheduledactualchecklistimage.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
			rovingplanscheduledactualchecklistimage.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));
			rovingplanscheduledactualchecklistimage.mRpsId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("rps_id"));
			rovingplanscheduledactualchecklistimage.mRcId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("rc_id"));

			return rovingplanscheduledactualchecklistimage;
		}
	}
}