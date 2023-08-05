using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class ShiftAudit
	{
		public static AuditCollection Audit(Shift shift,Shift shiftOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (shift.mCode != shiftOld.mCode)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, shift);
				audit.mField = "code";
				audit.mOldValue = shiftOld.mCode;
				audit.mNewValue = shift.mCode;
				audit_collection.Add(audit);
			}

			if (shift.mIn != shiftOld.mIn)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, shift);
				audit.mField = "in";
				audit.mOldValue = shiftOld.mIn;
				audit.mNewValue = shift.mIn;
				audit_collection.Add(audit);
			}

			if (shift.mBreakOut != shiftOld.mBreakOut)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, shift);
				audit.mField = "break_out";
				audit.mOldValue = shiftOld.mBreakOut;
				audit.mNewValue = shift.mBreakOut;
				audit_collection.Add(audit);
			}

			if (shift.mBreakIn != shiftOld.mBreakIn)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, shift);
				audit.mField = "break_in";
				audit.mOldValue = shiftOld.mBreakIn;
				audit.mNewValue = shift.mBreakIn;
				audit_collection.Add(audit);
			}

			if (shift.mOut != shiftOld.mOut)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, shift);
				audit.mField = "out";
				audit.mOldValue = shiftOld.mOut;
				audit.mNewValue = shift.mOut;
				audit_collection.Add(audit);
			}

			if (shift.mNShift != shiftOld.mNShift)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, shift);
				audit.mField = "n_shift";
				audit.mOldValue = shiftOld.mNShift.ToString();
				audit.mNewValue = shift.mNShift.ToString();
				audit_collection.Add(audit);
			}

			if (shift.mMinutesNeed != shiftOld.mMinutesNeed)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, shift);
				audit.mField = "minutes_need";
				audit.mOldValue = shiftOld.mMinutesNeed.ToString();
				audit.mNewValue = shift.mMinutesNeed.ToString();
				audit_collection.Add(audit);
			}

			if (shift.mInSchedule != shiftOld.mInSchedule)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, shift);
				audit.mField = "in_schedule";
				audit.mOldValue = shiftOld.mInSchedule.ToString();
				audit.mNewValue = shift.mInSchedule.ToString();
				audit_collection.Add(audit);
			}

			if (shift.mBreakOutSchedule != shiftOld.mBreakOutSchedule)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, shift);
				audit.mField = "break_out_schedule";
				audit.mOldValue = shiftOld.mBreakOutSchedule.ToString();
				audit.mNewValue = shift.mBreakOutSchedule.ToString();
				audit_collection.Add(audit);
			}

			if (shift.mBreakInSchedule != shiftOld.mBreakInSchedule)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, shift);
				audit.mField = "break_in_schedule";
				audit.mOldValue = shiftOld.mBreakInSchedule.ToString();
				audit.mNewValue = shift.mBreakInSchedule.ToString();
				audit_collection.Add(audit);
			}

			if (shift.mOutSchedule != shiftOld.mOutSchedule)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, shift);
				audit.mField = "out_schedule";
				audit.mOldValue = shiftOld.mOutSchedule.ToString();
				audit.mNewValue = shift.mOutSchedule.ToString();
				audit_collection.Add(audit);
			}

			if (shift.mFlexiTime != shiftOld.mFlexiTime)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, shift);
				audit.mField = "flexi_time";
				audit.mOldValue = shiftOld.mFlexiTime.ToString();
				audit.mNewValue = shift.mFlexiTime.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, Shift shift)
		{
			audit.mUserFullName = shift.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_Shift);
			audit.mRowId = shift.mId;
			audit.mAction = 2;
		}
	}
}