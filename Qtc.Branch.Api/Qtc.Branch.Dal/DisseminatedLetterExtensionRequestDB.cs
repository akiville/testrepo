using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class DisseminatedLetterExtensionRequestDB
	{
		public static DisseminatedLetterExtensionRequest GetItem(int disseminatedletterextensionrequestId)
		{
			DisseminatedLetterExtensionRequest disseminatedletterextensionrequest = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDisseminatedLetterExtensionRequestSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", disseminatedletterextensionrequestId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						disseminatedletterextensionrequest = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return disseminatedletterextensionrequest;
		}

		public static DisseminatedLetterExtensionRequestCollection GetList(DisseminatedLetterExtensionRequestCriteria disseminatedletterextensionrequestCriteria)
		{
			DisseminatedLetterExtensionRequestCollection tempList = new DisseminatedLetterExtensionRequestCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDisseminatedLetterExtensionRequestSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@disseminated_letter_id", disseminatedletterextensionrequestCriteria.mDisseminatedLetterId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", disseminatedletterextensionrequestCriteria.mLmmId);
                
                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new DisseminatedLetterExtensionRequestCollection();
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

		public static int SelectCountForGetList(DisseminatedLetterExtensionRequestCriteria disseminatedletterextensionrequestCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDisseminatedLetterExtensionRequestSearchList";

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

		public static int Save(DisseminatedLetterExtensionRequest myDisseminatedLetterExtensionRequest)
		{
			if (!myDisseminatedLetterExtensionRequest.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a disseminatedletterextensionrequest in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDisseminatedLetterExtensionRequestInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@disseminated_letter_id", myDisseminatedLetterExtensionRequest.mDisseminatedLetterId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", myDisseminatedLetterExtensionRequest.mLmmId);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myDisseminatedLetterExtensionRequest.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@request_date", myDisseminatedLetterExtensionRequest.mRequestDate);
                Helpers.CreateParameter(myCommand, DbType.String, "@status", myDisseminatedLetterExtensionRequest.mStatus);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myDisseminatedLetterExtensionRequest.mEmployeeId);

                Helpers.SetSaveParameters(myCommand, myDisseminatedLetterExtensionRequest);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update disseminatedletterextensionrequest as it has been updated by someone else");
				}
				//myDisseminatedLetterExtensionRequest.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spDisseminatedLetterExtensionRequestDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static DisseminatedLetterExtensionRequest FillDataRecord(IDataRecord myDataRecord)
		{
			DisseminatedLetterExtensionRequest disseminatedletterextensionrequest = new DisseminatedLetterExtensionRequest();

			disseminatedletterextensionrequest.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			disseminatedletterextensionrequest.mDisseminatedLetterId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("disseminated_letter_id"));
			disseminatedletterextensionrequest.mLmmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_id"));
			disseminatedletterextensionrequest.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			disseminatedletterextensionrequest.mRequestDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("request_date"));
			disseminatedletterextensionrequest.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            disseminatedletterextensionrequest.mStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("status"));
            disseminatedletterextensionrequest.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));

            return disseminatedletterextensionrequest;
		}
	}
}