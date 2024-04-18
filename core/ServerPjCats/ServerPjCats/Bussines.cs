using MySql.Data.MySqlClient;
using System;
using GTANetworkAPI;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Mysqlx.Crud;
using System.Xml.Linq;

public class BussinesData
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Owner { get; set; }
    public int Price { get; set; }
    public int Balance { get; set; }
    public int MoneyGive { get; set; }
    public int Storage { get; set; }
    public Vector3 Position { get; set; }

    public BussinesData(int id, string name, int owner, int price, int balance, int moneygive, int storage, Vector3 position)
    {
        Id = id;
        Name = name;
        Owner = owner;
        Price = price;
        Balance = balance;
        MoneyGive = moneygive;
        Storage = storage;
        Position = position;
    }
}
public class Bussines : Script
{
    public static Random rnd = new Random();
    public static List<BussinesData> bussinesList = new List<BussinesData>();
    public static List<GTANetworkAPI.ColShape> bussinesListServer = new List<GTANetworkAPI.ColShape>();
    public static void BussinesListCreate()
    {
        deleteBussines();
        Checkbusines();
        foreach (BussinesData bussines in bussinesList)
        {
            createBussines(bussines);
        }
    }
    public static void deleteBussines()
    {
        bussinesList = new List<BussinesData>();
        foreach (ColShape thisbussines in bussinesListServer)
        {
            Marker m = thisbussines.GetData<Marker>(nameof(GTANetworkAPI.Marker));
            m.Delete();
            Blip b = thisbussines.GetData<Blip>(nameof(GTANetworkAPI.Blip));
            b.Delete();
            NAPI.ColShape.DeleteColShape(thisbussines);
        }

    }
    public static void createBussines(BussinesData thisbussines)
    {
        float scale = 1;
        var colShape = NAPI.ColShape.CreateSphereColShape(thisbussines.Position, scale, 0);
        colShape.SetData(nameof(GTANetworkAPI.Marker), NAPI.Marker.CreateMarker(1, new Vector3(thisbussines.Position.X, thisbussines.Position.Y, thisbussines.Position.Z - 1f), new Vector3(), new Vector3(), scale, new Color(255, 0, 0, 100), false, 0));
        colShape.SetData(nameof(GTANetworkAPI.Blip), NAPI.Blip.CreateBlip(108, new Vector3(thisbussines.Position.X, thisbussines.Position.Y, thisbussines.Position.Z - 1f), 1f, 25, "Бизнес", 255, 0f, true, 0, 0));
        colShape.SetData<BussinesData>("BussinesData", thisbussines);
        colShape.OnEntityEnterColShape += OnEntityEnterBussines;
        //colShape.OnEntityExitColShape += OnEntityExitColShape;
        bussinesListServer.Add(colShape);


    }
    public static void OnEntityEnterBussines(GTANetworkAPI.ColShape colShape, GTANetworkAPI.Player player)
    {
        BussinesData idbussines = colShape.GetData<BussinesData>("BussinesData");
        NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::OnChekpointBussines", idbussines.Name, idbussines.Owner, idbussines.Price, idbussines.Balance, idbussines.MoneyGive, idbussines.Storage, idbussines.Id);
    }
    public static void AddBusiinesMysql(string name, int owner, int price, int balance, int moneygive, int storage, Vector3 positinon)
    {
        string selectQuery = "INSERT INTO bussines (name, owner, price, balance, moneygive, storage, posx, posy, posz) VALUES (@name, @owner, @price, @balance, @moneygive, @storage, @posx, @posy, @posz)";
        MySqlCommand selectCommand = new MySqlCommand(selectQuery);
        selectCommand.Parameters.AddWithValue("@name", name);
        selectCommand.Parameters.AddWithValue("@owner", owner);
        selectCommand.Parameters.AddWithValue("@price", price);
        selectCommand.Parameters.AddWithValue("@balance", balance);
        selectCommand.Parameters.AddWithValue("@moneygive", moneygive);
        selectCommand.Parameters.AddWithValue("@storage", storage);
        string posx = positinon.X.ToString("0.0000", System.Globalization.CultureInfo.GetCultureInfo("uk-UA"));
        string posy = positinon.Y.ToString("0.0000", System.Globalization.CultureInfo.GetCultureInfo("uk-UA"));
        string posz = positinon.Z.ToString("0.0000", System.Globalization.CultureInfo.GetCultureInfo("uk-UA"));
        selectCommand.Parameters.AddWithValue("@posx", posx);
        selectCommand.Parameters.AddWithValue("@posy", posy);
        selectCommand.Parameters.AddWithValue("@posz", posz);
        MySQL.QueryRead(selectCommand);

    }
    public static void Checkplayerbusines(int ownerid)
    {
        List<BussinesData> bussinesListplayer = new List<BussinesData>();
        string selectQuery = "SELECT * FROM bussines WHERE owner = @ownerid";
        MySqlCommand selectCommand = new MySqlCommand(selectQuery);
        selectCommand.Parameters.AddWithValue("@ownerid", ownerid);
        DataTable tb = MySQL.QueryRead(selectCommand);
        foreach (DataRow row in tb.Rows)
        {
            int id = Convert.ToInt32(row["id"]);
            string name = row["name"].ToString();
            int owner = Convert.ToInt32(row["owner"]);
            int price = Convert.ToInt32(row["price"]);
            int balance = Convert.ToInt32(row["balance"]);
            int moneyGive = Convert.ToInt32(row["moneygive"]);
            int storage = Convert.ToInt32(row["storage"]);
            float posX = Convert.ToSingle(row["posx"]);
            float posY = Convert.ToSingle(row["posy"]);
            float posZ = Convert.ToSingle(row["posz"]);
            bussinesListplayer.Add(new BussinesData(id, name, owner, price, balance, moneyGive, storage, new Vector3(posX, posY, posZ)));
        }

    }
    public static void Checkbusines()
    {
        string selectQuery = "SELECT * FROM bussines";
        MySqlCommand selectCommand = new MySqlCommand(selectQuery);
        DataTable tb = MySQL.QueryRead(selectCommand);
        foreach (DataRow row in tb.Rows)
        {
            int id = Convert.ToInt32(row["id"]);
            string name = row["name"].ToString();
            int owner = Convert.ToInt32(row["owner"]);
            int price = Convert.ToInt32(row["price"]);
            int balance = Convert.ToInt32(row["balance"]);
            int moneyGive = Convert.ToInt32(row["moneygive"]);
            int storage = Convert.ToInt32(row["storage"]);
            float posX = Convert.ToSingle(row["posx"]);
            float posY = Convert.ToSingle(row["posy"]);
            float posZ = Convert.ToSingle(row["posz"]);
            bussinesList.Add(new BussinesData(id, name, owner, price, balance, moneyGive, storage, new Vector3(posX, posY, posZ)));

        }
    }

