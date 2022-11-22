using Microsoft.Data.Sqlite;
using Sqli.Models;
using System.Collections.Generic;

namespace Sqli.Services
{
    public class EmployeeService : IEmployeeService
    {
        private const string DatabaseName = "employees.db";

        public EmployeeModel[] GetByName(string namePattern)
        {
            var connectionString = new SqliteConnectionStringBuilder { DataSource = DatabaseName }.ToString();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();

                cmd.CommandText = $"SELECT * FROM employees WHERE name like '%{namePattern}%';";

                var result = new List<EmployeeModel>();
                using (var reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        var employee = new EmployeeModel
                        {
                            Name = (string) reader["name"],
                            Email = (string) reader["email"],
                            Phone = (string) reader["phone"],
                            DateOfBirth = (string) reader["dob"],
                            Salary = (string) reader["salary"],
                        };
                        result.Add(employee);
                    }
                }

                return result.ToArray();
            }
        }

        public void ResetDatabase()
        {
            var connectionString = new SqliteConnectionStringBuilder { DataSource = DatabaseName }.ToString();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var createCommand = connection.CreateCommand();
                createCommand.CommandText = "DROP TABLE IF EXISTS employees;"
                                          + "CREATE TABLE employees (name TEXT, email TEXT, phone TEXT, dob TEXT, salary INT);";
                createCommand.ExecuteNonQuery();

                var insertCommand = connection.CreateCommand();
                insertCommand.CommandText = "INSERT INTO employees VALUES ('Alice', 'alice@bigco.rp', '202-555-5555', '04-01-1956', '$75,000');"
                                          + "INSERT INTO employees VALUES ('Bob', 'bob@bigco.rp', '323-867-5309', '12-31-1984', '$40,000');";
                insertCommand.ExecuteNonQuery();

            }
        }
    }
}