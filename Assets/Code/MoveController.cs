using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.Events;


public class MoveController : MonoBehaviour
{
    public List<Vector3> Path;
    public float Speed = 1;
    public float Elapsed = 0;

    public Animator Animator;

    public string IsWalkingParameter;
    public string SpeedParameter;

    public float Duration => Path.Length() / Speed;
    public bool HasArrived => Elapsed >= Duration;

    public Vector3 TargetPosition
    {
        get
        {
            if (HasArrived)
                return Path.Last();

            int position_index = GetPositionIndex();

            return Path[position_index].Lerped(
                Path[position_index + 1], 
                GetLocalProgress());
        }
    }

    private void Update()
    {
        if (Path == null || Path.Count < 2 ||
            HasArrived)
        {
            Animator.SetBool(IsWalkingParameter, false);
            return;
        }
        else
            Animator.SetBool(IsWalkingParameter, true);

        Animator.SetFloat(SpeedParameter, Speed);

        Elapsed += Time.deltaTime;
        transform.position = TargetPosition;

        int position_index = GetPositionIndex();

        transform.rotation = Quaternion.Slerp(
            transform.rotation, 
            Quaternion.LookRotation(Path[position_index + 1] - 
                                    Path[position_index]), 
            2 * Speed * Time.deltaTime);
    }

    public void Animate(Location A, Location B)
    {
        List<Vector3> path = A.GetPathTo(B);
        if (path == null)
            return;

        Path = path;
        Elapsed = 0;
    }

    int GetPositionIndex()
    {
        float traversed = Elapsed * Speed;

        for (int i = 0; i < Path.Count - 1; i++)
        {
            float distance = Path[i].Distance(Path[i + 1]);

            if (traversed <= distance)
                return i;
            else
                traversed -= distance;
        }

        return Path.Count - 2;
    }

    float GetLocalProgress()
    {
        int position_index = GetPositionIndex();

        float local_distance_traveled = 
            (Elapsed * Speed) - 
            Path.GetRange(0, position_index + 1).Length();

        float local_distance = Path[position_index].Distance(
                               Path[position_index + 1]);

        return local_distance_traveled / local_distance;
    }
}
