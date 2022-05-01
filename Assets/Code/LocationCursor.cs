using UnityEngine;
using System.Collections;
using System.Linq;

public class LocationCursor : MapCursor
{
    protected override void Update()
    {
        if(The.Floor.IsTouched)
            Location = The.Floor.LocationPointedAt;

        base.Update();

        if (The.Floor.WasLeftClicked)
        {
            Critter critter = Location.Residents
                .SelectComponents<Resident, Critter>().FirstOrDefault();

            if (critter != null)
                critter.IsSelected = true;
        }
    }
}