using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml.Linq;

public class Money : Script
{
    public static bool RemoveMoneyPlayer(Player player, long money)
    {
        long moneyPlayer = CheckMoneyPlayer(player.Name);
        if (moneyPlayer > money)
        {
            moneyPlayer = moneyPlayer - money;
            UpdateMoneyDB(player.Name,moneyPlayer.ToString());
            player.SetData<string>("PLAYER_MONEY", moneyPlayer.ToString());
            NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::SendMoney", moneyPlayer.ToString());
            return true;
        }
        else
        {
            player.SendChatMessage("У вас недостаточно Денег!");
            return false;

        }
    }
    public static bool AddMoneyPlayerByID(int playerid, long money)
    {
        long moneyPlayer = CheckMoneyPlayerByID(playerid);
        moneyPlayer = moneyPlayer + money;
        UpdateMoneyDBByID(playerid, moneyPlayer.ToString());
        return true;
    }
    public static bool AddMoneyPlayer(Player player, long money)
    {
        long moneyPlayer = CheckMoneyPlayer(player.Name);
        moneyPlayer = moneyPlayer + money;
        UpdateMoneyDB(player.Name, moneyPlayer.ToString());
        player.SetData<string>("PLAYER_MONEY", moneyPlayer.ToString());
        NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::SendMoney", moneyPlayer.ToString());
        return true;
    }
    public static void UpdateMoneyDB(string name, string money)
    {
        string selectQuery = "UPDATE users SET money = @money WHERE name = @name;";
        MySqlCommand selectCommand = new MySqlCommand(selectQuery);
        selectCommand.Parameters.AddWithValue("@money", money);
        selectCommand.Parameters.AddWithValue("@name", name);
        MySQL.QueryRead(selectCommand);
    }
    public static void UpdateMoneyDBByID(int id, string money)
    {
        string selectQuery = "UPDATE users SET money = @money WHERE id = @id;";
        MySqlCommand selectCommand = new MySqlCommand(selectQuery);
        selectCommand.Parameters.AddWithValue("@money", money);
        selectCommand.Parameters.AddWithValue("@id", id);
        MySQL.QueryRead(selectCommand);
    }
    public static long CheckMoneyPlayer(string name)
    {
        string selectQuery = "SELECT * FROM users WHERE name = @name";
        MySqlCommand selectCommand = new MySqlCommand(selectQuery);
        selectCommand.Parameters.AddWithValue("@name", name);
        DataTable tb = MySQL.QueryRead(selectCommand);
        if (tb.Rows.Count > 0)
        {
            long money = Convert.ToInt64(tb.Rows[0]["money"]);
            return money;
        }
        return 0;
    }
    public static long CheckMoneyPlayerByID(int id)
    {
        string selectQuery = "SELECT * FROM users WHERE id = @id";
        MySqlCommand selectCommand = new MySqlCommand(selectQuery);
        selectCommand.Parameters.AddWithValue("@id", id);
        DataTable tb = MySQL.QueryRead(selectCommand);
        if (tb.Rows.Count > 0)
        {
            long money = Convert.ToInt64(tb.Rows[0]["money"]);
            return money;
        }
        return 0;
    }
}
