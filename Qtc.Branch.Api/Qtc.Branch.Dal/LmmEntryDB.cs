using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class LmmEntryDB
	{
		public static LmmEntry GetItem(int lmmentryId)
		{
			LmmEntry lmmentry = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLmmEntrySelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", lmmentryId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						lmmentry = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return lmmentry;
		}

		public static LmmEntryCollection GetList(LmmEntryCriteria lmmentryCriteria)
		{
			LmmEntryCollection tempList = new LmmEntryCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLmmEntrySearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new LmmEntryCollection();
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

		public static int SelectCountForGetList(LmmEntryCriteria lmmentryCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLmmEntrySearchList";

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

		public static int Save(LmmEntry myLmmEntry)
		{
			if (!myLmmEntry.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a lmmentry in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLmmEntryInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.String, "@type", myLmmEntry.mType);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", myLmmEntry.mLmmId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@with_entry", myLmmEntry.mWithEntry);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@total_item", myLmmEntry.mTotalItem);
				Helpers.CreateParameter(myCommand, DbType.String, "@lmm_name", myLmmEntry.mLmmName);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@sales_date", myLmmEntry.mSalesDate);

				Helpers.SetSaveParameters(myCommand, myLmmEntry);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update lmmentry as it has been updated by someone else");
				}
				//myLmmEntry.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spLmmEntryDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static LmmEntry FillDataRecord(IDataRecord myDataRecord)
		{
			LmmEntry lmmentry = new LmmEntry();

			lmmentry.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			lmmentry.mType = myDataRecord.GetString(myDataRecord.GetOrdinal("type"));
			lmmentry.mLmmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_id"));
			lmmentry.mWithEntry = myDataRecord.GetInt32(myDataRecord.GetOrdinal("with_entry"));
			lmmentry.mTotalItem = myDataRecord.GetInt32(myDataRecord.GetOrdinal("total_item"));
			lmmentry.mLmmName = myDataRecord.GetString(myDataRecord.GetOrdinal("lmm_name"));
			lmmentry.mSalesDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("sales_date"));
			lmmentry.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return lmmentry;
		}
	}
}