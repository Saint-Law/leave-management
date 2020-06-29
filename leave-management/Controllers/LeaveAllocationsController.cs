using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace leave_management.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class LeaveAllocationsController : Controller
    {
        public readonly ILeaveTypeRepository _leaverepo;
        public readonly ILeaveAllocationRepository _leaveallocationrepo;
        public readonly IMapper _Mapper;
        private readonly UserManager<Employee> _userManager;

        public LeaveAllocationsController(ILeaveTypeRepository leaverepo, ILeaveAllocationRepository leaveallocationrepo, IMapper Mapper, UserManager<Employee> userManager)
        {
            _leaveallocationrepo = leaveallocationrepo;
            _leaverepo = leaverepo;
            _Mapper = Mapper;
            _userManager = userManager;
        }

        // GET: LeaveAllocationsController
        public ActionResult Index()
        {
            var leavetypes = _leaverepo.FindAll().ToList();
            var mappedLeaveTypes = _Mapper.Map<List<LeaveType>, List<LeaveTypeVM>>(leavetypes);
            var model = new CreateLeaveAllocationVM
            {
                LeaveTypes = mappedLeaveTypes,
                NumberUpdated = 0
            };
            return View(model);
        }

        public ActionResult SetLeave(int id)
        {
            var leavetypes = _leaverepo.FindById(id);
            var employees = _userManager.GetUsersInRoleAsync("Employee").Result;
            foreach (var emp in employees)
            {
                //retriving leave type for which we are setting the employee
                if (_leaveallocationrepo.CheckAllocation(id, emp.Id))
                    continue;
                var allocation = new LeaveAllocaitionVM
                {
                    DateCreated = DateTime.Now,
                    EmployeeId = emp.Id,
                    LeaveTypeId = id,
                    NumberOfDays = leavetypes.DefaultDays,
                    Period = DateTime.Now.Year
                };
                var leaveallocation = _Mapper.Map<LeaveAllocation>(allocation);
                _leaveallocationrepo.Create(leaveallocation);
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult ListEmployees()
        {
            var employees = _userManager.GetUsersInRoleAsync("Employee").Result;
            var model = _Mapper.Map<List<EmployeeVM>>(employees);
            return View(model);
        }

        // GET: LeaveAllocationsController/Details/5
        public ActionResult Details(string id)
        {
            var employee = _Mapper.Map<EmployeeVM>(_userManager.FindByIdAsync(id).Result);
            var allocations = _Mapper.Map<List<LeaveAllocaitionVM>>(_leaveallocationrepo.GetLeaveAllocationsByEmployee(id));
            var model = new ViewAllocationVM
            {
                Employee = employee,
                LeaveAllocaitions = allocations
            };
            return View(model);
        }

        // GET: LeaveAllocationsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveAllocationsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveAllocationsController/Edit/5
        public ActionResult Edit(int id)
        {
            var leaveallocaton = _leaveallocationrepo.FindById(id);
            var model = _Mapper.Map<EditLeaveAllocationVM>(leaveallocaton);
            return View(model);
        }

        // POST: LeaveAllocationsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditLeaveAllocationVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var record = _leaveallocationrepo.FindById(model.Id);
                record.NumberOfDays = model.NumberofDays;
                var isSuccess = _leaveallocationrepo.Update(record);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Error while saving");
                    return View(model);
                }
                return RedirectToAction(nameof(Details), new { id = model.EmployeeId });
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveAllocationsController/Delete/5
        public ActionResult Delete(int id)
        {

            return View();
        }

        // POST: LeaveAllocationsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
