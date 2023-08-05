using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class UserAudit
	{
		public static AuditCollection Audit(User user,User userOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (user.mFirstName != userOld.mFirstName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, user);
				audit.mField = "first_name";
				audit.mOldValue = userOld.mFirstName;
				audit.mNewValue = user.mFirstName;
				audit_collection.Add(audit);
			}

			if (user.mMiddleName != userOld.mMiddleName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, user);
				audit.mField = "middle_name";
				audit.mOldValue = userOld.mMiddleName;
				audit.mNewValue = user.mMiddleName;
				audit_collection.Add(audit);
			}

			if (user.mLastName != userOld.mLastName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, user);
				audit.mField = "last_name";
				audit.mOldValue = userOld.mLastName;
				audit.mNewValue = user.mLastName;
				audit_collection.Add(audit);
			}

			if (user.mRole != userOld.mRole)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, user);
				audit.mField = "role";
				audit.mOldValue = userOld.mRole.ToString();
				audit.mNewValue = user.mRole.ToString();
				audit_collection.Add(audit);
			}

			if (user.mUserName != userOld.mUserName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, user);
				audit.mField = "user_name";
				audit.mOldValue = userOld.mUserName;
				audit.mNewValue = user.mUserName;
				audit_collection.Add(audit);
			}

			if (user.mPassword != userOld.mPassword)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, user);
				audit.mField = "password";
				audit.mOldValue = userOld.mPassword;
				audit.mNewValue = user.mPassword;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, User user)
		{
			audit.mUserFullName = user.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_User);
			audit.mRowId = user.mId;
			audit.mAction = 2;
		}
	}
}