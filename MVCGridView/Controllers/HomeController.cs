using DemoData;
using OKHMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OKHMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Manage(int? pageNumber)
        {
            var model = new HospitalModel();
            model.PageNumber = (pageNumber == null ? 1 : Convert.ToInt32(pageNumber));
            model.PageSize = 4;

            List<Hospital> hospitals = GetAllFreshHospitals();

            if (hospitals != null)
            {
                model.Hospitals = hospitals.OrderBy(x => x.Id)
                         .Skip(model.PageSize * (model.PageNumber - 1))
                         .Take(model.PageSize).ToList();

                model.TotalCount = hospitals.Count();
                var page = (model.TotalCount / model.PageSize) - (model.TotalCount % model.PageSize == 0 ? 1 : 0);
                model.PagerCount = page + 1;
            }

            return View(model);
        }

        public ActionResult GetHospitals(string sidx, string sord, int page, int rows)
        {
            var hospitals = GetAllFreshHospitals();

            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            int totalRecords = hospitals.Count();
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);

            var data = hospitals.OrderBy(x => x.Id)
                         .Skip(pageSize * (page - 1))
                         .Take(pageSize).ToList();

            var jsonData = new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = data
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetHospitalById(int id)
        {
            var hospitals = GetAllFreshHospitals().Where(x => x.Id == id);

            if (hospitals != null)
            {
                var model = new HospitalModel();

                foreach (var item in hospitals)
                {
                    model.Id = item.Id;
                    model.Name = item.Name;
                    model.City = item.City;
                }

                return PartialView("_GridEditPartial", model);
            }

            return View();
        }

        public ActionResult UpdateHospital(HospitalModel model)
        {
            if (!string.IsNullOrEmpty(model.Name) && !string.IsNullOrEmpty(model.City))
            {
                var hospitals = (List<Hospital>)Session["Hospitals"];

                hospitals.First(d => d.Id == model.Id).Name = model.Name;
                hospitals.First(d => d.Id == model.Id).City = model.City;

                Session["Hospitals"] = hospitals;

                return Content("Hospital updated!!", "text/html");
            }
            else
            {
                return Content("Empty fields, nothing was saved!!", "text/html");
            }
        }

        public ActionResult InsertHospitalView()
        {
            return PartialView("_GridInsertPartial", new HospitalModel());
        }

        public ActionResult InsertHospital(HospitalModel model)
        {
            if (!string.IsNullOrEmpty(model.Name) && !string.IsNullOrEmpty(model.City))
            {
                var hospitals = (List<Hospital>)Session["Hospitals"];
                // create unique id
                int maxid = 0;
                if (hospitals.Count > 0)
                {
                    maxid = hospitals.Max(t => t.Id) + 1;
                }
                else
                {
                    maxid = 1;
                }
                hospitals.Add(new DemoData.Hospital(maxid, model.Name, model.City));
                Session["Hospitals"] = hospitals;

                return Content("Hospital updated!!", "text/html");
            }
            else
            {
                return Content("Empty fields, nothing was saved!!", "text/html");
            }
        }

        public ActionResult DeleteHospital(HospitalModel model)
        {
            var hospitals = (List<Hospital>)Session["Hospitals"];
            var itemToRemove = hospitals.Single(r => r.Id == model.Id);
            hospitals.Remove(itemToRemove);
            Session["Hospitals"] = hospitals;

            return Content("Hospital Deleted!!", "text/html");
        }

        public List<Hospital> GetAllFreshHospitals()
        {
            var hospitals = new List<Hospital>();
            if (Session["Hospitals"] == null)
            {
                hospitals = Hospital.GetSampleHospitals();
                Session["Hospitals"] = hospitals;
            }
            else
            {
                hospitals = (List<Hospital>)Session["Hospitals"];
            }

            return hospitals;
        }
    }
}
