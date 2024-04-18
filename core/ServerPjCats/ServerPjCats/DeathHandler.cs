using GTANetworkAPI;
using MySqlX.XDevAPI;
using System.Threading;

public class DeathHandler : Script
{

    [ServerEvent(Event.PlayerDeath)]
    public void OnPlayerDeath(GTANetworkAPI.Player player, GTANetworkAPI.Player killer, System.UInt32 asd)
    {
        NAPI.Player.SpawnPlayer(player, new Vector3(298.39362, -584.58405, 43.26084));
        player.Health = 100;
    }
}
