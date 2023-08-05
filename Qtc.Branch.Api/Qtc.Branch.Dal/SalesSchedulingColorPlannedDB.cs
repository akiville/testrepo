using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class SalesSchedulingColorPlannedDB
	{
		public static SalesSchedulingColorPlanned GetItem(int salesschedulingcolorplannedId)
		{
			SalesSchedulingColorPlanned salesschedulingcolorplanned = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spSalesSchedulingColorPlannedSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", salesschedulingcolorplannedId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						salesschedulingcolorplanned = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return salesschedulingcolorplanned;
		}

		public static SalesSchedulingColorPlannedCollection GetList(SalesSchedulingColorPlannedCriteria salesschedulingcolorplannedCriteria)
		{
			SalesSchedulingColorPlannedCollection tempList = new SalesSchedulingColorPlannedCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spSalesSchedulingColorPlannedSearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new SalesSchedulingColorPlannedCollection();
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

		public static int SelectCountForGetList(SalesSchedulingColorPlannedCriteria salesschedulingcolorplannedCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spSalesSchedulingColorPlannedSearchList";

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

		public static int Save(SalesSchedulingColorPlanned mySalesSchedulingColorPlanned)
		{
			if (!mySalesSchedulingColorPlanned.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a salesschedulingcolorplanned in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spSalesSchedulingColorPlannedInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@order_by", mySalesSchedulingColorPlanned.mOrderBy);
				Helpers.CreateParameter(myCommand, DbType.String, "@name", mySalesSchedulingColorPlanned.mName);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", mySalesSchedulingColorPlanned.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.String, "@back_color", mySalesSchedulingColorPlanned.mBackColor);
				Helpers.CreateParameter(myCommand, DbType.String, "@fore_color", mySalesSchedulingColorPlanned.mForeColor);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@required_rtw_no", mySalesSchedulingColorPlanned.mRequiredRtwNo);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@required_nov", mySalesSchedulingColorPlanned.mRequiredNov);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@is_working", mySalesSchedulingColorPlanned.mIsWorking);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@man_power_count", mySalesSchedulingColorPlanned.mManPowerCount);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_unlimited", mySalesSchedulingColorPlanned.mIsUnlimited);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@number_of_use_per_week", mySalesSchedulingColorPlanned.mNumberOfUsePerWeek);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_paid", mySalesSchedulingColorPlanned.mIsPaid);
				Helpers.CreateParameter(myCommand, DbType.String, "@back_color_hex", mySalesSchedulingColorPlanned.mBackColorHex);
				Helpers.CreateParameter(myCommand, DbType.String, "@for_color_hex", mySalesSchedulingColorPlanned.mForColorHex);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_active", mySalesSchedulingColorPlanned.mIsActive);

				Helpers.SetSaveParameters(myCommand, mySalesSchedulingColorPlanned);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update salesschedulingcolorplanned as it has been updated by someone else");
				}
				//mySalesSchedulingColorPlanned.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spSalesSchedulingColorPlannedDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static SalesSchedulingColorPlanned FillDataRecord(IDataRecord myDataRecord)
		{
			SalesSchedulingColorPlanned salesschedulingcolorplanned = new SalesSchedulingColorPlanned();

			salesschedulingcolorplanned.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			salesschedulingcolorplanned.mOrderBy = myDataRecord.GetInt32(myDataRecord.GetOrdinal("order_by"));
			salesschedulingcolorplanned.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));
			salesschedulingcolorplanned.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			salesschedulingcolorplanned.mBackColor = myDataRecord.GetString(myDataRecord.GetOrdinal("back_color"));
			salesschedulingcolorplanned.mForeColor = myDataRecord.GetString(myDataRecord.GetOrdinal("fore_color"));
			salesschedulingcolorplanned.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			salesschedulingcolorplanned.mRequiredRtwNo = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("required_rtw_no"));
			salesschedulingcolorplanned.mRequiredNov = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("required_nov"));
			salesschedulingcolorplanned.mIsWorking = myDataRecord.GetInt32(myDataRecord.GetOrdinal("is_working"));
			salesschedulingcolorplanned.mManPowerCount = myDataRecord.GetInt32(myDataRecord.GetOrdinal("man_power_count"));
			salesschedulingcolorplanned.mIsUnlimited = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_unlimited"));
			salesschedulingcolorplanned.mNumberOfUsePerWeek = myDataRecord.GetInt32(myDataRecord.GetOrdinal("number_of_use_per_week"));
			salesschedulingcolorplanned.mIsPaid = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_paid"));
			salesschedulingcolorplanned.mBackColorHex = myDataRecord.GetString(myDataRecord.GetOrdinal("back_color_hex"));
			salesschedulingcolorplanned.mForColorHex = myDataRecord.GetString(myDataRecord.GetOrdinal("for_color_hex"));
			salesschedulingcolorplanned.mIsActive = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_active"));

			return salesschedulingcolorplanned;
		}
	}
}