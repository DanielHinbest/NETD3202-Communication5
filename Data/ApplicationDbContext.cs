﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NETD3202_Communication5.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NETD3202_Communication5.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Score> Scores { get; set; }

        public DbSet<Team> Teams { get; set; }
    }
}
