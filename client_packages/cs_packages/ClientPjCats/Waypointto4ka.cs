using RAGE;
using System;
using System.Collections.Generic;
using System.Text;

public class Waypointto4ka : Events.Script
{
    public static Vector3 positionWaypoint = new Vector3(284, -582, 43);
    public Waypointto4ka() 
    {
        Events.OnPlayerCreateWaypoint += OnPlayerCreateWaypoint;
    }
    public void OnPlayerCreateWaypoint(Vector3 position)
    {

        positionWaypoint = new Vector3(position.X, position.Y, position.Z);
    }
}
