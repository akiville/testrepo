using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class HrLetterHistoryMovementAudit
	{
		public static AuditCollection Audit(HrLetterHistoryMovement hrletterhistorymovement,HrLetterHistoryMovement hrletterhistorymovementOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (hrletterhistorymovement.mHrLetterId != hrletterhistorymovementOld.mHrLetterId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletterhistorymovement);
				audit.mField = "hr_letter_id";
				audit.mOldValue = hrletterhistorymovementOld.mHrLetterId.ToString();
				audit.mNewValue = hrletterhistorymovement.mHrLetterId.ToString();
				audit_collection.Add(audit);
			}

			if (hrletterhistorymovement.mSequence != hrletterhistorymovementOld.mSequence)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletterhistorymovement);
				audit.mField = "sequence";
				audit.mOldValue = hrletterhistorymovementOld.mSequence.ToString();
				audit.mNewValue = hrletterhistorymovement.mSequence.ToString();
				audit_collection.Add(audit);
			}

			if (hrletterhistorymovement.mAction != hrletterhistorymovementOld.mAction)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletterhistorymovement);
				audit.mField = "action";
				audit.mOldValue = hrletterhistorymovementOld.mAction;
				audit.mNewValue = hrletterhistorymovement.mAction;
				audit_collection.Add(audit);
			}

			if (hrletterhistorymovement.mRider != hrletterhistorymovementOld.mRider)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletterhistorymovement);
				audit.mField = "rider";
				audit.mOldValue = hrletterhistorymovementOld.mRider;
				audit.mNewValue = hrletterhistorymovement.mRider;
				audit_collection.Add(audit);
			}

			if (hrletterhistorymovement.mSeriesNumber != hrletterhistorymovementOld.mSeriesNumber)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletterhistorymovement);
				audit.mField = "series_number";
				audit.mOldValue = hrletterhistorymovementOld.mSeriesNumber;
				audit.mNewValue = hrletterhistorymovement.mSeriesNumber;
				audit_collection.Add(audit);
			}

			if (hrletterhistorymovement.mDestination != hrletterhistorymovementOld.mDestination)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletterhistorymovement);
				audit.mField = "destination";
				audit.mOldValue = hrletterhistorymovementOld.mDestination;
				audit.mNewValue = hrletterhistorymovement.mDestination;
				audit_collection.Add(audit);
			}

			if (hrletterhistorymovement.mDateTrip != hrletterhistorymovementOld.mDateTrip)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletterhistorymovement);
				audit.mField = "date_trip";
				audit.mOldValue = hrletterhistorymovementOld.mDateTrip;
				audit.mNewValue = hrletterhistorymovement.mDateTrip;
				audit_collection.Add(audit);
			}

			if (hrletterhistorymovement.mBranchName != hrletterhistorymovementOld.mBranchName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletterhistorymovement);
				audit.mField = "branch_name";
				audit.mOldValue = hrletterhistorymovementOld.mBranchName;
				audit.mNewValue = hrletterhistorymovement.mBranchName;
				audit_collection.Add(audit);
			}

			if (hrletterhistorymovement.mDate != hrletterhistorymovementOld.mDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletterhistorymovement);
				audit.mField = "date";
				audit.mOldValue = hrletterhistorymovementOld.mDate.ToString();
				audit.mNewValue = hrletterhistorymovement.mDate.ToString();
				audit_collection.Add(audit);
			}

			if (hrletterhistorymovement.mActionBy != hrletterhistorymovementOld.mActionBy)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletterhistorymovement);
				audit.mField = "action_by";
				audit.mOldValue = hrletterhistorymovementOld.mActionBy;
				audit.mNewValue = hrletterhistorymovement.mActionBy;
				audit_collection.Add(audit);
			}

			if (hrletterhistorymovement.mDatestamp != hrletterhistorymovementOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletterhistorymovement);
				audit.mField = "datestamp";
				audit.mOldValue = hrletterhistorymovementOld.mDatestamp.ToString();
				audit.mNewValue = hrletterhistorymovement.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, HrLetterHistoryMovement hrletterhistorymovement)
		{
			audit.mUserFullName = hrletterhistorymovement.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_HrLetterHistoryMovement);
			audit.mRowId = hrletterhistorymovement.mId;
			audit.mAction = 2;
		}
	}
}