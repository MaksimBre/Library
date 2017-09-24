using Library.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Library.DataAccessLayer.DBAccess
{
    public class Writers
    {
        private readonly SqlConnection connection;

        internal Writers(SqlConnection connection)
        {
            this.connection = connection ?? throw new ArgumentNullException("connection", "Valid connection is mandatory!");
        }

        public IEnumerable<Writer> GetAll()
        {
            using (SqlCommand command = new SqlCommand("EXEC WriterGetAll", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<Writer> writer = new List<Writer>();
                    while (reader.Read())
                        writer.Add(CreateWriter(reader));

                    return writer;
                }
            }
        }

        public Writer GetById(int id)
        {
            using (SqlCommand command = new SqlCommand("EXEC WriterGetById @Id", connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        return CreateWriter(reader);

                    return null;
                }
            }
        }

        public IEnumerable<Writer> Search(string name)
        {
            using (SqlCommand command = new SqlCommand("EXEC WriterSearch @Name ", connection))
            {
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = "%" + name + "%";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<Writer> writers = new List<Writer>();
                    while (reader.Read())
                        writers.Add(CreateWriter(reader));

                    return writers;
                }
            }
        }

        public int Insert(Writer writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer", "Valid writer is mandatory!");

            using (SqlCommand command = new SqlCommand("EXEC WriterInsert @Name ,@Biography, @NoOfBooks ", connection))
            {
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = writer.Name;
                command.Parameters.Add("@Biography", SqlDbType.NVarChar).Value = writer.Biography;
                command.Parameters.Add("@NoOfBooks", SqlDbType.Int).Value = writer.NoOfBooks.Optional();

                return (int)command.ExecuteScalar();
            }
        }

        public void Update(Writer writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer", "Valid writer is mandatory!");

            using (SqlCommand command = new SqlCommand("EXEC WriterUpdate @Id, @Name, @Biography, @NoOfBooks", connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = writer.Id;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = writer.Name;
                command.Parameters.Add("@Biography", SqlDbType.NVarChar).Value = writer.Biography;
                command.Parameters.Add("@NoOfBooks", SqlDbType.NVarChar).Value = writer.NoOfBooks.Optional();

                command.ExecuteNonQuery();
            }
        }

        public void Delete(Writer writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer", "Valid writer is mandatory!");

            using (SqlCommand command = new SqlCommand("EXEC WriterDelete @Id ", connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = writer.Id;
                command.ExecuteNonQuery();
            }
        }

        private Writer CreateWriter(IDataReader reader)
        {
            return new Writer((int)reader["Id"], reader["Name"] as string, reader["Biography"] as string, reader["NoOfBooks"].DBNullTo<int?>());
        }
    }
}
