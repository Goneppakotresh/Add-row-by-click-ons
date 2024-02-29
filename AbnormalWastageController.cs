using IEMS_WEB.Areas.ProductionMaster.Interface;
using IEMS_WEB.Areas.ProductionMaster.Models.RequestModel;
using IEMS_WEB.Areas.ProductionMaster.Models.ResponseModel;
using IEMS_WEB.Comman;
using IEMS_WEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace IEMS_WEB.Areas.ProductionMaster.Controllers
{
    [Area("ProductionMaster")]
    public class AbnormalWastageController : Controller
    {
        private readonly IAbnormalWastage _abnormalWastage;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AbnormalWastageController(IAbnormalWastage abnormal, IWebHostEnvironment web) 
        {
            _abnormalWastage = abnormal;
            _webHostEnvironment = web;
        }
        [HttpGet]
        public async Task<IActionResult> AbnormalWastage()
        {
            AbnormalWastageResponseModel model = new AbnormalWastageResponseModel();
            AbnormalWastageRequestModel requestModel = new AbnormalWastageRequestModel();
            try
            {
                #region Get Token
                requestModel.Token = User.Identity.GetToken();
                requestModel.UserID = Convert.ToInt32(User.Identity.GetId());
                requestModel.refId = Convert.ToString(User.Identity.GetUserRefId());
                requestModel.licenseeCode = Convert.ToString(User.Identity.GetLicenseeCode());
                #endregion
                model = await _abnormalWastage.GetAbnormalWastageList(requestModel);
                ViewBag.lstVatName = model.lstVatName;
                ViewBag.lstItemName = model.lstItemName;
                ViewBag.lstReasonName = model.lstReasonName;

            }
            catch (Exception ex)
            {
                TempData["Alert"] = CommonMessageServices.ShowAlert(Alerts.Danger, ex.Message);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AbnormalWastage(AbnormalWastageResponseModel reqModel,IFormFile file)
        {
            BaseModel model = new BaseModel();
            try
            {
                #region Get Token
                reqModel.Token = User.Identity.GetToken();
                reqModel.UserID = Convert.ToInt32(User.Identity.GetId());
                reqModel.refId = Convert.ToString(User.Identity.GetUserRefId());
                reqModel.licenseeCode = Convert.ToString(User.Identity.GetLicenseeCode());
                #endregion
                #region fileUpload
                string uploadsfolder = Path.Combine(_webHostEnvironment.WebRootPath, "assets\\images");
                if (!Directory.Exists(uploadsfolder))
                {
                    Directory.CreateDirectory(uploadsfolder);
                }
                string fileName=Path.GetFileName(file.FileName);
                string fileSavePath = Path.Combine(uploadsfolder, fileName);
                using(FileStream stream=new FileStream(fileSavePath, FileMode.Create))
                {
                    await stream.CopyToAsync(stream);
                }
                #endregion
                reqModel.Model.Fileupload = fileSavePath;


                model = await _abnormalWastage.InsertOrUpdateAbnormalWastage(reqModel);
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
            return RedirectToAction("AbnormalWastage");
        }

        public async Task<IActionResult> ActiveDeactive(int AbnormalWastageId, int Status,int Command)
        {
            AbnormalWastageResponseModel reqModel = new AbnormalWastageResponseModel();
            BaseModel model = new BaseModel();
            try
            {
                reqModel.Status = Status;
                reqModel.Model.AbnormalWastageId = AbnormalWastageId;
                reqModel.Model.Command = Command;
                model = await _abnormalWastage.InsertOrUpdateAbnormalWastage(reqModel);
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

            return Json(new { data = model });
        }
    }
}
