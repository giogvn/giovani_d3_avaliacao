using Bogus;
using giovani_d3_avaliacao.Models;
using giovani_d3_avaliacao.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace giovani_d3_avaliacao.Repositories
{
    public class UserRepository : IUser
    {

        // EDITAR STRING DE CONEXÃO AO BD ABAIXO!
        private readonly string conStr = "";
        

        public int Create(User newUser)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {

                string queryInsert = "INSERT INTO Users (id, nome, email, senha) VALUES (@IdUser, @Name, @Email, @Password)";

                using(SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.Add("@IdUser", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar);
                    
                    cmd.Parameters["@IdUser"].Value = newUser.IdUser;
                    cmd.Parameters["@Name"].Value = newUser.Name;
                    cmd.Parameters["@Email"].Value = newUser.Email;
                    cmd.Parameters["@Password"].Value = newUser.Password;

                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int Delete(string idUser, string pwd)
        {
            using(SqlConnection con = new SqlConnection(conStr))
            {
                string queryDelete = "DELETE FROM Users WHERE id = @IdUser AND senha = @pwd;";
                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@IdUser", idUser);
                    cmd.Parameters.AddWithValue("@pwd", pwd);

                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }                
        }

        public bool Access(string idUser,string email, string pwd)
        {
            string? password ;
            using(SqlConnection con = new SqlConnection(conStr))
            {
                string queryFindUser = "SELECT senha FROM Users WHERE id = @IdUser AND email = @Email;";

                using(SqlCommand cmd = new SqlCommand(queryFindUser, con))
                {
                    cmd.Parameters.AddWithValue("@IdUser", idUser);
                    cmd.Parameters.AddWithValue("@Email", email);
                    
                    con.Open();
                    SqlDataReader rdr;

                    rdr = cmd.ExecuteReader();
                    if (rdr.Read()) password = rdr["senha"].ToString();
                    else return false;                
                }
            }

            if (password != pwd) return false;
            return true;
        }

        public int Update(User user, string field, string newValue)
        {
            string column = string.Empty;
                        
            switch (field)
            {

                case "Name":
                {
                    column = "@Name";
                    break;
                }

                case "Email":
                {
                    column = "@Email";
                    break;
                }

                case "Password":
                {
                    column = "@Password";
                    break;
                }

            }
            
            using(SqlConnection con = new SqlConnection(conStr))
            {
                string queryUpdate = $"UPDATE Users SET {column} = {newValue} WHERE id = @IdUser";
                using(SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@IdUser", user.IdUser);
                    cmd.Parameters.Add(column, SqlDbType.NVarChar);
                    
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }                  
            }            
        }

        public void Cancel()
        {
            System.Environment.Exit(0);
        }

        public void ShutDown()
        {
            System.Environment.Exit(0);
        }

        public void LogOut() {}

        public void LoadData()
        {
            Faker<User> UserFaker() => new Faker<User>()
                .RuleFor(d => d.IdUser, f => f.Random.Guid().ToString())
                .RuleFor(d => d.Name, f => f.Name.FullName())
                .RuleFor(d => d.Email, f => f.Internet.Email(f.Person.FirstName).ToLower())
                .RuleFor(d => d.Password, f => f.Random.Guid().ToString());

            List<User> Users = UserFaker().Generate(15000);

            using (SqlConnection con = new(conStr))
            {
                string queryInsert = "INSERT INTO Users (id, nome, email, senha) VALUES (@Iduser, @Name, @Email, @Pwd)";

                con.Open();

                foreach (var item in Users)
                {
                    using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                    {
                        cmd.Parameters.AddWithValue("@IdUser", item.IdUser);
                        cmd.Parameters.AddWithValue("@Name", item.Name);
                        cmd.Parameters.AddWithValue("@Email", item.Email);
                        cmd.Parameters.AddWithValue("@Pwd", item.Password);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public string GetUserName(string idUser)
        {
            using (SqlConnection con = new(conStr))
            {
                string querySelect = "SELECT nome FROM Users WHERE id = @Iduser;";
                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {
                    cmd.Parameters.AddWithValue("@IdUser", idUser);
                    con.Open();
                    SqlDataReader rdr;

                    rdr = cmd.ExecuteReader();
                    rdr.Read();
                   return rdr["nome"].ToString();
                }
            }
        }

    }
}