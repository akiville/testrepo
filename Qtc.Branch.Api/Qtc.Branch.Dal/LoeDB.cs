using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class LoeDB
	{
		public static Loe GetItem(int loeId)
		{
			Loe loe = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLoeSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", loeId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						loe = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return loe;
		}

		public static LoeCollection GetList(LoeCriteria loeCriteria)
		{
			LoeCollection tempList = new LoeCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLoeSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", loeCriteria.mBranchId);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new LoeCollection();
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

		public static int SelectCountForGetList(LoeCriteria loeCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLoeSearchList";

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

		public static int Save(Loe myLoe)
		{
			if (!myLoe.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a loe in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLoeInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@loe_id", myLoe.mLoeId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@sale_date", myLoe.mSaleDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@number", myLoe.mNumber);
				Helpers.CreateParameter(myCommand, DbType.Int16, "@audit_number", myLoe.mAuditNumber);
				Helpers.CreateParameter(myCommand, DbType.Int16, "@rfl_no", myLoe.mRflNo);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myLoe.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@type", myLoe.mType);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@mc_id", myLoe.mMcId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@requested_by_id", myLoe.mRequestedById);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@transacted_by_id", myLoe.mTransactedById);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@approved_by_id", myLoe.mApprovedById);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@code_id", myLoe.mCodeId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@audited_by_id", myLoe.mAuditedById);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myLoe.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.String, "@comment", myLoe.mComment);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@dis_approved", myLoe.mDisApproved);
				Helpers.CreateParameter(myCommand, DbType.String, "@dis_approved_reason", myLoe.mDisApprovedReason);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@witness_no", myLoe.mWitnessNo);
				Helpers.CreateParameter(myCommand, DbType.String, "@witness_name", myLoe.mWitnessName);
				Helpers.CreateParameter(myCommand, DbType.Double, "@amount", myLoe.mAmount);
				Helpers.CreateParameter(myCommand, DbType.String, "@loe_no_mc_bag", myLoe.mLoeNoMcBag);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@reason_id", myLoe.mReasonId);
				Helpers.CreateParameter(myCommand, DbType.String, "@reconcilling_form_no", myLoe.mReconcillingFormNo);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@aloe", myLoe.mAloe);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@xaloe", myLoe.mXaloe);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@reject", myLoe.mReject);
				Helpers.CreateParameter(myCommand, DbType.Byte, "@guest_count", myLoe.mGuestCount);

				Helpers.SetSaveParameters(myCommand, myLoe);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update loe as it has been updated by someone else");
				}
				//myLoe.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spLoeDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static Loe FillDataRecord(IDataRecord myDataRecord)
		{
			Loe loe = new Loe();

			loe.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			loe.mLoeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("loe_id"));
			loe.mSaleDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("sale_date"));
			loe.mNumber = myDataRecord.GetInt32(myDataRecord.GetOrdinal("number"));
			loe.mAuditNumber = myDataRecord.GetInt16(myDataRecord.GetOrdinal("audit_number"));
			loe.mRflNo = myDataRecord.GetInt16(myDataRecord.GetOrdinal("rfl_no"));
			loe.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			loe.mType = myDataRecord.GetInt32(myDataRecord.GetOrdinal("type"));
			loe.mMcId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("mc_id"));
			loe.mRequestedById = myDataRecord.GetInt32(myDataRecord.GetOrdinal("requested_by_id"));
			loe.mTransactedById = myDataRecord.GetInt32(myDataRecord.GetOrdinal("transacted_by_id"));
			loe.mApprovedById = myDataRecord.GetInt32(myDataRecord.GetOrdinal("approved_by_id"));
			loe.mCodeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("code_id"));
			loe.mAuditedById = myDataRecord.GetInt32(myDataRecord.GetOrdinal("audited_by_id"));
			loe.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			loe.mComment = myDataRecord.GetString(myDataRecord.GetOrdinal("comment"));
			loe.mDisApproved = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("dis_approved"));
			loe.mDisApprovedReason = myDataRecord.GetString(myDataRecord.GetOrdinal("dis_approved_reason"));
			loe.mWitnessNo = myDataRecord.GetInt32(myDataRecord.GetOrdinal("witness_no"));
			loe.mWitnessName = myDataRecord.GetString(myDataRecord.GetOrdinal("witness_name"));
			loe.mAmount = myDataRecord.GetDouble(myDataRecord.GetOrdinal("amount"));
			loe.mLoeNoMcBag = myDataRecord.GetString(myDataRecord.GetOrdinal("loe_no_mc_bag"));
			loe.mReasonId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("reason_id"));
			loe.mReconcillingFormNo = myDataRecord.GetString(myDataRecord.GetOrdinal("reconcilling_form_no"));
			loe.mAloe = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("aloe"));
			loe.mXaloe = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("xaloe"));
			loe.mReject = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("reject"));
			loe.mGuestCount = myDataRecord.GetByte(myDataRecord.GetOrdinal("guest_count"));
			loe.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            loe.mRequestendByName = myDataRecord.GetString(myDataRecord.GetOrdinal("requested_by_name"));
			return loe;
		}
	}
}