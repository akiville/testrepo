using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class AnnouncementReplyDB
	{
		public static AnnouncementReply GetItem(int announcementreplyId)
		{
			AnnouncementReply announcementreply = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAnnouncementReplySelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", announcementreplyId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						announcementreply = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return announcementreply;
		}

		public static AnnouncementReplyCollection GetList(AnnouncementReplyCriteria announcementreplyCriteria)
		{
			AnnouncementReplyCollection tempList = new AnnouncementReplyCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAnnouncementReplySearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", announcementreplyCriteria.mEmployeeId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@announement_id", announcementreplyCriteria.mAnnouncementId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new AnnouncementReplyCollection();
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

		public static int SelectCountForGetList(AnnouncementReplyCriteria announcementreplyCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAnnouncementReplySearchList";

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

		public static int Save(AnnouncementReply myAnnouncementReply)
		{
			if (!myAnnouncementReply.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a announcementreply in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAnnouncementReplyInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@announcement_id", myAnnouncementReply.mAnnouncementId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myAnnouncementReply.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_acknowledge", myAnnouncementReply.mIsAcknowledge);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myAnnouncementReply.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@aknowledgement_date", myAnnouncementReply.mAknowledgementDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datetime", myAnnouncementReply.mDatetime);

				Helpers.SetSaveParameters(myCommand, myAnnouncementReply);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update announcementreply as it has been updated by someone else");
				}
				//myAnnouncementReply.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spAnnouncementReplyDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static AnnouncementReply FillDataRecord(IDataRecord myDataRecord)
		{
			AnnouncementReply announcementreply = new AnnouncementReply();

			announcementreply.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			announcementreply.mAnnouncementId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("announcement_id"));
			announcementreply.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			announcementreply.mIsAcknowledge = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_acknowledge"));
			announcementreply.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			announcementreply.mAknowledgementDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("aknowledgement_date"));
			announcementreply.mDatetime = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datetime"));
			announcementreply.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return announcementreply;
		}
	}
}