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
        public ActionResult Index()
        {
            var LT = _repo.FindAll().ToList();
            var m = _Mapper.Map<List<LeaveType>, List<LeaveTypeVM>>(LT);
            return View(m);
        }

        // GET: LeaveTypes/Details/5
        public ActionResult Details(int id)
        {
            if(!_repo.isExist(id))
            {
                return NotFound();
            }
            var leavetype = _repo.FindById(id);
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
        public ActionResult Create(LeaveTypeVM collection)
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

                var isSuccess = _repo.Create(leaveType);
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
        public ActionResult Edit(int id)
        {
            if (!_repo.isExist(id))
            {
                return NotFound();
            }

            var leavetype = _repo.FindById(id);
            var collection = _Mapper.Map<LeaveTypeVM>(leavetype);

            return View(collection);
        }

        // POST: LeaveTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LeaveTypeVM collection)
        {
            try
            {
                //TODO: Add update logic here 
                if (!ModelState.IsValid)
                {
                    return View(collection);
                }
                var leaveType = _Mapper.Map<LeaveType>(collection);
                var isSuccess = _repo.Update(leaveType);
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
        public ActionResult Delete(int id)
        {
            var leavetype = _repo.FindById(id);
            if (leavetype == null)
            {
                return NotFound();
            }
            var isSuccess = _repo.Delete(leavetype);
            if (!isSuccess)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, LeaveTypeVM collection)
        {
            try
            {
                //TODO: Add delete logic here
                var leavetype = _repo.FindById(id);
                if (leavetype == null)
                {
                    return NotFound();
                }
                var isSuccess = _repo.Delete(leavetype);
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
