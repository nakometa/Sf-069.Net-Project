namespace SportsHub.DAL.Data
{
    public class Configuration
    {
        public static string GetConnectionString()
        {
            // Change 'SportsHub' for a different database name
            return @"Server=(LocalDb)\LocalDB;Database=SportsHub;Trusted_Connection=True;";
        }
    }
}
