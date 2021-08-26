using UnityEngine;
using System.Collections;

public class TacticalUI : MonoBehaviour
{
    public CritterUI CritterUI;

    private void Update()
    {
        CritterUI.gameObject.SetActive(Critter.Selected != null);
    }
}
