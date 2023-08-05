using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class RequestTopicAudit
	{
		public static AuditCollection Audit(RequestTopic requesttopic,RequestTopic requesttopicOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (requesttopic.mName != requesttopicOld.mName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requesttopic);
				audit.mField = "name";
				audit.mOldValue = requesttopicOld.mName;
				audit.mNewValue = requesttopic.mName;
				audit_collection.Add(audit);
			}

			if (requesttopic.mDescription != requesttopicOld.mDescription)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requesttopic);
				audit.mField = "description";
				audit.mOldValue = requesttopicOld.mDescription;
				audit.mNewValue = requesttopic.mDescription;
				audit_collection.Add(audit);
			}

			if (requesttopic.mCategory != requesttopicOld.mCategory)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requesttopic);
				audit.mField = "category";
				audit.mOldValue = requesttopicOld.mCategory;
				audit.mNewValue = requesttopic.mCategory;
				audit_collection.Add(audit);
			}

			if (requesttopic.mWelcomeMessage != requesttopicOld.mWelcomeMessage)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requesttopic);
				audit.mField = "welcome_message";
				audit.mOldValue = requesttopicOld.mWelcomeMessage;
				audit.mNewValue = requesttopic.mWelcomeMessage;
				audit_collection.Add(audit);
			}

			if (requesttopic.mUserId != requesttopicOld.mUserId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requesttopic);
				audit.mField = "user_id";
				audit.mOldValue = requesttopicOld.mUserId.ToString();
				audit.mNewValue = requesttopic.mUserId.ToString();
				audit_collection.Add(audit);
			}

			if (requesttopic.mDatestamp != requesttopicOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requesttopic);
				audit.mField = "datestamp";
				audit.mOldValue = requesttopicOld.mDatestamp.ToString();
				audit.mNewValue = requesttopic.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, RequestTopic requesttopic)
		{
			audit.mUserFullName = requesttopic.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_RequestTopic);
			audit.mRowId = requesttopic.mId;
			audit.mAction = 2;
		}
	}
}