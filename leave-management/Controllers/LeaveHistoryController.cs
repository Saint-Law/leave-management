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
    public class LeaveHistoryController : Controller
    {
        public readonly ILeaveHistoryRepository _repo;
        public readonly IMapper _Mapper;

        public LeaveHistoryController(ILeaveHistoryRepository repo, IMapper Mapper)
        {
            _repo = repo;
            _Mapper = Mapper;
        }

        // GET: LeaveHistoryController
        public ActionResult Index()
        {
            return View();
        }

        // GET: LeaveHistoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LeaveHistoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveHistoryController/Create
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

        // GET: LeaveHistoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeaveHistoryController/Edit/5
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

        // GET: LeaveHistoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveHistoryController/Delete/5
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
