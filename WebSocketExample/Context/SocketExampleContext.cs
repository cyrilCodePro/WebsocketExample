using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSocketExample.Model;

namespace WebSocketExample.Context
{
    public class SocketExampleContext : DbContext
    {
        public SocketExampleContext(DbContextOptions<SocketExampleContext> options) : base(options)
        { }

        public DbSet<Student> Student { get; set; }
    }
}
