using System.ComponentModel;

namespace PredictiveMaintenance.Enums
{
    public enum FailuresEnums
    {
        [Description("No Failure")]
        None = 0,

        [Description("Power Failure")]
        PWF = 1,

        [Description("Tool Wear Failure")]
        TWF = 2,

        [Description("Overstrain Failure")]
        OSF = 3,

        [Description("Heat Dissipation Failure")]
        HDF = 4, 
        
        [Description("Random Failure")]
        RNF = 5,
    }
}
