using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class SalesSchedulingCutoffDB
	{
		public static SalesSchedulingCutoff GetItem(int salesschedulingcutoffId)
		{
			SalesSchedulingCutoff salesschedulingcutoff = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spSalesSchedulingCutoffSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", salesschedulingcutoffId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						salesschedulingcutoff = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return salesschedulingcutoff;
		}

		public static SalesSchedulingCutoffCollection GetList(SalesSchedulingCutoffCriteria salesschedulingcutoffCriteria)
		{
			SalesSchedulingCutoffCollection tempList = new SalesSchedulingCutoffCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spSalesSchedulingCutoffSearchList";

                Helpers.CreateParameter(myCommand, DbType.String, "@date", salesschedulingcutoffCriteria.mDate);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new SalesSchedulingCutoffCollection();
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

		public static int SelectCountForGetList(SalesSchedulingCutoffCriteria salesschedulingcutoffCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spSalesSchedulingCutoffSearchList";

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

		public static int Save(SalesSchedulingCutoff mySalesSchedulingCutoff)
		{
			if (!mySalesSchedulingCutoff.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a salesschedulingcutoff in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spSalesSchedulingCutoffInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.DateTime, "@start_date", mySalesSchedulingCutoff.mStartDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@end_date", mySalesSchedulingCutoff.mEndDate);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_final", mySalesSchedulingCutoff.mIsFinal);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@mirrored_cutoff_id", mySalesSchedulingCutoff.mMirroredCutoffId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@cut_off_id", mySalesSchedulingCutoff.mCutOffId);

                Helpers.SetSaveParameters(myCommand, mySalesSchedulingCutoff);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update salesschedulingcutoff as it has been updated by someone else");
				}
				//mySalesSchedulingCutoff.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spSalesSchedulingCutoffDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static SalesSchedulingCutoff FillDataRecord(IDataRecord myDataRecord)
		{
			SalesSchedulingCutoff salesschedulingcutoff = new SalesSchedulingCutoff();

			salesschedulingcutoff.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			salesschedulingcutoff.mStartDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("start_date"));
			salesschedulingcutoff.mEndDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("end_date"));
			salesschedulingcutoff.mIsFinal = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_final"));
			salesschedulingcutoff.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			salesschedulingcutoff.mMirroredCutoffId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("mirrored_cutoff_id"));
            salesschedulingcutoff.mCutOffId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("cut_off_id"));

			return salesschedulingcutoff;
		}
	}
}