using Google.Protobuf.WellKnownTypes;
using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Xml.Linq;

public class RemoteEvents : Script
{
    [RemoteEvent("CLIENT:SERVER::saveHair")]
    public async void saveHair(Player player, int drawable, int color, int color2)
    {
        string selectQuery = "UPDATE users SET hair = @hair, haircolor = @haircolor, haircolor2 = @haircolor2 WHERE name = @name;";
        MySqlCommand selectCommand = new MySqlCommand(selectQuery);
        selectCommand.Parameters.AddWithValue("@name", player.Name);
        selectCommand.Parameters.AddWithValue("@hair", drawable);
        selectCommand.Parameters.AddWithValue("@haircolor", color);
        selectCommand.Parameters.AddWithValue("@haircolor2", color2);
        await MySQL.QueryReadAsync(selectCommand);

    }
    [RemoteEvent("CLIENT:SERVER::ChoseHairColor")]
    public void ChoseHairColor(Player player, int drawable)
    {
        NAPI.Util.ConsoleOutput("zagluska na smenu hair");
        //player.SetCustomization(genderValue, headBlend, byte.MinValue, (byte)haircolor, (byte)haircolor2, faceFeatures, headOvelay, new Decoration[] { });

    }
    [RemoteEvent("CLIENT:SERVER::ChoseHair")]
    public void ChoseHair(Player player, int drawable)
    {
        player.SetClothes(2, drawable, 0);

    }
    [RemoteEvent("CLIENT:SERVER::CLIENT_CREATE_WATPOINT")]
    public void OnClientCreateWaypoint(Player player, float posX, float posY, float posZ)
    {
        player.Position = new Vector3(posX, posY, posZ+4);
    }
    [RemoteEvent("CLIENT:SERVER::SendLoginData")]
    public async void SendLoginData(Player player, string login, string password, int ispasswordhash)
    {
        if (ispasswordhash == 0) { 
             password = GetMd5Hash(password);
             password = GetMd5Hash(password);
        }
        string selectQuery = "SELECT * FROM users WHERE name = @name AND password = @password";
        MySqlCommand selectCommand = new MySqlCommand(selectQuery);
        selectCommand.Parameters.AddWithValue("@name", login);
        selectCommand.Parameters.AddWithValue("@password", password);
        DataTable tb = await MySQL.QueryReadAsync(selectCommand);
        if (tb.Rows.Count > 0)
        {
            int hair = Convert.ToInt16(tb.Rows[0]["hair"]);
            int haircolor = Convert.ToInt16(tb.Rows[0]["haircolor"]);
            int haircolor2 = Convert.ToInt16(tb.Rows[0]["haircolor2"]);
            int posx = Convert.ToInt16(tb.Rows[0]["posx"]);
            int posy = Convert.ToInt16(tb.Rows[0]["posy"]);
            int posz = Convert.ToInt16(tb.Rows[0]["posz"]);
            int adminstatus = Convert.ToInt16(tb.Rows[0]["admin"]);
            string money = Convert.ToString(tb.Rows[0]["money"]);
            int playerid = Convert.ToInt16(tb.Rows[0]["id"]);
            bool genderValue = Convert.ToBoolean(tb.Rows[0]["Gender"]);
            DataTable playercars = Cars.CheckPlayerCars(playerid);
            DateTime currentDate = DateTime.Now;
            string playerIP = player.Address;
            player.SetData<int>("PLAYER_ID", playerid);
            player.SetData<int>("IS_ADMIN", adminstatus);
            player.SetData<string>("SERVER_DATE", currentDate.ToString());
            player.SetData<string>("PLAYER_MONEY", money);
            player.SetData<string>("PLAYERS_COUNT", Events.CountPlayers.ToString());
            HeadBlend headBlend = new HeadBlend()
            {
                ShapeFirst = 21,
                ShapeSecond = 0,
                ShapeThird = 0,
                SkinFirst = 21,
                SkinSecond = 0,
                SkinThird = 0,
                ShapeMix = 0.5f,
                SkinMix = 0.5f,
                ThirdMix = 0
            };
            float[] faceFeatures = new float[20] 
            { 
                0,0,0, 0, 0,
                0,0,0, 0, 0,
                0,0,0, 0, 0,
                0,0,0, 0, 0
            };
            Dictionary<int, HeadOverlay> headOvelay = new Dictionary<int, HeadOverlay>();
            player.SetCustomization(genderValue, headBlend, byte.MinValue, (byte)haircolor, (byte)haircolor2, faceFeatures, headOvelay, new Decoration[] { });
            player.Dimension = 0;
            player.Position = new Vector3(posx, posy, posz);
            player.Name = login.ToString();
            player.SetClothes(4, 15, 3); // trusilia
            player.SetClothes(2, hair, 1); // hair 
            NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::SendServerDate", currentDate.ToString());
            NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::SendMoney", money);
            NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::SendCountPlayers", Events.CountPlayers.ToString());
            NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::SendCarList", playercars);
            NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::LOGINSUCCESSFUL", playerid);
            setPlayerIP(player.Name, playerIP);
            NAPI.Server.GetMaxPlayers();
        }
        else NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::LOGINFAILED");
    }

