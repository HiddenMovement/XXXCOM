using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TurnScheduler : MonoBehaviour
{
    Dictionary<Turn, int> turn_dates = new Dictionary<Turn, int>();
    int current_turn_date = 0;

    IEnumerable<Turn> CurrentTurns
    {
        get
        {
            return turn_dates.Keys
                .Where(turn => turn_dates[turn] == current_turn_date && 
                               !turn.HasEnded);
        }
    }

    private void Update()
    {
        if (CurrentTurns.Count() == 0)
        {
            IEnumerable<Turn> future_turns = turn_dates.Keys
                .Where(turn => turn_dates[turn] > current_turn_date);

            if (future_turns.Count() > 0)
                current_turn_date = future_turns.Min(turn => turn_dates[turn]);
        }

        foreach (Turn turn in CurrentTurns.ToList())
        {
            if (!turn.HasBegun)
                turn.Begin();
            else if (turn.IsReadyToEnd)
                turn.End();
        }
    }

    public void Schedule(Turn turn, int duration)
    {
        turn_dates[turn] = current_turn_date + duration;
    }
}
