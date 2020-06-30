using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODLSystem.Library.Common.EntityModel
{
    public class PLCEntityModel
    {
        [Display(Name = "Pilot Zone")]
        public string PilotZone { get; set; }
        [Display(Name = "Distribution Source")]
        public string DistributionSource { get; set; }
        [Display(Name = "Reading Date")]
        public string ReadingDate { get; set; }
        [Display(Name = "ESR Level(Mtr)")]
        public decimal ESRLevel { get; set; }
        [Display(Name = "Flow Pressure(bar)")]
        public decimal FlowPressure { get; set; }
        [Display(Name = "Chlorine Analyzer(PPM)")]
        public decimal ChlorineAnalyzer { get; set; }
        [Display(Name = "Last Water Flow Reading(MQ/h)")]
        public decimal LastWaterFlowReading { get; set; }
        [Display(Name = "Total Water Supplied(MQ)")]
        public decimal TotalWater { get; set; }
    }
}
