using System.Security.Cryptography;

namespace PredictiveMaintenance.Models
{
    public static class GenerateFakeModels
    {
        public static PredictiveMaintenanceModel GeneratePWF()
        {
            return new PredictiveMaintenanceModel() { Type = "3", AirTemperature = 147, ProcessTemperature = 308, RotationalSpeed = 1399, Torque = 61, ToolWear = 61 };
        }
        public static PredictiveMaintenanceModel GenerateTWF()
        {
            return new PredictiveMaintenanceModel() { Type = "1", AirTemperature = 1, ProcessTemperature = 1, RotationalSpeed = 1, Torque = 1, ToolWear = 1 };
        }
        public static PredictiveMaintenanceModel GenerateOSF()
        {
            return new PredictiveMaintenanceModel() { Type = "1", AirTemperature = 1, ProcessTemperature = 1, RotationalSpeed = 1, Torque = 1, ToolWear = 1 };
        }
        public static PredictiveMaintenanceModel GenerateHDF()
        {
            return new PredictiveMaintenanceModel() { Type = "1", AirTemperature = 1, ProcessTemperature = 1, RotationalSpeed = 1, Torque = 1, ToolWear = 1 };
        }
        public static PredictiveMaintenanceModel GenerateRNF()
        {
            return new PredictiveMaintenanceModel() { Type = "1", AirTemperature = 1, ProcessTemperature = 1, RotationalSpeed = 1, Torque = 1, ToolWear = 1 };
        }
    }
}
