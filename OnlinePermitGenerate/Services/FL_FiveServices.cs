using IEMS_WEB.Areas.DepoTransfers.Models.Request;
using IEMS_WEB.Areas.DepoTransfers.Models.Response;
using IEMS_WEB.Areas.OnlinePermitGenerate.Interface;
using IEMS_WEB.Areas.OnlinePermitGenerate.Models.Request;
using IEMS_WEB.Areas.OnlinePermitGenerate.Models.Response;
using IEMS_WEB.Comman;
using IEMS_WEB.Models;
using Newtonsoft.Json;

namespace IEMS_WEB.Areas.OnlinePermitGenerate.Services
{
    public class FL_FiveServices : IFL_Five
    {
        public async Task<FL_FiveResponseModel> GetSanctionDetails(FL_FiveRequestModel reqModel)
        {
            FL_FiveResponseModel resModel = new FL_FiveResponseModel();

            using (var client = new HttpClient())
            {
                try
                {
                    #region Call API
                    string str = JsonConvert.SerializeObject(reqModel);
                    string url = URLPORTServices.GetURL(URLPORT.DepoEntryGate);
                    var response = await HttpClientHelper.POSTAPI(url + "PermitGeneration/FetchFL5Details", str, reqModel.Token);
                    #endregion
                    if (response != null)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        resModel = Newtonsoft.Json.JsonConvert.DeserializeObject<FL_FiveResponseModel>(data);
                    }
                    else
                    {
                        resModel.Status = 0;
                        resModel.Message = "Somethink Went Wrong";
                    }
                }
                catch (Exception ex)
                {
                    resModel.Status = 0;
                    resModel.Message = ex.Message;
                }
                return resModel;
            }
        }
        public async Task<BaseModel> SaveFL_Five(FL_FiveSaveModel reqModel)
        {
            BaseModel responseModel = new BaseModel();
            using (var client = new HttpClient())
            {
                try
                {

                    #region Call API
                    string str = JsonConvert.SerializeObject(reqModel);
                    string url = URLPORTServices.GetURL(URLPORT.DepoEntryGate);
                    var response = await HttpClientHelper.POSTAPI(url + "PermitGeneration/SaveFL5Details", str, reqModel.Token);
                    #endregion
                    if (response != null)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        responseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<BaseModel>(data);
                    }
                    else
                    {
                        responseModel.Status = 0;
                        responseModel.Message = "Somethink Went Wrong";
                    }
                }
                catch (Exception ex)
                {
                    responseModel.Status = 0;
                    responseModel.Message = ex.Message;
                }
                return responseModel;
            }
        }

        public async Task<FLFiveDropDownList> GetFLFiveDropDownList(FL_FiveDropDownRequestModel reqModel)
        {
            FLFiveDropDownList resModel = new FLFiveDropDownList();

            using (var client = new HttpClient())
            {
                try
                {
                    #region Call API
                    string str = JsonConvert.SerializeObject("");
                    string url = URLPORTServices.GetURL(URLPORT.DepoEntryGate);
                    var response = await HttpClientHelper.POSTAPI(url + "PermitGeneration/FetchFL5Dropdown", str, reqModel.Token);
                    #endregion
                    if (response != null)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        resModel = Newtonsoft.Json.JsonConvert.DeserializeObject<FLFiveDropDownList>(data);
                    }
                    else
                    {
                        resModel.Status = 0;
                        resModel.Message = "Somethink Went Wrong";
                    }
                }
                catch (Exception ex)
                {
                    resModel.Status = 0;
                    resModel.Message = ex.Message;
                }
                return resModel;
            }
        }

        public async Task<Fl_FiveListResponse> GetFl_FiveList(Fl_FiveListRequestModel reqModel)
        {

            Fl_FiveListResponse resModel = new Fl_FiveListResponse();

            using (var client = new HttpClient())
            {
                try
                {
                    #region Call API
                    string str = JsonConvert.SerializeObject(reqModel);
                    string url = URLPORTServices.GetURL(URLPORT.DepoEntryGate);
                    var response = await HttpClientHelper.POSTAPI(url + "PermitGeneration/FetchFL5ReportDetails", str, reqModel.Token);
                    #endregion
                    if (response != null)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        resModel = Newtonsoft.Json.JsonConvert.DeserializeObject<Fl_FiveListResponse>(data);
                    }
                    else
                    {
                        resModel.Status = 0;
                        resModel.Message = "Somethink Went Wrong";
                    }
                }
                catch (Exception ex)
                {
                    resModel.Status = 0;
                    resModel.Message = ex.Message;
                }
                return resModel;
            }
        }


        public async Task<FL5ReportResponseModel> GetFl5Reports(FLReportRequestModel reqModel)
        {
            FL5ReportResponseModel resModel = new FL5ReportResponseModel();

            using (var client = new HttpClient())
            {
                try
                {
                    #region Call API
                    string str = JsonConvert.SerializeObject(reqModel);
                    string url = URLPORTServices.GetURL(URLPORT.DepoEntryGate);
                    var response = await HttpClientHelper.POSTAPI(url + "PermitGeneration/FL5Report", str, reqModel.Token);
                    #endregion
                    if (response != null)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        resModel = Newtonsoft.Json.JsonConvert.DeserializeObject<FL5ReportResponseModel>(data);
                    }
                    else
                    {
                        resModel.Status = 0;
                        resModel.Message = "Somethink Went Wrong";
                    }
                }
                catch (Exception ex)
                {
                    resModel.Status = 0;
                    resModel.Message = ex.Message;
                }
                return resModel;
            }
        }
    }
}
