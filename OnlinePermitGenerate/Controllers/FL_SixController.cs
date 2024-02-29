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

namespace IEMS_WEB.Areas.OnlinePermitGenerate.Controllers
{
    [Area("OnlinePermitGenerate")]
    public class FL_SixController : Controller
    {
        private readonly IDropDownList _dropDownList;
        private readonly IFL_Six _fl_Six;
        private readonly IFL_Five _flFive;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public FL_SixController(IDropDownList IDropDownList, IFL_Six fl_Six, IFL_Five FL_Five, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _dropDownList = IDropDownList;
            _fl_Six = fl_Six;
            _flFive = FL_Five;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> FL_SixList(string FromDate = "", string ToDate = "", string FL6No = "")
        {
            Fl_SixListResponse List = new Fl_SixListResponse();
            Fl_SixListRequestModel reqModel = new Fl_SixListRequestModel();
            try
            {
                reqModel.FromDate = FromDate ?? "";
                reqModel.ToDate = ToDate ?? "";
                reqModel.FL6No = FL6No ?? "";
                reqModel.refId = User.Identity.GetUserRefId();
                reqModel.UserID = User.Identity.GetId();
                reqModel.licenseeCode = User.Identity.GetLicenseeCode();
                reqModel.LicenseeType = User.Identity.GetLicenseeType();
                reqModel.Token = User.Identity.GetToken();
                List = await _fl_Six.GetFl_SixList(reqModel);

            }
            catch (Exception ex)
            {

            }
            return View(List);
        }

        [HttpGet]
        public async Task<IActionResult> FL_Six(string PermitNO = "", string Type = "SPIRITIMPORT")
        {
            FLFiveDropDownList DropDown = new FLFiveDropDownList();
            FL_SixSaveRequestModel model = new FL_SixSaveRequestModel();
            FL_FiveDropDownRequestModel reqDropDown = new FL_FiveDropDownRequestModel();
            reqDropDown.Token = User.Identity.GetToken();
            if (!string.IsNullOrEmpty(PermitNO))
            {
                FL_SixRequestModel req = new FL_SixRequestModel();
                req.PermitNO = PermitNO;
                req.Type = Type;
                req.refId = User.Identity.GetUserRefId();
                req.UserID = User.Identity.GetId();
                req.licenseeCode = User.Identity.GetLicenseeCode();
                req.LicenseeType = User.Identity.GetLicenseeType();
                req.Token = User.Identity.GetToken();
                model.BtnDisbled = 1;
                model.ResModel = await _fl_Six.GetPermitDetails(req);

                model.model.GenerateNo = model.ResModel.GeneratedNo;
            }
            model.model.Type = Type;
            DropDown = await _flFive.GetFLFiveDropDownList(reqDropDown);
            ViewBag.RouteList = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(PermitNO))
            {
                ViewBag.RouteList = await _dropDownList.GetRouteMasterDropDown(new RouteDetailRequest() { ConsignerId = model.ResModel.ConsignerId.ToString(), ConsigneeId = model.ResModel.ConsigneeId.ToString(), Token = User.Identity.GetToken() });
            }
            ViewBag.TransportLIST = DropDown.TransportLIST;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> FL_Six(FL_SixSaveRequestModel reqModel)
        {
            reqModel.model.FL5No = reqModel.ResModel.Fl5No;
            reqModel.model.Fl5Date = reqModel.ResModel.Fl5Date;

            #region Remove Server Side Validation by Rajveer
            ModelState.Clear();
            TryValidateModel(reqModel);
            ModelState.Remove("ResModel.ConsignerAddress");
            ModelState.Remove("ResModel.GeneratedNo");
            ModelState.Remove("ResModel.ConsigneeAddress");
            ModelState.Remove("model.TPDate");
            ModelState.Remove("model.TPNo");

            #endregion


            if (ModelState.IsValid)
            {
                BaseModel model = new BaseModel();
                try
                {
                    #region Get Token
                    reqModel.model.Token = User.Identity.GetToken();
                    reqModel.model.UserID = Convert.ToInt32(User.Identity.UserId());
                    reqModel.model.refId = User.Identity.GetUserRefId();
                    reqModel.model.UserID = User.Identity.GetId();
                    reqModel.model.licenseeCode = User.Identity.GetLicenseeCode();
                    reqModel.model.LicenseeType = User.Identity.GetLicenseeType();
                    #endregion

                    reqModel.model.TPNo = "";
                    model = await _fl_Six.SaveFL_Six(reqModel.model);
                    if (model != null && model.ResponseModel == 1)
                    {
                        TempData["Alert"] = CommonMessageServices.ShowAlert(Alerts.Success, model.Message);
                    }
                    else if (model != null && model.ResponseModel == 0)
                    {
                        TempData["Alert"] = CommonMessageServices.ShowAlert(Alerts.Danger, model.Message);
                    }
                }
                catch (Exception ex)
                {
                    TempData["Alert"] = CommonMessageServices.ShowAlert(Alerts.Danger, ex.Message);
                }
                return RedirectToAction("FL_SixList");
            }
            else
            {
                FLFiveDropDownList DropDown = new FLFiveDropDownList();
                FL_FiveDropDownRequestModel reqDropDown = new FL_FiveDropDownRequestModel();
                DropDown = await _flFive.GetFLFiveDropDownList(reqDropDown);
                ViewBag.RouteList = await _dropDownList.GetRouteMasterDropDown(new RouteDetailRequest() { ConsignerId = reqModel.ResModel.ConsignerId.ToString(), ConsigneeId = reqModel.ResModel.ConsigneeId.ToString() });
                ViewBag.TransportLIST = DropDown.TransportLIST;
                reqModel.BtnDisbled = 1;
                return View(reqModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> FL_SixReports(string SanctionNo = "", string PermitNo = "", string Type = "SPIRITIMPORT")
        {
            FL6ReportResponseModel model = new FL6ReportResponseModel();
            byte[] filebytes;
            try
            {
                if (!string.IsNullOrEmpty(SanctionNo) && !string.IsNullOrEmpty(PermitNo))
                {
                    FLReportRequestModel req = new FLReportRequestModel();
                    req.generatedNo = PermitNo;
                    req.reqType = "FL5";
                    req.reportType = Type;
                    req.refId = User.Identity.GetUserRefId();
                    req.UserID = User.Identity.GetId();
                    req.licenseeCode = User.Identity.GetLicenseeCode();
                    req.Token = User.Identity.GetToken();
                    req.LicenseeType = User.Identity.GetLicenseeType();

                    model = await _fl_Six.GetFl6Reports(req);
                    string Logopath = _hostingEnvironment.WebRootPath + DepartmentLogo.GetDeptLogo("excise");
                    filebytes = new byte[10];/*Reports.SpriteFL6(model, Logopath.ToLower());*/
                }

            }
            catch (Exception ex)
            {

            }
            return PartialView("FL_SixReports", model);
        }

        public async Task<FileResult> DownlloadDierctFileFl6(string SanctionNo = "", string PermitNo = "", string Type = "SPIRITIMPORT")
        {
            FL6ReportResponseModel model = new FL6ReportResponseModel();

            byte[] filebytes;
            if (!string.IsNullOrEmpty(SanctionNo) && !string.IsNullOrEmpty(PermitNo))
            {
                FLReportRequestModel req = new FLReportRequestModel();
                req.generatedNo = PermitNo;
                req.reportType = "FL6";
                req.reqType = Type;


                req.refId = User.Identity.GetUserRefId();
                req.UserID = User.Identity.GetId();
                req.licenseeCode = User.Identity.GetLicenseeCode();
                req.LicenseeType = User.Identity.GetLicenseeType();
                req.Token = User.Identity.GetToken();
                model = await _fl_Six.GetFl6Reports(req);
                string Logopath = _hostingEnvironment.WebRootPath + DepartmentLogo.GetDeptLogo("excise");

                filebytes = Reports.SpriteFL6(model, Logopath);
            }
            else
            {
                filebytes = new byte[0];
            }

            //load file data however you please
            return File(filebytes, "application/pdf", "FL6_" + model.FL6No + ".pdf");




        }
    }
}
