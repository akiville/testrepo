using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class MessengerAudit
	{
		public static AuditCollection Audit(Messenger messenger,Messenger messengerOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (messenger.mTitle != messengerOld.mTitle)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messenger);
				audit.mField = "title";
				audit.mOldValue = messengerOld.mTitle;
				audit.mNewValue = messenger.mTitle;
				audit_collection.Add(audit);
			}

			if (messenger.mRemarks != messengerOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messenger);
				audit.mField = "remarks";
				audit.mOldValue = messengerOld.mRemarks;
				audit.mNewValue = messenger.mRemarks;
				audit_collection.Add(audit);
			}

			if (messenger.mDateCreated != messengerOld.mDateCreated)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messenger);
				audit.mField = "date_created";
				audit.mOldValue = messengerOld.mDateCreated.ToString();
				audit.mNewValue = messenger.mDateCreated.ToString();
				audit_collection.Add(audit);
			}

			if (messenger.mDisplayReplyToEveryone != messengerOld.mDisplayReplyToEveryone)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messenger);
				audit.mField = "display_reply_to_everyone";
				audit.mOldValue = messengerOld.mDisplayReplyToEveryone.ToString();
				audit.mNewValue = messenger.mDisplayReplyToEveryone.ToString();
				audit_collection.Add(audit);
			}

			if (messenger.mUserId != messengerOld.mUserId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messenger);
				audit.mField = "user_id";
				audit.mOldValue = messengerOld.mUserId.ToString();
				audit.mNewValue = messenger.mUserId.ToString();
				audit_collection.Add(audit);
			}

			if (messenger.mDatestamp != messengerOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messenger);
				audit.mField = "datestamp";
				audit.mOldValue = messengerOld.mDatestamp.ToString();
				audit.mNewValue = messenger.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, Messenger messenger)
		{
			audit.mUserFullName = messenger.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_Messenger);
			audit.mRowId = messenger.mId;
			audit.mAction = 2;
		}
	}
}