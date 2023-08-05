using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class CategoryAudit
	{
		public static AuditCollection Audit(Category category,Category categoryOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (category.mCategoryId != categoryOld.mCategoryId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, category);
				audit.mField = "category_id";
				audit.mOldValue = categoryOld.mCategoryId.ToString();
				audit.mNewValue = category.mCategoryId.ToString();
				audit_collection.Add(audit);
			}

			if (category.mName != categoryOld.mName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, category);
				audit.mField = "name";
				audit.mOldValue = categoryOld.mName;
				audit.mNewValue = category.mName;
				audit_collection.Add(audit);
			}

			if (category.mRfs != categoryOld.mRfs)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, category);
				audit.mField = "rfs";
				audit.mOldValue = categoryOld.mRfs.ToString();
				audit.mNewValue = category.mRfs.ToString();
				audit_collection.Add(audit);
			}

			if (category.mSorting != categoryOld.mSorting)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, category);
				audit.mField = "sorting";
				audit.mOldValue = categoryOld.mSorting.ToString();
				audit.mNewValue = category.mSorting.ToString();
				audit_collection.Add(audit);
			}

			if (category.mImageLink != categoryOld.mImageLink)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, category);
				audit.mField = "image_link";
				audit.mOldValue = categoryOld.mImageLink;
				audit.mNewValue = category.mImageLink;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, Category category)
		{
			audit.mUserFullName = category.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_Category);
			audit.mRowId = category.mId;
			audit.mAction = 2;
		}
	}
}