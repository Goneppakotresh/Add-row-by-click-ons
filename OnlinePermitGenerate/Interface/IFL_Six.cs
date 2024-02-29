using IEMS_WEB.Areas.DepoTransfers.Models.Response;
using IEMS_WEB.Areas.OnlinePermitGenerate.Models.Request;
using IEMS_WEB.Areas.OnlinePermitGenerate.Models.Response;
using IEMS_WEB.Models;

namespace IEMS_WEB.Areas.OnlinePermitGenerate.Interface
{
    public interface IFL_Six
    {
        Task<Fl_SixListResponse> GetFl_SixList(Fl_SixListRequestModel reqModel);
        Task<FL_SixResponseModel> GetPermitDetails(FL_SixRequestModel reqModel);
        Task<FL6ReportResponseModel> GetFl6Reports(FLReportRequestModel reqModel);
        Task<BaseModel> SaveFL_Six(FL_SixRequestSaveModel reqModel);
    }
}
