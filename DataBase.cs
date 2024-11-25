namespace bibaboba3.Classes
{
    internal class Database
    {
        private string connectionString = @"Data Source=DESKTOP-56U78RR; Initial Catalog=Globa; Integrated Security=True;";
        public SqlConnection connection;

        public Database()
        {
            connection = new SqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            connection.Open();
        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public bool Auth(string username, string password)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM [User] WHERE Login = @Login AND Password = @Password", connection);
                command.Parameters.AddWithValue("@Login", username);
                command.Parameters.AddWithValue("@Password", password);
                using var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    int roleId = reader.GetInt32(4); // Получить значение поля RoleId
                    string roleName = roleId == 1 ? "Администратор" : "Работник"; // Определить роль на основе значения RoleId
                    MessageBox.Show($"Login successful! Your role: {roleName}");
                    return true;
                }
                else
                {
                    MessageBox.Show("Incorrect username or password");
                    return false;
                }
            }
        }
    }

}
