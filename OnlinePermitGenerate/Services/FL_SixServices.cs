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
    public class FL_SixServices : IFL_Six
    {
        public async Task<FL_SixResponseModel> GetPermitDetails(FL_SixRequestModel reqModel)
        {
            FL_SixResponseModel resModel = new FL_SixResponseModel();

            using (var client = new HttpClient())
            {
                try
                {
                    #region Call API
                    string str = JsonConvert.SerializeObject(reqModel);
                    string url = URLPORTServices.GetURL(URLPORT.DepoEntryGate);
                    var response = await HttpClientHelper.POSTAPI(url + "PermitGeneration/FetchFL6Details", str, reqModel.Token);
                    #endregion
                    if (response != null)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        resModel = Newtonsoft.Json.JsonConvert.DeserializeObject<FL_SixResponseModel>(data);
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
        public async Task<BaseModel> SaveFL_Six(FL_SixRequestSaveModel reqModel)
        {
            BaseModel responseModel = new BaseModel();
            using (var client = new HttpClient())
            {
                try
                {

                    #region Call API
                    string str = JsonConvert.SerializeObject(reqModel);
                    string url = URLPORTServices.GetURL(URLPORT.DepoEntryGate);
                    var response = await HttpClientHelper.POSTAPI(url + "PermitGeneration/SaveFL6Details", str, reqModel.Token);
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

        public async Task<Fl_SixListResponse> GetFl_SixList(Fl_SixListRequestModel reqModel)
        {

            Fl_SixListResponse resModel = new Fl_SixListResponse();

            using (var client = new HttpClient())
            {
                try
                {
                    #region Call API
                    string str = JsonConvert.SerializeObject(reqModel);
                    string url = URLPORTServices.GetURL(URLPORT.DepoEntryGate);
                    var response = await HttpClientHelper.POSTAPI(url + "PermitGeneration/FetchFL6ReportDetails", str, reqModel.Token);
                    #endregion
                    if (response != null)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        resModel = Newtonsoft.Json.JsonConvert.DeserializeObject<Fl_SixListResponse>(data);
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

        public async Task<FL6ReportResponseModel> GetFl6Reports(FLReportRequestModel reqModel)
        {

            FL6ReportResponseModel resModel = new FL6ReportResponseModel();

            using (var client = new HttpClient())
            {
                try
                {
                    #region Call API
                    string str = JsonConvert.SerializeObject(reqModel);
                    string url = URLPORTServices.GetURL(URLPORT.DepoEntryGate);
                    var response = await HttpClientHelper.POSTAPI(url + "PermitGeneration/FL6Report", str, reqModel.Token);
                    #endregion
                    if (response != null)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        resModel = Newtonsoft.Json.JsonConvert.DeserializeObject<FL6ReportResponseModel>(data);
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
