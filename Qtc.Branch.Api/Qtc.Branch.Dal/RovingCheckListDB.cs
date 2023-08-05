using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class RovingCheckListDB
	{
		public static RovingCheckList GetItem(int rovingchecklistId)
		{
			RovingCheckList rovingchecklist = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingCheckListSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", rovingchecklistId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						rovingchecklist = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return rovingchecklist;
		}

		public static RovingCheckListCollection GetList(RovingCheckListCriteria rovingchecklistCriteria)
		{
			RovingCheckListCollection tempList = new RovingCheckListCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingCheckListSearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new RovingCheckListCollection();
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

		public static int SelectCountForGetList(RovingCheckListCriteria rovingchecklistCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingCheckListSearchList";

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

		public static int Save(RovingCheckList myRovingCheckList)
		{
			if (!myRovingCheckList.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a rovingchecklist in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingCheckListInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myRovingCheckList.mRecordId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@checklist_category_id", myRovingCheckList.mChecklistCategoryId);
				Helpers.CreateParameter(myCommand, DbType.String, "@name", myRovingCheckList.mName);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myRovingCheckList.mRemarks);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "@disable", myRovingCheckList.mDisable);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@points", myRovingCheckList.mPoints);
				Helpers.SetSaveParameters(myCommand, myRovingCheckList);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update rovingchecklist as it has been updated by someone else");
				}
				//myRovingCheckList.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spRovingCheckListDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static RovingCheckList FillDataRecord(IDataRecord myDataRecord)
		{
			RovingCheckList rovingchecklist = new RovingCheckList();

			rovingchecklist.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			rovingchecklist.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));
			rovingchecklist.mChecklistCategoryId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("checklist_category_id"));
			rovingchecklist.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));
			rovingchecklist.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			rovingchecklist.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            rovingchecklist.mChecklistCategoryIdName = myDataRecord.GetString(myDataRecord.GetOrdinal("checklist_category_id_name"));
            rovingchecklist.mPoints = myDataRecord.GetInt32(myDataRecord.GetOrdinal("points"));
			return rovingchecklist;
		}
	}
}