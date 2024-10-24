﻿
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<EventOrganizer> EventsOrganizers { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().HasKey(e => e.Id); 

            modelBuilder.Entity<Ticket>().HasKey(t => t.Id); 

            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("Role")
                .HasValue<Client>("Client")
                .HasValue<EventOrganizer>("Event Organizer")
                .HasValue<SuperAdmin>("Super Admin");

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Event) 
                .WithMany(e => e.Tickets)  
                .HasForeignKey(t => t.EventId) 
                .OnDelete(DeleteBehavior.Cascade);  

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Client)
                .WithMany(c => c.MyTickets)
                .HasForeignKey(t => t.ClientId)
                .OnDelete(DeleteBehavior.SetNull);

            //modelBuilder.Entity<EventOrganizer>()
            //    .HasKey(o => o.Id);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.EventOrganizer)
                .WithMany(o => o.MyEvents)
                .HasForeignKey(e => e.EventOrganizerId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<SuperAdmin>().HasData(
                new SuperAdmin
                {
                    Id = 1,
                    Name = "francoc3",
                    Email = "francocaivano2002@gmail.com",
                    Password = "Contraseña",
                    Phone = "3415526384",
                    Role = "Super Admin"
                }
            );

            modelBuilder.Entity<EventOrganizer>().HasData(
                new EventOrganizer
                {
                    Id = 2,
                    Name = "francob3",
                    Email = "francoberlochi@gmail.com",
                    Password = "Contraseña1",
                    Phone = "3415522312",
                    Role = "Event Organizer",
                    MyEvents = new List<Event>()
                }
            );

            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    Id = 3,
                    Name = "ulisesdb1",
                    Email = "ulisesdebonis@gmail.com",
                    Password = "Contraseña3",
                    Phone = "3415522313",
                    Role = "Client",
                    Age = 20,
                    MyTickets = new List<Ticket>()
                }
            );




            base.OnModelCreating(modelBuilder);
        }
    }
}
