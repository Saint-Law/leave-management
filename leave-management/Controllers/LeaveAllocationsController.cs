using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using leave_management.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace leave_management.Controllers
{
    public class LeaveAllocationsController : Controller
    {
        public readonly ILeaveAllocationRepository _repo;
        public readonly IMapper _Mapper;

        public LeaveAllocationsController(ILeaveAllocationRepository repo, IMapper Mapper)
        {
            _repo = repo;
            _Mapper = Mapper;
        }

        // GET: LeaveAllocationsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: LeaveAllocationsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
            return View();
        }

        // POST: LeaveAllocationsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
