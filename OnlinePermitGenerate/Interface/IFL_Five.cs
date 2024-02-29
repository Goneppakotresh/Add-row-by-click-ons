using IEMS_WEB.Areas.DepoTransfers.Models.Response;
using IEMS_WEB.Areas.OnlinePermitGenerate.Models.Request;
using IEMS_WEB.Areas.OnlinePermitGenerate.Models.Response;
using IEMS_WEB.Models;

namespace IEMS_WEB.Areas.OnlinePermitGenerate.Interface
{
    public interface IFL_Five
    {
        Task<Fl_FiveListResponse> GetFl_FiveList(Fl_FiveListRequestModel reqModel);
        Task<FL_FiveResponseModel> GetSanctionDetails(FL_FiveRequestModel reqModel);
        Task<BaseModel> SaveFL_Five(FL_FiveSaveModel reqModel);

        Task<FLFiveDropDownList> GetFLFiveDropDownList(FL_FiveDropDownRequestModel reqModel);

        Task<FL5ReportResponseModel> GetFl5Reports(FLReportRequestModel reqModel);

    }
}
