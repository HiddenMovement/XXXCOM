using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class AbilityMenu : CritterUIElement
{
    public AbilitySquare AbilitySquarePrefab;

    public Grid AbilitySquaresGrid;
    public RectTransform AbilitySquaresGridContainer;

    public IEnumerable<AbilitySquare> AbilitySquares
    { get { return AbilitySquaresGrid.GetComponentsInChildren<AbilitySquare>(); } }

    private void Start()
    {
        foreach (Transform child in AbilitySquaresGrid.transform)
            Destroy(child.gameObject);
    }

    private void Update()
    {
        int ability_count = Critter.Abilities.Count();

        float target_container_width =
            ability_count * AbilitySquarePrefab.RectTransform.sizeDelta.x +
            AbilitySquaresGrid.Margin * (ability_count - 1);

        AbilitySquaresGridContainer.sizeDelta =
            AbilitySquaresGridContainer.sizeDelta.XChangedTo(target_container_width);


        foreach (AbilitySquare ability_square in AbilitySquares.ToList())
            if (!Critter.Abilities.Contains(ability_square.Ability))
                Destroy(ability_square.gameObject);

        foreach (Ability ability in Critter.Abilities)
        {
            IEnumerable<Ability> abilities =
                AbilitySquares.Select(ability_square => ability_square.Ability);

            if (!abilities.Contains(ability))
            {
                AbilitySquare ability_square = Instantiate(AbilitySquarePrefab);
                ability_square.transform.SetParent(AbilitySquaresGrid.transform);
                ability_square.Ability = ability;
            }
        }
    }
}
