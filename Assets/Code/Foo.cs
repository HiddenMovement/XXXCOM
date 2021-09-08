using System;
using System.Collections.Generic;
using System.Linq;

public class Character
{
    public string Name;
    public int Confusion = 0;

    public Character(string name)
    {
        Name = name;
    }

    public SayEvent Say(string message)
    {
        return new SayEvent(this, message);
    }

    public class SayEvent : StoryEvent
    {
        public Character Character;
        public string Message;

        public bool HasBeenRead { get; set; }

        public override bool IsFinished => HasBeenRead;

        public SayEvent(Character character, string message)
        {
            Character = character;
            Message = message;
        }

        public override void Happen()
        {
            throw new System.NotImplementedException();
        }
    }
}


public abstract class StoryEvent
{
    public abstract bool IsFinished { get; }
    public abstract void Happen();
}


public class Instant : StoryEvent
{
    public System.Action Action;

    public override bool IsFinished => true;

    public Instant(System.Action action)
    {
        Action = action;
    }

    public override void Happen()
    {
        Action();
    }
}


public class Jump : StoryEvent
{
    public Chapter Chapter;

    public override bool IsFinished => true;

    public Jump(Chapter chapter)
    {
        Chapter = chapter;
    }

    public override void Happen()
    {
        Chapter.Continue();
    }
}

public class Choice : StoryEvent
{
    public Dictionary<string, StoryEvent> Choices;
    public StoryEvent EventChosen = null;//****naming, also don't like "storyevent"

    public override bool IsFinished => EventChosen == null &&
                                       EventChosen.IsFinished;

    public Choice(Dictionary<string, StoryEvent> choices)
    {
        Choices = choices;
    }

    public override void Happen()
    {
        EventChosen.Happen();
    }
}

public class If : StoryEvent
{
    bool condition_result;//****naming

    public Func<bool> ConditionFunc;//****naming
    public StoryEvent True, False;//****naming

    public override bool IsFinished => condition_result ? True.IsFinished : 
                                                          False.IsFinished;

    public If(Func<bool> ConditionFunc_, StoryEvent true_event, StoryEvent false_event)
    {
        ConditionFunc = ConditionFunc_;
        True = true_event;
        False = false_event;
    }

    public override void Happen()
    {
        condition_result = ConditionFunc();

        if (condition_result)
            True.Happen();
        else
            False.Happen();
    }
}


public class Chapter
{
    public List<StoryEvent> Events;
    public StoryEvent CurrentEvent;

    public Chapter(List<StoryEvent> events)
    {
        Events = events;
    }

    public void Continue()
    {
        Current = this;
    }

    public void Update()//****Some other way than an Update method?
    {
        if (CurrentEvent == null)
        {
            CurrentEvent = Events.First();
            CurrentEvent.Happen();
        }
        else if (CurrentEvent.IsFinished)
        {
            if (CurrentEvent == Events.Last())
                Current = null;
            else
            {
                CurrentEvent = Events.NextElement(CurrentEvent);
                CurrentEvent.Happen();
            }
        }
    }

    public static Chapter Current;
}


//****Connnecting to gameplay characters
//Modifying a Say based on data
//Are chapters storyevents? (recursive)
//bool Happen() to implement blocking and non-blocking storyevents? (this implies Update() => Tell() or something)
//unlabeled subchapters

public static class Story
{
    public static Character Narrator = new Character("");
    public static Character Sammy = new Character("Samantha Bangtail");
    public static Character Emilie = new Character("Emilie Bangtail");

    public static Chapter Chapter1 = new Chapter(Utility.List<StoryEvent>
    (
        Narrator.Say("Hello!"),
        Sammy.Say("Right back at ya!"),
        Emilie.Say("Who are you talking to?"),
        Choice(Utility.Dictionary<string, StoryEvent>
        (
            "Didn't you hear that?", Jump(Chapter3),
            "Err, nothing!", Instant(() => { Emilie.Confusion += 2; })
        )),
        Jump(Chapter2)
    ));
    public static Chapter Chapter2;
    public static Chapter Chapter3;

    static Story()
    {

    }

    static Jump Jump(Chapter chapter)
    {
        return new Jump(chapter);
    }

    static Instant Instant(System.Action action)
    {
        return new Instant(action);
    }

    static Choice Choice(Dictionary<string, StoryEvent> choices)
    {
        return new Choice(choices);
    }
}