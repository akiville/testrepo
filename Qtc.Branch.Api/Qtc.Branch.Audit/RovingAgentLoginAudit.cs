using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class RovingAgentLoginAudit
	{
		public static AuditCollection Audit(RovingAgentLogin rovingagentlogin,RovingAgentLogin rovingagentloginOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (rovingagentlogin.mRovingAgentId != rovingagentloginOld.mRovingAgentId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingagentlogin);
				audit.mField = "roving_agent_id";
				audit.mOldValue = rovingagentloginOld.mRovingAgentId.ToString();
				audit.mNewValue = rovingagentlogin.mRovingAgentId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingagentlogin.mBranchId != rovingagentloginOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingagentlogin);
				audit.mField = "branch_id";
				audit.mOldValue = rovingagentloginOld.mBranchId.ToString();
				audit.mNewValue = rovingagentlogin.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingagentlogin.mRpsId != rovingagentloginOld.mRpsId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingagentlogin);
				audit.mField = "rps_id";
				audit.mOldValue = rovingagentloginOld.mRpsId.ToString();
				audit.mNewValue = rovingagentlogin.mRpsId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingagentlogin.mTimeIn != rovingagentloginOld.mTimeIn)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingagentlogin);
				audit.mField = "time_in";
				audit.mOldValue = rovingagentloginOld.mTimeIn.ToString();
				audit.mNewValue = rovingagentlogin.mTimeIn.ToString();
				audit_collection.Add(audit);
			}

			if (rovingagentlogin.mTimeInLatitude != rovingagentloginOld.mTimeInLatitude)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingagentlogin);
				audit.mField = "time_in_latitude";
				audit.mOldValue = rovingagentloginOld.mTimeInLatitude;
				audit.mNewValue = rovingagentlogin.mTimeInLatitude;
				audit_collection.Add(audit);
			}

			if (rovingagentlogin.mTimeInLongitude != rovingagentloginOld.mTimeInLongitude)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingagentlogin);
				audit.mField = "time_in_longitude";
				audit.mOldValue = rovingagentloginOld.mTimeInLongitude;
				audit.mNewValue = rovingagentlogin.mTimeInLongitude;
				audit_collection.Add(audit);
			}

			if (rovingagentlogin.mTimeInAddress != rovingagentloginOld.mTimeInAddress)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingagentlogin);
				audit.mField = "time_in_address";
				audit.mOldValue = rovingagentloginOld.mTimeInAddress;
				audit.mNewValue = rovingagentlogin.mTimeInAddress;
				audit_collection.Add(audit);
			}

			if (rovingagentlogin.mTimeOut != rovingagentloginOld.mTimeOut)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingagentlogin);
				audit.mField = "time_out";
				audit.mOldValue = rovingagentloginOld.mTimeOut;
				audit.mNewValue = rovingagentlogin.mTimeOut;
				audit_collection.Add(audit);
			}

			if (rovingagentlogin.mTimeOutLatitude != rovingagentloginOld.mTimeOutLatitude)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingagentlogin);
				audit.mField = "time_out_latitude";
				audit.mOldValue = rovingagentloginOld.mTimeOutLatitude;
				audit.mNewValue = rovingagentlogin.mTimeOutLatitude;
				audit_collection.Add(audit);
			}

			if (rovingagentlogin.mTimeOutLongitude != rovingagentloginOld.mTimeOutLongitude)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingagentlogin);
				audit.mField = "time_out_longitude";
				audit.mOldValue = rovingagentloginOld.mTimeOutLongitude;
				audit.mNewValue = rovingagentlogin.mTimeOutLongitude;
				audit_collection.Add(audit);
			}

			if (rovingagentlogin.mTimeOutAddress != rovingagentloginOld.mTimeOutAddress)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingagentlogin);
				audit.mField = "time_out_address";
				audit.mOldValue = rovingagentloginOld.mTimeOutAddress;
				audit.mNewValue = rovingagentlogin.mTimeOutAddress;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, RovingAgentLogin rovingagentlogin)
		{
			audit.mUserFullName = rovingagentlogin.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_RovingAgentLogin);
			audit.mRowId = rovingagentlogin.mId;
			audit.mAction = 2;
		}
	}
}