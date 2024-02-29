using IEMS_WEB.Areas.OnlinePermitGenerate.Models.Request;
using IEMS_WEB.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace IEMS_WEB.Areas.OnlinePermitGenerate.Models.Response
{
    public class Fl_FiveList
    {
        public string Type { get; set; }
        public string GenerateNo { get; set; }
        public string ConsignerName { get; set; }
        public string ConsigneeName { get; set; }
        public string ConsignerCategory { get; set; }
        public string ConsigneeCategory { get; set; }
        public string FLNo { get; set; }
        public string BalanceSanctionQty { get; set; }
        public string RequestedQty { get; set; }
        public string Unit { get; set; }
        public string SanctionQty { get; set; }
    }
    public class Fl_FiveListResponse : BaseModel
    {
        public Fl_FiveListResponse()
        {
            List = new List<Fl_FiveList>();
        }
        public List<Fl_FiveList> List { get; set; }

        public string ReqType { get; set; }
        public string ModuleType { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string ReqValues { get; set; }

    }

    public class FL_FiveResponseModel : BaseModel
    {
        public FL_FiveResponseModel()
        {
            FeeModel = new FL5Fees();
            ProductDetailsList = new List<ProductDetails>();
        }
        public string SanctionNo { get; set; }
        public string SanctionBalance { get; set; }
        public string ConsignerCategory { get; set; }
        [Required]
        public string ConsignerCategoryName { get; set; }
        public string ConsigneeCategory { get; set; }
        [Required]
        public string ConsigneeCategoryName { get; set; }
        public int ConsignerId { get; set; }
        [Required]
        public string ConsignerName { get; set; }
        public string ConsignerCity { get; set; }
        public int ConsigneeId { get; set; }
        [Required]
        public string ConsigneeName { get; set; }
        public string ConsigneeCity { get; set; }

        public string FL5SerialNO { get; set; } = "";
        public string DataOfIssue { get; set; } = "";
        public string RouteName { get; set; } = "";
        public string ValidFl5Date { get; set; } = "";
        public string SealNo { get; set; } = "";
        public string NameOfEscort { get; set; } = "";
        public string Strength { get; set; } = "1";
        public int BtnDisbled { get; set; } = 0;

        public FL5Fees FeeModel { get; set; }

        public List<ProductDetails> ProductDetailsList { get; set; }
    }

    public class FL5Fees
    {
        [Required]
        public string UnitName { get; set; } = "";
        [Required]
        public string FeeTypeName { get; set; } = "";
        public string BuddgetHead { get; set; } = "";
        public string GroupName { get; set; } = "";
        [Required]
        public decimal Amount { get; set; } = 0;
        public decimal BalanceAmount { get; set; } = 0;
    }
    public class FLFiveDropDownList : BaseModel
    {
        public FLFiveDropDownList()
        {
            DEOList = new List<SelectListItem>();
            TransportLIST = new List<SelectListItem>();
            Product_GroupList = new List<SelectListItem>();
            Product_CategoryList = new List<SelectListItem>();
            BrandList = new List<SelectListItem>();
            PackingList = new List<SelectListItem>();
            LicenseeCategoryList = new List<SelectListItem>();
        }
        public List<SelectListItem> DEOList { get; set; }
        public List<SelectListItem> LicenseList { get; set; }
        public List<SelectListItem> TransportLIST { get; set; }
        public List<SelectListItem> Product_GroupList { get; set; }
        public List<SelectListItem> Product_CategoryList { get; set; }
        public List<SelectListItem> BrandList { get; set; }
        public List<SelectListItem> PackingList { get; set; }

        public List<SelectListItem> RouteList { get; set; }

        public List<SelectListItem> LicenseeCategoryList { get; set; }

    }

}
