using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

public class WaypointData
{
    public Vector3 Position { get; set; }
    public string Name { get; set; }
    public int BlipSprite { get; set; }

    public WaypointData(Vector3 position, string name, int blipSprite)
    {
        Position = position;
        Name = name;
        BlipSprite = blipSprite;
    }
}
public class Waypoints
{
    private static List<WaypointData> waypointList = new List<WaypointData>
    {
        new WaypointData(new Vector3(-816.79175f, -183.73183f, 38f), "cyrulnia", 480),
        new WaypointData(new Vector3(-38.53791f, -1101.5898f, 26.4f), "autosail", 220),
        new WaypointData(new Vector3(291.81415f, -581.9476f, 43.172573f), "Delivery", 85)
    };
    public static void WaypointsListCreate()
    {
        // Створення ваїпоінтів з даних ліста
        foreach (WaypointData waypoint in waypointList)
        {
            createwaypoint(waypoint.Position, waypoint.Name, waypoint.BlipSprite);
        }
    }
    public static void createwaypoint(Vector3 position, string name, int blipsprite)
    {
        float scale = 1;
        var colShape = NAPI.ColShape.CreateSphereColShape(position, scale, 0);
        colShape.SetData(nameof(GTANetworkAPI.Marker), NAPI.Marker.CreateMarker(1, new Vector3(position.X, position.Y, position.Z - 1f), new Vector3(), new Vector3(), scale, new Color(255, 0, 0, 100), false, 0));
        colShape.SetData<string>("Name", name);
        colShape.OnEntityEnterColShape += OnEntityEnterColShape;
        colShape.OnEntityExitColShape += OnEntityExitColShape;

        NAPI.Blip.CreateBlip(blipsprite, position, 1f, 0, name, 255, 0f, true, 0, 0);
    }
    private static void OnEntityEnterColShape(GTANetworkAPI.ColShape colShape, GTANetworkAPI.Player player)
    {
        if (colShape.HasData("Name"))
        {
            if (colShape.GetData<string>("Name") == "cyrulnia")
            {
                player.Health = 10;
                player.SendChatMessage("darova zaebal");
                NAPI.Util.ConsoleOutput("in");
                NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::OnChekpoint");
            }
            if (colShape.GetData<string>("Name") == "autosail")
            {
                CarShop.OnChekpointAutoSail(player);
            }
            if (colShape.GetData<string>("Name") == "Delivery")
            {
                Delivery.OnChekpointDelivery(player);
            }
        }
    }
    private static void OnEntityExitColShape(GTANetworkAPI.ColShape colShape, GTANetworkAPI.Player player)
    {
        if (colShape.HasData("Name"))
        {
            if (colShape.GetData<string>("Name") == "cyrulnia")
            {
                player.Health = 100;
                player.SendChatMessage("nu i uebui");
                NAPI.Util.ConsoleOutput("out");
            }
        }
    }

    //public static void createwaypointcyrulnia() 
    //{ 
    //float scale = 1;
    //var position = new Vector3(-816.79175, -183.73183, 37);
    //var colShape = NAPI.ColShape.CreateSphereColShape(position, scale, 0);
    //colShape.SetData(nameof(GTANetworkAPI.Marker), NAPI.Marker.CreateMarker(1, position, new Vector3(), new Vector3(), scale, new Color(255, 0, 0, 100), false, 0));
    //    colShape.OnEntityEnterColShape += OnEntityEnterColShape;
    //    colShape.OnEntityExitColShape += OnEntityExitColShape;

    //    NAPI.Blip.CreateBlip(480, new Vector3(-816.79175, -183.73183, 36.568913), 1f, 0, "cyrulnia", 255, 0f, true, 0, 0); // Приклад координат, де буде розташований блип
    //}
    //private static void OnEntityExitColShape(GTANetworkAPI.ColShape colShape, GTANetworkAPI.Player player)
    //{
    //    player.Health = 100;
    //    player.SendChatMessage("nu i uebui");
    //    NAPI.Util.ConsoleOutput("out");
    //}

    //private static void OnEntityEnterColShape(GTANetworkAPI.ColShape colShape, GTANetworkAPI.Player player)
    //{
    //    player.Health = 10;
    //    player.SendChatMessage("darova zaebal");
    //    NAPI.Util.ConsoleOutput("in");
    //    NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::OnChekpoint");
    //}
}
