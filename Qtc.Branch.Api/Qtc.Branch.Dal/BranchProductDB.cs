using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class BranchProductDB
	{
		public static BranchProduct GetItem(int branchproductId)
		{
			BranchProduct branchproduct = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spBranchProductSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", branchproductId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						branchproduct = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return branchproduct;
		}

		public static BranchProductCollection GetList(BranchProductCriteria branchproductCriteria)
		{
			BranchProductCollection tempList = new BranchProductCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spBranchProductSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", branchproductCriteria.mLmmId);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new BranchProductCollection();
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

		public static int SelectCountForGetList(BranchProductCriteria branchproductCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spBranchProductSearchList";

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

		public static int Save(BranchProduct myBranchProduct)
		{
			if (!myBranchProduct.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a branchproduct in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spBranchProductInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myBranchProduct.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@product_id", myBranchProduct.mProductId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@user_id", myBranchProduct.mUserId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myBranchProduct.mDatestamp);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "@allow_ibw", myBranchProduct.mAllowIbw);

				Helpers.SetSaveParameters(myCommand, myBranchProduct);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update branchproduct as it has been updated by someone else");
				}
				//myBranchProduct.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spBranchProductDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static BranchProduct FillDataRecord(IDataRecord myDataRecord)
		{
			BranchProduct branchproduct = new BranchProduct();

			branchproduct.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			branchproduct.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			branchproduct.mProductId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("product_id"));
			branchproduct.mUserId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("user_id"));
			branchproduct.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
			branchproduct.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            branchproduct.mAllowIbw = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("allow_ibw"));
			//branchproduct.mConcurrencyId = (byte[]) myDataRecord.GetValue(myDataRecord.GetOrdinal("concurrency_id"));

			return branchproduct;
		}
	}
}