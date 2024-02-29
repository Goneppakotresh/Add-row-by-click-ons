using IEMS_WEB.Models;
using System.ComponentModel.DataAnnotations;

namespace IEMS_WEB.Areas.OnlinePermitGenerate.Models.Response
{
    public class Fl_SixList
    {
        public string FL6No { get; set; }
        public string FL6Date { get; set; }
        public string Type { get; set; }
        public string ConsignerName { get; set; }
        public string ConsigneeName { get; set; }
        public string FL5No { get; set; }
    }
    public class Fl_SixListResponse : BaseModel
    {
        public Fl_SixListResponse()
        {
            List = new List<Fl_SixList>();
        }
        public List<Fl_SixList> List { get; set; }
        public string FL6No { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }

    public class FL_SixResponseModel : BaseModel
    {
        public string Fl5No { get; set; }
        [Required]
        public string Fl5Date { get; set; }
        public string GeneratedNo { get; set; }
        public string ConsigneeCategory { get; set; }
        public string ConsignerCategory { get; set; }
        public string ConsignerId { get; set; }
        public string ConsigneeId { get; set; }
        public string ConsignerName { get; set; }
        public string ConsignerCity { get; set; }
        public string ConsigneeName { get; set; }
        public string ConsigneeCity { get; set; }
        public string TotalBl { get; set; }
        public string TotalLPL { get; set; }

        public string ConsigneeAddress { get; set; }
        public string ConsignerAddress { get; set; }

        public string Strength { get; set; }

        public int BtnDisbled { get; set; } = 0;
    }
}

