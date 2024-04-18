using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

public class MySQL
{
    //private static readonly string connStr = "server=127.0.0.1;user=Daleiter;database=RAGEPjCats;password=Daleiter22!;Pooling=true;SslMode=None;";
    private static readonly string connStr = "server=192.168.31.11;user=Dale1ter;database=RAGEPjCats;password=Thuglife22;Pooling=true;SslMode=None;";

    public static void Test()
    {
        while (true)
        {
            using MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                NAPI.Util.ConsoleOutput("подключение успешно");
                conn.Close();
                break;
            }
            catch (System.Exception ex)
            {
                NAPI.Util.ConsoleOutput(ex.ToString());
                Thread.Sleep(5000);
            }
        }
    }
    public static void Query(MySqlCommand command)
    {
        if (command == null || command.CommandText.Length <1) { NAPI.Util.ConsoleOutput("Wrong Command argument"); return; }
        using MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            conn.Open();
            command.Connection = conn;
            command.ExecuteNonQuery();
            conn.Close();
        }
        catch (System.Exception ex)
        {
            NAPI.Util.ConsoleOutput(ex.ToString());
        }
    }   
    public static DataTable QueryRead(MySqlCommand command)
    {
        if (command == null || command.CommandText.Length < 1) { NAPI.Util.ConsoleOutput("Wrong Command argument"); return null; }
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(connStr);
                connection.Open();
                command.Connection = connection;

                using MySqlDataReader reader = command.ExecuteReader();
                using DataTable dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
            catch (System.Exception ex)
            {
                NAPI.Util.ConsoleOutput(ex.ToString());
                return null;
            }
            
        }
    }

    
    public static async Task<DataTable> QueryReadAsync(MySqlCommand command)
    {
        if (command == null || command.CommandText.Length < 1) { NAPI.Util.ConsoleOutput("Wrong Command argument"); return null; }
        {
            try
            {
                using MySqlConnection connection = new MySqlConnection(connStr);
                await connection.OpenAsync();
                command.Connection = connection;
                using DbDataReader reader = await command.ExecuteReaderAsync();
                using DataTable dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
            catch (System.Exception ex)
            {
                NAPI.Util.ConsoleOutput(ex.ToString());
                return null;
            }

        }
    }
}