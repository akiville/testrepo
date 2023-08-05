using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class MessengerDetailAudit
	{
		public static AuditCollection Audit(MessengerDetail messengerdetail,MessengerDetail messengerdetailOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (messengerdetail.mMessengerId != messengerdetailOld.mMessengerId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messengerdetail);
				audit.mField = "messenger_id";
				audit.mOldValue = messengerdetailOld.mMessengerId.ToString();
				audit.mNewValue = messengerdetail.mMessengerId.ToString();
				audit_collection.Add(audit);
			}

			if (messengerdetail.mMessage != messengerdetailOld.mMessage)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messengerdetail);
				audit.mField = "message";
				audit.mOldValue = messengerdetailOld.mMessage;
				audit.mNewValue = messengerdetail.mMessage;
				audit_collection.Add(audit);
			}

			if (messengerdetail.mEmployeeId != messengerdetailOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messengerdetail);
				audit.mField = "employee_id";
				audit.mOldValue = messengerdetailOld.mEmployeeId.ToString();
				audit.mNewValue = messengerdetail.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (messengerdetail.mDateCreated != messengerdetailOld.mDateCreated)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messengerdetail);
				audit.mField = "date_created";
				audit.mOldValue = messengerdetailOld.mDateCreated.ToString();
				audit.mNewValue = messengerdetail.mDateCreated.ToString();
				audit_collection.Add(audit);
			}

			if (messengerdetail.mDatestamp != messengerdetailOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messengerdetail);
				audit.mField = "datestamp";
				audit.mOldValue = messengerdetailOld.mDatestamp.ToString();
				audit.mNewValue = messengerdetail.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			if (messengerdetail.mIsWhisper != messengerdetailOld.mIsWhisper)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messengerdetail);
				audit.mField = "is_whisper";
				audit.mOldValue = messengerdetailOld.mIsWhisper.ToString();
				audit.mNewValue = messengerdetail.mIsWhisper.ToString();
				audit_collection.Add(audit);
			}

			if (messengerdetail.mWhisperTo != messengerdetailOld.mWhisperTo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messengerdetail);
				audit.mField = "whisper_to";
				audit.mOldValue = messengerdetailOld.mWhisperTo.ToString();
				audit.mNewValue = messengerdetail.mWhisperTo.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, MessengerDetail messengerdetail)
		{
			audit.mUserFullName = messengerdetail.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_MessengerDetail);
			audit.mRowId = messengerdetail.mId;
			audit.mAction = 2;
		}
	}
}