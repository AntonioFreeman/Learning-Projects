using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebExamenator.Models
{
    public class ExamContext : DbContext 
    {
        public DbSet<Exam> Exams { get; set; }
        public DbSet<TextTask> TextTasks { get; set; }
    }
}