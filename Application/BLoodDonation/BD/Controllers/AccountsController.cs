using BD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseLayer;

namespace BD.Controllers
{
    public class AccountsController : Controller
    {
        AA_BDEntities DB = new AA_BDEntities();
        public ActionResult AllNewUserRequests()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var users = DB.UserTables.Where(u => u.AccountStatusID == 1).ToList();
            return View(users);

        }

        public ActionResult UserDetails(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var user = DB.UserTables.Find(id);
            return View(user);
        }



        public ActionResult UserApproved(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var user = DB.UserTables.Find(id);
            user.AccountStatusID = 2;
            DB.Entry(user).State = System.Data.Entity.EntityState.Modified;
            DB.SaveChanges();

            return RedirectToAction("AllNewUserRequests");
        }

        public ActionResult UserRejected(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var user = DB.UserTables.Find(id);
            user.AccountStatusID = 3;
            DB.Entry(user).State = System.Data.Entity.EntityState.Modified;
            DB.SaveChanges();

            return RedirectToAction("AllNewUserRequests");
        }


        public ActionResult AddNewDonorByBloodBank()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var collectbloodMV = new CollectBloodMV();
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", "0");
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupTables.ToList(), "BloodGroupID", "BloodGroup", "0");
            //ViewBag.GenderID = new SelectList(DB.Genders.ToList(), "GenderID", "Gender1", "0");
            return View(collectbloodMV);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult AddNewDonorByBloodBank(CollectBloodMV collectBloodMV)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            int bloodbankID = 0;
            string bloodbankid = Convert.ToString(Session["BloodBankID"]);
            int.TryParse(bloodbankid, out bloodbankID);
            var currentdate = DateTime.Now.Date;
            var currentcampaign = DB.CampaignTables.Where(c => c.CampaignDate == currentdate && c.BloodBankID == bloodbankID).FirstOrDefault();
            if (ModelState.IsValid)
            {
                using (var transaction = DB.Database.BeginTransaction())
                {
                    try
                    {
                        var checkdonor = DB.DonorTables.Where(d => d.CNIC.Trim().Replace("-", "") == collectBloodMV.DonorDetails.CNIC.Trim().Replace("-", "")).FirstOrDefault();
                        if (checkdonor == null)
                        {
                            var user = new UserTable();
                            user.UserName = collectBloodMV.DonorDetails.FullName.Trim();
                            user.Password = "123";
                            user.EmailAddress = collectBloodMV.DonorDetails.EmailAddress;
                            user.AccountStatusID = 2;
                            user.UserTypeID = 2;
                            user.Description = "Add By Blood Bank";
                            DB.UserTables.Add(user);
                            DB.SaveChanges();

                            var donor = new DonorTable();
                            donor.FullName = collectBloodMV.DonorDetails.FullName;
                            donor.BloodGroupID = collectBloodMV.BloodGroupID;
                            donor.ContactNo = collectBloodMV.DonorDetails.ContactNo;
                            donor.Location = collectBloodMV.DonorDetails.Location;
                            donor.LastDonationDate = DateTime.Now;
                            donor.CNIC = collectBloodMV.DonorDetails.CNIC;
                            donor.CityID = collectBloodMV.CityID;
                            donor.UserID = user.UserID;
                            DB.DonorTables.Add(donor);
                            DB.SaveChanges();
                            checkdonor = DB.DonorTables.Where(d => d.CNIC.Trim().Replace("-", "") == collectBloodMV.DonorDetails.CNIC.Trim().Replace("-", "")).FirstOrDefault();
                        }

                        else if ((DateTime.Now - checkdonor.LastDonationDate).TotalDays < 120)
                        {
                            ModelState.AddModelError(string.Empty, "Donor has already donated blood in 120 days!");
                            transaction.Rollback();
                        }
                       // else
                      // {

                            var checkbloodgroupstock = DB.BloodBankStockTables.Where(s => s.BloodBankID == bloodbankID && s.BloodGroupID == collectBloodMV.BloodGroupID).FirstOrDefault();
                            if (checkbloodgroupstock == null)
                            {
                                var bloodbankstock = new BloodBankStockTable();
                                bloodbankstock.BloodBankID = bloodbankID;
                                bloodbankstock.BloodGroupID = collectBloodMV.BloodGroupID;
                                bloodbankstock.Status = true;
                                bloodbankstock.Quantity = 0;
                                bloodbankstock.Description = "";
                                DB.BloodBankStockTables.Add(bloodbankstock);
                                DB.SaveChanges();
                                checkbloodgroupstock = DB.BloodBankStockTables.Where(s => s.BloodBankID == bloodbankID && s.BloodGroupID == collectBloodMV.BloodGroupID).FirstOrDefault();
                            }
                            checkbloodgroupstock.Quantity += collectBloodMV.Quantity;
                            DB.Entry(checkbloodgroupstock).State = System.Data.Entity.EntityState.Modified;
                            DB.SaveChanges();

                            var collectblooddetail = new BloodStockDetail();
                            collectblooddetail.DonorID = checkdonor.DonorID;
                            collectblooddetail.BloodBankStockID = checkbloodgroupstock.BloodBankStockID;
                            collectblooddetail.BloodGroupID = collectBloodMV.BloodGroupID;
                            collectblooddetail.Quantity = collectBloodMV.Quantity;
                            collectblooddetail.DonatedDateTime = DateTime.Now;
                            collectblooddetail.CampaignID = currentcampaign.CampaignID;
                            DB.BloodStockDetails.Add(collectblooddetail);
                            DB.SaveChanges();

                            checkdonor.LastDonationDate = DateTime.Now;
                            DB.Entry(checkdonor).State = System.Data.Entity.EntityState.Modified;
                            DB.SaveChanges();
                            transaction.Commit();
                            return RedirectToAction("BloodBankStock", "BloodBank");
                       // }
                    }
                    catch
                    {
                        ModelState.AddModelError(string.Empty, "Please Provide Correct Information!");
                        //transaction.Rollback();
                    }
                }     
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please Provide Donor Full Details!");
            }
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", collectBloodMV.CityID);
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupTables.ToList(), "BloodGroupID", "BloodGroup", collectBloodMV.BloodGroupID);
            //ViewBag.GenderID = new SelectList(DB.Genders.ToList(), "GenderID", "Gender1", collectBloodMV.);
            return View(collectBloodMV);
        }

    }
}