using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class AddBackDB
	{
		public static AddBack GetItem(int addbackId)
		{
			AddBack addback = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAddBackSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", addbackId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						addback = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return addback;
		}

		public static AddBackCollection GetList(AddBackCriteria addbackCriteria)
		{
			AddBackCollection tempList = new AddBackCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAddBackSearchList";

                Helpers.CreateParameter(myCommand, DbType.String, "@add_back_status", addbackCriteria.mAddBackStatus);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", addbackCriteria.mBranchId);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new AddBackCollection();
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

		public static int SelectCountForGetList(AddBackCriteria addbackCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAddBackSearchList";

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

		public static int Save(AddBack myAddBack)
		{
			if (!myAddBack.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a addback in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAddBackInsertUpdateSingleItem";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@product_avail_id", myAddBack.mProductAvailId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myAddBack.mBranchId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@personnel_id", myAddBack.mPersonnelId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@product_id", myAddBack.mProductId);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@sales_date", myAddBack.mSalesDate);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@add_back_qty", myAddBack.mAddBackQty);
                Helpers.CreateParameter(myCommand, DbType.String, "@add_back_reason", myAddBack.mAddBackReason);
                Helpers.CreateParameter(myCommand, DbType.String, "@add_back_status", myAddBack.mAddBackStatus);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@prior_qty", myAddBack.mPriorQty);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@avail_qty", myAddBack.mAvailQty);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@approved_by_id", myAddBack.mApprovedById);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@approval_date", myAddBack.mApprovalDate);
                Helpers.CreateParameter(myCommand, DbType.String, "@approval_remarks", myAddBack.mApprovalRemarks);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myAddBack.mDatestamp);

                Helpers.SetSaveParameters(myCommand, myAddBack);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update addback as it has been updated by someone else");
				}
				//myAddBack.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spAddBackDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static AddBack FillDataRecord(IDataRecord myDataRecord)
		{
			AddBack addback = new AddBack();

			addback.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			addback.mProductAvailId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("product_avail_id"));
			addback.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			addback.mPersonnelId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("personnel_id"));
			addback.mProductId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("product_id"));
			addback.mSalesDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("sales_date"));
			addback.mAddBackQty = myDataRecord.GetInt32(myDataRecord.GetOrdinal("add_back_qty"));
			addback.mAddBackReason = myDataRecord.GetString(myDataRecord.GetOrdinal("add_back_reason"));
			addback.mAddBackStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("add_back_status"));
			addback.mPriorQty = myDataRecord.GetInt32(myDataRecord.GetOrdinal("prior_qty"));
			addback.mAvailQty = myDataRecord.GetInt32(myDataRecord.GetOrdinal("avail_qty"));
			addback.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
			addback.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            addback.mBranch = myDataRecord.GetString(myDataRecord.GetOrdinal("branch"));
            addback.mProduct = myDataRecord.GetString(myDataRecord.GetOrdinal("product"));
            addback.mPersonnel = myDataRecord.GetString(myDataRecord.GetOrdinal("personnel"));
            addback.mApprovedById = myDataRecord.GetInt32(myDataRecord.GetOrdinal("approved_by_id"));
            addback.mApprovalDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("approval_date"));
            addback.mApprovalRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("approval_remarks"));
            
            //addback.mConcurrencyId = (byte[]) myDataRecord.GetValue(myDataRecord.GetOrdinal("concurrency_id"));

			return addback;
		}
	}
}