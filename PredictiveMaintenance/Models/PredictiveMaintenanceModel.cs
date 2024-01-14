using Microsoft.EntityFrameworkCore;
using PredictiveMaintenance.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PredictiveMaintenance.Models
{

    public class PredictiveMaintenanceModel
    {
        [Key] 
        public int UDI { get; set; }
        public string? ProductID { get; set; } = "Engine1";
        public string? Type { get; set; } = "1.0";
        public double AirTemperature { get; set; } = 31.650;
        public double ProcessTemperature { get; set; } = 40.950;
        public double RotationalSpeed { get; set; } = 1256;
        public double Torque { get; set; } = 58.700;
        public int ToolWear { get; set; } = 213;
        public int PredictionFromModel { get; set; }
        public FailuresEnums FailuresEnums { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

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



