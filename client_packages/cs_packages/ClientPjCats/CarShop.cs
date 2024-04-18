using RAGE;
using RAGE.Elements;
using RAGE.Game;
using RAGE.Ui;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

public class CarShop
{
    public static HtmlWindow CarShopWindow;
    public static Camera carshopcamera;
    public static string colorcarshop = "0";
    public static string colorcarshop2 = "0";
    public static void changeCarColor1InCarShop(object[] args)
    {
        colorcarshop = args[0].ToString();
        Events.CallRemote("CLIENT:SERVER::changeCarColor1InCarShop", colorcarshop);
    }
    public static void changeCarColor2InCarShop(object[] args)
    {
        colorcarshop2 = args[0].ToString();
        Events.CallRemote("CLIENT:SERVER::changeCarColor2InCarShop", colorcarshop2);
    }
    public static void ChangeCarInCarShop(object[] args)
    {
        string car = args[0].ToString();
        Events.CallRemote("CLIENT:SERVER::ChangeCarInCarShop", car);
    }
    
    public static void BuyCar(object[] args)
    {
        string car = args[0].ToString();
        string money = args[1].ToString();
        Events.CallRemote("CLIENT:SERVER::BuyCar", car, money, colorcarshop, colorcarshop2);
    }
    public static void EnterCarShop(object[] args)
    {
        Ui.DisplayRadar(false);
        Chat.Activate(false);
        carshopcamera = new Camera((ushort)Cam.CreateCameraWithParams(Misc.GetHashKey("DEFAULT_SCRIPTED_CAMERA"), -42f, -1099, 27.422338f, 0, 0, 3.9309103f, 70.0f, true, 2),0);
        Cam.PointCamAtCoord(carshopcamera.Id, -44.69792f, -1094.3165f, 26.422338f);
        Cam.SetCamActive(carshopcamera.Id, true);
        Cam.RenderScriptCams(true, true, 1, true, false, 0);
        Cursor.ShowCursor(true, true);
        CarShopWindow = new HtmlWindow("package://cef/cars/salon/index.html");
        CarShopWindow.Active = true;
    }
    public static void ExitCarShop(object[] args)
    {
        CarShopWindow.Destroy();
        Ui.DisplayRadar(true);
        Chat.Activate(true);
        Cam.RenderScriptCams(false, true, 1, true, false, 0);
        carshopcamera = null;
        Cursor.ShowCursor(false, false);
        Events.CallRemote("CLIENT:SERVER::ExitCarShop");
    }
    //    -43,317345, -1103,607, 26,422338 camera
    //0, 0, 3,9309103
    //-44,69792, -1094,3165, 26,422338 car
    //0, 0, 114,68938
    //-47,954292, -1090,9191, 26,422338
    //0, 0, -158,1875
}
