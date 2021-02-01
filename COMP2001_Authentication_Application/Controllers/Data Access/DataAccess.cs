using Microsoft.EntityFrameworkCore;
using COMP2001_Authentication_Application.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using BCryptHash = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace COMP2001_Authentication_Application.Data
{
    public class DataAccess : DbContext
    {

        #region constructors

        public DataAccess (DbContextOptions<DataAccess> options): base(options) {}

        #endregion constructors

        #region methods

        /// <summary>
        /// Registers a new user with the database, including back-end validation logic
        /// </summary>
        /// <param name="User">contains the users model object from the incoming HTTP request</param>
        /// <param name="ResponseMessage">the output response message from the database</param>
        public void Register(UserModel User, out string ResponseMessage)
        {
            // TODO: provide validation logic here

            // Hash password


            // Create response message, so its direction can be declared to output
            SqlParameter response = new SqlParameter("@ResponseMessage", SqlDbType.VarChar, 128);
            response.Direction = ParameterDirection.Output;

            // Execute Register stored procedure 
            Database.ExecuteSqlRaw("EXEC Register @Forename, @Surname, @Email, @Password, @ResponseMessage OUTPUT",
                new SqlParameter("@Forename", User.Forename),
                new SqlParameter("@Surname", User.Surname),
                new SqlParameter("@Email", User.Email),
                new SqlParameter("@Password", User.Password),
                response);

            ResponseMessage = response.Value.ToString();
        }
        
        /// <summary>
        /// Validate the users login credentials and then create a session for this time-frame
        /// </summary>
        /// <param name="user">contains the users model object from the incoming HTTP request</param>
        public void ValidateUser(UserModel User)
        {
            // Create password output parameter, so its direction can be declared to return
            SqlParameter accountPassword = new SqlParameter("@AccountPassword", SqlDbType.VarChar, 128);
            accountPassword.Direction = ParameterDirection.ReturnValue;

            // Attempt to fetch the users password, if they exist
            Database.ExecuteSqlRaw("EXEC FetchPassword @Password, @AccountPassword",
                new SqlParameter("@Password", User.Password),
                accountPassword);

            
        }

        #endregion methods


        public DbSet<UserModel> UserModel { get; set; }
    }
}
