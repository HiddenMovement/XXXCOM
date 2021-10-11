using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Squad : MonoBehaviour
{
    public int Initiative = 0;

    public IEnumerable<Critter> Critters
    {
        get
        {
            return The.Map.GetComponentsInChildren<Critter>()
                .Where(critter => critter.Squad == this);
        }
    }

    private void Start()
    {
        The.TurnScheduler.Schedule(new SquadTurn(this), 
                                   20 - Initiative);
    }
}

public class SquadTurn : Turn
{
    Squad squad;

    public override bool IsReadyToEnd
    {
        get
        {
            IEnumerable<Critter> active_critters = squad.Critters
                .Where(critter => critter.Entity.Attributes[Attribute.Energy] > 0);

            return active_critters.Count() == 0;
        }
    }

    public SquadTurn(Squad squad_)
    {
        squad = squad_;
    }

    public override void Begin()
    {
        base.Begin();

        foreach (Critter critter in squad.Critters)
            critter.Entity.Triggers.OnBeginTurn();
    }

    public override void End()
    {
        base.End();

        foreach (Critter critter in squad.Critters)
            critter.Entity.Triggers.OnEndTurn();

        The.TurnScheduler.Schedule(
            new SquadTurn(squad), 
            GameObject.FindObjectsOfType<Squad>().Count());
    }
}