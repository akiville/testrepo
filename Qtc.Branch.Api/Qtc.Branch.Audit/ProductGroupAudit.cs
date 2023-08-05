using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class ProductGroupAudit
	{
		public static AuditCollection Audit(ProductGroup productgroup,ProductGroup productgroupOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (productgroup.mProductGroupId != productgroupOld.mProductGroupId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productgroup);
				audit.mField = "product_group_id";
				audit.mOldValue = productgroupOld.mProductGroupId.ToString();
				audit.mNewValue = productgroup.mProductGroupId.ToString();
				audit_collection.Add(audit);
			}

			if (productgroup.mName != productgroupOld.mName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productgroup);
				audit.mField = "name";
				audit.mOldValue = productgroupOld.mName;
				audit.mNewValue = productgroup.mName;
				audit_collection.Add(audit);
			}

			if (productgroup.mDatestamp != productgroupOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productgroup);
				audit.mField = "datestamp";
				audit.mOldValue = productgroupOld.mDatestamp.ToString();
				audit.mNewValue = productgroup.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, ProductGroup productgroup)
		{
			audit.mUserFullName = productgroup.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_ProductGroup);
			audit.mRowId = productgroup.mId;
			audit.mAction = 2;
		}
	}
}