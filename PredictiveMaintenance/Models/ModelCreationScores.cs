using Microsoft.EntityFrameworkCore;
using PredictiveMaintenance.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PredictiveMaintenance.Models
{

    public class ModelCreationScores
    {
        [Key] 
        public string FileName { get; set; }     
        public string JSonData { get; set; }
    }
}



