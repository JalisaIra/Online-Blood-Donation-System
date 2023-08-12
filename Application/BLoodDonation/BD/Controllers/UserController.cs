using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BD.Models;
using DatabaseLayer;

namespace BD.Controllers
{
    public class UserController : Controller
    {
        AA_BDEntities DB = new AA_BDEntities();
        // GET: User
        public ActionResult UserProfile(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var user = DB.UserTables.Find(id);
            return View(user);
        }

        public ActionResult EditUserProfile(int? id)

        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            
            var userprofile = new RegistrationMV();
            var user = DB.UserTables.Find(id);

            userprofile.UserTypeID = user.UserTypeID;

            
            userprofile.User.UserID = user.UserID;
            userprofile.User. UserName = user.UserName;
           
            userprofile.User. EmailAddress = user.EmailAddress;
            userprofile.User.AccountStatusID = user.AccountStatusID;
            userprofile.User.UserTypeID = user.UserTypeID;
            userprofile.User. Description = user.Description;

            if (user.SeekerTables.Count > 0)
            {
                var seeker = user.SeekerTables.FirstOrDefault();
                userprofile.Seeker.SeekerID = seeker.SeekerID;
                userprofile.Seeker.FullName = seeker.FullName;
                userprofile.Seeker.Age = seeker.Age;
                userprofile.Seeker.CityID = seeker.CityID;

                userprofile.Seeker.BloodGroupID = seeker.BloodGroupID;

                userprofile.Seeker.ContactNo = seeker.ContactNo;
                userprofile.Seeker.CNIC = seeker.CNIC;
                userprofile.Seeker.GenderID = seeker.GenderID;

                userprofile.Seeker.RegistrationDate = seeker.RegistrationDate;
                userprofile.Seeker.Address = seeker.Address;
                userprofile.Seeker.UserID = seeker.UserID;
                userprofile.ContactNo = seeker.ContactNo;
                userprofile.CityID = seeker.CityID;
                userprofile.BloodGroupID = seeker.BloodGroupID;
                userprofile.GenderID = seeker.GenderID;


            }
            else if (user.HospitalTables.Count > 0)
            {
                var hospital = user.HospitalTables.FirstOrDefault();
                userprofile.Hospital.HospitalID = hospital.HospitalID;
                userprofile.Hospital.FullName = hospital.FullName;
                userprofile.Hospital.Address = hospital.Address;
                userprofile.Hospital.PhoneNo = hospital.PhoneNo;
                userprofile.Hospital.WebSite = hospital.WebSite;
                userprofile.Hospital.Email = hospital.Email;
                userprofile.Hospital.Location = hospital.Location;
                userprofile.Hospital.CityID = hospital.HospitalID;

                userprofile.Hospital.UserID = hospital.HospitalID;
                userprofile.ContactNo = hospital.PhoneNo;
                userprofile.CityID = hospital.CityID;

            }
            else if (user.BloodBankTables.Count > 0)
            {
                var bloodbank = user.BloodBankTables.FirstOrDefault();
                userprofile.BloodBank.BloodBankID = bloodbank.BloodBankID;
                userprofile.BloodBank.BloodBankName = bloodbank.BloodBankName;
                userprofile.BloodBank.Address = bloodbank.Address;
                userprofile.BloodBank.PhoneNo = bloodbank.PhoneNo;
                userprofile.BloodBank.Location = bloodbank.Location;
                userprofile.BloodBank.WebSite = bloodbank.WebSite;
                userprofile.BloodBank.Email = bloodbank.Email;
                userprofile.BloodBank.CityID = bloodbank.CityID;

                userprofile.BloodBank.UserID = bloodbank.UserID;
                userprofile.ContactNo = bloodbank.PhoneNo;
                userprofile.CityID = bloodbank.CityID;
            }
            else if (user.DonorTables.Count > 0)
            {
                var donor = user.DonorTables.FirstOrDefault();
                userprofile.Donor.DonorID = donor.DonorID;
                userprofile.Donor. FullName = donor.FullName;
                userprofile.Donor.BloodGroupID = donor.BloodGroupID;
                userprofile.Donor.LastDonationDate = donor.LastDonationDate;
                userprofile.Donor.ContactNo = donor.ContactNo;
                userprofile.Donor.CNIC = donor.CNIC;
                userprofile.Donor.Location = donor.Location;
                userprofile.Donor.CityID = donor.CityID;
                userprofile.Donor.UserID = donor.UserID;
                userprofile.ContactNo = donor.ContactNo;
                userprofile.CityID = donor.CityID;
                userprofile.BloodGroupID = donor.BloodGroupID;


            }
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City",userprofile.CityID);
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupTables.ToList(), "BloodGroupID", "BloodGroup", userprofile.BloodGroupID);
            ViewBag.GenderID = new SelectList(DB.Genders.ToList(), "GenderID", "Gender1", userprofile.GenderID);
            return View(userprofile);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUserProfile(RegistrationMV userprofile)

        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if(ModelState.IsValid)
            {
                var checkuseremail = DB.UserTables.Where(u => u.EmailAddress == userprofile.User.EmailAddress && u.UserID != userprofile.User.UserID).FirstOrDefault();
                    if (checkuseremail == null)
                    {
                    try 
                    { 

                        var user = DB.UserTables.Find(userprofile.User.UserID);
                        user.EmailAddress = userprofile.User.EmailAddress;
                        DB.Entry(user).State = System.Data.Entity.EntityState.Modified;
                        DB.SaveChanges();

                        if (userprofile.Donor.DonorID > 0)
                        {
                            var donor = DB.DonorTables.Find(userprofile.Donor.DonorID);
                            donor.FullName = userprofile.Donor.FullName;
                            donor.BloodGroupID = userprofile.BloodGroupID;
                            donor.ContactNo = userprofile.Donor.ContactNo;
                            donor.CNIC = userprofile.Donor.CNIC;
                            donor.CityID = userprofile.CityID;
                            donor.Location = userprofile.Donor.Location;
                            DB.Entry(donor).State = System.Data.Entity.EntityState.Modified;
                            DB.SaveChanges();
                        }
                        else if (userprofile.Seeker.SeekerID > 0)
                        {
                            var seeker = DB.SeekerTables.Find(userprofile.Seeker.SeekerID);
                            seeker.FullName = userprofile.Seeker.FullName;
                            seeker.BloodGroupID = userprofile.BloodGroupID;
                            seeker.GenderID = userprofile.GenderID;
                            seeker.Age = userprofile.Seeker.Age;
                            seeker.ContactNo = userprofile.Seeker.ContactNo;
                            seeker.CNIC = userprofile.Seeker.CNIC;
                            seeker.CityID = userprofile.Seeker.CityID;
                            seeker.Address = userprofile.Seeker.Address;
                            DB.Entry(seeker).State = System.Data.Entity.EntityState.Modified;
                            DB.SaveChanges();

                        }
                        else if (userprofile.BloodBank.BloodBankID > 0)
                        {
                            var bloodbank = DB.BloodBankTables.Find(userprofile.BloodBank.BloodBankID);
                            bloodbank.BloodBankName = userprofile.BloodBank.BloodBankName;
                            bloodbank.PhoneNo = userprofile.BloodBank.PhoneNo;
                            bloodbank.Email = userprofile.BloodBank.Email;
                            bloodbank.WebSite = userprofile.BloodBank.WebSite;
                            bloodbank.CityID = userprofile.CityID;
                            bloodbank.Address = userprofile.BloodBank.Address;
                            DB.Entry(bloodbank).State = System.Data.Entity.EntityState.Modified;
                            DB.SaveChanges();


                        }
                        else if (userprofile.Hospital.HospitalID > 0)
                        {
                            var hospital = DB.HospitalTables.Find(userprofile.Hospital.HospitalID);
                            hospital.FullName = userprofile.Hospital.FullName;
                            hospital.PhoneNo = userprofile.Hospital.PhoneNo;
                            hospital.Email = userprofile.Hospital.Email;
                            hospital.WebSite = userprofile.Hospital.WebSite;
                            hospital.CityID = userprofile.CityID;
                            hospital.Address = userprofile.Hospital.Address;
                            DB.Entry(hospital).State = System.Data.Entity.EntityState.Modified;
                            DB.SaveChanges();


                        }
                        return RedirectToAction("UserProfile", "User", new { id = userprofile.User.UserID });
                    }
                    catch 
                    {

                    ModelState.AddModelError(string.Empty, "Some Data is Incorrect!Please Provide Correct Details.");

                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty,"User Email is Already Exist!");
                    
                }
                
                
           }
            else
            {
                ModelState.AddModelError(string.Empty, "Some Data is Incorrect!Please Provide Correct Details.");
            }
             
            ViewBag.CityID = new SelectList(DB.CityTables.ToList(), "CityID", "City", userprofile.CityID);
            ViewBag.BloodGroupID = new SelectList(DB.BloodGroupTables.ToList(), "BloodGroupID", "BloodGroup", userprofile.BloodGroupID);
            return View(userprofile);
        }
    }
}