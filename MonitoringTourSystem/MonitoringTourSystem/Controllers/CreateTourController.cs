﻿
using MonitoringTourSystem.EntityFramework;
using MonitoringTourSystem.Models;
using MonitoringTourSystem.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MonitoringTourSystem.Controllers
{
    public class CreateTourController : Controller
    {
        static string pathImage;
        public readonly monitoring_tourEntities MonitoringTourSystem = new monitoring_tourEntities();
        // GET: CreateTour
        public ActionResult Index()
        {
            GetPlaceForTourSchedule();
            return View();
        }
        public ActionResult GetPlaceForTourSchedule()
        {
            var listPlaceTour = MonitoringTourSystem.places.ToList();
            var listTourGuide = MonitoringTourSystem.tourguides.ToList();
            var model = new CreateTourViewModel() { ListPlace = listPlaceTour, ListTourGuide = listTourGuide};
            return PartialView("Index", model);
        }

        [HttpGet]
        public JsonResult GetListPlace(int id)
        {
            var listPlaceTour = from place in MonitoringTourSystem.places
                                where place.province_id == id.ToString()
                                select place;

   
            var jsonString = JsonConvert.SerializeObject(listPlaceTour);
            return Json(jsonString, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetListTourGuide()
        {
            var listTourGuide = MonitoringTourSystem.tourguides.ToList();
            var jsonString = JsonConvert.SerializeObject(listTourGuide);
            return Json(jsonString, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetProvince()
        {
            var listProvince = MonitoringTourSystem.provinces.ToList();
            var jsonString = JsonConvert.SerializeObject(listProvince);
            return Json(jsonString, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddNewTour(tour obj)
        {
            if (pathImage == null)
            {
                var result = new { Success = true, Message = "Dang Upload hinh anh" };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int tourID;
                try
                {
                    tourID = MonitoringTourSystem.tours.Max(x => x.tour_id);
                }
                catch
                {
                    tourID = -1;
                }
                var statusTour = StatusTour.Opening;
                try
                {
                    var tourModel = new tour()
                    {
                        tourguide_id = obj.tourguide_id,
                        tour_code = obj.tour_code,
                        manager_id = obj.manager_id,
                        tour_name = obj.tour_name,
                        departure_date = obj.departure_date,
                        return_date = obj.return_date,
                        tourist_quantity = obj.tourist_quantity,
                        status = statusTour.ToString(),
                        description = obj.description,
                        day = obj.day,
                        cover_photo = pathImage,
                    };

                    for(int i = 0; i < obj.ListTourSchedule.Count; i++)
                    {
                        obj.ListTourSchedule[i].tour_id = tourID + 1;
                    }
                    if (AddNewTour(tourModel, obj.ListTourSchedule))
                    {
                        Response.StatusCode = (int)HttpStatusCode.OK;
                        var result = new { Success = true, Message = "Add Successful" };
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        var result = new { Success = false, Message = "Add Fail" };
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    var result = new { Success = false, Message = ex.Message };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
        }
        // Add data to database
        public bool AddNewTour(tour tourModel, List<tour_schedule> tourScheduleModel)
        {
            try
            {
                // Insert database to tour table
                using (var context = new monitoring_tourEntities())
                {
                    var tourModelData = context.Set<tour>();
                    tourModelData.Add(tourModel);
                    foreach(var item in tourScheduleModel)
                    {
                        var tourScheduleData = context.Set<tour_schedule>();
                        tourScheduleData.Add(item);
                        context.SaveChanges();
                    }
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Content/Images"), pic);

                pathImage = path;
                file.SaveAs(path);
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
            }
            return null;
        }
    }
}