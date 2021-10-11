using UnityEngine;
using System.Collections;
using System.Linq;

public class LocationCursor : MapCursor
{
    protected override void Update()
    {
        Location = The.Floor.LocationPointedAt;
        base.Update();

        if (InputUtility.WasMouseLeftReleased)
        {
            Critter critter = Location.Residents
                .SelectComponents<Resident, Critter>().FirstOrDefault();

            if (critter != null)
                critter.IsSelected = true;
        }
    }
}