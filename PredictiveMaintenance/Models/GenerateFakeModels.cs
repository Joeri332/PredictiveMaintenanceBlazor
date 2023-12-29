using System.Security.Cryptography;
using PredictiveMaintenance.Enums;

namespace PredictiveMaintenance.Models
{
    public static class GenerateFakeModels
    {
        private static Random random = new Random(); 

        public static string getRandomEngineEnum()
        {
            Array values = Enum.GetValues(typeof(EngineEnums));
            EngineEnums randomEngine = (EngineEnums)values.GetValue(random.Next(values.Length));
            return randomEngine.ToString();
        }
        public static PredictiveMaintenanceModel GeneratePWF()
        {
            return new PredictiveMaintenanceModel() { ProductID = getRandomEngineEnum(), Type = "1", AirTemperature = 147, ProcessTemperature = 308, RotationalSpeed = 1399, Torque = 61, ToolWear = 61, PredictionFromModel = 1, FailuresEnums = FailuresEnums.PWF };
        }
        public static PredictiveMaintenanceModel GenerateTWF()
        {
            return new PredictiveMaintenanceModel() { ProductID = getRandomEngineEnum(), Type = "1", AirTemperature = 1, ProcessTemperature = 1, RotationalSpeed = 1, Torque = 1, ToolWear = 1, PredictionFromModel = 1, FailuresEnums = FailuresEnums.TWF };
        }
        public static PredictiveMaintenanceModel GenerateOSF()
        {
            return new PredictiveMaintenanceModel() { ProductID = getRandomEngineEnum(), Type = "1", AirTemperature = 1, ProcessTemperature = 1, RotationalSpeed = 1, Torque = 1, ToolWear = 1, PredictionFromModel = 1, FailuresEnums = FailuresEnums.OSF };
        }
        public static PredictiveMaintenanceModel GenerateHDF()
        {
            return new PredictiveMaintenanceModel() { ProductID = getRandomEngineEnum(), Type = "1", AirTemperature = 1, ProcessTemperature = 1, RotationalSpeed = 1, Torque = 1, ToolWear = 1, PredictionFromModel = 1, FailuresEnums = FailuresEnums.HDF };
        }
        public static PredictiveMaintenanceModel GenerateRNF()
        {
            return new PredictiveMaintenanceModel() { ProductID = getRandomEngineEnum(), Type = "1", AirTemperature = 1, ProcessTemperature = 1, RotationalSpeed = 1, Torque = 1, ToolWear = 1, PredictionFromModel = 1, FailuresEnums = FailuresEnums.RNF};
        }
    }
}
