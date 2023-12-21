using System.Security.Cryptography;
using PredictiveMaintenance.Enums;

namespace PredictiveMaintenance.Models
{
    public static class GenerateFakeModels
    {
        public static PredictiveMaintenanceModel GeneratePWF()
        {
            return new PredictiveMaintenanceModel() { Type = "1", AirTemperature = 147, ProcessTemperature = 308, RotationalSpeed = 1399, Torque = 61, ToolWear = 61, PredictionFromModel = 1, FailuresEnums = FailuresEnums.PWF };
        }
        public static PredictiveMaintenanceModel GenerateTWF()
        {
            return new PredictiveMaintenanceModel() { Type = "2", AirTemperature = 1, ProcessTemperature = 1, RotationalSpeed = 1, Torque = 1, ToolWear = 1, PredictionFromModel = 1, FailuresEnums = FailuresEnums.TWF };
        }
        public static PredictiveMaintenanceModel GenerateOSF()
        {
            return new PredictiveMaintenanceModel() { Type = "3", AirTemperature = 1, ProcessTemperature = 1, RotationalSpeed = 1, Torque = 1, ToolWear = 1, PredictionFromModel = 1, FailuresEnums = FailuresEnums.OSF };
        }
        public static PredictiveMaintenanceModel GenerateHDF()
        {
            return new PredictiveMaintenanceModel() { Type = "4", AirTemperature = 1, ProcessTemperature = 1, RotationalSpeed = 1, Torque = 1, ToolWear = 1, PredictionFromModel = 1, FailuresEnums = FailuresEnums.HDF };
        }
        public static PredictiveMaintenanceModel GenerateRNF()
        {
            return new PredictiveMaintenanceModel() { Type = "5", AirTemperature = 1, ProcessTemperature = 1, RotationalSpeed = 1, Torque = 1, ToolWear = 1, PredictionFromModel = 1, FailuresEnums = FailuresEnums.RNF};
        }
    }
}
