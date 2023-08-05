using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class TraineeAudit
	{
		public static AuditCollection Audit(Trainee trainee,Trainee traineeOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (trainee.mEmployeeId != traineeOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainee);
				audit.mField = "employee_id";
				audit.mOldValue = traineeOld.mEmployeeId.ToString();
				audit.mNewValue = trainee.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (trainee.mIsTrainee != traineeOld.mIsTrainee)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainee);
				audit.mField = "is_trainee";
				audit.mOldValue = traineeOld.mIsTrainee.ToString();
				audit.mNewValue = trainee.mIsTrainee.ToString();
				audit_collection.Add(audit);
			}

			if (trainee.mBranchId != traineeOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainee);
				audit.mField = "branch_id";
				audit.mOldValue = traineeOld.mBranchId.ToString();
				audit.mNewValue = trainee.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (trainee.mLmmId != traineeOld.mLmmId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainee);
				audit.mField = "lmm_id";
				audit.mOldValue = traineeOld.mLmmId.ToString();
				audit.mNewValue = trainee.mLmmId.ToString();
				audit_collection.Add(audit);
			}

			if (trainee.mStartDate != traineeOld.mStartDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainee);
				audit.mField = "start_date";
				audit.mOldValue = traineeOld.mStartDate.ToString();
				audit.mNewValue = trainee.mStartDate.ToString();
				audit_collection.Add(audit);
			}

			if (trainee.mEndDate != traineeOld.mEndDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainee);
				audit.mField = "end_date";
				audit.mOldValue = traineeOld.mEndDate.ToString();
				audit.mNewValue = trainee.mEndDate.ToString();
				audit_collection.Add(audit);
			}

			if (trainee.mDatestamp != traineeOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainee);
				audit.mField = "datestamp";
				audit.mOldValue = traineeOld.mDatestamp.ToString();
				audit.mNewValue = trainee.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, Trainee trainee)
		{
			audit.mUserFullName = trainee.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_Trainee);
			audit.mRowId = trainee.mId;
			audit.mAction = 2;
		}
	}
}