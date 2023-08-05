using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class SalesSchedulingReasonCodeDB
	{
		public static SalesSchedulingReasonCode GetItem(int salesschedulingreasoncodeId)
		{
			SalesSchedulingReasonCode salesschedulingreasoncode = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spSalesSchedulingReasonCodeSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", salesschedulingreasoncodeId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						salesschedulingreasoncode = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return salesschedulingreasoncode;
		}

		public static SalesSchedulingReasonCodeCollection GetList(SalesSchedulingReasonCodeCriteria salesschedulingreasoncodeCriteria)
		{
			SalesSchedulingReasonCodeCollection tempList = new SalesSchedulingReasonCodeCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spSalesSchedulingReasonCodeSearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new SalesSchedulingReasonCodeCollection();
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

		public static int SelectCountForGetList(SalesSchedulingReasonCodeCriteria salesschedulingreasoncodeCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spSalesSchedulingReasonCodeSearchList";

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

		public static int Save(SalesSchedulingReasonCode mySalesSchedulingReasonCode)
		{
			if (!mySalesSchedulingReasonCode.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a salesschedulingreasoncode in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spSalesSchedulingReasonCodeInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.String, "@name", mySalesSchedulingReasonCode.mName);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", mySalesSchedulingReasonCode.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@module_id", mySalesSchedulingReasonCode.mModuleId);

				Helpers.SetSaveParameters(myCommand, mySalesSchedulingReasonCode);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update salesschedulingreasoncode as it has been updated by someone else");
				}
				//mySalesSchedulingReasonCode.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spSalesSchedulingReasonCodeDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static SalesSchedulingReasonCode FillDataRecord(IDataRecord myDataRecord)
		{
			SalesSchedulingReasonCode salesschedulingreasoncode = new SalesSchedulingReasonCode();

			salesschedulingreasoncode.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			salesschedulingreasoncode.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));
			salesschedulingreasoncode.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			salesschedulingreasoncode.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			salesschedulingreasoncode.mModuleId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("module_id"));

			return salesschedulingreasoncode;
		}
	}
}