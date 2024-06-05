using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN_Slot9
{
    public record Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
    public class ManageCategories
    {
        SqlConnection connection;
        SqlCommand command;
        string ConnectionString = "Server=Riversy\\SQLEXPRESS; uid=sa;pwd=sa;database=MyStore";
        public List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();
            connection = new SqlConnection(ConnectionString);
            string SQL = "Select CategoryID, CategoryName from Categories";
            command = new SqlCommand(SQL, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        categories.Add(new Category
                        {
                            CategoryID = reader.GetInt32("CategoryID"),
                            CategoryName = reader.GetString("CategoryName")
                        });
                    }
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return categories;
        }
        public void InsertCategory(Category category)
        {
            connection = new SqlConnection(ConnectionString);
            command = new SqlCommand("Insert Categories values(@CategoryName)", connection);
        }

        public void UpdateCategory(Category category)
        {
            connection = new SqlConnection(ConnectionString);
            String SQL = "Update Categories SET CategoryName=@CategoryName where CategoryID=@CategoryID";
            command = new SqlCommand(SQL, connection);
            command.Parameters.AddWithValue("@CategoryID", category.CategoryID);
            command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void DeleteCategory(Category category)
        {
            connection = new SqlConnection(ConnectionString);
            string SQL = "Delete Categories where CategoryID=@CategoryID"; command = new SqlCommand(SQL, connection);
            command.Parameters.AddWithValue("@CategoryID", category.CategoryID);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            } catch (Exception ex) {
                        throw new Exception(ex.Message);
            }
            finally{
                connection.Close();
            }
        }//end DeleteCategory

    }
}