    public static void PayDay()
    {
        foreach (BussinesData bussines in bussinesList)
        {
            if (bussines.Owner != -1 & bussines.Storage > 75)
            {
                Money.AddMoneyPlayerByID(bussines.Owner, bussines.MoneyGive);
                remmoneyBussines(bussines.Id);
            }
        }
    }

    [RemoteEvent("CLIENT:SERVER::buyBussines")]
    public static void BuyBissnes(Player player, int bussinesid)
    {
        foreach (BussinesData bussines in bussinesList)
            {
            if (bussines.Id == bussinesid)
                {
                if (bussines.Owner == -1)
                {
                    if (Money.RemoveMoneyPlayer(player, bussines.Price))
                    {
                        updateMSQLBussines(player.GetData<int>("PLAYER_ID"), bussinesid);
                        bussines.Owner = player.GetData<int>("PLAYER_ID"); 
                        NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::CloseBussines");
                    }
                }
            }
        }
    }
    [RemoteEvent("CLIENT:SERVER::sellBussines")]
    public static void SellBissnes(Player player, int bussinesid)
    {
        foreach (BussinesData bussines in bussinesList)
        {
            if (bussines.Id == bussinesid)
            {
                if (bussines.Owner == player.GetData<int>("PLAYER_ID"))
                {
                    Money.AddMoneyPlayer(player, bussines.Price);
                    updateMSQLBussines(-1, bussinesid);
                    bussines.Owner = -1;
                    NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::CloseBussines");
                }
            }
        }
    }
    [RemoteEvent("CLIENT:SERVER::addmoneyBussines")]
    public static void addmoneyBussines(Player player, int bussinesid)
    {
        foreach (BussinesData bussines in bussinesList)
        {
            if (bussines.Id == bussinesid & bussines.Owner == player.GetData<int>("PLAYER_ID"))
            {
                long money = (100 - bussines.Storage) * 100; 
                if (Money.RemoveMoneyPlayer(player, money))
                {
                    updateMSQLBussinesMoney(100, bussinesid);
                    bussines.Storage = 100;
                    NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::CloseBussines");
                }
            }
        }
    }
    public static void remmoneyBussines(int bussinesid)
    {
        foreach (BussinesData bussines in bussinesList)
        {
            if (bussines.Id == bussinesid & bussines.Owner != -1)
            {
                int random = rnd.Next(0, 25);
                updateMSQLBussinesMoney(bussines.Storage - random, bussinesid);
                bussines.Storage = bussines.Storage - random;
            }
        }
    }

    public static void updateMSQLBussines(int ownerid, int bizid)
    {
        string selectQuery = "UPDATE bussines SET owner = @ownerid WHERE id = @bizid";
        MySqlCommand selectCommand = new MySqlCommand(selectQuery);
        selectCommand.Parameters.AddWithValue("@ownerid", ownerid);
        selectCommand.Parameters.AddWithValue("@bizid", bizid);
        MySQL.QueryRead(selectCommand);
    }
    public static void updateMSQLBussinesMoney(int moneybiz, int bizid)
    {
        string selectQuery = "UPDATE bussines SET storage = @moneybiz WHERE id = @bizid";
        MySqlCommand selectCommand = new MySqlCommand(selectQuery);
        selectCommand.Parameters.AddWithValue("@moneybiz", moneybiz);
        selectCommand.Parameters.AddWithValue("@bizid", bizid);
        MySQL.QueryRead(selectCommand);
    }
}
