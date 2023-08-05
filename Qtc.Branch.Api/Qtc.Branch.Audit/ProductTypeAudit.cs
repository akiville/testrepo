using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class ProductTypeAudit
	{
		public static AuditCollection Audit(ProductType producttype,ProductType producttypeOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (producttype.mName != producttypeOld.mName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, producttype);
				audit.mField = "name";
				audit.mOldValue = producttypeOld.mName;
				audit.mNewValue = producttype.mName;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, ProductType producttype)
		{
			audit.mUserFullName = producttype.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_ProductType);
			audit.mRowId = producttype.mId;
			audit.mAction = 2;
		}
	}
}