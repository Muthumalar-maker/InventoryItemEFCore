
using InventoryItemEFCore.Models;
using InventoryItemEFCore.Repository;

namespace InventoryItemEFCore.Services
{
    public class AuditLogService
    {
        private readonly AuditLogRepository auditLogRepository;

        public AuditLogService()
        {
            auditLogRepository = new AuditLogRepository();
        }

        // 1. Record an Activity
        public void LogActivity(
            int entityId,
            string action,
            string details)
        {
            var auditLog = new AuditLog
            {
                EntityName = "InventoryItem",
                EntityId = entityId,
                Action = action,
                Details = details,
                CreatedAt = DateTime.Now
            };

            auditLogRepository.Add(auditLog);
        }

        // 2. View All Audit Logs
        public void ViewAllAuditLogs()
        {
            var logs = auditLogRepository.GetAll();

            if (logs.Count == 0)
            {
                Console.WriteLine("No audit logs found.");
                return;
            }

            Console.WriteLine("\n=== AUDIT LOG HISTORY ===");

            foreach (var log in logs)
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine($"Audit ID    : {log.AuditLogId}");
                Console.WriteLine($"Entity Name : {log.EntityName}");
                Console.WriteLine($"Entity ID   : {log.EntityId}");
                Console.WriteLine($"Action      : {log.Action}");
                Console.WriteLine($"Details     : {log.Details}");
                Console.WriteLine($"Created At  : {log.CreatedAt}");
            }

            Console.WriteLine("--------------------------------");
        }

        // 3. View Audit Logs by Inventory Item ID
        public void ViewLogsByItemId()
        {
            Console.Write("Enter Inventory Item ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
            {
                Console.WriteLine("Invalid Item ID.");
                return;
            }

            var logs = auditLogRepository.GetByEntityId(id);

            if (logs.Count == 0)
            {
                Console.WriteLine("No audit history found for this item.");
                return;
            }

            Console.WriteLine($"\n=== AUDIT HISTORY FOR ITEM {id} ===");

            foreach (var log in logs)
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine($"Action     : {log.Action}");
                Console.WriteLine($"Details    : {log.Details}");
                Console.WriteLine($"Created At : {log.CreatedAt}");
            }
        }
    }
}
