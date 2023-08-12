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
    public class BloodGroupController : Controller
    {
        AA_BDEntities DB = new AA_BDEntities();
        public ActionResult AllBloodGroups()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var bloodgroups = DB.BloodGroupTables.ToList();
            var listbloodgroups = new List<BloodGroupMV>();
            foreach (var bloodgroup in bloodgroups)
            {
                var addbloodgroup = new BloodGroupMV();
                addbloodgroup.BloodGroupID = bloodgroup.BloodGroupID;
                addbloodgroup.BloodGroup = bloodgroup.BloodGroup;
                listbloodgroups.Add(addbloodgroup);
            }
            return View(listbloodgroups);
        }

        public ActionResult Create()
        {
            var bloodgroup = new BloodGroupMV();
            return View(bloodgroup);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BloodGroupMV bloodGroupMV)
        {
            if (ModelState.IsValid)
            {
                var checkbloodgroup = DB.BloodGroupTables.Where(b => b.BloodGroup == bloodGroupMV.BloodGroup).FirstOrDefault();
                if (checkbloodgroup == null)
                {

                    var bloodGroupTable = new BloodGroupTable();
                    bloodGroupTable.BloodGroupID = bloodGroupMV.BloodGroupID;
                    bloodGroupTable.BloodGroup = bloodGroupMV.BloodGroup;
                    DB.BloodGroupTables.Add(bloodGroupTable);
                    DB.SaveChanges();
                    return RedirectToAction("AllBloodGroups");
                }
                else
                {
                    ModelState.AddModelError("BloodGroup", "All Ready Exist!");
                }
            }
            return View(bloodGroupMV);
        }

        public ActionResult Edit(int? id)
        {
            var bloodGroup = DB.BloodGroupTables.Find(id);
            if (bloodGroup == null)
            {
                return HttpNotFound();
            }
            var bloodGroupMV = new BloodGroupMV();
            bloodGroupMV.BloodGroupID = bloodGroup.BloodGroupID;
            bloodGroupMV.BloodGroup = bloodGroup.BloodGroup;
            return View(bloodGroupMV);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BloodGroupMV bloodGroupMV)
        {
            if (ModelState.IsValid)
            {
                var checkbloodgroup = DB.BloodGroupTables.Where(b => b.BloodGroup == bloodGroupMV.BloodGroup && b.BloodGroupID != bloodGroupMV.BloodGroupID).FirstOrDefault();
                if (checkbloodgroup == null)
                {
                    var bloodGroupTable = new BloodGroupTable();
                    bloodGroupTable.BloodGroupID = bloodGroupMV.BloodGroupID;
                    bloodGroupTable.BloodGroup = bloodGroupMV.BloodGroup;
                    DB.Entry(bloodGroupTable).State = EntityState.Modified;
                    DB.SaveChanges();
                    return RedirectToAction("AllBloodGroups");
                }
                else
                {
                    ModelState.AddModelError("BloodGroup", "All Ready Exist!");
                }
            }
            return View(bloodGroupMV);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bloodGroup = DB.BloodGroupTables.Find(id);
            if (bloodGroup == null)
            {
                return HttpNotFound();
            }
            var bloodGroupMV = new BloodGroupMV();
            bloodGroupMV.BloodGroupID = bloodGroup.BloodGroupID;
            bloodGroupMV.BloodGroup = bloodGroup.BloodGroup;
            return View(bloodGroupMV);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int? id)
        {
            var bloodGroup = DB.BloodGroupTables.Find(id);
            DB.BloodGroupTables.Remove(bloodGroup);
            DB.SaveChanges();
            return RedirectToAction("AllBloodGroups");
        }
    }
}