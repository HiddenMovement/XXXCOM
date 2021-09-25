using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;


public class Move : TargetLocationAbility
{
    public int MoveCount = 0;

    public override int CostMultiplier => GetMovesRequired();

    public override bool CanDo
    {
        get
        {
            if (!base.CanDo)
                return false;

            return Critter.MoveController.HasArrived && 
                   GetPath() != null;
        }
    }

    protected override void Start()
    {
        base.Start();

        AddResponse<OnBeginTurn>(() => Critter.SelectedAbility = this);
        AddResponse<OnBeginTurn>(() => MoveCount = 0);
    }

    public List<Vector3> GetPath()
    {
        return Critter.Location.GetPathTo(Target);
    }

    public override void Do()
    {
        base.Do();

        MoveCount += GetMovesRequired();

        Critter.MoveController.Animate(Resident.Location, Target);

        Target.AddResident(Resident);
    }

    public int GetMoveRange(int index)
    {
        float modifier = Mathf.Max(
            0.6f - index * 0.2f, 
            0.2f);

        return (Attributes[Attribute.Speed] * modifier).Round();
    }

    public int GetNextMoveRange()
    {
        return GetMoveRange(MoveCount);
    }

    public int GetMovesRequired(float distance)
    {
        if (Attributes[Attribute.Speed] == 0)
            return int.MaxValue;

        int moves_required = 0;

        while (distance > 0)
            distance -= GetMoveRange(MoveCount + moves_required++);

        return moves_required;
    }

    public int GetMovesRequired(List<Vector3> path)
    {
        return GetMovesRequired(path.Length());
    }

    public int GetMovesRequired()
    {
        List<Vector3> path = GetPath();
        if (path == null)
            return -1;

        return GetMovesRequired(path);
    }
}
