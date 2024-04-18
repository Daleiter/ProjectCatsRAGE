using GTANetworkAPI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using System.Text;

public class Cars : Script
{
    [RemoteEvent("CLIENT:SERVER::SpawnCar")]
    public static void SpawnCar(Player player, string id, string ownerid, string vhash, int color1, int color2, string numperplate)
    {
        if (!player.HasData("Vechicle")) { 
        Vector3 PlayerPos = NAPI.Entity.GetEntityPosition(player);
        Vehicle myveh1 = NAPI.Vehicle.CreateVehicle(NAPI.Util.GetHashKey(vhash), new Vector3(PlayerPos.X + 1f, PlayerPos.Y + 2f, PlayerPos.Z + 1f), 10f, color1, color2, numperplate);
        NAPI.Vehicle.SetVehicleNeonState(myveh1, true);
        NAPI.Vehicle.SetVehicleNeonColor(myveh1, 255, 0, 0);
        player.SetIntoVehicle(myveh1, 0);
        NAPI.Chat.SendChatMessageToPlayer(player, $"Игрок: {player.Name} | Заспавнил: {vhash}");
        myveh1.SetData<string>("Owner", player.Name);
        player.SetData<Vehicle>("Vechicle", myveh1);
        } else { 
        NAPI.Entity.DeleteEntity(player.GetData<Vehicle>("Vechicle"));
            player.ResetData("Vechicle");
        }
    }
    [RemoteEvent("CLIENT:SERVER::CarRepair")]
    public static void CarRepair(Player player)
    {
        if (player.IsInVehicle) { 
        player.Vehicle.Repair();
        }
        //if (player.HasData("Vechicle"))
        //{
        //    Vehicle Car = player.GetData<Vehicle>("Vechicle");
        //    Car.Repair();
        //}
    }
    [RemoteEvent("CLIENT:SERVER::CarEngine")]
    public static void CarEngine(Player player)
    {
        if (player.IsInVehicle)
            {
                if (player.Vehicle.GetData<string>("Owner") == player.Name)
                {
                player.Vehicle.EngineStatus = !player.Vehicle.EngineStatus;
                    if (player.Vehicle.EngineStatus)
                    {
                        player.SendChatMessage("Личный транспорт запущен");
                    }
                    else
                    {
                        player.SendChatMessage("Личный транспорт заглушен");
                    }
                }
                else
                {
                player.SendChatMessage("У вас нет доступа к етому транспорту");
            }
        }
        
    }
    [RemoteEvent("CLIENT:SERVER::CarFlip")]
    public static void CarFlip(Player player)
    {
        if (player.IsInVehicle)
        {
            player.Vehicle.Rotation = new Vector3(0, 0, player.Vehicle.Rotation.Z);
        }
        //if (player.HasData("Vechicle"))
        //{
        //    Vehicle Car = player.GetData<Vehicle>("Vechicle");
        //    Car.Position = player.Position;
        //    Car.Rotation = new Vector3(0, 0, Car.Rotation.Z);
        //}
    }
    [RemoteEvent("CLIENT:SERVER::CarLock")]
    public static void CarLock(Player player)
    {
        if (player.HasData("Vechicle"))
        {
            Vehicle Car = player.GetData<Vehicle>("Vechicle");
            Car.Locked = !Car.Locked;
            if (Car.Locked) { 
            player.SendChatMessage("Личный транспорт закрыт");
            }
            else
            {
                player.SendChatMessage("Личный транспорт открыт");
            }
        }
    }
    public static DataTable CheckPlayerCars(int ownerid)
    {
        string selectQuery = "SELECT * FROM cars WHERE ownerid = @ownerid";
        MySqlCommand selectCommand = new MySqlCommand(selectQuery);
        selectCommand.Parameters.AddWithValue("@ownerid", ownerid);
        DataTable tb = MySQL.QueryRead(selectCommand);
        return tb;
    }
    
}
