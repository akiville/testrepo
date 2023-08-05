using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class RequestMessageAudit
	{
		public static AuditCollection Audit(RequestMessage requestmessage,RequestMessage requestmessageOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (requestmessage.mBranchId != requestmessageOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestmessage);
				audit.mField = "branch_id";
				audit.mOldValue = requestmessageOld.mBranchId.ToString();
				audit.mNewValue = requestmessage.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (requestmessage.mUserId != requestmessageOld.mUserId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestmessage);
				audit.mField = "user_id";
				audit.mOldValue = requestmessageOld.mUserId.ToString();
				audit.mNewValue = requestmessage.mUserId.ToString();
				audit_collection.Add(audit);
			}

			if (requestmessage.mTopicId != requestmessageOld.mTopicId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestmessage);
				audit.mField = "topic_id";
				audit.mOldValue = requestmessageOld.mTopicId.ToString();
				audit.mNewValue = requestmessage.mTopicId.ToString();
				audit_collection.Add(audit);
			}

			if (requestmessage.mMessage != requestmessageOld.mMessage)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestmessage);
				audit.mField = "message";
				audit.mOldValue = requestmessageOld.mMessage;
				audit.mNewValue = requestmessage.mMessage;
				audit_collection.Add(audit);
			}

			if (requestmessage.mOriginalMessageId != requestmessageOld.mOriginalMessageId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestmessage);
				audit.mField = "original_message_id";
				audit.mOldValue = requestmessageOld.mOriginalMessageId.ToString();
				audit.mNewValue = requestmessage.mOriginalMessageId.ToString();
				audit_collection.Add(audit);
			}

			if (requestmessage.mIsSeen != requestmessageOld.mIsSeen)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestmessage);
				audit.mField = "is_seen";
				audit.mOldValue = requestmessageOld.mIsSeen.ToString();
				audit.mNewValue = requestmessage.mIsSeen.ToString();
				audit_collection.Add(audit);
			}

			if (requestmessage.mDateSeen != requestmessageOld.mDateSeen)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestmessage);
				audit.mField = "date_seen";
				audit.mOldValue = requestmessageOld.mDateSeen.ToString();
				audit.mNewValue = requestmessage.mDateSeen.ToString();
				audit_collection.Add(audit);
			}

			if (requestmessage.mMessageDate != requestmessageOld.mMessageDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestmessage);
				audit.mField = "message_date";
				audit.mOldValue = requestmessageOld.mMessageDate.ToString();
				audit.mNewValue = requestmessage.mMessageDate.ToString();
				audit_collection.Add(audit);
			}

			if (requestmessage.mDatestamp != requestmessageOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestmessage);
				audit.mField = "datestamp";
				audit.mOldValue = requestmessageOld.mDatestamp.ToString();
				audit.mNewValue = requestmessage.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, RequestMessage requestmessage)
		{
			audit.mUserFullName = requestmessage.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_RequestMessage);
			audit.mRowId = requestmessage.mId;
			audit.mAction = 2;
		}
	}
}