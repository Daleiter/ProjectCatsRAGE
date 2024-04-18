using RAGE;
using RAGE.Game;
using RAGE.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Numerics;
using Newtonsoft.Json;
using RAGE.Ui;
public class Cars
{
    public static HtmlWindow CarHUD;
    public static JArray CarList;
    public static float carspeed;
    public static bool IsInCar = false;
    public static HtmlWindow SpeedHUD;

    public static void UpdateSpeedVehicle(List<Events.TickNametagData> nametags)
    {
        if (RAGE.Elements.Player.LocalPlayer.Vehicle != null )
        {
            SpeedHUD.ExecuteJs($"document.dispatchEvent(new CustomEvent('SendSpeedToHUD', {{ detail: {{ speedfromclient: '{Convert.ToInt32(RAGE.Elements.Player.LocalPlayer.Vehicle.GetSpeed() * 3.6)}' }} }}));");
        }
        else
        {
            if (SpeedHUD != null)
            {
                SpeedHUD.Destroy();
                SpeedHUD = null;
            }
        }
    }
    
    public static void EnterVehicle(RAGE.Elements.Vehicle vehicleId, int seatId)
    { 
        SpeedHUD = new HtmlWindow("package://cef/HUD/speedometer/index.html");
        SpeedHUD.Active = true;
    }
    public static void LeaveVehicle(RAGE.Elements.Vehicle vehicleId, int seatId)
    {
        SpeedHUD.Destroy();
    }
    
        public static void spawnCar(object[] args)
    {
        string id = (string)args[0];
        string ownerid = (string)args[1];
        string vhash = (string)args[2];
        string color1 = (string)args[3];
        string color2 = (string)args[4];
        string numperplate = (string)args[5];
        Events.CallRemote("CLIENT:SERVER::SpawnCar", id, ownerid, vhash, color1, color2, numperplate);
    }
    public static void ShowCarList()
    {
        if (CarHUD == null) {
            CarHUD = new HtmlWindow("package://cef/cars/index.html");
            CarHUD.Active = true;
            Cursor.ShowCursor(true, true);
            string carListJson = JsonConvert.SerializeObject(CarList);
            CarHUD.ExecuteJs($"document.dispatchEvent(new CustomEvent('SendCarListToHUD', {{ detail: {{ carlist: '{carListJson}' }} }}));");
        } else 
        { CarHUD.Destroy();
            Cursor.ShowCursor(false, false);
            CarHUD = null;
        }
    }
    public static void SendCarList(object[] args)
    {
        string carstojs =  args[0].ToString();
        CarList = JArray.Parse(carstojs);
        foreach (JObject obj in CarList)
        {
            string ownerValue = (string)obj["id"];

        }    


        Type typeOfFirstElement = args[0].GetType();

    }
    public static void Flip()
    {
        Events.CallRemote("CLIENT:SERVER::CarFlip");
    }
    public static void Repair()
    {
        Events.CallRemote("CLIENT:SERVER::CarRepair");
    }
    public static void Engine()
    {
        Events.CallRemote("CLIENT:SERVER::CarEngine");
    }
    public static void Lock()
    {
        Events.CallRemote("CLIENT:SERVER::CarLock");
    }

}