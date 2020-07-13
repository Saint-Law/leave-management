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
using Microsoft.AspNetCore.Mvc;

namespace leave_management.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class LeaveTypesController : Controller
    {
        public readonly ILeaveTypeRepository _repo;
        public readonly IMapper _Mapper;
      
        public LeaveTypesController(ILeaveTypeRepository repo, IMapper Mapper)
        {
            _repo = repo;
            _Mapper = Mapper;
        }

       
        // GET: LeaveTypes
        public async Task<ActionResult> Index()
        {
            var LT = await _repo.FindAll();
            var m = _Mapper.Map<List<LeaveType>, List<LeaveTypeVM>>(LT.ToList());
            return View(m);
        }

        // GET: LeaveTypes/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var isExists = await _repo.isExist(id);
            if (!isExists)
            {
                return NotFound();
            }
            var leavetype = await _repo.FindById(id);
            var collection = _Mapper.Map<LeaveTypeVM>(leavetype);
            return View(collection);
        }

        // GET: LeaveTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LeaveTypeVM collection)
        {
            try
            {
                //TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View(collection);
                }

                var leaveType = _Mapper.Map<LeaveType>(collection);
                leaveType.DateCreated = DateTime.Now;

                var isSuccess = await _repo.Create(leaveType);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something Went Wrong....");
                    return View(collection);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something Went Wrong....");
                return View();
            }
        }

        // GET: LeaveTypes/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var isExists = await _repo.isExist(id);
            if (!isExists)
            {
                return NotFound();
            }

            var leavetype = await _repo.FindById(id);
            var collection = _Mapper.Map<LeaveTypeVM>(leavetype);
            return View(collection);
        }

        // POST: LeaveTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LeaveTypeVM collection)
        {
            try
            {
                //TODO: Add update logic here 
                if (!ModelState.IsValid)
                {
                    return View(collection);
                }
                var leaveType = _Mapper.Map<LeaveType>(collection);
                var isSuccess = await _repo.Update(leaveType);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something Went Wrong.....");
                    return View(collection);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something Went Wrong.....");
                return View(collection);
            }
        }

        // GET: LeaveTypes/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var leavetype = await _repo.FindById(id);
            if (leavetype == null)
            {
                return NotFound();
            }
            var isSuccess = await _repo.Delete(leavetype);
            if (!isSuccess)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, LeaveTypeVM collection)
        {
            try
            {
                //TODO: Add delete logic here
                var leavetype = await _repo.FindById(id);
                if (leavetype == null)
                {
                    return NotFound();
                }
                var isSuccess = await _repo.Delete(leavetype);
                if (!isSuccess)
                {
                    return View(collection);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(collection);
            }
        }
    }
}
