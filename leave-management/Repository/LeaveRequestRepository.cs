using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _db;
        public LeaveRequestRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(LeaveRequest entity)
        {
            await _db.LeaveRequests.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(LeaveRequest entity)
        {
            _db.LeaveRequests.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<LeaveRequest>> FindAll()
        {
            var history = await _db.LeaveRequests
                .Include(t => t.RequestingEmployee)
                .Include(t => t.ApprovedBy)
                .Include(t => t.LeaveType)
                .ToListAsync();
            return history;
        }

        public async Task<LeaveRequest> FindById(int id)
        {
            var history = await _db.LeaveRequests
                .Include(t => t.RequestingEmployee)
                .Include(t => t.ApprovedBy)
                .Include(t => t.LeaveType)
                .FirstOrDefaultAsync(t => t.Id == id);
            return history;
        }

        public async Task<ICollection<LeaveRequest>> GetLeaveRequestsByEmployee(string employeeid)
        {
            var leaveRequests = await FindAll();
            return leaveRequests.Where(q => q.RequestingEmployeeId == employeeid)
            .ToList();
        }

        public async Task<bool> isExist(int id)
        {
            var exists = _db.LeaveTypes.AnyAsync(x => x.Id == id);
            return await exists;
        }

        public async Task<bool> Save()
        {
            var chan = _db.SaveChangesAsync();
            return await chan > 0;
        }

        public async Task<bool> Update(LeaveRequest entity)
        {
            _db.LeaveRequests.Update(entity);
            return await Save();
        }
    }
}
