using UnityEngine;
using System.Collections;

public class AbilityUI<T> : CritterUIElement where T : Ability
{
    public RectTransform Content;

    public T Ability
    {
        get
        {
            if (Critter.SelectedAbility is T)
                return Critter.SelectedAbility as T;

            return null;
        }
    }

    private void OnEnable()
    {
        Content.gameObject.SetActive(false);
    }

    protected virtual void Update()
    {
        if (Content.gameObject.activeSelf)
        {
            if (Ability == null)
                OnDeselected();
        }
        else if (Ability != null)
            OnSelected();
    }

    protected virtual void OnSelected()
    { Content.gameObject.SetActive(true); }

    protected virtual void OnDeselected()
    { Content.gameObject.SetActive(false); }
}
