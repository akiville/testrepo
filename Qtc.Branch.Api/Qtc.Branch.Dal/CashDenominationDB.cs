using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class CashDenominationDB
	{
		public static CashDenomination GetItem(int cashdenominationId)
		{
			CashDenomination cashdenomination = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spCashDenominationSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", cashdenominationId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						cashdenomination = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return cashdenomination;
		}

		public static CashDenominationCollection GetList(CashDenominationCriteria cashdenominationCriteria)
		{
			CashDenominationCollection tempList = new CashDenominationCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spCashDenominationSearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new CashDenominationCollection();
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

		public static int SelectCountForGetList(CashDenominationCriteria cashdenominationCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spCashDenominationSearchList";

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

		public static int Save(CashDenomination myCashDenomination)
		{
			if (!myCashDenomination.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a cashdenomination in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spCashDenominationInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.String, "@name", myCashDenomination.mName);

				Helpers.SetSaveParameters(myCommand, myCashDenomination);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update cashdenomination as it has been updated by someone else");
				}
				//myCashDenomination.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spCashDenominationDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static CashDenomination FillDataRecord(IDataRecord myDataRecord)
		{
			CashDenomination cashdenomination = new CashDenomination();

			cashdenomination.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			cashdenomination.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));
			cashdenomination.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			//cashdenomination.mConcurrencyId = (byte[]) myDataRecord.GetValue(myDataRecord.GetOrdinal("concurrency_id"));

			return cashdenomination;
		}
	}
}