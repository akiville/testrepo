using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class BranchOdsCompanyDB
	{
		public static BranchOdsCompany GetItem(int branchodscompanyId)
		{
			BranchOdsCompany branchodscompany = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spBranchOdsCompanySelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", branchodscompanyId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						branchodscompany = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return branchodscompany;
		}

		public static BranchOdsCompanyCollection GetList(BranchOdsCompanyCriteria branchodscompanyCriteria)
		{
			BranchOdsCompanyCollection tempList = new BranchOdsCompanyCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spBranchOdsCompanySearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", branchodscompanyCriteria.mBranchId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@odc_company_id", branchodscompanyCriteria.mOdcCompanyId);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new BranchOdsCompanyCollection();
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

		public static int SelectCountForGetList(BranchOdsCompanyCriteria branchodscompanyCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spBranchOdsCompanySearchList";

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

		public static int Save(BranchOdsCompany myBranchOdsCompany)
		{
			if (!myBranchOdsCompany.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a branchodscompany in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spBranchOdsCompanyInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myBranchOdsCompany.mRecordId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myBranchOdsCompany.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@odc_company_id", myBranchOdsCompany.mOdcCompanyId);

				Helpers.SetSaveParameters(myCommand, myBranchOdsCompany);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update branchodscompany as it has been updated by someone else");
				}
				//myBranchOdsCompany.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spBranchOdsCompanyDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static BranchOdsCompany FillDataRecord(IDataRecord myDataRecord)
		{
			BranchOdsCompany branchodscompany = new BranchOdsCompany();

			branchodscompany.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			branchodscompany.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));
			branchodscompany.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			branchodscompany.mOdcCompanyId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("odc_company_id"));
			branchodscompany.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return branchodscompany;
		}
	}
}