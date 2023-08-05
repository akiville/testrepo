using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class MessengerGpsAudit
	{
		public static AuditCollection Audit(MessengerGps messengergps,MessengerGps messengergpsOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (messengergps.mUserId != messengergpsOld.mUserId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messengergps);
				audit.mField = "user_id";
				audit.mOldValue = messengergpsOld.mUserId.ToString();
				audit.mNewValue = messengergps.mUserId.ToString();
				audit_collection.Add(audit);
			}

			if (messengergps.mLatitude != messengergpsOld.mLatitude)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messengergps);
				audit.mField = "latitude";
				audit.mOldValue = messengergpsOld.mLatitude.ToString();
				audit.mNewValue = messengergps.mLatitude.ToString();
				audit_collection.Add(audit);
			}

			if (messengergps.mLongitude != messengergpsOld.mLongitude)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messengergps);
				audit.mField = "longitude";
				audit.mOldValue = messengergpsOld.mLongitude.ToString();
				audit.mNewValue = messengergps.mLongitude.ToString();
				audit_collection.Add(audit);
			}

			if (messengergps.mDeviceDate != messengergpsOld.mDeviceDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messengergps);
				audit.mField = "device_date";
				audit.mOldValue = messengergpsOld.mDeviceDate.ToString();
				audit.mNewValue = messengergps.mDeviceDate.ToString();
				audit_collection.Add(audit);
			}

			if (messengergps.mDatestamp != messengergpsOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, messengergps);
				audit.mField = "datestamp";
				audit.mOldValue = messengergpsOld.mDatestamp.ToString();
				audit.mNewValue = messengergps.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, MessengerGps messengergps)
		{
			audit.mUserFullName = messengergps.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_MessengerGps);
			audit.mRowId = messengergps.mId;
			audit.mAction = 2;
		}
	}
}