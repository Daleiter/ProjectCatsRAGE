using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class DeliveryData
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Vector3 Position { get; set; }
    public int Price { get; set; }
    public int Height { get; set; }

    public DeliveryData(int id, string name, Vector3 position,  int price, int height)
    {
        Id = id;
        Name = name;
        Position = position;
        Price = price;
        Height = height;
    }
}
public class Delivery : Script
{
    public static List<DeliveryData> deliveryList = new List<DeliveryData>();
    public static void addDeliveryData()
    {
        deliveryList.Add(new DeliveryData(1,"test", new Vector3(1,1,1), 100, 50));
        deliveryList.Add(new DeliveryData(2, "tes2", new Vector3(2, 2, 2), 200, 20));
    }
    public static void OnChekpointDelivery(Player player)
    {
        NAPI.ClientEvent.TriggerClientEvent(player, "SERVER:CLIENT::OnChekpointDelivery", deliveryList);
    }

    [RemoteEvent("CLIENT:SERVER::takeDeliveryOrder")]
    public static void takeDeliveryOrder(Player player, int orderid)
    {
        player.SetData<int>("DeliveryOrderID", orderid);

        if (player.HasData("Vechicle")) { 
            Cars.SpawnCar(player, "0", "0", "pounder", 1, 1, "Delivery");
            Cars.SpawnCar(player, "0", "0", "pounder", 1, 1, "Delivery");
        }
        else
        {
            Cars.SpawnCar(player, "0", "0", "pounder", 1, 1, "Delivery");
        }
    }
}
