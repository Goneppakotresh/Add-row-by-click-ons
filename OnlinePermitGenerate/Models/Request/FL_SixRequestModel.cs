using IEMS_WEB.Areas.OnlinePermitGenerate.Models.Response;
using IEMS_WEB.Areas.SpiritImport.Interface;
using IEMS_WEB.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace IEMS_WEB.Areas.OnlinePermitGenerate.Models.Request
{
    public class Fl_SixListRequestModel : BaseModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string FL6No { get; set; }
    }
    public class FL_SixRequestModel : BaseModel
    {
        public string PermitNO { get; set; }
        public string GenerateNo { get; set; }
        public string Type { get; set; }
    }

    public class FL_SixRequestSaveModel : BaseModel
    {
        [Required]
        public string FL5No { get; set; }
        [Required]
        public string Fl5Date { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string TruckNo { get; set; }
        [Required]
        public string PersonCarrying { get; set; }

        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Please Select")]
        public int RouteID { get; set; }
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Please enter")]
        public int ValidityDay { get; set; }
        [Required]
        public string ValidityDate { get; set; }

        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Please Select")]
        public int TransportMode { get; set; }

        public string TPNo { get; set; } = "";
        public string TPDate { get; set; } = "";

        [Required]
        public string GenerateNo { get; set; }

    }

    public class FL_SixSaveRequestModel : BaseModel
    {
        public FL_SixSaveRequestModel()
        {
            ResModel = new FL_SixResponseModel();
            model = new FL_SixRequestSaveModel();
        }
        public FL_SixResponseModel ResModel { get; set; }
        public FL_SixRequestSaveModel model { get; set; }

        public int BtnDisbled { get; set; }
    }

    public class FL6ReportResponseModel : BaseModel
    {
        public FL6ReportResponseModel() { lstProduct = new List<ProductModel>(); }
        public List<ProductModel> lstProduct { get; set; }
        public string consignerName { get; set; }
        public string consignerAddress { get; set; }
        public string consigneeName { get; set; }
        public string consigneeAddress { get; set; }
        public string FL5NO { get; set; }
        public string Fl6ValidityDate { get; set; }
        public string FL6No { get; set; }
        public string FL6Date { get; set; }
        public string days { get; set; }
        public string vehicleNo { get; set; }
        public string personCarrying { get; set; }
        public string routeName { get; set; }
        public string deoConsigner { get; set; }
        public string deoConsignee { get; set; }
        public string consignerCategory { get; set; }
        public string consigneeCategory { get; set; }
    }



}
