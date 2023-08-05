using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class AuditMessageImageDB
	{
		public static AuditMessageImage GetItem(int auditmessageimageId)
		{
			AuditMessageImage auditmessageimage = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAuditMessageImageSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", auditmessageimageId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						auditmessageimage = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return auditmessageimage;
		}

		public static AuditMessageImageCollection GetList(AuditMessageImageCriteria auditmessageimageCriteria)
		{
			AuditMessageImageCollection tempList = new AuditMessageImageCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAuditMessageImageSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "audit_message_detail_id", auditmessageimageCriteria.mAuditMessageDetailId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new AuditMessageImageCollection();
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

		public static int SelectCountForGetList(AuditMessageImageCriteria auditmessageimageCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAuditMessageImageSearchList";

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

		public static int Save(AuditMessageImage myAuditMessageImage)
		{
			if (!myAuditMessageImage.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a auditmessageimage in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAuditMessageImageInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@audit_message_detail_id", myAuditMessageImage.mAuditMessageDetailId);
				Helpers.CreateParameter(myCommand, DbType.String, "@image_link", myAuditMessageImage.mImageLink);
				Helpers.CreateParameter(myCommand, DbType.String, "@image_title", myAuditMessageImage.mImageTitle);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myAuditMessageImage.mDatestamp);

				Helpers.SetSaveParameters(myCommand, myAuditMessageImage);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update auditmessageimage as it has been updated by someone else");
				}
				//myAuditMessageImage.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spAuditMessageImageDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static AuditMessageImage FillDataRecord(IDataRecord myDataRecord)
		{
			AuditMessageImage auditmessageimage = new AuditMessageImage();

			auditmessageimage.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			auditmessageimage.mAuditMessageDetailId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("audit_message_detail_id"));
			auditmessageimage.mImageLink = myDataRecord.GetString(myDataRecord.GetOrdinal("image_link"));
			auditmessageimage.mImageTitle = myDataRecord.GetString(myDataRecord.GetOrdinal("image_title"));
			auditmessageimage.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
			auditmessageimage.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return auditmessageimage;
		}
	}
}