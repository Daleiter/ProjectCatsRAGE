using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using System.Text;

public class CarShop : Script {
    
    [RemoteEvent("CLIENT:SERVER::changeCarColor1InCarShop")]
    public static void changeCarColor1InCarShop(Player player, int color)
    {
        NAPI.Task.Run(() =>
        {
            if (player.HasData("VechicleShop"))
            {
                Vehicle car = player.GetData<Vehicle>("VechicleShop");
                NAPI.Vehicle.SetVehiclePrimaryColor(car, color);
                player.SetData<Vehicle>("VechicleShop", car);
            }
        });
    }
    [RemoteEvent("CLIENT:SERVER::changeCarColor2InCarShop")]
    public static void changeCarColor2InCarShop(Player player, int color)
    {
        NAPI.Task.Run(() =>
        {
            if (player.HasData("VechicleShop"))
            {
                Vehicle car = player.GetData<Vehicle>("VechicleShop");
                NAPI.Vehicle.SetVehicleSecondaryColor(car, color);
                player.SetData<Vehicle>("Vechicle", car);
            }
        });
    }
    [RemoteEvent("CLIENT:SERVER::BuyCar")] 
    public static void BuyCar(Player player, string car, long money, int colorcar, int colorcar2)
    {
        NAPI.Task.Run(() =>
        {
            if (Money.RemoveMoneyPlayer(player, money)) { 
            int playerid = player.GetData<int>("PLAYER_ID");
            if (player.HasData("VechicleShop"))
            {
                NAPI.Entity.DeleteEntity(player.GetData<Vehicle>("VechicleShop"));
                player.ResetData("VechicleShop");
            }
            AddBuyBarToDB(playerid.ToString(), car, colorcar, colorcar2, "Project Cats");
            Vehicle myveh1 = NAPI.Vehicle.CreateVehicle(NAPI.Util.GetHashKey(car), new Vector3(-59.050503, -1115.6681, 26.43526), 10f, colorcar, colorcar2, "ProjCats");
            if (player.HasData("Vechicle"))
            {
                NAPI.Entity.DeleteEntity(player.GetData<Vehicle>("Vechicle"));
                player.ResetData("Vechicle");
            }
            player.SetData<Vehicle>("Vechicle", myveh1);
            DataTable playercars = Cars.CheckPlayerCars(playerid);
            NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::SendCarList", playercars);
            }
        });
    }
    [RemoteEvent("CLIENT:SERVER::ChangeCarInCarShop")] 
    public static void ChangeCarInCarShop(Player player, string car)
    {
        NAPI.Task.Run(() =>
        {
            if (player.HasData("VechicleShop"))
            {
                NAPI.Entity.DeleteEntity(player.GetData<Vehicle>("VechicleShop"));
                player.ResetData("VechicleShop");
            }
            VehicleHash vehicleHash = NAPI.Util.VehicleNameToModel($"{car}");
            Vehicle myveh1 = NAPI.Vehicle.CreateVehicle(vehicleHash, new Vector3(-44.69792, -1094.3165, 26.422338), 180f, 2, 2, "Project Cats");
            player.SetData<Vehicle>("VechicleShop", myveh1);
        });
    }
    [RemoteEvent("CLIENT:SERVER::ExitCarShop")]
    public static void ExitCarShop(Player player)
    {
        NAPI.Task.Run(() =>
        {
            player.Dimension = 0;
            if (player.HasData("VechicleShop"))
            {
                NAPI.Entity.DeleteEntity(player.GetData<Vehicle>("VechicleShop"));
                player.ResetData("VechicleShop");
            }
        });
    }
    public static void OnChekpointAutoSail(Player player)
    {
        player.ResetData("VechicleShop");
        player.Position = new Vector3(-47.954292, -1090.9191, 26.422338);
        player.Rotation = new Vector3(0, 0, -158.1875);
        player.Dimension = player.Id;

        NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::OnChekpointAutoSail");
    }
    public static void AddBuyBarToDB(string ownerid, string vhash, int color1, int color2, string numperplate)
    {
        string selectQuery = "INSERT INTO cars (ownerid, vhash, color1, color2, numperplate) VALUES (@ownerid, @vhash, @color1, @color2, @numperplate);";
        MySqlCommand selectCommand = new MySqlCommand(selectQuery);
        selectCommand.Parameters.AddWithValue("@ownerid", ownerid);
        selectCommand.Parameters.AddWithValue("@vhash", vhash);
        selectCommand.Parameters.AddWithValue("@color1", color1);
        selectCommand.Parameters.AddWithValue("@color2", color2);
        selectCommand.Parameters.AddWithValue("@numperplate", numperplate);
        MySQL.QueryRead(selectCommand);
    }
}