using RAGE.Ui;
using RAGE;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RAGE.Elements;
using System.Drawing;

public class Delivery
{
    public static HtmlWindow DeliveryWindow;
    public static void OnChekpointDelivery(object[] args)
    {
        DeliveryWindow = new HtmlWindow("package://cef/work_delivery/index.html");
        DeliveryWindow.Active = true;
        Cursor.ShowCursor(true, true); 
        JArray datajs = (JArray)args[0];
        string strdata = JsonConvert.SerializeObject(datajs);
        string script = $"document.dispatchEvent(new CustomEvent('SenDeliveryListToHUD', {{ detail: {{ deliveryList: '{strdata}' }} }}));";
        DeliveryWindow.ExecuteJs(script);
    }
    public static void closeDelivery(object[] args)
    {
        DeliveryWindow.Destroy();
        Cursor.ShowCursor(false, false);
        DeliveryWindow =null;
    }
   public static void takeDeliveryOrder(object[] args)
    {
        Events.CallRemote("CLIENT:SERVER::takeDeliveryOrder", args[0]);
        DeliveryWindow.Destroy();
        Cursor.ShowCursor(false, false);
        DeliveryWindow = null;
        Vector3 pos = new Vector3(283.81415f, -575.9476f, 44.172573f);
        Marker marker = new Marker(1, pos, 1, new Vector3(), new Vector3(), new RGBA(255,0,0,100));
        Blip blip = new Blip(12, pos, "Delivery");
        Colshape col = new Colshape(1, 1);
        col.Position = pos;
        col.Dimension = 0;
        col.OnEnter = OnPlayerEnterColshape;

    }

    //private static void OnPlayerEnterColshape(Events.CancelEventArgs cancel)
    //{
    //    throw new NotImplementedException();
    //}

    public static void OnPlayerEnterColshape(RAGE.Elements.Colshape colshape, Events.CancelEventArgs cancel)
    {
        RAGE.Chat.Output($"You entered colshape id:{colshape.Id}");
    }
}
