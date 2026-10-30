
using InventoryItemEFCore.Data;
using InventoryItemEFCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace InventoryItemEFCore.Repository
{
    public class AuditLogRepository
    {
        // 1. Add Audit Log
        public void Add(AuditLog auditLog)
        {
            using var context = new ApplicationDbContext();

            context.AuditLogs.Add(auditLog);
            context.SaveChanges();
        }

        // 2. Get All Audit Logs
        public List<AuditLog> GetAll()
        {
            using var context = new ApplicationDbContext();

            return context.AuditLogs
                .OrderBy(log => log.AuditLogId)
                .ToList();
        }

        // 3. Get Audit Logs by Entity ID
        public List<AuditLog> GetByEntityId(int entityId)
        {
            using var context = new ApplicationDbContext();

            return context.AuditLogs
                .Where(log => log.EntityId == entityId)
                .OrderByDescending(log => log.CreatedAt)
                .ToList();
        }
    }
}
