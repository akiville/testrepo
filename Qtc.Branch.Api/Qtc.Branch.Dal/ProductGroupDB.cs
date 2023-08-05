using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class ProductGroupDB
	{
		public static ProductGroup GetItem(int productgroupId)
		{
			ProductGroup productgroup = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spProductGroupSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", productgroupId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						productgroup = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return productgroup;
		}

		public static ProductGroupCollection GetList(ProductGroupCriteria productgroupCriteria)
		{
			ProductGroupCollection tempList = new ProductGroupCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spProductGroupSearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new ProductGroupCollection();
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

		public static int SelectCountForGetList(ProductGroupCriteria productgroupCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spProductGroupSearchList";

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

		public static int Save(ProductGroup myProductGroup)
		{
			if (!myProductGroup.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a productgroup in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spProductGroupInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@product_group_id", myProductGroup.mProductGroupId);
				Helpers.CreateParameter(myCommand, DbType.String, "@name", myProductGroup.mName);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myProductGroup.mDatestamp);

				Helpers.SetSaveParameters(myCommand, myProductGroup);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update productgroup as it has been updated by someone else");
				}
				//myProductGroup.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spProductGroupDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static ProductGroup FillDataRecord(IDataRecord myDataRecord)
		{
			ProductGroup productgroup = new ProductGroup();

			productgroup.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			productgroup.mProductGroupId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("product_group_id"));
			productgroup.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));
			productgroup.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
			productgroup.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return productgroup;
		}
	}
}