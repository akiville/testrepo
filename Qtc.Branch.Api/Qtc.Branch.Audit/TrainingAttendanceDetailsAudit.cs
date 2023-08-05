using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class TrainingAttendanceDetailsAudit
	{
		public static AuditCollection Audit(TrainingAttendanceDetails trainingattendancedetails,TrainingAttendanceDetails trainingattendancedetailsOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (trainingattendancedetails.mTrainingAttendanceId != trainingattendancedetailsOld.mTrainingAttendanceId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendancedetails);
				audit.mField = "training_attendance_id";
				audit.mOldValue = trainingattendancedetailsOld.mTrainingAttendanceId.ToString();
				audit.mNewValue = trainingattendancedetails.mTrainingAttendanceId.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendancedetails.mDateCreated != trainingattendancedetailsOld.mDateCreated)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendancedetails);
				audit.mField = "date_created";
				audit.mOldValue = trainingattendancedetailsOld.mDateCreated.ToString();
				audit.mNewValue = trainingattendancedetails.mDateCreated.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendancedetails.mTypeStatus != trainingattendancedetailsOld.mTypeStatus)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendancedetails);
				audit.mField = "type_status";
				audit.mOldValue = trainingattendancedetailsOld.mTypeStatus;
				audit.mNewValue = trainingattendancedetails.mTypeStatus;
				audit_collection.Add(audit);
			}

			if (trainingattendancedetails.mAttendanceId != trainingattendancedetailsOld.mAttendanceId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendancedetails);
				audit.mField = "attendance_id";
				audit.mOldValue = trainingattendancedetailsOld.mAttendanceId.ToString();
				audit.mNewValue = trainingattendancedetails.mAttendanceId.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, TrainingAttendanceDetails trainingattendancedetails)
		{
			audit.mUserFullName = trainingattendancedetails.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_TrainingAttendanceDetails);
			audit.mRowId = trainingattendancedetails.mId;
			audit.mAction = 2;
		}
	}
}