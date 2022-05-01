using UnityEngine;
using System.Collections;

public class TacticalUI : MonoBehaviour
{
    public RectTransform Container;

    public CritterUI CritterUI;

    private void Update()
    {
        Container.gameObject.SetActive(!The.Narrator.IsReading);
        CritterUI.gameObject.SetActive(Critter.Selected != null);
    }
}
