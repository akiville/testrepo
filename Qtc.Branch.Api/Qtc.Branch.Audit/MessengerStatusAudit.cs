using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class MessengerStatusAudit
	{
		public static AuditCollection Audit(MessengerStatus messengerstatus,MessengerStatus messengerstatusOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (messengerstatus.mMessengerDetailId != messengerstatusOld.mMessengerDetailId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messengerstatus);
				audit.mField = "messenger_detail_id";
				audit.mOldValue = messengerstatusOld.mMessengerDetailId.ToString();
				audit.mNewValue = messengerstatus.mMessengerDetailId.ToString();
				audit_collection.Add(audit);
			}

			if (messengerstatus.mEmployeeId != messengerstatusOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messengerstatus);
				audit.mField = "employee_id";
				audit.mOldValue = messengerstatusOld.mEmployeeId.ToString();
				audit.mNewValue = messengerstatus.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (messengerstatus.mIsSeen != messengerstatusOld.mIsSeen)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messengerstatus);
				audit.mField = "is_seen";
				audit.mOldValue = messengerstatusOld.mIsSeen.ToString();
				audit.mNewValue = messengerstatus.mIsSeen.ToString();
				audit_collection.Add(audit);
			}

			if (messengerstatus.mIsAcknowledge != messengerstatusOld.mIsAcknowledge)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messengerstatus);
				audit.mField = "is_acknowledge";
				audit.mOldValue = messengerstatusOld.mIsAcknowledge.ToString();
				audit.mNewValue = messengerstatus.mIsAcknowledge.ToString();
				audit_collection.Add(audit);
			}

			if (messengerstatus.mIsLiked != messengerstatusOld.mIsLiked)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messengerstatus);
				audit.mField = "is_liked";
				audit.mOldValue = messengerstatusOld.mIsLiked.ToString();
				audit.mNewValue = messengerstatus.mIsLiked.ToString();
				audit_collection.Add(audit);
			}

			if (messengerstatus.mDateSeen != messengerstatusOld.mDateSeen)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messengerstatus);
				audit.mField = "date_seen";
				audit.mOldValue = messengerstatusOld.mDateSeen.ToString();
				audit.mNewValue = messengerstatus.mDateSeen.ToString();
				audit_collection.Add(audit);
			}

			if (messengerstatus.mDateAcknowledge != messengerstatusOld.mDateAcknowledge)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messengerstatus);
				audit.mField = "date_acknowledge";
				audit.mOldValue = messengerstatusOld.mDateAcknowledge.ToString();
				audit.mNewValue = messengerstatus.mDateAcknowledge.ToString();
				audit_collection.Add(audit);
			}

			if (messengerstatus.mDatestamp != messengerstatusOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messengerstatus);
				audit.mField = "datestamp";
				audit.mOldValue = messengerstatusOld.mDatestamp.ToString();
				audit.mNewValue = messengerstatus.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, MessengerStatus messengerstatus)
		{
			audit.mUserFullName = messengerstatus.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_MessengerStatus);
			audit.mRowId = messengerstatus.mId;
			audit.mAction = 2;
		}
	}
}