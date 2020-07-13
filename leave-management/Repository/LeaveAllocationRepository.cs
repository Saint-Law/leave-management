using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveAllocationRepository : ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _db;
        public LeaveAllocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CheckAllocation(int leavetypeid, string employeeid)
        {
            var period = DateTime.Now.Year;
            var allocations = await FindAll();
            return allocations.Where(x => x.EmployeeId == employeeid 
                                        && x.LeaveTypeId == leavetypeid 
                                        && x.Period == period)
                .Any();
        }

        public async Task<bool> Create(LeaveAllocation entity)
        {
            await _db.LeaveAllocations.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Remove(entity);
            return await Save();
        }

        public async Task<ICollection<LeaveAllocation>> FindAll()
        {
            var allocate = await _db.LeaveAllocations
                .Include(r => r.LeaveType)
                .Include(r => r.Employee)
                .ToListAsync();
            return allocate;
        }

        public async Task<LeaveAllocation> FindById(int id)
        {
            var allocate = await _db.LeaveAllocations
                .Include(r => r.LeaveType)
                .Include(r => r.Employee)
                .FirstOrDefaultAsync( r => r.Id == id);
            return allocate;
        }

        public async Task<ICollection<LeaveAllocation>> GetLeaveAllocationsByEmployee(string employeeid)
        {
            var period = DateTime.Now.Year;
            var allocaitons = await FindAll();
            return allocaitons.Where(r => r.EmployeeId == employeeid && r.Period == period)
                 .ToList();
        }

        public async Task<LeaveAllocation> GetLeaveAllocationsByEmployeeAndType(string employeeid, int leavetypeid)
        {
            var period = DateTime.Now.Year;
            var allocations = await FindAll();
            return allocations.FirstOrDefault(r => r.EmployeeId == employeeid 
                                                    && r.Period == period 
                                                    && r.LeaveTypeId == leavetypeid);
        }

        public async Task<bool> isExist(int id)
        {
            var exists = await _db.LeaveTypes.AnyAsync(x => x.Id == id);
            return exists;
        }

        public async Task<bool> Save()
        {
            var hint = await _db.SaveChangesAsync();
            return hint > 0;
        }

        public async Task<bool> Update(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Update(entity);
            return await Save();
        }
    }
}
