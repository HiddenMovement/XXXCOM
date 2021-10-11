using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class EntityCursor : MapCursor
{
    public Entity Target { get { return Location.Entity; } }

    protected override void Update()
    {
        Func<Location, float> GetDistance = 
            location => location.transform.position.Distance(
                        The.Floor.LocationPointedAt.transform.position);

        Location = The.Map.Entities
            .Select(entity => entity.GetComponent<Resident>().Location)
            .MinElement(GetDistance);

        base.Update();
    }
}
