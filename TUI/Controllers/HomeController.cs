using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TUI.IDAL;
using TUI.Framework;
using TUI.DTO;
using System.IO;
using System.Collections.ObjectModel;
using System.Globalization;

namespace TUI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// The i home ds
        /// </summary>
        private IHomeDS iHomeDS = UnityManager.Resolve<IHomeDS>();
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Plannings the specified planning.
        /// </summary>
        /// <param name="planning">The planning.</param>
        /// <returns></returns>
        [ActionName("Planning")]
        public ActionResult Planning(Planning planning)
        {
            ModelState.Clear();
            return View("Planning", planning);
        }
        /// <summary>
        /// Plans the register.
        /// </summary>
        /// <param name="planning">The planning.</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Planning")]
        public ActionResult PlanRegister(Planning planning)
        {
            if (ModelState.IsValid)
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    byte[] fileData = null;
                    using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                    {
                        fileData = binaryReader.ReadBytes(Request.Files[0].ContentLength);
                        planning.Photos.Add(fileData);
                    }
                }
                iHomeDS.PlanRegister(planning);
                return RedirectToAction("Joining");
            }
            return View(planning);
        }

        /// <summary>
        /// Joinings the specified join do.
        /// </summary>
        /// <param name="joinDO">The join do.</param>
        /// <returns></returns>
        [ActionName("Joining")]
        public ActionResult Joining(JoinDO joinDO)
        {
            ModelState.Clear();
            return View(joinDO);
        }

        /// <summary>
        /// Searches the planned.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="joinDO">The join do.</param>
        /// <returns></returns>
        public ActionResult SearchPlanned(int page, int rows, JoinDO joinDO)
        {
            Collection<Planning> planningList = new Collection<Planning>();
            planningList = iHomeDS.SearchPlanned(joinDO);
            int pageSize = rows;
            int totalRecords = planningList.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = (
                from data in planningList
                select new
                {
                    cell = new string[]
						{
																																				
							data.State ?? "",
                            data.City ?? "",
                            data.Place ?? "",
                            data.Name ?? "",
                           data.CreatedOn.ToString("MM/dd/yyyy",CultureInfo.CurrentCulture),
                            data.JoinCount.ToString(CultureInfo.CurrentCulture),	
                            data.PlanId.ToString(CultureInfo.CurrentCulture),		
						}
                }).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the state list.
        /// </summary>
        /// <returns></returns>
        public ActionResult GetStateList()
        {
            Collection<DataSource> dataSourceList = new Collection<DataSource>();
            dataSourceList = iHomeDS.GetStateList();
            return Json(new { dataSourceList }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Gets the city list.
        /// </summary>
        /// <param name="Stateid">The stateid.</param>
        /// <returns></returns>
        public ActionResult GetCityList(int? Stateid)
        {
            Collection<DataSource> dataSourceList = new Collection<DataSource>();
            dataSourceList = iHomeDS.GetCityList(Stateid);
            return Json(new { dataSourceList }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Joins the tui.
        /// </summary>
        /// <param name="joinDO">The join do.</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Joining")]
        public ActionResult JoinTUI(JoinDO joinDO)
        {
            if (ModelState.IsValid)
            {
                iHomeDS.JoinTUI(joinDO);
            }
            return View(joinDO);
        }
    }
}
