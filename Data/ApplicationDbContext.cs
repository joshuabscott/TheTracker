using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheTracker.Models;

namespace TheTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext<TTUser>//this is the class that connects directly to the Database
    {
        //ApplicatioDbContext is a representation of our database by links our classes to our Database entities
        //allows us to connect to default database using appsettins.json
        //This is were our Authorization and Authentication comes from
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //MODIFY #14 /Base Controllers with CRUD Operations/ DBSets
        public virtual DbSet<Company> Companies { get; set; } = default!;
        public virtual DbSet<Invite> Invites { get; set; } = default!;
        public virtual DbSet<Notification> Notifications { get; set; } = default!;
        //public virtual DbSet<NotificationType> NotificationTypes { get; set; } = default!;

        public virtual DbSet<Project> Projects { get; set; } = default!;
        public virtual DbSet<ProjectPriority> ProjectPriorities { get; set; } = default!;

        public virtual DbSet<Ticket> Tickets { get; set; } = default!;
        public virtual DbSet<TicketAttachment> TicketAttachments { get; set; } = default!;
        public virtual DbSet<TicketComment> TicketComments { get; set; } = default!;
        public virtual DbSet<TicketHistory> TicketHistories { get; set; } = default!;
        public virtual DbSet<TicketPriority> TicketPriorities { get; set; } = default!;
        public virtual DbSet<TicketStatus> TicketStatuses { get; set; } = default!;
        public virtual DbSet<TicketType> TicketTypes { get; set; } = default!;
    }
}
