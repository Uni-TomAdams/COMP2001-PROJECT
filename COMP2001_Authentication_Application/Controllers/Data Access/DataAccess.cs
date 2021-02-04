using Microsoft.EntityFrameworkCore;
using COMP2001_Authentication_Application.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using COMP2001_Authentication_Application.Helpers;
using System;

namespace COMP2001_Authentication_Application.Data
{
    public class DataAccess : DbContext
    {
        #region constructors

        public DataAccess (DbContextOptions<DataAccess> options): base(options) {}

        #endregion constructors

        #region methods

        /// <summary>
        ///     Registers a new user with the database, including back-end validation logic
        /// </summary>
        /// <param name="User">contains the users model object from the incoming HTTP request</param>
        /// <param name="ResponseMessage">the output response message from the database</param>
        public void Register(UserModel User, out string ResponseMessage)
        {
            // TODO: provide validation logic here

            // Hash password
            string hashedPassword = HashingHelper.HashPassword(User.Password);

            // Create response message, so its direction can be declared to output
            SqlParameter response = new SqlParameter("@ResponseMessage", SqlDbType.VarChar, 128);
            response.Direction = ParameterDirection.Output;

            // Execute Register stored procedure 
            Database.ExecuteSqlRaw("EXEC Register @Forename, @Surname, @Email, @Password, @ResponseMessage OUTPUT",
                new SqlParameter("@Forename", User.Forename),
                new SqlParameter("@Surname", User.Surname),
                new SqlParameter("@Email", User.Email),
                new SqlParameter("@Password", hashedPassword),
                response);

            ResponseMessage = response.Value.ToString();
        }
        
        /// <summary>
        ///     Validate the users login credentials and then create a session for this time-frame
        /// </summary>
        /// <param name="user">contains the users model object from the incoming HTTP request</param>
        public int ValidateUser(UserModel User)
        {
            // TODO: provide validation logic here

            // Hash incoming password, to be matched against database password
            string HashedPassword = HashingHelper.HashPassword(User.Password);

            // Attempt to formulate return value
            SqlParameter verificationStatus = new SqlParameter("ReturnValue", SqlDbType.Int, 128);
            verificationStatus.Direction = ParameterDirection.Output;

            // Attempt to validate users credentials
            Database.ExecuteSqlRaw("EXEC @returnValue = ValidateUser @Email, @Password",
                verificationStatus,
                new SqlParameter("@Email", User.Email),
                new SqlParameter("@Password", HashedPassword));

            // Return validity of credentials
            return Convert.ToInt32(verificationStatus.Value);
        }

        /// <summary>
        ///     Update the specified columns of a user based on values provided from the request object,
        ///     it also handles whether or not a specific field has been given data, or is null / empty.
        /// </summary>
        /// <param name="User"></param>
        public void UpdateUser(UserModel User, int userID)
        {
            // TODO: provide validation logic here

            // Attempt to update the specific values on each column and handle null/empty columns
            Database.ExecuteSqlRaw("EXEC UpdateUser @id, @Forename, @Surname, @Email, @Password",
                new SqlParameter("@id", userID),
                new SqlParameter("@Forename", string.IsNullOrEmpty(User.Forename) ? Convert.DBNull : User.Forename),
                new SqlParameter("@Surname", string.IsNullOrEmpty(User.Surname) ? Convert.DBNull : User.Surname),
                new SqlParameter("@Email", string.IsNullOrEmpty(User.Email) ? Convert.DBNull : User.Email),
                new SqlParameter("@Password", string.IsNullOrEmpty(User.Password) ? Convert.DBNull : HashingHelper.HashPassword(User.Password)));
        }

        #endregion methods

        public DbSet<UserModel> UserModel { get; set; }
    }
}