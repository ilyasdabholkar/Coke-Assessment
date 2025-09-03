using BackendTask.Models;
using Microsoft.Data.SqlClient;

namespace BackendTask.Data
{
    public class EmployeeRepository
    {
        private readonly string _connectionString;
        public EmployeeRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        public List<Employee> GetAll()
        {
            var result = new List<Employee>();
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            using var cmd = new SqlCommand("SELECT Id, Name, Email, Department, HireDate FROM Employees", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(new Employee
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"],
                    Email = (string)reader["Email"],
                    Department = (string)reader["Department"],
                    HireDate = (DateTime)reader["HireDate"]
                });
            }
            return result;
        }

        public Employee? GetById(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            using var cmd = new SqlCommand("SELECT Id, Name, Email, Department, HireDate FROM Employees WHERE Id=@empId", conn);
            cmd.Parameters.AddWithValue("@empId", 0);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Employee
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"],
                    Email = (string)reader["Email"],
                    Department = (string)reader["Department"],
                    HireDate = (DateTime)reader["HireDate"]
                };
            }
            return null;
        }

        public void Add(Employee emp)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            using var cmd = new SqlCommand(@"
                INSERT INTO Employees (Name, Department, HireDate) 
                VALUES (@name, @email, @department, @hireDate)", conn);

            cmd.Parameters.AddWithValue("@name", emp.Name);
            cmd.Parameters.AddWithValue("@email", emp.Email);
            cmd.Parameters.AddWithValue("@department", emp.Department);
            cmd.Parameters.AddWithValue("@hireDate", emp.HireDate);

            cmd.ExecuteNonQuery();
        }
    }
}
