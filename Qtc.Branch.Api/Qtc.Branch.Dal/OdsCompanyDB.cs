using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class OdsCompanyDB
	{
		public static OdsCompany GetItem(int odscompanyId)
		{
			OdsCompany odscompany = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spOdsCompanySelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", odscompanyId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						odscompany = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return odscompany;
		}

		public static OdsCompanyCollection GetList(OdsCompanyCriteria odscompanyCriteria)
		{
			OdsCompanyCollection tempList = new OdsCompanyCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spOdsCompanySearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new OdsCompanyCollection();
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

		public static int SelectCountForGetList(OdsCompanyCriteria odscompanyCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spOdsCompanySearchList";

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

		public static int Save(OdsCompany myOdsCompany)
		{
			if (!myOdsCompany.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a odscompany in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spOdsCompanyInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myOdsCompany.mRecordId);
				Helpers.CreateParameter(myCommand, DbType.String, "@code", myOdsCompany.mCode);
				Helpers.CreateParameter(myCommand, DbType.String, "@name", myOdsCompany.mName);

				Helpers.SetSaveParameters(myCommand, myOdsCompany);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update odscompany as it has been updated by someone else");
				}
				//myOdsCompany.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spOdsCompanyDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static OdsCompany FillDataRecord(IDataRecord myDataRecord)
		{
			OdsCompany odscompany = new OdsCompany();

			odscompany.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			odscompany.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));
			odscompany.mCode = myDataRecord.GetString(myDataRecord.GetOrdinal("code"));
			odscompany.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));
			odscompany.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return odscompany;
		}
	}
}