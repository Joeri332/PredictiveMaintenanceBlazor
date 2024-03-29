﻿namespace PredictiveMaintenance.Database
{
    using Microsoft.EntityFrameworkCore;
    using PredictiveMaintenance.Models;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Person> People { get; set; }
        public DbSet<PredictiveMaintenanceModel> MaintenanceData { get; set; }
        public DbSet<OperatorFeedback> OperatorFeedbacks { get; set; }
        public DbSet<ModelCreationScores> Scores { get; set; }
    }
}
