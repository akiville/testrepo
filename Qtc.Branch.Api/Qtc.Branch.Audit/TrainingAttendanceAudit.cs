using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class TrainingAttendanceAudit
	{
		public static AuditCollection Audit(TrainingAttendance trainingattendance,TrainingAttendance trainingattendanceOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (trainingattendance.mRecordId != trainingattendanceOld.mRecordId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance);
				audit.mField = "record_id";
				audit.mOldValue = trainingattendanceOld.mRecordId.ToString();
				audit.mNewValue = trainingattendance.mRecordId.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance.mDateCreated != trainingattendanceOld.mDateCreated)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance);
				audit.mField = "date_created";
				audit.mOldValue = trainingattendanceOld.mDateCreated.ToString();
				audit.mNewValue = trainingattendance.mDateCreated.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance.mEmployeeId != trainingattendanceOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance);
				audit.mField = "employee_id";
				audit.mOldValue = trainingattendanceOld.mEmployeeId.ToString();
				audit.mNewValue = trainingattendance.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance.mLmmId != trainingattendanceOld.mLmmId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance);
				audit.mField = "lmm_id";
				audit.mOldValue = trainingattendanceOld.mLmmId.ToString();
				audit.mNewValue = trainingattendance.mLmmId.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance.mBranchId != trainingattendanceOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance);
				audit.mField = "branch_id";
				audit.mOldValue = trainingattendanceOld.mBranchId.ToString();
				audit.mNewValue = trainingattendance.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance.mOrientationDate != trainingattendanceOld.mOrientationDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance);
				audit.mField = "orientation_date";
				audit.mOldValue = trainingattendanceOld.mOrientationDate.ToString();
				audit.mNewValue = trainingattendance.mOrientationDate.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, TrainingAttendance trainingattendance)
		{
			audit.mUserFullName = trainingattendance.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_TrainingAttendance);
			audit.mRowId = trainingattendance.mId;
			audit.mAction = 2;
		}
	}
}