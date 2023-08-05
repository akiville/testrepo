using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class CategoryDB
	{
		public static Category GetItem(int categoryId)
		{
			Category category = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spCategorySelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", categoryId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						category = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return category;
		}

		public static CategoryCollection GetList(CategoryCriteria categoryCriteria)
		{
			CategoryCollection tempList = new CategoryCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spCategorySearchList";


				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new CategoryCollection();
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

		public static int SelectCountForGetList(CategoryCriteria categoryCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spCategorySearchList";

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

		public static int Save(Category myCategory)
		{
			if (!myCategory.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a category in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spCategoryInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@category_id", myCategory.mCategoryId);
				Helpers.CreateParameter(myCommand, DbType.String, "@name", myCategory.mName);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@rfs", myCategory.mRfs);
				Helpers.CreateParameter(myCommand, DbType.Int16, "@sorting", myCategory.mSorting);
				Helpers.CreateParameter(myCommand, DbType.String, "@image_link", myCategory.mImageLink);

				Helpers.SetSaveParameters(myCommand, myCategory);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update category as it has been updated by someone else");
				}
				//myCategory.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spCategoryDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static Category FillDataRecord(IDataRecord myDataRecord)
		{
			Category category = new Category();

			category.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			category.mCategoryId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("category_id"));
			category.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));
			category.mRfs = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("rfs"));
			category.mSorting = myDataRecord.GetInt16(myDataRecord.GetOrdinal("sorting"));
			category.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			//category.mConcurrencyId = (byte[]) myDataRecord.GetValue(myDataRecord.GetOrdinal("concurrency_id"));
			category.mImageLink = myDataRecord.GetString(myDataRecord.GetOrdinal("image_link"));

			return category;
		}
	}
}