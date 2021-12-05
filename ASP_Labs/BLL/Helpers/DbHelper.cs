
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WebApp.DAL.Entities;
using WebApp.DAL.Enums;

namespace WebApp.BLL.Helpers
{
    public static class DbHelper
    {
        public delegate List<T> DataMapper<T>(SqlDataReader reader);
        public static async Task<List<T>> ExecuteSP<T>(string connString, string SPName, List<SqlParameter> Params, DataMapper<T> dataMapper = null)
        {
            try
            {
                DataTable dataTable = new DataTable();
                var retVal = new List<T>();

                using (SqlConnection Connection = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand(SPName, Connection);
                    Connection.Open();
                    cmd.Parameters.AddRange(Params.ToArray());
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (dataMapper != null)
                        {
                            retVal = dataMapper(reader);
                        }
                        reader.Close();
                    }
                    Connection.Close();
                }
                return retVal;
            }
            catch (SqlException e)
            {
                Console.WriteLine("ConvertToList Exception: " + e.ToString());
                return new List<T>();
            }
        }

        public static List<T> ExecuteSPSync<T>(string connString, string SPName, List<SqlParameter> Params, DataMapper<T> dataMapper = null)
        {
            try
            {
                DataTable dataTable = new DataTable();
                var retVal = new List<T>();

                using (SqlConnection Connection = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand(SPName, Connection);
                    Connection.Open();
                    cmd.Parameters.AddRange(Params.ToArray());
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (dataMapper != null)
                        {
                            retVal = dataMapper(reader);
                        }
                        reader.Close();
                    }
                    Connection.Close();
                }
                return retVal;
            }
            catch (SqlException e)
            {
                Console.WriteLine("ConvertToList Exception: " + e.ToString());
                return new List<T>();
            }
        }

        //additional func-s
        public static List<Product> ReaderToProduct(SqlDataReader reader)
        {
            var pList = new List<Product>();
            while (reader.Read())
            {
                pList.Add(new Product()
                {
                    Id = (Guid)reader["ProductId"],
                    Name = (string)reader["Name"],
                    Platform = (Platforms)(int)reader["Platform"],
                    DateCreated = (DateTime)reader["DateCreated"],
                    TotalRating = (double)(decimal)reader["TotalRating"],
                    Genre = (GamesGenres)Enum.Parse(typeof(GamesGenres), (string)reader["Genre"]),
                    Rating = (GamesRating)(int)reader["Rating"],
                    Logo = (string)reader["Logo"],
                    Background = (string)reader["Background"],
                    Price = (decimal)reader["Price"],
                    Count = (int)reader["Count"],
                    IsDeleted = (bool)reader["IsDeleted"],
                });
            }
            return pList;
        }

        public static List<Platforms> ReaderToPlatforms(SqlDataReader reader)
        {
            var pList = new List<Platforms>();
            while (reader.Read())
            {
                pList.Add(
                    (Platforms)(int)reader["Platform"]
                );
            }
            return pList;
        }

        public static List<Guid> ReaderToProductId(SqlDataReader reader)
        {
            var pList = new List<Guid>();
            while (reader.Read())
            {
                pList.Add(
                   (Guid)reader["ProductId"]
                );
            }
            return pList;
        }
    }
}
