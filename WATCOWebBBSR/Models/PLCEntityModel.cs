using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WATCOWebBBSR.Models
{
    public class PLCEntityModel
    {
        [DisplayName("Pilot Zone")]
        public decimal PilotZone { get; set; }
        [DisplayName("Distribution Source")]
        public decimal DistributionSource { get; set; }
        [DisplayName("Reading Date")]
        public DateTime? ReadingDate { get; set; }
        [DisplayName("ESR Level")]
        public decimal ESRLevel { get; set; }
        [DisplayName("Flow Pressure")]
        public decimal FlowPressure { get; set; }
        [DisplayName("Chlorine Analyzer")]
        public decimal ChlorineAnalyzer { get; set; }
        [DisplayName("Last Water Flow Reading")]
        public decimal LastWaterFlowReading { get; set; }
        [DisplayName("Total Water Flow")]
        [Required]
        public decimal TotalWaterFlow { get; set; }
    }


}