using System.Data.SqlClient;

namespace webapi
{
    public class NorthwindDB
    {
        private string connectionString = "Server=MARCOS-LEGION;Database=AdventureWorks2019;Trusted_Connection=True; TrustServerCertificate=True;";

        public List<Product> GetProductsPaginated(string searchQuery, int page, int pageSize)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = @"
                SELECT *
                FROM (
                    SELECT *, ROW_NUMBER() OVER (ORDER BY ProductID) AS RowNum
                    FROM [AdventureWorks2019].[Production].[Product]
                    WHERE SOUNDEX(Name)  LIKE '%' + SOUNDEX(@searchQuery) + '%'
                ) AS PagedResults
                WHERE RowNum >= (@pageNumber - 1) * @pageSize + 1
                AND RowNum <= @pageNumber * @pageSize";


                command.Parameters.AddWithValue("@searchQuery", searchQuery);
                command.Parameters.AddWithValue("@pageNumber", page);
                command.Parameters.AddWithValue("@pageSize", pageSize);

                SqlDataReader reader = command.ExecuteReader();
                List<Product> products = new List<Product>();
                while (reader.Read())
                {
                    Product product = new Product();
                    product.ProductID = reader.GetInt32(0);
                    product.Name = reader.GetString(1);
                    product.ProductNumber = reader.GetString(2);
                    product.Price = reader.GetDecimal(8);
                    products.Add(product);
                }
                return products;
            }
        }
    }
}
