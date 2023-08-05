using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class MessageAudit
	{
		public static AuditCollection Audit(Message message,Message messageOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (message.mMessageDate != messageOld.mMessageDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, message);
				audit.mField = "message_date";
				audit.mOldValue = messageOld.mMessageDate.ToString();
				audit.mNewValue = message.mMessageDate.ToString();
				audit_collection.Add(audit);
			}

			if (message.mReceiverId != messageOld.mReceiverId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, message);
				audit.mField = "receiver_id";
				audit.mOldValue = messageOld.mReceiverId.ToString();
				audit.mNewValue = message.mReceiverId.ToString();
				audit_collection.Add(audit);
			}

			if (message.mSenderId != messageOld.mSenderId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, message);
				audit.mField = "sender_id";
				audit.mOldValue = messageOld.mSenderId.ToString();
				audit.mNewValue = message.mSenderId.ToString();
				audit_collection.Add(audit);
			}

			if (message.mMessage != messageOld.mMessage)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, message);
				audit.mField = "message";
				audit.mOldValue = messageOld.mMessage;
				audit.mNewValue = message.mMessage;
				audit_collection.Add(audit);
			}

			if (message.mTitle != messageOld.mTitle)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, message);
				audit.mField = "title";
				audit.mOldValue = messageOld.mTitle;
				audit.mNewValue = message.mTitle;
				audit_collection.Add(audit);
			}

			if (message.mReplyToId != messageOld.mReplyToId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, message);
				audit.mField = "reply_to_id";
				audit.mOldValue = messageOld.mReplyToId.ToString();
				audit.mNewValue = message.mReplyToId.ToString();
				audit_collection.Add(audit);
			}

			if (message.mDatestamp != messageOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, message);
				audit.mField = "datestamp";
				audit.mOldValue = messageOld.mDatestamp.ToString();
				audit.mNewValue = message.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, Message message)
		{
			audit.mUserFullName = message.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_Message);
			audit.mRowId = message.mId;
			audit.mAction = 2;
		}
	}
}