namespace PredictiveMaintenance.Models
{

    public class PredictionPythonDto
    {
        public string? Type { get; set; }
        public double AirTemperature { get; set; }
        public double ProcessTemperature { get; set; }
        public double RotationalSpeed { get; set; }
        public double Torque { get; set; }
        public int ToolWear { get; set; }    
    }
}



