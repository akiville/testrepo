using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class TrainingAttendanceStatusAudit
	{
		public static AuditCollection Audit(TrainingAttendanceStatus trainingattendancestatus,TrainingAttendanceStatus trainingattendancestatusOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (trainingattendancestatus.mStatus != trainingattendancestatusOld.mStatus)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendancestatus);
				audit.mField = "status";
				audit.mOldValue = trainingattendancestatusOld.mStatus;
				audit.mNewValue = trainingattendancestatus.mStatus;
				audit_collection.Add(audit);
			}

			if (trainingattendancestatus.mRemarks != trainingattendancestatusOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendancestatus);
				audit.mField = "remarks";
				audit.mOldValue = trainingattendancestatusOld.mRemarks;
				audit.mNewValue = trainingattendancestatus.mRemarks;
				audit_collection.Add(audit);
			}

			if (trainingattendancestatus.mUserId != trainingattendancestatusOld.mUserId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendancestatus);
				audit.mField = "user_id";
				audit.mOldValue = trainingattendancestatusOld.mUserId.ToString();
				audit.mNewValue = trainingattendancestatus.mUserId.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendancestatus.mDatestamp != trainingattendancestatusOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendancestatus);
				audit.mField = "datestamp";
				audit.mOldValue = trainingattendancestatusOld.mDatestamp.ToString();
				audit.mNewValue = trainingattendancestatus.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendancestatus.mTextColor != trainingattendancestatusOld.mTextColor)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendancestatus);
				audit.mField = "text_color";
				audit.mOldValue = trainingattendancestatusOld.mTextColor;
				audit.mNewValue = trainingattendancestatus.mTextColor;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, TrainingAttendanceStatus trainingattendancestatus)
		{
			audit.mUserFullName = trainingattendancestatus.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_TrainingAttendanceStatus);
			audit.mRowId = trainingattendancestatus.mId;
			audit.mAction = 2;
		}
	}
}