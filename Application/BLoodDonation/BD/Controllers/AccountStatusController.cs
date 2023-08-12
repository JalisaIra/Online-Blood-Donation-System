using BD.Models;
using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BD.Controllers
{
    public class AccountStatusController : Controller
    {
        AA_BDEntities DB = new AA_BDEntities();
        public ActionResult AllAccountStatus()
        {

            var accountstatuses = DB.AccountStatusTables.ToList();
            var listaccountstatuses = new List<AccountStatusMV>();
            foreach (var accountstatus in accountstatuses)
            {
                var addaccountstatusmv = new AccountStatusMV();
                addaccountstatusmv.AccountStatusID = accountstatus.AccountStatusID;
                addaccountstatusmv.AccountStatus = accountstatus.AccountStatus;
                listaccountstatuses.Add(addaccountstatusmv);
            }
            return View(listaccountstatuses);
        }

        public ActionResult Create()
        {
            var accountstatus = new AccountStatusMV();
            return View(accountstatus);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountStatusMV accountStatusMV)
        {
            if (ModelState.IsValid)
            {
                var checkaccountstatus = DB.AccountStatusTables.Where(b => b.AccountStatus == accountStatusMV.AccountStatus).FirstOrDefault();
                if (checkaccountstatus == null)
                {

                    var accountStatusTable = new AccountStatusTable();
                    accountStatusTable.AccountStatusID = accountStatusMV.AccountStatusID;
                    accountStatusTable.AccountStatus = accountStatusMV.AccountStatus;
                    DB.AccountStatusTables.Add(accountStatusTable);
                    DB.SaveChanges();
                    return RedirectToAction("AllAccountStatus");
                }
                else
                {
                    ModelState.AddModelError("AccountStatus", "All Ready Exist!");
                }
            }
            return View(accountStatusMV);
        }

        public ActionResult Edit(int? id)
        {
            var accountstatus = DB.AccountStatusTables.Find(id);
            if (accountstatus == null)
            {
                return HttpNotFound();
            }
            var AccountStatusMV = new AccountStatusMV();
            AccountStatusMV.AccountStatusID = accountstatus.AccountStatusID;
            AccountStatusMV.AccountStatus = accountstatus.AccountStatus;
            return View(AccountStatusMV);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountStatusMV accountStatusMV)
        {
            if (ModelState.IsValid)
            {
                var checkbloodgroup = DB.AccountStatusTables.Where(b => b.AccountStatus == accountStatusMV.AccountStatus && b.AccountStatusID != accountStatusMV.AccountStatusID).FirstOrDefault();
                if (checkbloodgroup == null)
                {
                    var accountStatusTable = new AccountStatusTable();
                    accountStatusTable.AccountStatusID = accountStatusMV.AccountStatusID;
                    accountStatusTable.AccountStatus = accountStatusMV.AccountStatus;
                    DB.Entry(accountStatusTable).State = EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("AllAccountStatus");
                }
                else
                {
                    ModelState.AddModelError("BloodGroup", "All Ready Exist!");
                }
            }
            return View(accountStatusMV);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var accountstatuses = DB.AccountStatusTables.Find(id);
            if (accountstatuses == null)
            {
                return HttpNotFound();
            }
            var AccountStatusMV = new AccountStatusMV();
            AccountStatusMV.AccountStatusID = accountstatuses.AccountStatusID;
            AccountStatusMV.AccountStatus = accountstatuses.AccountStatus;
            return View(AccountStatusMV);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int? id)
        {
            var accountstatus = DB.AccountStatusTables.Find(id);
            DB.AccountStatusTables.Remove(accountstatus);
            DB.SaveChanges();
            return RedirectToAction("AllAccountStatus");
        }
    }
}