﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimibotWebserver.Models;

namespace MimibotWebserver.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {

        }

        public DbSet<Speech> Speechs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Upload> Uploads { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<MimibotWebserver.Models.Mark> Mark { get; set; }
        public DbSet<Sms> Sms { get; set; }


    }
}
