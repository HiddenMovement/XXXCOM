using UnityEngine;
using System.Collections;
using System.Linq;


[RequireComponent(typeof(Resident))]
public class MapCursor : MonoBehaviour
{
    public Location Location { get { return Resident.Location; } }

    public Resident Resident { get { return GetComponent<Resident>(); } }

    private void Update()
    {
        Resident.Location = The.Floor.LocationPointedAt;

        if(InputUtility.WasMouseLeftReleased)
        {
            Critter critter = Resident.Location.Residents
                .SelectComponents<Resident, Critter>().FirstOrDefault();

            if (critter != null)
                critter.IsSelected = true;
        } 
    }
}
