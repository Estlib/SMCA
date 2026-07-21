using Microsoft.EntityFrameworkCore;
using SMCA.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMCA.Database
{
    public class DBConfiguration : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostContent> PostContents { get; set; }
        public DbSet<PostError> PostErrors { get; set; }
        public DbSet<PostModel> PostDatas { get; set; }
        public DbSet<PostSettings> PostSettingses { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO: optimise this shit later
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            string databaseFolder = Path.Combine(appDataFolder, "SMCA");

            Directory.CreateDirectory(databaseFolder);

            string databasePath = Path.Combine(databaseFolder, "SMCA-app.mdf");

            string connectionString =
                $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={databasePath};Initial Catalog=SMCAAppDb;Integrated Security=True;";

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
