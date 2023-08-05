using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class MessengerParticipantAudit
	{
		public static AuditCollection Audit(MessengerParticipant messengerparticipant,MessengerParticipant messengerparticipantOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (messengerparticipant.mEmployeeId != messengerparticipantOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messengerparticipant);
				audit.mField = "employee_id";
				audit.mOldValue = messengerparticipantOld.mEmployeeId.ToString();
				audit.mNewValue = messengerparticipant.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (messengerparticipant.mIsAdmin != messengerparticipantOld.mIsAdmin)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messengerparticipant);
				audit.mField = "is_admin";
				audit.mOldValue = messengerparticipantOld.mIsAdmin.ToString();
				audit.mNewValue = messengerparticipant.mIsAdmin.ToString();
				audit_collection.Add(audit);
			}

			if (messengerparticipant.mMessengerId != messengerparticipantOld.mMessengerId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messengerparticipant);
				audit.mField = "messenger_id";
				audit.mOldValue = messengerparticipantOld.mMessengerId.ToString();
				audit.mNewValue = messengerparticipant.mMessengerId.ToString();
				audit_collection.Add(audit);
			}

			if (messengerparticipant.mDatestamp != messengerparticipantOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messengerparticipant);
				audit.mField = "datestamp";
				audit.mOldValue = messengerparticipantOld.mDatestamp.ToString();
				audit.mNewValue = messengerparticipant.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, MessengerParticipant messengerparticipant)
		{
			audit.mUserFullName = messengerparticipant.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_MessengerParticipant);
			audit.mRowId = messengerparticipant.mId;
			audit.mAction = 2;
		}
	}
}