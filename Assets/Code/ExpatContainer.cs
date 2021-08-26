using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class ExpatContainer : MonoBehaviour
{
    public Transform Origin;


    private void Update()
    {
        if (Origin == null)
            Destroy(gameObject);

        foreach (Transform child in transform)
            child.gameObject.SetActive(Origin.gameObject.activeSelf);
    }


    public static IEnumerable<ExpatContainer> ExpatContainers
    { get { return The.ExpatContainersContainer.GetComponentsInChildren<ExpatContainer>(); } }

    public static ExpatContainer GetExpatContainer(Transform origin)
    {
        ExpatContainer expat_container = ExpatContainers
            .FirstOrDefault(expat_container_ => expat_container_.Origin == origin);

        if(expat_container == null)
        {
            expat_container = new GameObject("ExpatContainer")
                .AddComponent<ExpatContainer>();
            expat_container.transform.SetParent(The.ExpatContainersContainer);
            expat_container.Origin = origin;
        }

        return expat_container;
    }

    public static void Expatriate(Transform transform, Transform origin = null)
    {
        if (origin == null)
            origin = transform.parent;

        transform.SetParent(GetExpatContainer(origin).transform);
    }
}

public static class ExpatExtensions
{
    public static void Expatriate(this Transform transform, Transform origin = null)
    {
        ExpatContainer.Expatriate(transform, origin);
    }
}
