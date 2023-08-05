using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class DisseminatedLetterSmsReplyDB
	{
		public static DisseminatedLetterSmsReply GetItem(int disseminatedlettersmsreplyId)
		{
			DisseminatedLetterSmsReply disseminatedlettersmsreply = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDisseminatedLetterSmsReplySelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", disseminatedlettersmsreplyId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						disseminatedlettersmsreply = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return disseminatedlettersmsreply;
		}

		public static DisseminatedLetterSmsReplyCollection GetList(DisseminatedLetterSmsReplyCriteria disseminatedlettersmsreplyCriteria)
		{
			DisseminatedLetterSmsReplyCollection tempList = new DisseminatedLetterSmsReplyCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDisseminatedLetterSmsReplySearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new DisseminatedLetterSmsReplyCollection();
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

		public static int SelectCountForGetList(DisseminatedLetterSmsReplyCriteria disseminatedlettersmsreplyCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDisseminatedLetterSmsReplySearchList";

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

		public static int Save(DisseminatedLetterSmsReply myDisseminatedLetterSmsReply)
		{
			if (!myDisseminatedLetterSmsReply.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a disseminatedlettersmsreply in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDisseminatedLetterSmsReplyInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@hr_letter_id", myDisseminatedLetterSmsReply.mHrLetterId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@actual_expiration_date", myDisseminatedLetterSmsReply.mActualExpirationDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@response_date", myDisseminatedLetterSmsReply.mResponseDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", myDisseminatedLetterSmsReply.mLmmId);

				Helpers.SetSaveParameters(myCommand, myDisseminatedLetterSmsReply);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				//if (numberOfRecordsAffected == 0)
				//{
				//	throw new DBConcurrencyException("Can't update disseminatedlettersmsreply as it has been updated by someone else");
				//}
				//myDisseminatedLetterSmsReply.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spDisseminatedLetterSmsReplyDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static DisseminatedLetterSmsReply FillDataRecord(IDataRecord myDataRecord)
		{
			DisseminatedLetterSmsReply disseminatedlettersmsreply = new DisseminatedLetterSmsReply();

			disseminatedlettersmsreply.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			disseminatedlettersmsreply.mHrLetterId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("hr_letter_id"));
			disseminatedlettersmsreply.mActualExpirationDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("actual_expiration_date"));
			disseminatedlettersmsreply.mResponseDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("response_date"));
			disseminatedlettersmsreply.mLmmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_id"));
			disseminatedlettersmsreply.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return disseminatedlettersmsreply;
		}
	}
}