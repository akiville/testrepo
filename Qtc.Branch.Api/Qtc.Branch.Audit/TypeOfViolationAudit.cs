using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class TypeOfViolationAudit
	{
		public static AuditCollection Audit(TypeOfViolation typeofviolation,TypeOfViolation typeofviolationOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (typeofviolation.mName != typeofviolationOld.mName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, typeofviolation);
				audit.mField = "name";
				audit.mOldValue = typeofviolationOld.mName;
				audit.mNewValue = typeofviolation.mName;
				audit_collection.Add(audit);
			}

			if (typeofviolation.mRemarks != typeofviolationOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, typeofviolation);
				audit.mField = "remarks";
				audit.mOldValue = typeofviolationOld.mRemarks;
				audit.mNewValue = typeofviolation.mRemarks;
				audit_collection.Add(audit);
			}

			if (typeofviolation.mIsRfda != typeofviolationOld.mIsRfda)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, typeofviolation);
				audit.mField = "is_rfda";
				audit.mOldValue = typeofviolationOld.mIsRfda.ToString();
				audit.mNewValue = typeofviolation.mIsRfda.ToString();
				audit_collection.Add(audit);
			}

			if (typeofviolation.mTypeofsanctionId != typeofviolationOld.mTypeofsanctionId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, typeofviolation);
				audit.mField = "typeofsanction_id";
				audit.mOldValue = typeofviolationOld.mTypeofsanctionId.ToString();
				audit.mNewValue = typeofviolation.mTypeofsanctionId.ToString();
				audit_collection.Add(audit);
			}

			if (typeofviolation.mRuleName != typeofviolationOld.mRuleName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, typeofviolation);
				audit.mField = "rule_name";
				audit.mOldValue = typeofviolationOld.mRuleName;
				audit.mNewValue = typeofviolation.mRuleName;
				audit_collection.Add(audit);
			}

			if (typeofviolation.mArticleNo != typeofviolationOld.mArticleNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, typeofviolation);
				audit.mField = "article_no";
				audit.mOldValue = typeofviolationOld.mArticleNo;
				audit.mNewValue = typeofviolation.mArticleNo;
				audit_collection.Add(audit);
			}

			if (typeofviolation.mSectionNo != typeofviolationOld.mSectionNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, typeofviolation);
				audit.mField = "section_no";
				audit.mOldValue = typeofviolationOld.mSectionNo;
				audit.mNewValue = typeofviolation.mSectionNo;
				audit_collection.Add(audit);
			}

			if (typeofviolation.mTypeofvioaltionruleId != typeofviolationOld.mTypeofvioaltionruleId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, typeofviolation);
				audit.mField = "typeofvioaltionrule_id";
				audit.mOldValue = typeofviolationOld.mTypeofvioaltionruleId.ToString();
				audit.mNewValue = typeofviolation.mTypeofvioaltionruleId.ToString();
				audit_collection.Add(audit);
			}

			if (typeofviolation.mOffenseLevel != typeofviolationOld.mOffenseLevel)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, typeofviolation);
				audit.mField = "offense_level";
				audit.mOldValue = typeofviolationOld.mOffenseLevel.ToString();
				audit.mNewValue = typeofviolation.mOffenseLevel.ToString();
				audit_collection.Add(audit);
			}

			if (typeofviolation.mIsPerformance != typeofviolationOld.mIsPerformance)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, typeofviolation);
				audit.mField = "is_performance";
				audit.mOldValue = typeofviolationOld.mIsPerformance.ToString();
				audit.mNewValue = typeofviolation.mIsPerformance.ToString();
				audit_collection.Add(audit);
			}

			if (typeofviolation.mRecordId != typeofviolationOld.mRecordId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, typeofviolation);
				audit.mField = "record_id";
				audit.mOldValue = typeofviolationOld.mRecordId.ToString();
				audit.mNewValue = typeofviolation.mRecordId.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, TypeOfViolation typeofviolation)
		{
			audit.mUserFullName = typeofviolation.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_TypeOfViolation);
			audit.mRowId = typeofviolation.mId;
			audit.mAction = 2;
		}
	}
}