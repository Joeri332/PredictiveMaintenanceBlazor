using Microsoft.EntityFrameworkCore;
using PredictiveMaintenance.Enums;
using System.ComponentModel.DataAnnotations;

namespace PredictiveMaintenance.Models
{

    public class PredictiveMaintenanceModel
    {
        [Key] 
        public int UDI { get; set; }      
        public string? ProductID { get; set; }    
        public string? Type { get; set; }
        public double AirTemperature { get; set; }
        public double ProcessTemperature { get; set; }
        public double RotationalSpeed { get; set; }
        public double Torque { get; set; }
        public int ToolWear { get; set; }
        public int PredictionFromModel { get; set; }
        public FailuresEnums FailuresEnums { get; set; }

        public PredictionPythonDto ToDto()
        {
            return new PredictionPythonDto
            {
                Type = this.Type,
                AirTemperature = this.AirTemperature,
                ProcessTemperature = this.ProcessTemperature,
                RotationalSpeed = this.RotationalSpeed,
                Torque = this.Torque,
                ToolWear = this.ToolWear
            };
        }
    }
}



