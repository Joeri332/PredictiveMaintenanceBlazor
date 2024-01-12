using Microsoft.EntityFrameworkCore;
using PredictiveMaintenance.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PredictiveMaintenance.Models
{

    public class GraphScores
    {
        public string Name { get; set; }     
        public double Number { get; set; }
    }
}



