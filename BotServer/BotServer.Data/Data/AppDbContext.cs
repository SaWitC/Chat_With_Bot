﻿using BotServer.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotServer.Data.DataCofigurations;

namespace BotServer.Data.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }

        public DbSet<ChatModel> Chats { get; set; }
        public DbSet<MessageModel> Messages { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<RemindModel> Reminds { get; set; }
        public DbSet<HubConnections>HubConnections { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ChatConfiguration());
            builder.ApplyConfiguration(new MessageConfiguration());
        }

    }
}
