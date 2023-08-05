using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class IbwDetailDB
	{
		public static IbwDetail GetItem(int ibwdetailId)
		{
			IbwDetail ibwdetail = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spIbwDetailSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", ibwdetailId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						ibwdetail = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return ibwdetail;
		}

		public static IbwDetailCollection GetList(IbwDetailCriteria ibwdetailCriteria)
		{
			IbwDetailCollection tempList = new IbwDetailCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spIbwDetailSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@ibw_id", ibwdetailCriteria.mIbwId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new IbwDetailCollection();
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

		public static int SelectCountForGetList(IbwDetailCriteria ibwdetailCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spIbwDetailSearchList";

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

		public static int Save(IbwDetail myIbwDetail)
		{
			if (!myIbwDetail.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a ibwdetail in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spIbwDetailInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@ibw_id", myIbwDetail.mIbwId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@product_id", myIbwDetail.mProductId);
				Helpers.CreateParameter(myCommand, DbType.Double, "@quantity", myIbwDetail.mQuantity);
				Helpers.CreateParameter(myCommand, DbType.Double, "@checked_quantity", myIbwDetail.mCheckedQuantity);
				Helpers.CreateParameter(myCommand, DbType.String, "@nov_no", myIbwDetail.mNovNo);

				Helpers.SetSaveParameters(myCommand, myIbwDetail);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update ibwdetail as it has been updated by someone else");
				}
				//myIbwDetail.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spIbwDetailDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static IbwDetail FillDataRecord(IDataRecord myDataRecord)
		{
			IbwDetail ibwdetail = new IbwDetail();

			ibwdetail.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			ibwdetail.mIbwId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ibw_id"));
			ibwdetail.mProductId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("product_id"));
			ibwdetail.mQuantity = myDataRecord.GetDouble(myDataRecord.GetOrdinal("quantity"));
			ibwdetail.mCheckedQuantity = myDataRecord.GetDouble(myDataRecord.GetOrdinal("checked_quantity"));
			ibwdetail.mNovNo = myDataRecord.GetString(myDataRecord.GetOrdinal("nov_no"));
			ibwdetail.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            ibwdetail.mProductName = myDataRecord.GetString(myDataRecord.GetOrdinal("product_name"));

            return ibwdetail;
		}
	}
}