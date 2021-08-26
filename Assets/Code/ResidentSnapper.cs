using UnityEngine;
using System.Collections;

[ExecuteAlways]
[RequireComponent(typeof(Resident))]
public class ResidentSnapper : MonoBehaviour
{
    Vector3 last_position = Vector3.zero;

    public Resident Resident { get { return GetComponent<Resident>(); } }

    private void Update()
    {
        /*if (last_position != transform.position)
        {
            Resident.Location = The.Floor.NodePointedAt;
            transform.position = Resident.Location.transform.position;
        }

        last_position = transform.position;*/
    }
}
