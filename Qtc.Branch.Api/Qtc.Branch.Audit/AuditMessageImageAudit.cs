using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class AuditMessageImageAudit
	{
		public static AuditCollection Audit(AuditMessageImage auditmessageimage,AuditMessageImage auditmessageimageOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (auditmessageimage.mAuditMessageDetailId != auditmessageimageOld.mAuditMessageDetailId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, auditmessageimage);
				audit.mField = "audit_message_detail_id";
				audit.mOldValue = auditmessageimageOld.mAuditMessageDetailId.ToString();
				audit.mNewValue = auditmessageimage.mAuditMessageDetailId.ToString();
				audit_collection.Add(audit);
			}

			if (auditmessageimage.mImageLink != auditmessageimageOld.mImageLink)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, auditmessageimage);
				audit.mField = "image_link";
				audit.mOldValue = auditmessageimageOld.mImageLink;
				audit.mNewValue = auditmessageimage.mImageLink;
				audit_collection.Add(audit);
			}

			if (auditmessageimage.mImageTitle != auditmessageimageOld.mImageTitle)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, auditmessageimage);
				audit.mField = "image_title";
				audit.mOldValue = auditmessageimageOld.mImageTitle;
				audit.mNewValue = auditmessageimage.mImageTitle;
				audit_collection.Add(audit);
			}

			if (auditmessageimage.mDatestamp != auditmessageimageOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, auditmessageimage);
				audit.mField = "datestamp";
				audit.mOldValue = auditmessageimageOld.mDatestamp.ToString();
				audit.mNewValue = auditmessageimage.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, AuditMessageImage auditmessageimage)
		{
			audit.mUserFullName = auditmessageimage.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_AuditMessageImage);
			audit.mRowId = auditmessageimage.mId;
			audit.mAction = 2;
		}
	}
}