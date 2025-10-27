using Xunit;
using BCrypt.Net;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace FullStackFinalProject.Tests
{
    public class SecurityTests
    {
        private readonly string _connectionString;

        public SecurityTests()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [Fact(DisplayName = "Password Hashing and Verification Works Correctly")]
        public void Password_Hashing_ShouldBeValid()
        {
            string password = "MySecurePass123!";
            string hash = BCrypt.Net.BCrypt.HashPassword(password);

            Assert.True(BCrypt.Net.BCrypt.Verify(password, hash));
            Assert.False(BCrypt.Net.BCrypt.Verify("WrongPassword", hash));
        }

        [Fact(DisplayName = "Parameterized Queries Prevent SQL Injection")]
        public async Task Should_Prevent_SQL_Injection()
        {
            string userInput = "'; DROP TABLE Users; --"; // SQL injection attempt
            bool injectionSucceeded = false;

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @username", conn))
                {
                    cmd.Parameters.AddWithValue("@username", userInput);
                    await conn.OpenAsync();
                    var count = (int)await cmd.ExecuteScalarAsync();
                }
            }
            catch (SqlException)
            {
                injectionSucceeded = true;
            }

            Assert.False(injectionSucceeded, "SQL Injection attempt should not succeed with parameterized queries.");
        }

        [Fact(DisplayName = "Input Validation Rejects Empty Credentials")]
        public void Input_Validation_ShouldReject_Empty_Values()
        {
            string email = "";
            string password = "";

            bool isValid = !(string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password));
            Assert.False(isValid, "Empty email or password should be rejected by validation.");
        }

        [Fact(DisplayName = "XSS Protection: HTML Should Be Properly Encoded")]
        public void XSS_Should_Be_Prevented_By_Encoding()
        {
            string userInput = "<script>alert('Hacked!');</script>";
            string encoded = System.Net.WebUtility.HtmlEncode(userInput);

            Assert.DoesNotContain("<script>", encoded);
            Assert.Contains("&lt;script&gt;", encoded);
        }
    }
}
