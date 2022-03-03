using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using ToDo_API.Models;
using System.Data;

namespace ToDo_API.Repository
{
    public class ToDoRepository
    {

        IConfiguration _configuration;
        public ToDoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        internal IEnumerable<ToDoModel>? GetToDos()
        {
            try
            {
                // get connection string from configuration file
                string connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                // open the sql connection using connection string
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // set sql command parameters
                    var sqlCommand = new SqlCommand("SELECT * FROM ToDo", connection)
                    {
                        CommandType = CommandType.Text
                    };

                    using (SqlDataReader reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        // Currency symbols are present
                        if (reader != null && reader.HasRows)
                        {
                            // init dictionary variable 
                            List<ToDoModel> toDos = new List<ToDoModel>();

                            // get ordinals
                            int idOrdinal = reader.GetOrdinal("Id");
                            int titleOrdinal = reader.GetOrdinal("Title");
                            int descriptionOrdinal = reader.GetOrdinal("Title");

                            // fill the property data in property list
                            while (reader.Read())
                            {
                                toDos.Add(new ToDoModel()
                                {
                                    Id = reader.GetInt32(idOrdinal),
                                    Title = reader.GetString(titleOrdinal),
                                    Description = reader.GetString(descriptionOrdinal)
                                });
                            }

                            reader.Close();
                            connection.Close();

                            // return currency symbol list
                            return toDos;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            // if nothing found return null
            return null;
        }
        internal ToDoModel GetToDo(int id)
        {
            try
            {
                // get connection string from configuration file
                string connectionString =  Environment.GetEnvironmentVariable("ConnectionString");

                // open the sql connection using connection string
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // set sql command parameters
                    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM ToDo WHERE Id=@id", connection)
                    {
                        CommandType = CommandType.Text
                    };

                    sqlCommand.Parameters.Add(new SqlParameter("@id", id));

                    using (SqlDataReader reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        // Currency symbols are present
                        if (reader != null && reader.HasRows)
                        {
                            // init dictionary variable 
                            ToDoModel toDo = new ToDoModel();

                            // get ordinals
                            int idOrdinal = reader.GetOrdinal("Id");
                            int titleOrdinal = reader.GetOrdinal("Title");
                            int descriptionOrdinal = reader.GetOrdinal("Title");

                            // fill the property data in property list
                            while (reader.Read())
                            {
                                toDo = new ToDoModel()
                                {
                                    Id = reader.GetInt32(idOrdinal),
                                    Title = reader.GetString(titleOrdinal),
                                    Description = reader.GetString(descriptionOrdinal)
                                };
                            }

                            reader.Close();
                            connection.Close();

                            // return currency symbol list
                            return toDo;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            // if nothing found return null
            return null;
        }

        internal bool Insert(ToDoModel toDoModel)
        {
            try
            {
                // get connection string from configuration file
                string connectionString =  Environment.GetEnvironmentVariable("ConnectionString");

                // open the sql connection using connection string
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // set sql command parameters
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO ToDo(Title, Description) VALUES(@title, @description)", connection)
                    {
                        CommandType = CommandType.Text
                    };

                    sqlCommand.Parameters.Add(new SqlParameter("@title", toDoModel.Title));
                    sqlCommand.Parameters.Add(new SqlParameter("@description", toDoModel.Description));

                    int result = sqlCommand.ExecuteNonQuery();

                    connection.Close();

                    return result > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return false;
        }
        internal bool Update(int id, ToDoModel toDo)
        {
            try
            {
                // get connection string from configuration file
                string connectionString =  Environment.GetEnvironmentVariable("ConnectionString");

                // open the sql connection using connection string
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // set sql command parameters
                    SqlCommand sqlCommand = new SqlCommand("UPDATE ToDo SET Title = @title, Description = @description WHERE Id = @id", connection)
                    {
                        CommandType = CommandType.Text
                    };

                    sqlCommand.Parameters.Add(new SqlParameter("@id", id));
                    sqlCommand.Parameters.Add(new SqlParameter("@title", toDo.Title));
                    sqlCommand.Parameters.Add(new SqlParameter("@description", toDo.Description));

                    int result = sqlCommand.ExecuteNonQuery();

                    connection.Close();

                    return result > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return false;
        }

        internal bool Delete(int id)
        {
            try
            {
                // get connection string from configuration file
                string connectionString =  Environment.GetEnvironmentVariable("ConnectionString");

                // open the sql connection using connection string
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // set sql command parameters
                    SqlCommand sqlCommand = new SqlCommand("DELETE FROM ToDo WHERE Id = @id", connection)
                    {
                        CommandType = CommandType.Text
                    };

                    sqlCommand.Parameters.Add(new SqlParameter("@id", id));

                    int result = sqlCommand.ExecuteNonQuery();

                    connection.Close();

                    return result > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return false;
        }
    }
}