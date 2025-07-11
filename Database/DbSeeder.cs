using AduanasTec.Models;

namespace AduanasTec.Database
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext db)
        {
            if (!db.Usuarios.Any(u => u.Username == "admin"))
            {
                db.Usuarios.Add(new Usuario
                {
                    Username = "admin",
                    PasswordHash = "admin123" 
                });
                db.SaveChanges();
            }
        }
    }
} 