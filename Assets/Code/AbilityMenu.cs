using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class AbilityMenu : CritterUIElement
{
    public AbilitySquare AbilitySquarePrefab;

    public RectTransform AbilitySquaresContainer,
                         AbilitySquaresContainerContainer;

    public IEnumerable<AbilitySquare> AbilitySquares
    { get { return AbilitySquaresContainer.GetComponentsInChildren<AbilitySquare>(); } }

    private void Start()
    {
        foreach (Transform child in AbilitySquaresContainer)
            Destroy(child.gameObject);
    }

    private void Update()
    {
        float target_container_width =
            Critter.Abilities.Count() *
            AbilitySquarePrefab.RectTransform.sizeDelta.x;

        AbilitySquaresContainerContainer.sizeDelta =
            AbilitySquaresContainerContainer.sizeDelta.XChangedTo(target_container_width);


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
                ability_square.transform.SetParent(AbilitySquaresContainer);
                ability_square.Ability = ability;
            }
        }
    }
}
