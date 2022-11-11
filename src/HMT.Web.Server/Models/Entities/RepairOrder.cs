using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMT.Web.Server.Models.Entities
{
    public class RepairOrder
    {
        [Key]
        public int OrderId { get; set; }
        public string SomeUniqueThingInDb { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;

        // Behavior relating to RepairOrder should live here. That's what we mean by rich domain model.

        public int GetCostOfRepairOrder()
        {
            if (Reason == "Crash")
            {
                return 2000;
            }
            else
            {
                return 20;
            }
        }
    }
}
