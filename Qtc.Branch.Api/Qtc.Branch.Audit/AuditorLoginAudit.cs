using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class AuditorLoginAudit
	{
		public static AuditCollection Audit(AuditorLogin auditorlogin,AuditorLogin auditorloginOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (auditorlogin.mUsername != auditorloginOld.mUsername)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, auditorlogin);
				audit.mField = "username";
				audit.mOldValue = auditorloginOld.mUsername;
				audit.mNewValue = auditorlogin.mUsername;
				audit_collection.Add(audit);
			}

			if (auditorlogin.mPassword != auditorloginOld.mPassword)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, auditorlogin);
				audit.mField = "password";
				audit.mOldValue = auditorloginOld.mPassword;
				audit.mNewValue = auditorlogin.mPassword;
				audit_collection.Add(audit);
			}

			if (auditorlogin.mFirstname != auditorloginOld.mFirstname)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, auditorlogin);
				audit.mField = "firstname";
				audit.mOldValue = auditorloginOld.mFirstname;
				audit.mNewValue = auditorlogin.mFirstname;
				audit_collection.Add(audit);
			}

			if (auditorlogin.mLastname != auditorloginOld.mLastname)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, auditorlogin);
				audit.mField = "lastname";
				audit.mOldValue = auditorloginOld.mLastname;
				audit.mNewValue = auditorlogin.mLastname;
				audit_collection.Add(audit);
			}

			if (auditorlogin.mMiddlename != auditorloginOld.mMiddlename)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, auditorlogin);
				audit.mField = "middlename";
				audit.mOldValue = auditorloginOld.mMiddlename;
				audit.mNewValue = auditorlogin.mMiddlename;
				audit_collection.Add(audit);
			}

			if (auditorlogin.mPositionName != auditorloginOld.mPositionName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, auditorlogin);
				audit.mField = "position_name";
				audit.mOldValue = auditorloginOld.mPositionName;
				audit.mNewValue = auditorlogin.mPositionName;
				audit_collection.Add(audit);
			}

			if (auditorlogin.mDatestamp != auditorloginOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, auditorlogin);
				audit.mField = "datestamp";
				audit.mOldValue = auditorloginOld.mDatestamp.ToString();
				audit.mNewValue = auditorlogin.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, AuditorLogin auditorlogin)
		{
			audit.mUserFullName = auditorlogin.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_AuditorLogin);
			audit.mRowId = auditorlogin.mId;
			audit.mAction = 2;
		}
	}
}