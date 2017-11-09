using DBModel.Model;
using DBModel.NHibernate;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModel
{
    public static class DB
    {
        private const string CONN = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = D:\work\Calc\DBModel\AppData\HypeDB.mdf; Integrated Security = True";
        public static bool AddFavorite(Favorite fav)
        {
            using (ISession session = NHHelper.OpenSession())
            {
                using (ITransaction tr = session.BeginTransaction())
                {
                    try
                    {
                        session.SaveOrUpdate(fav);
                    }
                    catch (Exception ex)
                    {
                        tr.Rollback();
                        return false;
                    }
                    tr.Commit();
                }
            }
            return true;
        }

        [Obsolete("Используйте GetFavorites(string username). Данный метод будет удален в следующей версии", false)]
        public static IList<Favorite> GetFavorites()
        {
            return new List<Favorite>();
        }

        public static IList<Favorite> GetFavorites(string username)
        {
            using (ISession session = NHHelper.OpenSession())
            {
                var query = session.QueryOver<Favorite>()
                    .JoinQueryOver(f => f.User)
                        .And(u => u.Login == username);

                return query.List<Favorite>();
            }
        }


        public static bool AddOperationHistory(OperationHistory item)
        {
            using (var connection = new SqlConnection(CONN))
            {
                // create command object with SQL query and link to connection object
                SqlCommand command = new SqlCommand("INSERT INTO OperationHistory (Name, Args, Result, ExecTime) VALUES(@Name, @Args, @Result, @ExecTime)",
                    connection);

                // create your parameters
                command.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar);
                command.Parameters.Add("@Args", System.Data.SqlDbType.NVarChar);
                command.Parameters.Add("@Result", System.Data.SqlDbType.Float);
                command.Parameters.Add("@ExecTime", System.Data.SqlDbType.BigInt);

                // set values to parameters from textboxes
                command.Parameters["@Name"].Value = item.Name;
                command.Parameters["@Args"].Value = item.Args;
                command.Parameters["@Result"].Value = item.Result;
                command.Parameters["@ExecTime"].Value = item.ExecTime;

                // open sql connection
                connection.Open();

                // execute the query and return number of rows affected, should be one
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public static OperationHistory GetOperationHistory(string name, string args)
        {
            var result = new OperationHistory();

            var connection = new SqlConnection(CONN);
            List<OperationHistory> operations = new List<OperationHistory>();
            using (connection)
            {
                // Вот тут не додумался как составить запрос чтобы получить операцию через параметры
                SqlCommand command = new SqlCommand(
                  "SELECT Id, Name, Args, Result, ExecTime FROM OperationHistory;",
                  connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var res = new OperationHistory();
                        res.Id = reader.GetInt64(0);
                        res.Name = reader.GetString(1);
                        res.Args = reader.GetString(2);
                        res.Result = reader.IsDBNull(3) ? 0d : reader.GetDouble(3);
                        res.ExecTime = reader.GetInt64(4);
                        operations.Add(res);
                    }
                }
                reader.Close();
            }
            result = operations.FirstOrDefault(o => o.Name == name && o.Args == args);
            return result;
        }

        public static IList<string> GetHeavyOperations()
        {
            var result = new List<string>();

            var connection = new SqlConnection(CONN);

            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT Name, AVG(ExecTime) FROM OperationHistory GROUP BY Name;", connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        var avgTime = reader.GetInt64(1);

                        if (avgTime > 1000)
                        {
                            result.Add(reader.GetString(0));

                        }
                    }
                }
                reader.Close();
            }

            return result;
        }

        public static bool DeleteFavorite(string name)
        {
            using (var connection = new SqlConnection(CONN))
            {
                // create command object with SQL query and link to connection object
                string sqlExpression = String.Format("DELETE  FROM Favorite WHERE Name = '{0}'", name);
                SqlCommand command = new SqlCommand(sqlExpression, connection);

                // open sql connection
                connection.Open();

                // execute the query and return number of rows affected, should be one
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
