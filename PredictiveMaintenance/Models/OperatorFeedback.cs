﻿using PredictiveMaintenance.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class OperatorFeedback
{
    [Key]
    public int FeedbackID { get; set; }  
    public int UDI { get; set; }
    [ForeignKey("UDI")]
    public virtual PredictiveMaintenanceModel PredictiveMaintenance { get; set; }
    public bool IsPredictionCorrect { get; set; } 
    [MaxLength(500)] 
    public string SolutionUsed { get; set; } 
}
