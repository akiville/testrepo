using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class BranchRequisitionDetailsDB
	{
		public static BranchRequisitionDetails GetItem(int branchrequisitiondetailsId)
		{
			BranchRequisitionDetails branchrequisitiondetails = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spBranchRequisitionDetailsSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", branchrequisitiondetailsId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						branchrequisitiondetails = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return branchrequisitiondetails;
		}

		public static BranchRequisitionDetailsCollection GetList(BranchRequisitionDetailsCriteria branchrequisitiondetailsCriteria)
		{
			BranchRequisitionDetailsCollection tempList = new BranchRequisitionDetailsCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spBranchRequisitionDetailsSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", branchrequisitiondetailsCriteria.mBranchId);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@sales_date", branchrequisitiondetailsCriteria.mSalesDate);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_requisition_id", branchrequisitiondetailsCriteria.mBranchRequisitionId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new BranchRequisitionDetailsCollection();
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

		public static int SelectCountForGetList(BranchRequisitionDetailsCriteria branchrequisitiondetailsCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spBranchRequisitionDetailsSearchList";

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

		public static int Save(BranchRequisitionDetails myBranchRequisitionDetails)
		{
			if (!myBranchRequisitionDetails.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a branchrequisitiondetails in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spBranchRequisitionDetailsInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_requisition_id", myBranchRequisitionDetails.mBranchRequisitionId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@product_id", myBranchRequisitionDetails.mProductId);
				Helpers.CreateParameter(myCommand, DbType.Decimal, "@avail_qty", myBranchRequisitionDetails.mAvailQty);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@user_id", myBranchRequisitionDetails.mUserId);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myBranchRequisitionDetails.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myBranchRequisitionDetails.mDatestamp);

				Helpers.SetSaveParameters(myCommand, myBranchRequisitionDetails);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update branchrequisitiondetails as it has been updated by someone else");
				}
				//myBranchRequisitionDetails.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spBranchRequisitionDetailsDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static BranchRequisitionDetails FillDataRecord(IDataRecord myDataRecord)
		{
			BranchRequisitionDetails branchrequisitiondetails = new BranchRequisitionDetails();

			branchrequisitiondetails.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			branchrequisitiondetails.mBranchRequisitionId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_requisition_id"));
			branchrequisitiondetails.mProductId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("product_id"));
			branchrequisitiondetails.mAvailQty = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("avail_qty"));
			branchrequisitiondetails.mUserId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("user_id"));
			branchrequisitiondetails.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			branchrequisitiondetails.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
			branchrequisitiondetails.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            branchrequisitiondetails.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));
            branchrequisitiondetails.mUnit = myDataRecord.GetString(myDataRecord.GetOrdinal("unit"));


            return branchrequisitiondetails;
		}
	}
}