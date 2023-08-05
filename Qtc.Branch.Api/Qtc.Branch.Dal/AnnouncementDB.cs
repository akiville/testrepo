using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class AnnouncementDB
	{
		public static Announcement GetItem(int announcementId)
		{
			Announcement announcement = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAnnouncementSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", announcementId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						announcement = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return announcement;
		}

		public static AnnouncementCollection GetList(AnnouncementCriteria announcementCriteria)
		{
			AnnouncementCollection tempList = new AnnouncementCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAnnouncementSearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new AnnouncementCollection();
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

		public static int SelectCountForGetList(AnnouncementCriteria announcementCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAnnouncementSearchList";

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

		public static int Save(Announcement myAnnouncement)
		{
			if (!myAnnouncement.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a announcement in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAnnouncementInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.String, "@message", myAnnouncement.mMessage);
				Helpers.CreateParameter(myCommand, DbType.String, "@header", myAnnouncement.mHeader);
				Helpers.CreateParameter(myCommand, DbType.String, "@footer", myAnnouncement.mFooter);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_sent", myAnnouncement.mDateSent);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@user_id", myAnnouncement.mUserId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datetime", myAnnouncement.mDatetime);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_posted", myAnnouncement.mIsPosted);

				Helpers.SetSaveParameters(myCommand, myAnnouncement);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update announcement as it has been updated by someone else");
				}
				//myAnnouncement.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spAnnouncementDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static Announcement FillDataRecord(IDataRecord myDataRecord)
		{
			Announcement announcement = new Announcement();

			announcement.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			announcement.mMessage = myDataRecord.GetString(myDataRecord.GetOrdinal("message"));
			announcement.mHeader = myDataRecord.GetString(myDataRecord.GetOrdinal("header"));
			announcement.mFooter = myDataRecord.GetString(myDataRecord.GetOrdinal("footer"));
			announcement.mDateSent = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_sent"));
			announcement.mUserId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("user_id"));
			announcement.mDatetime = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datetime"));
			announcement.mIsPosted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_posted"));
			announcement.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return announcement;
		}
	}
}