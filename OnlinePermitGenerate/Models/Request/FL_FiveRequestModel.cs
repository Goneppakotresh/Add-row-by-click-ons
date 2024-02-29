using IEMS_WEB.Areas.OnlinePermitGenerate.Models.Response;
using IEMS_WEB.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace IEMS_WEB.Areas.OnlinePermitGenerate.Models.Request
{

    public class Fl_FiveListRequestModel : BaseModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string ModuleName { get; set; }
        public string ReqValues { get; set; }
        public string ReqName { get; set; }
    }
    public class FL_FiveRequestModel : BaseModel
    {
        public string GeneratedNo { get; set; }
        public string PermitNo { get; set; }
        public string Type { get; set; }
        public string DEO_ID { get; set; } = "";
    }

    public class FL_FiveDropDownRequestModel : BaseModel
    {
        public string Type { get; set; } = "";
    }
    public class FL_FiveSaveModel : BaseModel
    {
        public FL_FiveSaveModel()
        {
            ProductDetailsList = new List<ProductDetails>();
            //ProductDetailsList.Add(new ProductDetails());
            SanctionDetailsModel = new FL_FiveResponseModel();
            ;
        }

        public int DEO_ID { get; set; }

        [Required]
        public string Remarks { get; set; } = "";

        public string Type { get; set; } = "";
        public string SanctionNo { get; set; } = "";
        public string GeneratedNo { get; set; } = "";
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Please enter greater than 0")]
        public int ValidityDay { get; set; } = 0;
        [Required]
        public string ValidityDate { get; set; } = "";
        [Required]
        public string SealNo { get; set; }
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Please Select")]
        public int TransportMode { get; set; }
        [Required]
        public string Fl4Number { get; set; }
        [Required]
        public string FL4Date { get; set; }
        [Required]
        public string NameOfEscort { get; set; }

        public List<ProductDetails> ProductDetailsList { get; set; }

        public FL_FiveResponseModel SanctionDetailsModel { get; set; }

        public int RouteID { get; set; }

    }

    public class ProductDetails
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; } = "";
        public string CategoryID { get; set; } = "";
        public int BrandID { get; set; } = 0;
        public int PackingID { get; set; } = 0;
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Please enter greater than 0")]
        public string BL_Qty { get; set; } = "0";

        public string LPL_Qty { get; set; } = "0";
        public int ItemDetailId { get; set; } = 0;
        public string ItemDetailName { get; set; } = "";
        public int ItemTypeId { get; set; } = 0;
        public string ItemTypeName { get; set; } = "";
        public string UnitName { get; set; } = "";


    }

    public class FLReportRequestModel : BaseModel
    {
        public FLReportRequestModel() { }
        public string reportType { get; set; }
        public string reqType { get; set; }
        public string generatedNo { get; set; }

    }


    #region Reports
    public class FL5ReportResponseModel : BaseModel
    {
        public FL5ReportResponseModel()
        {
            lstProduct = new List<ProductModel>();
            lstChallan = new List<ChallanModel>();
        }
        public string FL5NO { get; set; }
        public string FL5Date { get; set; }
        public string consignerName { get; set; }
        public string consignerAddress { get; set; }
        public string consigneeName { get; set; }
        public string consigneeAddress { get; set; }
        public string routeName { get; set; }
        public string Fl5ValidityDate { get; set; }
        public string sealNo { get; set; }
        public string nameOfEscort { get; set; }
        public string consignerCategory { get; set; }
        public string consigneeCategory { get; set; }
        public string transportMode { get; set; }


        public List<ProductModel> lstProduct { get; set; }
        public List<ChallanModel> lstChallan { get; set; }

    }

    public class ProductModel
    {
        public string productName { get; set; }
        public string brandName { get; set; }
        public string packing { get; set; }
        public string batchNo { get; set; }
        public string BLQty { get; set; }
        public string LPLQty { get; set; }
        public string wastageBLQty { get; set; }
        public string wastageLPLQty { get; set; }
        public string caseQty { get; set; }
        public string strength { get; set; }
        public string dutyType { get; set; }
        public string dutyRate { get; set; }
        public string dutyAmount { get; set; }
    }
    public class ChallanModel
    {
        public string challanNo { get; set; }
        public string challanDate { get; set; }
        public string challanDateChallanNo { get; set; }
        public string bankName { get; set; }
        public string feeType { get; set; }
        public string challanAmount { get; set; }
        public string availAmount { get; set; }
        public string totalFee { get; set; }
        public string adjustmentAmount { get; set; }
        public string availBalance { get; set; }
    }
    #endregion
}
