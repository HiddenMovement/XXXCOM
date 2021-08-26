using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(Critter))]
public class Triggers : MonoBehaviour
{
    public OnBeginBattle OnBeginBattle { get; private set; }
    public OnEndBattle OnEndBattle { get; private set; }
    public OnBeginTurn OnBeginTurn { get; private set; }
    public OnEndTurn OnEndTurn { get; private set; }
    public OnTakeDamage OnTakeDamage { get; private set; }
    public OnDie OnDie { get; private set; }

    public Critter Critter { get { return GetComponent<Critter>(); } }

    private void Start()
    {
        OnBeginBattle = () => Propagate<OnBeginTurn>(r => r());
        OnEndBattle = () => Propagate<OnEndTurn>(r => r());
        OnBeginTurn = () => Propagate<OnBeginTurn>(r => r());
        OnEndTurn = () => Propagate<OnEndTurn>(r => r());
        OnTakeDamage = (damage) => Propagate<OnTakeDamage>(r => r(damage));
        OnDie = () => Propagate<OnDie>(r => r());
    }

    void Propagate<T>(System.Action<T> InvokeResponse) where T : Delegate
    {
        Func<Trait, IEnumerable<(T Response, int Priority)>>
        GetResponsesAndPriorities = trait =>
        {
            return trait.GetResponses<T>().Select(
                response => (response, trait.GetResponsePriority(response)));
        };

        List<T> responses = Critter.Traits
            .SelectMany(trait => GetResponsesAndPriorities(trait))
            .OrderBy(pair => pair.Priority)
            .Select(pair => pair.Response)
            .ToList();

        foreach (T response in responses)
            InvokeResponse(response);
    }
}

public delegate void OnBeginBattle();
public delegate void OnEndBattle();
public delegate void OnBeginTurn();
public delegate void OnEndTurn();
public delegate void OnTakeDamage(int damage);
public delegate void OnDie();

