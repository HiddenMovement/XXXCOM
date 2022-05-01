using System;
using System.Collections;
using UnityEngine;

public class PauseForEffect : Passage
{
    bool pause_has_begun = false;
    float ElapsedSeconds = 0;

    public float SecondsToWait;
    public Passage Passage;

    public override Passage NextPassage
    {
        get
        {
            if (ElapsedSeconds < SecondsToWait)
                return null;

            if (Passage != null)
                return Passage.NextPassage;
            else
                return base.NextPassage;
        }
    }

    private void Update()
    {
        if(pause_has_begun)
            ElapsedSeconds += Time.deltaTime;
    }

    public override void Read()
    {
        base.Read();

        Passage.Read();

        pause_has_begun = true;
    }


    public static PauseForEffect Make(float seconds_to_wait, 
                                      Passage passage = null)
    {
        PauseForEffect pause = Passage.Make<PauseForEffect>();
        pause.SecondsToWait = seconds_to_wait;
        pause.Passage = passage;

        if (passage != null)
            passage.transform.SetParent(pause.transform);

        return pause;
    }
}


public static class PauseForEffectExtensions
{
    public static PauseForEffect ThenWait(this Passage passage, 
                                          TimeSpan duration)
    {
        return PauseForEffect.Make((float)duration.TotalSeconds, passage);
    }
}