    [RemoteEvent("CLIENT:SERVER::CheckHUDData")]
    public async void CheckHUDData(Player player)
    {
        try { 
        string currentDateplayer = player.GetData<string>("SERVER_DATE");
        string currentDate = DateTime.Now.ToString();
        if (currentDateplayer != currentDate)
        {
            player.SetData<string>("SERVER_DATE", currentDate.ToString());
            NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::SendServerDate", currentDate.ToString());
        }
        string CountPlayersplayer = player.GetData<string>("PLAYERS_COUNT");
        if (CountPlayersplayer != Events.CountPlayers.ToString())
        {
            player.SetData("PLAYERS_COUNT", Events.CountPlayers.ToString());
            NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::SendCountPlayers", Events.CountPlayers.ToString());
        }
        string selectQuery = "SELECT * FROM users WHERE name = @name";
        MySqlCommand selectCommand = new MySqlCommand(selectQuery);
        selectCommand.Parameters.AddWithValue("@name", player.Name);
        DataTable tb = await MySQL.QueryReadAsync(selectCommand);
        string moneyplayer = player.GetData<string>("PLAYER_MONEY");
        string money = Convert.ToString(tb.Rows[0]["money"]);
        if (moneyplayer != money)
        {
            player.SetData<string>("PLAYER_MONEY", money);
            NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::SendMoney", money);
        }
        }
        catch { }
    }
    [RemoteEvent("CLIENT:SERVER::taxigoto")]
    public void taxigoto(Player player, float posX, float posY, float posZ)
    {
        if (player.GetData<int>("IS_ADMIN") == 1)
        {
            player.Position = new Vector3(posX, posY, posZ + 6);
        }
        else
        {
            if (Money.RemoveMoneyPlayer(player, 500))
            {
                player.Position = new Vector3(posX, posY, posZ + 6);
            }
        }
    }
    static string GetMd5Hash(string input)
    {
        using (MD5 md5 = MD5.Create())
        {
            // Перетворення рядка в байтовий масив
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            // Обчислення хешу MD5
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Конвертування байтів хешу в рядок шістнадцяткового вигляду
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }

            // Повернення отриманого хешу
            return sb.ToString();
        }
    }
    
    public static void setPlayerIP(string name, string playerIP)
    {
        string selectQuery = "UPDATE users SET ip = @ip WHERE name = @name;";
        MySqlCommand selectCommand = new MySqlCommand(selectQuery);
        selectCommand.Parameters.AddWithValue("@name", name);
        selectCommand.Parameters.AddWithValue("@ip", playerIP);
        MySQL.QueryRead(selectCommand);
    }
    public static (string, string) getPlayerIP(string playerIP)
    {
        string selectQuery = "SELECT * FROM users WHERE ip = @ip";
        MySqlCommand selectCommand = new MySqlCommand(selectQuery);
        selectCommand.Parameters.AddWithValue("@ip", playerIP);
        DataTable tb = MySQL.QueryRead(selectCommand);
        if (tb.Rows.Count > 0)
        { 
            string name = Convert.ToString(tb.Rows[0]["name"]);
            string hash = Convert.ToString(tb.Rows[0]["password"]);
            return (name, hash);
        }
        else
        {
            return (null, null);
        }
    }
    public static string DataTableToString(DataTable dataTable)
    {
        StringBuilder sb = new StringBuilder();

        // Append column names
        foreach (DataColumn column in dataTable.Columns)
        {
            sb.Append(column.ColumnName).Append("\t");
        }
        sb.AppendLine();

        // Append rows
        foreach (DataRow row in dataTable.Rows)
        {
            foreach (var item in row.ItemArray)
            {
                sb.Append(item).Append("\t");
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }
}