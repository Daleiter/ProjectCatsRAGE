using GTANetworkAPI;
using Mysqlx.Crud;
using System.Xml.Linq;

public class Commands : Script
{
    [Command("getpos")]
    public void Cmd_getpos(Player player)
    {
        Vector3 playerPosition = player.Position;
        Vector3 playerRotation = player.Rotation;
        NAPI.Util.ConsoleOutput($"{playerPosition.X}, {playerPosition.Y}, {playerPosition.Z}");
        NAPI.Util.ConsoleOutput($"{playerRotation.X}, {playerRotation.Y}, {playerRotation.Z}");
        player.SendChatMessage($"Player position{playerPosition.X}, {playerPosition.Y}, {playerPosition.Z}");
        player.SendChatMessage($"Player rotation{playerRotation.X}, {playerRotation.Y}, {playerRotation.Z}");
    }
    [Command("hp")]
    public void setHp(Player player, int count = 100)
    {
        if (player.GetData<int>("IS_ADMIN") == 0) return;
        player.Health = count;
    }
    [Command("armor")]
    public void setArmor(Player player, int count = 100)
    {
        if (player.GetData<int>("IS_ADMIN") == 0) return;
        player.Armor = count;
    }
    [Command("kill")]
    public void setKill(Player player, Player player2 = null)
    {
        if (player.GetData<int>("IS_ADMIN") == 0) return;
        if (player2 == null)
        player.Health = 0;
        else player2.Health = 0;
    }
    [Command("veh")]
    public void veh(Player player, VehicleHash vehicleHash, int color1 = 1, int color2 = 1, string platenumber = "Admin")
    {
        if (player.GetData<int>("IS_ADMIN") == 0) return;
        NAPI.Util.ConsoleOutput(vehicleHash.ToString());
        Vector3 PlayerPos = NAPI.Entity.GetEntityPosition(player);
        Vehicle myveh1 = NAPI.Vehicle.CreateVehicle(vehicleHash, new Vector3(PlayerPos.X + 1f, PlayerPos.Y + 2f, PlayerPos.Z + 1f), 10f, color1, color2, platenumber);
        NAPI.Vehicle.SetVehicleNeonState(myveh1, true);
        NAPI.Vehicle.SetVehicleNeonColor(myveh1, 255, 0, 0);
        NAPI.Chat.SendChatMessageToPlayer(player, $"Игроку: {player.Name} | Выдано: {vehicleHash}");

    }
    [Command("car")]
    public void car(Player player, string car, int color1 = 1, int color2 = 1, string platenumber = "Admin")
    {
        if (player.GetData<int>("IS_ADMIN") == 0) return;
        NAPI.Util.ConsoleOutput(car.ToString());
        Vector3 PlayerPos = NAPI.Entity.GetEntityPosition(player);
        Vehicle myveh1 = NAPI.Vehicle.CreateVehicle(NAPI.Util.GetHashKey(car), new Vector3(PlayerPos.X + 1f, PlayerPos.Y + 2f, PlayerPos.Z + 1f), 10f, color1, color2, platenumber);
        //NAPI.Vehicle.SetVehicleNeonState(myveh1, true);
        //NAPI.Vehicle.SetVehicleNeonColor(myveh1, 255, 0, 0);
        NAPI.Chat.SendChatMessageToPlayer(player, $"Игроку: {player.Name} | Выдано: {car}");
        player.SetIntoVehicle(myveh1, 0);

    }
    [Command("money")]
    public void money(Player player, int money = 1000)
    {
        if (player.GetData<int>("IS_ADMIN") == 0) return;
        Money.AddMoneyPlayer(player, money);
        NAPI.Chat.SendChatMessageToPlayer(player, $"Игроку: {player.Name} | Выдано: {money}$");
    }
    [Command("payday")]
    public void payday(Player player)
    {
        if (player.GetData<int>("IS_ADMIN") == 0) return;
        Bussines.PayDay();
        NAPI.Chat.SendChatMessageToPlayer(player, $"PayDay успешен");

    }
    [Command("tp")]
    public void tp(Player player, float x, float y, float z)
    {
        if (player.GetData<int>("IS_ADMIN") == 0) return;
        player.Position = new Vector3(x,y,z);
        player.SendChatMessage($"Player position{player.Position.X}, {player.Position.Y}, {player.Position.Z}");
    }
    [Command("tpa")]
    public void tpa(Player player, Player player2)
    {
        if (player.GetData<int>("IS_ADMIN") == 0) return;
        player.Position = player2.Position;
        player.SendChatMessage($"Player position{player.Position.X}, {player.Position.Y}, {player.Position.Z}");
    }
    [Command("cbiz")]
    public void createbiz(Player player, string name, int price)
    {
        if (player.GetData<int>("IS_ADMIN") == 0) return;
        Bussines.AddBusiinesMysql(name, -1, price, 0, (price / 10), 0, new Vector3(player.Position.X, player.Position.Y, player.Position.Z));
        Bussines.BussinesListCreate();
        player.SendChatMessage($"New biz add {name}, {price}, at {player.Position.X}, {player.Position.Y}, {player.Position.Z}");
    }
    [Command("delbiz")]
    public void delbiz(Player player)
    {
        if (player.GetData<int>("IS_ADMIN") == 0) return;
        Bussines.deleteBussines();
        player.SendChatMessage($"All Biz delete");
    }
    [Command("restartbiz")]
    public void restartbiz(Player player)
    {
        if (player.GetData<int>("IS_ADMIN") == 0) return;
        Bussines.BussinesListCreate();
        player.SendChatMessage($"All Biz restored");
    }
}