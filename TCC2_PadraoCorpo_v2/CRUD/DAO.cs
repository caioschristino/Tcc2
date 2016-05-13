using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.Web;
using System.Configuration;
using System.Data.Common;

namespace TCC_CRUD
{
    public class DAO
    {
        private string connServer;
        private string connLogin;
        private string connPassword;
        private string connDb;

        //private SqlConnection conn;
        private IDbConnection conn;
        //private SqlCommand command;
        private IDbCommand command;
        //private SqlDataReader dataReader;
        private IDataReader dataReader;
        //private SqlDataAdapter dataAdapter;
        private DbDataAdapter dataAdapter;
        private DataSet dataSet;

        public DAO()
        {
            this.initialize("DB_TCC_CAIO");
        }

        public DAO(string dataBaseName)
        {
            this.initialize(dataBaseName.ToString());
        }

        private void initialize(string dataBaseName)
        {
            this.conection(dataBaseName.Trim());
        }

        private void conection(string dataBase)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            this.conn = factory.CreateConnection();
            this.conn.ConnectionString = string.Format("Data Source=hoth.homologacao.net;Initial Catalog={0};User Id=sa;Password=jaranco;", dataBase);
        }
        
        public DataSet createDataSet(string nomeTabela, string sql, int timeout = 0)
        {
            try
            {
                this.conn.Open();
                DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");

                dataAdapter = factory.CreateDataAdapter();
                DbCommand command = factory.CreateCommand();
                command.CommandText = sql.Trim();
                command.Connection = conn as DbConnection;

                dataAdapter.SelectCommand = command;

                dataSet = new DataSet();
                if (timeout > 0)
                {
                    dataAdapter.SelectCommand.CommandTimeout = timeout;
                }

                dataAdapter.Fill(dataSet, nomeTabela.Trim());
                conn.Close();
                return dataSet;

            }
            catch (System.Exception e)
            {
                throw new Exception("Impossível criar DataSet: " + e.Message);
            }
        }
        
        public SqlDataReader createDataReader(string sql)
        {
            try
            {
                conn.Open();
                command = conn.CreateCommand();
                command.CommandText = sql;
                dataReader = command.ExecuteReader();
                return (SqlDataReader)dataReader;
            }
            catch (System.Exception e)
            {
                throw new Exception("Impossível criar DataReader: " + e.Message);
            }
        }

        public int execute(string sql)
        {
            try
            {
                conn.Open();
                command = conn.CreateCommand();
                command.CommandText = sql;
                return command.ExecuteNonQuery();
            }
            catch (System.Exception e)
            {
                throw new Exception("DAO: " + e.Message);
            }
        }

        public void close()
        {
            try
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                }

                if (conn.State == ConnectionState.Connecting)
                {
                    conn.Close();
                }
            }
            catch (System.Exception e)
            {
                throw new Exception("DAO: " + e.Message);
            }
        }

    }
}