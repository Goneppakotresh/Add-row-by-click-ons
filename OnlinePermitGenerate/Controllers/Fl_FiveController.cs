using IEMS_WEB.Areas.OnlinePermitGenerate.Interface;
using IEMS_WEB.Areas.OnlinePermitGenerate.Models.Request;
using IEMS_WEB.Areas.OnlinePermitGenerate.Models.Response;
using IEMS_WEB.Areas.Wallet.Models.Response;
using IEMS_WEB.Comman;
using IEMS_WEB.Interface;
using IEMS_WEB.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Org.BouncyCastle.Ocsp;

namespace IEMS_WEB.Areas.OnlinePermitGenerate.Controllers
{
    [Area("OnlinePermitGenerate")]
    public class Fl_FiveController : Controller
    {
        private readonly IFL_Five _flFive;
        private readonly IDropDownList _dropDownList;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public Fl_FiveController(IDropDownList IDropDownList, IFL_Five FL_Five, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _dropDownList = IDropDownList;
            _flFive = FL_Five;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> FL_FiveList(string FromDate = "", string ToDate = "", string ModuleName = "", string ReqValues = "", string ReqName = "")
        {
            Fl_FiveListResponse List = new Fl_FiveListResponse();
            Fl_FiveListRequestModel reqModel = new Fl_FiveListRequestModel();
            try
            {
                reqModel.FromDate = FromDate;
                reqModel.ToDate = ToDate;
                reqModel.ModuleName = ModuleName;
                reqModel.ReqValues = ReqValues;
                reqModel.ReqName = ReqName;
                reqModel.refId = User.Identity.GetUserRefId();
                reqModel.UserID = User.Identity.GetId();
                reqModel.licenseeCode = User.Identity.GetLicenseeCode();
                reqModel.Token = User.Identity.GetToken();
                reqModel.LicenseeType = User.Identity.GetLicenseeType();
                List = await _flFive.GetFl_FiveList(reqModel);

            }
            catch (Exception ex)
            {

            }
            return View(List);
        }
        [HttpGet]
        public async Task<IActionResult> FL_FiveReports(string SanctionNo = "", string PermitNo = "", string Type = "SPIRITIMPORT")
        {
            FL_FiveSaveModel model = new FL_FiveSaveModel();
            try
            {
                if (!string.IsNullOrEmpty(SanctionNo) && !string.IsNullOrEmpty(PermitNo))
                {
                    FL_FiveRequestModel req = new FL_FiveRequestModel();
                    req.GeneratedNo = SanctionNo;
                    req.Type = Type;
                    req.PermitNo = PermitNo;
                    req.refId = User.Identity.GetUserRefId();
                    req.UserID = User.Identity.GetId();
                    req.licenseeCode = User.Identity.GetLicenseeCode();
                    req.Token = User.Identity.GetToken();
                    req.LicenseeType = User.Identity.GetLicenseeType();
                    model.SanctionDetailsModel = await _flFive.GetSanctionDetails(req);
                    model.ProductDetailsList = model.SanctionDetailsModel.ProductDetailsList;
                    model.SanctionDetailsModel.BtnDisbled = 1;
                    model.Type = Type;
                    model.Message = Newtonsoft.Json.JsonConvert.SerializeObject(req);
                }

            }
            catch (Exception ex)
            {

            }
            return PartialView("FL_FiveReports", model);
        }
        public async Task<FileResult> DownlloadDierctFileFl5(string SanctionNo = "", string PermitNo = "", string Type = "SPIRITIMPORT")
        {
            FL5ReportResponseModel model = new FL5ReportResponseModel();
            byte[] filebytes;
            if (!string.IsNullOrEmpty(SanctionNo) && !string.IsNullOrEmpty(PermitNo))
            {
                FLReportRequestModel req = new FLReportRequestModel();
                req.generatedNo = PermitNo;
                req.reqType = Type;
                req.reportType = "FL5";
                req.refId = User.Identity.GetUserRefId();
                req.UserID = User.Identity.GetId();
                req.licenseeCode = User.Identity.GetLicenseeCode();
                req.Token = User.Identity.GetToken();
                req.LicenseeType = User.Identity.GetLicenseeType();
                model = await _flFive.GetFl5Reports(req);
                string Logopath = _hostingEnvironment.WebRootPath + DepartmentLogo.GetDeptLogo("excise");
                filebytes = Reports.SpriteFL5(model, Logopath.ToLower());
            }
            else
            {
                filebytes = new byte[0];
            }

            //load file data however you please
            return File(filebytes, "application/pdf", "FL5_" + model.FL5NO + ".pdf");
        }
        //public async Task<FileResult> DownlloadFileFl5(string data = "", string Type = "SPIRITIMPORT")
        //{
        //    FL_FiveSaveModel model = new FL_FiveSaveModel();
        //    FL_FiveRequestModel req = new FL_FiveRequestModel();
        //    byte[] filebytes;
        //    try
        //    {
        //        req.refId = User.Identity.GetUserRefId();
        //        req.UserID = User.Identity.GetId();
        //        req.licenseeCode = User.Identity.GetLicenseeCode();
        //        req.Token = User.Identity.GetToken();
        //        req.LicenseeType = User.Identity.GetLicenseeType();


        //        req = Newtonsoft.Json.JsonConvert.DeserializeObject<FL_FiveRequestModel>(data);
        //        model.SanctionDetailsModel = await _flFive.GetSanctionDetails(req);
        //        model.ProductDetailsList = model.SanctionDetailsModel.ProductDetailsList;
        //        model.Type = Type;
        //        string Logopath = _hostingEnvironment.WebRootPath + DepartmentLogo.GetDeptLogo("excise");
        //        filebytes = Reports.SpriteFL5(model, Logopath.ToLower());
        //    }
        //    catch (Exception ex)
        //    {
        //        filebytes = new byte[10];
        //    }

        //    //load file data however you please
        //    return File(filebytes, "application/pdf", "FL5_"+model.SanctionDetailsModel.FL5SerialNO+".pdf");
        //}

        [HttpGet]
        public async Task<ActionResult> SelectDropDown(int ParentID = 0, string DropDownType = "")
        {
            List<SelectListItem> itm = new List<SelectListItem>();
            try
            {
                DropDownModel ObjDd = new DropDownModel();
                ObjDd.DropDownType = DropDownType;
                ObjDd.ParentID = ParentID;
                ObjDd.Token = User.Identity.GetToken();
                itm = await _dropDownList.GetDropDown(ObjDd);

            }
            catch (Exception ex)
            {

            }
            return Json(itm);
        }

        [HttpGet]
        public async Task<IActionResult> FL_Five(string SanctionNo = "", string DEOID = "", string Type = "SPIRITIMPORT")
        {
            DropDownModel ObjDd = new DropDownModel();
            FL_FiveSaveModel model = new FL_FiveSaveModel();
            FLFiveDropDownList DropDown = new FLFiveDropDownList();
            FL_FiveDropDownRequestModel reqDropDown = new FL_FiveDropDownRequestModel();
            reqDropDown.Token = User.Identity.GetToken();
            DropDown = await _flFive.GetFLFiveDropDownList(reqDropDown);
            if (!string.IsNullOrEmpty(SanctionNo))
            {
                FL_FiveRequestModel req = new FL_FiveRequestModel();
                req.GeneratedNo = SanctionNo;
                req.Type = Type;
                req.DEO_ID = DEOID;
                req.PermitNo = "";
                req.UserID = User.Identity.GetId();
                req.licenseeCode = User.Identity.GetLicenseeCode();
                req.refId = User.Identity.GetUserRefId();
                req.LicenseeType = Convert.ToString(User.Identity.GetLicenseeType());
                req.Token = User.Identity.GetToken();

                model.SanctionDetailsModel = await _flFive.GetSanctionDetails(req);
                model.ProductDetailsList = model.SanctionDetailsModel.ProductDetailsList;
                model.SanctionDetailsModel.BtnDisbled = 1;
                model.SanctionDetailsModel.SanctionNo = SanctionNo;
                model.Type = Type;
                model.SanctionDetailsModel.ConsigneeCategoryName = !string.IsNullOrEmpty(model.SanctionDetailsModel.ConsigneeCategory) ? DropDown.Product_CategoryList.Where(s => s.Value == model.SanctionDetailsModel.ConsigneeCategory).FirstOrDefault().Text : "";
                model.SanctionDetailsModel.ConsignerCategoryName = !string.IsNullOrEmpty(model.SanctionDetailsModel.ConsignerCategory) ? DropDown.Product_CategoryList.Where(s => s.Value == model.SanctionDetailsModel.ConsignerCategory).FirstOrDefault().Text : "";
                model.DEO_ID =Convert.ToInt32(DEOID);
            }

            ViewBag.LicenseeCategoryList = DropDown.LicenseeCategoryList;
            ViewBag.LicenseList = DropDown.LicenseList;
            ViewBag.DEOLISt = DropDown.DEOList;
            ViewBag.TransportLIST = DropDown.TransportLIST;
            ViewBag.GroupLIST = DropDown.Product_GroupList;
            ViewBag.CategoryLIST = DropDown.Product_CategoryList;
            ViewBag.BrandLIST = DropDown.BrandList;
            ViewBag.PackingLIST = DropDown.PackingList;
            ViewBag.RouteList = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(SanctionNo))
            {
                ViewBag.RouteList = await _dropDownList.GetRouteMasterDropDown(new RouteDetailRequest() { ConsignerId = model.SanctionDetailsModel.ConsignerId.ToString(), ConsigneeId = model.SanctionDetailsModel.ConsigneeId.ToString() });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> FL_Five(FL_FiveSaveModel reqModel)
        {

            if (reqModel.ProductDetailsList.Count > 0)
            {
                foreach (var itm in reqModel.ProductDetailsList)
                {
                    itm.LPL_Qty = Convert.ToString(Math.Round((((100 - Convert.ToDecimal(reqModel.SanctionDetailsModel.Strength)) / 100)
                        * Convert.ToDecimal(itm.BL_Qty)), 3));
                }
            }
            reqModel.SanctionDetailsModel.SanctionNo = reqModel.SanctionNo;
            if (ModelState.IsValid)
            {
                BaseModel model = new BaseModel();
                try
                {
                    #region Get Token
                    reqModel.Token = User.Identity.GetToken();
                    reqModel.UserID = Convert.ToInt32(User.Identity.GetId());
                    reqModel.refId = User.Identity.GetUserRefId();
                    reqModel.LicenseeType = Convert.ToString(User.Identity.GetLicenseeType());
                    #endregion

                    reqModel.GeneratedNo = reqModel.SanctionNo;

                    model = await _flFive.SaveFL_Five(reqModel);
                    if (model != null && model.ResponseModel == 1)
                    {
                        TempData["Alert"] = CommonMessageServices.ShowAlert(Alerts.Success, model.Message);
                    }
                    else if (model != null && model.ResponseModel == 0)
                    {
                        TempData["Alert"] = CommonMessageServices.ShowAlert(Alerts.Warning, model.Message);
                    }
                }
                catch (Exception ex)
                {
                    TempData["Alert"] = CommonMessageServices.ShowAlert(Alerts.Danger, ex.Message);
                }
                return RedirectToAction("FL_FiveList");
            }
            else
            {
                #region Fill DropDown
                FLFiveDropDownList DropDown = new FLFiveDropDownList();
                FL_FiveDropDownRequestModel reqDropDown = new FL_FiveDropDownRequestModel();
                reqDropDown.Token = User.Identity.GetToken();
                DropDown = await _flFive.GetFLFiveDropDownList(reqDropDown);
                ViewBag.LicenseeCategoryList = DropDown.LicenseeCategoryList;
                ViewBag.LicenseList = DropDown.LicenseList;
                ViewBag.DEOLISt = DropDown.DEOList;
                ViewBag.TransportLIST = DropDown.TransportLIST;
                ViewBag.GroupLIST = DropDown.Product_GroupList;
                ViewBag.CategoryLIST = DropDown.Product_CategoryList;
                ViewBag.BrandLIST = DropDown.BrandList;
                ViewBag.PackingLIST = DropDown.PackingList;
                ViewBag.RouteList = DropDown.RouteList;
                reqModel.SanctionDetailsModel.BtnDisbled = 1;
                #endregion
                return View(reqModel);
            }
        }
    }
}
