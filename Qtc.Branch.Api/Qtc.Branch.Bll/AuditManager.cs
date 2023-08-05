using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Dal;
using System;
namespace Qtc.Branch.Bll
{
    public class AuditManager
    {
        public static string GetNewNumber(string tableName)
        {
            return AuditDB.GetNewNumber(tableName);
        }

        public static DateTime GetDateToday()
        {
            return AuditDB.GetDateToday();
        }

        public static void AuditInsert(bool isSubItem, string userName, int tableId, int rowId, string description)
        {
            BusinessEntities.Audit audit =
                new BusinessEntities.Audit();

            audit.mUserFullName = userName;
            audit.mTableId = tableId;
            audit.mRowId = rowId;
            audit.mAction = 1;
            audit.mDescription = description;

            Save(audit);
        }

        public static void AuditUpdate(bool isSubItem, string userName, int tableId, int rowId, string field, string oldValue, string newValue, string description)
        {
            BusinessEntities.Audit audit =
                 new BusinessEntities.Audit();

            audit.mUserFullName = userName;
            audit.mTableId = tableId;
            audit.mRowId = rowId;
            audit.mField = field;
            audit.mOldValue = oldValue;
            audit.mNewValue = newValue;
            audit.mDescription = description;
            audit.mAction = 2;

            Save(audit);
        }

        public static void AuditDelete(bool isSubItem, string userName, int tableId, int rowId, string description)
        {
            BusinessEntities.Audit audit =
                new BusinessEntities.Audit();

            audit.mUserFullName = userName;
            audit.mTableId = tableId;
            audit.mRowId = rowId;
            audit.mAction = 3;
            audit.mDescription = description;

            Save(audit);
        }

        public static void AuditPrint(string userName, int tableId, int rowId, string description)
        {
            BusinessEntities.Audit audit =
                new BusinessEntities.Audit();

            audit.mUserFullName = userName;
            audit.mTableId = tableId;
            audit.mRowId = rowId;
            audit.mAction = 4;
            audit.mDescription = description;

            Save(audit);
        }

        public static void AuditOthers(string userName, string description)
        {
            BusinessEntities.Audit audit =
                new BusinessEntities.Audit();

            audit.mUserFullName = userName;
            audit.mTableId = 0;
            audit.mRowId = 0;
            audit.mAction = 5;
            audit.mDescription = description;

            Save(audit);
        }

        public static void AuditOthers(string userName, int rowId, string description)
        {
            BusinessEntities.Audit audit =
                new BusinessEntities.Audit();

            audit.mUserFullName = userName;
            audit.mTableId = 0;
            audit.mRowId = rowId;
            audit.mAction = 5;
            audit.mDescription = description;

            Save(audit);
        }
        
        public static void Save(BusinessEntities.Audit audit)
        {
            AuditDB.Save(audit);
        }

        public static AuditCollection GetList(AuditCriteria auditCriteria)
        {
            return AuditDB.GetList(auditCriteria);
        }

        public static void BackUpDatabase(string script, string path)
        {
            AuditDB.BackUpDatabase(script, path);
        }
    }
}
