using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class TypeOfReconcilingFormDB
	{
		public static TypeOfReconcilingForm GetItem(int typeofreconcilingformId)
		{
			TypeOfReconcilingForm typeofreconcilingform = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spTypeOfReconcilingFormSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", typeofreconcilingformId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						typeofreconcilingform = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return typeofreconcilingform;
		}

		public static TypeOfReconcilingFormCollection GetList(TypeOfReconcilingFormCriteria typeofreconcilingformCriteria)
		{
			TypeOfReconcilingFormCollection tempList = new TypeOfReconcilingFormCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spTypeOfReconcilingFormSearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new TypeOfReconcilingFormCollection();
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

		public static int SelectCountForGetList(TypeOfReconcilingFormCriteria typeofreconcilingformCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spTypeOfReconcilingFormSearchList";

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

		public static int Save(TypeOfReconcilingForm myTypeOfReconcilingForm)
		{
			if (!myTypeOfReconcilingForm.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a typeofreconcilingform in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spTypeOfReconcilingFormInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.String, "@name", myTypeOfReconcilingForm.mName);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myTypeOfReconcilingForm.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@required_check_previous", myTypeOfReconcilingForm.mRequiredCheckPrevious);

				Helpers.SetSaveParameters(myCommand, myTypeOfReconcilingForm);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update typeofreconcilingform as it has been updated by someone else");
				}
				//myTypeOfReconcilingForm.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spTypeOfReconcilingFormDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static TypeOfReconcilingForm FillDataRecord(IDataRecord myDataRecord)
		{
			TypeOfReconcilingForm typeofreconcilingform = new TypeOfReconcilingForm();

			typeofreconcilingform.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			typeofreconcilingform.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));
			typeofreconcilingform.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			typeofreconcilingform.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			typeofreconcilingform.mRequiredCheckPrevious = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("required_check_previous"));

			return typeofreconcilingform;
		}
	}
}