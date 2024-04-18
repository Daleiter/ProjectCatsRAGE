using RAGE;
using RAGE.Game;
using RAGE.Ui;
using YourNamespace.Inputs;


public class KeyHandler : Events.Script
  {
        public static HtmlWindow phone;
        public static int phoneopen = 0;
        public static int cursor = 0;
        public KeyHandler(){

      Key.Bind(KeyCodes.UpArrow, OpenPhone);
      Key.Bind(KeyCodes.P, choseCursor);
      Key.Bind(KeyCodes.F2, Cars.Repair);
        Key.Bind(KeyCodes.F3, Cars.ShowCarList);
        Key.Bind(KeyCodes.F4, Cars.Engine);
      Key.Bind(KeyCodes.F5, Cars.Lock);
        Key.Bind(KeyCodes.F6, Cars.Flip);
        Key.Bind(KeyCodes.F7, showHUD);
        Key.Bind(KeyCodes.Key_9, teleport);
    }
    public static void teleport() { 
    Events.CallRemote("CLIENT:SERVER::taxigoto", Waypointto4ka.positionWaypoint.X, Waypointto4ka.positionWaypoint.Y, Waypointto4ka.positionWaypoint.Z);
    }
    public static void showHUD()
        {
        Chat.Output("ShowHUD");
        HUD.HUDWindow.Active = true;

    }
    public static void OpenPhone(){
            if (phoneopen != 1)
            {
                phone = new HtmlWindow("package://cef/phone/index.html");
                phone.Active = true;
                phoneopen = 1;
                Cursor.ShowCursor(false, true);
            }
            else
            {
                phone.Destroy();
                phoneopen = 0;
                Cursor.ShowCursor(false, false);
            }
        }
        public static void choseCursor() {
            if (cursor != 1)
            {
                Cursor.ShowCursor(true, true);
                cursor = 1;
            }
            else
            {
                Cursor.ShowCursor(false, false);
                cursor = 0;
            }
        }

    
    
  }
