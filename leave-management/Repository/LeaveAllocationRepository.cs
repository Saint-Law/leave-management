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

        public bool CheckAllocation(int leavetypeid, string employeeid)
        {
            var period = DateTime.Now.Year;
            return FindAll()
                .Where(x => x.EmployeeId == employeeid && x.LeaveTypeId == leavetypeid && x.Period == period)
                .Any();
        }

        public bool Create(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Add(entity);
            return Save();
        }

        public bool Delete(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Remove(entity);
            return Save();
        }

        public ICollection<LeaveAllocation> FindAll()
        {
            var allocate = _db.LeaveAllocations
                .Include(r => r.LeaveType)
                .Include(r => r.Employee)
                .ToList();
            return allocate;
        }

        public LeaveAllocation FindById(int id)
        {
            var allocate = _db.LeaveAllocations
                .Include(r => r.LeaveType)
                .Include(r => r.Employee)
                .FirstOrDefault( r => r.Id == id);
            return allocate;
        }

        public ICollection<LeaveAllocation> GetLeaveAllocationsByEmployee(string id)
        {
            var period = DateTime.Now.Year;

            return FindAll()
                 .Where(r => r.EmployeeId == id && r.Period == period)
                 .ToList();
        }

        public bool isExist(int id)
        {
            var exists = _db.LeaveTypes.Any(x => x.Id == id);
            return exists;
        }

        public bool Save()
        {
            var hint = _db.SaveChanges();
            return hint > 0;
        }

        public bool Update(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Update(entity);
            return Save();
        }
    }
}
