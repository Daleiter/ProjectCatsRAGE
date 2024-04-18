using GTANetworkAPI;

public class MyScript : Script
{
    [Command("createcheckpoint")] // Команда для створення чекпоінта
    public void CreateCheckpoint(GTANetworkAPI.Player player)
    {
        // Створення чекпоінта
        GTANetworkAPI.Checkpoint checkpoint = NAPI.Checkpoint.CreateCheckpoint(0, new Vector3(-1853, 4562, 7), new Vector3(-1851, 4561, 8), 2f, new Color(255, 255, 0, 100));

        // Додавання чекпоінта до світу
        checkpoint.Dimension = 0; // Встановлення тієї ж самої розмірності, що й гравець
        //checkpoint.Visible = true; // Робимо чекпоінт видимим для гравців
    }
    [Command("col")]
    public void Colshape(Player player)
    {
        var scale = 2;
        var position = player.Position + new Vector3(0f, 0f, -1f);
        var colShape = NAPI.ColShape.CreateSphereColShape(position, scale, player.Dimension);
        colShape.SetData(nameof(GTANetworkAPI.Marker), NAPI.Marker.CreateMarker(1, position, new Vector3(), new Vector3(), scale *2, new Color(255,0,0,100), false, player.Dimension));
        colShape.OnEntityEnterColShape += OnEntityEnterColShape;
        colShape.OnEntityExitColShape += OnEntityExitColShape;
    }
    public void OnEntityEnterColShape(ColShape colShape, Player player)
    {
        player.Health = 10;
        player.SendChatMessage("darova zaebal");
        NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::OnChekpoint");
        NAPI.Util.ConsoleOutput("in");
    }

    public void OnEntityExitColShape(ColShape colShape, Player player)
    {
        player.Health = 100;
        player.SendChatMessage("nu i uebui");
        NAPI.Util.ConsoleOutput("out");
    }
}
