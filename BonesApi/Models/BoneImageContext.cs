using BonesApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace BonesApi.Models {
    public class BoneImageContext : DbContext {

        public DbSet<BoneImage> BoneImages { get; set; } = null!;

        
        private static string folder = Environment.CurrentDirectory;
        private static string DbPath = System.IO.Path.Join(folder, "/Database/BonesDB.db");
        public static string dataSourceString = $"Data Source={DbPath}";
        public BoneImageContext(DbContextOptions<BoneImageContext> options): base(options) { }
    }
}