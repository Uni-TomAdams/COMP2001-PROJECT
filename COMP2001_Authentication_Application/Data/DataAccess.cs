using Microsoft.EntityFrameworkCore;
using COMP2001_Authentication_Application.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace COMP2001_Authentication_Application.Data
{
    public class DataAccess : DbContext
    {
        public DataAccess (DbContextOptions<DataAccess> options): base(options)
        {
        }

        public void Register(UserModel user, out string ResponseMessage)
        {
            // Create response message, so its direction can be declared to output
            SqlParameter response = new SqlParameter("@ResponseMessage", SqlDbType.VarChar, 10);
            response.Direction = ParameterDirection.Output;

            // Execute Register stored procedure 
            Database.ExecuteSqlRaw("EXEC Register @Forename, @Surname, @Email, @Password, @ResponseMessage OUTPUT",
                new SqlParameter("@Forename", user.Forename),
                new SqlParameter("@Surname", user.Surname),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@Password", user.Password),
                response);

            ResponseMessage = response.Value.ToString();
        }

        public DbSet<UserModel> UserModel { get; set; }
    }
}
