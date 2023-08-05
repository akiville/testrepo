using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class ContactDB
	{
		public static Contact GetItem(int contactId)
		{
			Contact contact = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spContactSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", contactId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						contact = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return contact;
		}

		public static ContactCollection GetList(ContactCriteria contactCriteria)
		{
			ContactCollection tempList = new ContactCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spContactSearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new ContactCollection();
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

		public static int SelectCountForGetList(ContactCriteria contactCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spContactSearchList";

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

		public static int Save(Contact myContact)
		{
			if (!myContact.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a contact in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spContactInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.String, "@contact_no", myContact.mContactNo);
				Helpers.CreateParameter(myCommand, DbType.String, "@title", myContact.mTitle);
				Helpers.CreateParameter(myCommand, DbType.String, "@group_name", myContact.mGroupName);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myContact.mDatestamp);

				Helpers.SetSaveParameters(myCommand, myContact);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update contact as it has been updated by someone else");
				}
				//myContact.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spContactDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static Contact FillDataRecord(IDataRecord myDataRecord)
		{
			Contact contact = new Contact();

			contact.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			contact.mContactNo = myDataRecord.GetString(myDataRecord.GetOrdinal("contact_no"));
			contact.mTitle = myDataRecord.GetString(myDataRecord.GetOrdinal("title"));
			contact.mGroupName = myDataRecord.GetString(myDataRecord.GetOrdinal("group_name"));
			contact.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
			//contact.mConcurrencyId = (byte[]) myDataRecord.GetValue(myDataRecord.GetOrdinal("concurrency_id"));
			contact.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return contact;
		}
	}
}