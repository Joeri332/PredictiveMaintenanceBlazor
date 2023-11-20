using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PredictiveMaintenance.Models
{

    public class PredictiveMaintenanceModel
    {
        [Key] 
        public int UDI { get; set; }
      
        public string? ProductID { get; set; }
        //TODO change this to enumvalue type
        public string? Type { get; set; }
        public double AirTemperature { get; set; }
        public double ProcessTemperature { get; set; }
        public double RotationalSpeed { get; set; }
        public double Torque { get; set; }
        public int ToolWear { get; set; }
        public int PredictionFromModel { get; set; }
    }

}



