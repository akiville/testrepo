using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class RepairImageDB
	{
		public static RepairImage GetItem(int repairimageId)
		{
			RepairImage repairimage = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRepairImageSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", repairimageId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						repairimage = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return repairimage;
		}

		public static RepairImageCollection GetList(RepairImageCriteria repairimageCriteria)
		{
			RepairImageCollection tempList = new RepairImageCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRepairImageSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "request_repair_id", repairimageCriteria.mRequestRepairId);
				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new RepairImageCollection();
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

		public static int SelectCountForGetList(RepairImageCriteria repairimageCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRepairImageSearchList";

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

		public static int Save(RepairImage myRepairImage)
		{
			if (!myRepairImage.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a repairimage in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRepairImageInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@request_repair_id", myRepairImage.mRequestRepairId);
				Helpers.CreateParameter(myCommand, DbType.String, "@name", myRepairImage.mName);
				Helpers.CreateParameter(myCommand, DbType.String, "@image_file_name", myRepairImage.mImageFileName);
				Helpers.CreateParameter(myCommand, DbType.String, "@description", myRepairImage.mDescription);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myRepairImage.mRecordId);

				Helpers.SetSaveParameters(myCommand, myRepairImage);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update repairimage as it has been updated by someone else");
				}
				//myRepairImage.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "ptApi_spRepairImageDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static RepairImage FillDataRecord(IDataRecord myDataRecord)
		{
			RepairImage repairimage = new RepairImage();

			repairimage.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			repairimage.mRequestRepairId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("request_repair_id"));
			repairimage.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));
			repairimage.mImageFileName = myDataRecord.GetString(myDataRecord.GetOrdinal("image_file_name"));
			repairimage.mDescription = myDataRecord.GetString(myDataRecord.GetOrdinal("description"));
			repairimage.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			repairimage.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));

			return repairimage;
		}
	}
}