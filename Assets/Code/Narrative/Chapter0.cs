using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Linq;


public class Chapter0 : Chapter
{
    Character Sammy => The.Characters.Sammy;
    Character Emilie => The.Characters.Emilie;

    void Function0() { }
    void Function1() { }

    protected override List<Passage> Write()
    {
        return new List<Passage> 
        {

        ___Heading___("Start"),
        Sammy.EnterFrom(Stage.LeftWing).AndStandAt(Stage.Left),
        Emilie.EnterFrom(Stage.LeftWing).AndStandAt(Stage.Right),
        Sammy.Wear("Dance"),
        Sammy.Pose("Standing"),

        Narrate("Hello!"),
        Sammy.Say("Right back at ya!"),
        Emilie.Say("Who are you talking to?"),
        Sammy.Gain(4, "Paranoia"),
        Sammy.Choose(
            Suggest.Option("Didn't you hear that?")
                .WithConsequence(Read("Emilie Nope"))
                .If(Sammy["Paranoia"].IsLessThan(4)),

            Suggest.Option("Err, nothing - Look, I'm naked!")
                .WithConsequence(Read("Sammy Strip"))
        ),

        ___Heading___("Emilie Nope"),
        Emilie.Say("No?"),
        Sammy.Say("Cool, cool. Gotta go!"),
        Sammy.ExitTo(Stage.LeftWing),
        Happen(Function0),
        Emilie.ExitTo(Stage.RightWing),
        Read("End"),

        ___Heading___("Sammy Strip"),
        TravelTo("Club"),
        Sammy.Wear("Nude"),
        Emilie.Emote("Confused"),
        Emilie.Gain(2, "Confusion"),
        If(Emilie["Confusion"].IsGreaterThan(3))
            .Then(Read<Chapter2>()),
        Emilie.Say("Real nice, Sammy."),
        Emilie.ExitTo(Stage.RightWing),
        Sammy.Say("We're not done here, Emilie!"),
        Sammy.ExitTo(Stage.RightWing),

        ___Heading___("End"),
        Read<Chapter1>()

        };
    }
}

//main issues remaining
//filtering
//  NarratePassage/MessagePassage for narration + contains the global filter (should use in options as well)
//      Should choice belong to a character? (So that Character filter can be used)
//  SayPassage uses character filter, then global filter (no longer gets prefiltered string queries)
//  Alias commands for global + character aliasing
//  alias commands can be organized into a "prologue" chapter for cleanliness
//  In narrative editor, pronouns and alias handled by typing in normal text, then it automatically 
//      replaces names and pronouns with appropriate aliases based on user selection from dropdown 
//      list of characters. User can choose an arbitrary unique name to use to refer to each character. 
//      E.g. "Tom went to the store. He was hungry" => "%TheChosenOne.Name% went to the store. %TheChosenOne.Pronoun% was hungry"
//      ...What if we used reflection to get the value requested?
//  Call it "aliasing" instead? "nickname"? etc. 
//  "Translators" take an input string and then map it to another string. Maybe start with biggest key and then keep running over until no original matching text remains.
//    There is a localization Translator which takes entire lines of text from, e.g. a Say() command and turns it into the appropriate language. 
//    After that, there is a story or chapter Translator that can be used to customize text in the game,
//    And after that, there are character translators that can further customize text based on who says it. (E.g. nicknames). 
//    When text is processed, it simply goes through a Translator stack. What that stack looks like is arbitrary so we don't need to lose sleep over it at this stage in the design.
//      Could even allow user defined extra Translators
//Sprite layers?
//Choose state animation based on which state it is (costume/emote/pose)
//Chapters.ReadCount?
//And/or conditions

//string => Func<String> additional filter dictionary (accessible programmatically, not in editor)
//localization
//saving
//custom stage positions
//cheats
//  changing properties
//  back button
//versioning
//How to handle dynamic chapters?
//How to handle parallel chapters?
//Temporary property changes support?
//Chapter entry/exit code?
//Save repairing?
//Chapter "stack" (for going in a standalone chapter then coming back out where you went in)
//  Better to do via properties and conditions?
//Caching property deltas on each passage (on execute)?
//  Really, this just means saving game state before/after each passage. 

//need way to make animations run instaneously
//what about saves outside the narrative engine?
//  how to provide expressive saving that automatically supports loading/saving from narrative
//  Save utility that takes a key, an object, and a save format. User can supply own format (requires save and load methods) or use the default json serializer
//  Save is a folder with files in it.
//  Save itself is loaded/saved via a save class that the user can override to add in steps

//Saves would include 1) properties (easy json stuff),
//2) and a chapter + passage index
//in diff format; a series of steps showing property changes for each passage (and directions to find that passage)
//Each version would have an associated data structure that simply describes the structure of the narrative.
//When loading a save from a previous version, you find the matching spot in the old structure, then use use context clues
//  to find the new position in the current version's structure
//  This would be handled by an overrideable class so users can add special save repairing code
//  The default implementation would be to find the nearest passed title and then look up the matching named title in the current version

//The current assumption for story traversal is that there is no other reading state other than your current position.
//  So, we don't track where you are in other chapters, or keep a stack so you can return from a chapter like function. 
//  If this doesn't hold, then the only changes would be that you'd need to store changes to each chapter's current passage in addition to property diffs
//     (and, of course, you'd lose certain guarantees for save repairing and need more complex automatic repairing code)

//Recreating visual state
//  If we had a natural way for developers to indicate when the narrative is in a "clean" state (no information from before this moment can be observed)
//      then this is a simple matter of using the save diffs to start at that point and simulate all the visual nodes you visit to get to the current passage
//  Alternatively, simply use the beginning of a narative as that point. This works if narratives start and stop frequently (to give berth to the non-narrative game)
//      However, if the game is all-narrative, then this could incur unnaceptable loading times. 

//How to support and deal with visual programmability? Animations?
//  Makes loading visual state more difficult
//  Could have an animation class/interface that supports some default animations and can be extended/implemented 
//    Visual elements can only have one animation instance component attached to it, so giving it a new one deletes the old one.
//    Probably nix the above rule about only one animation. Devs may want to have permanent effects, and having them on separate
//      animations makes it really easy to reload visual state. Maybe have string "slots" that the animations occupy to support
//      simple replacement rules.
//  Could call it a "Dance". Also : default dance should support basic stuff that devs can access through plain methods.
//     E.g. something like Sammy.EnterFrom(Stage.LeftWing).AndStandAt(Stage.Left).While(WalkAnimation).For(2.Seconds())
//     or Sammy.Dance(CustomDance).For(1.Seconds()).Every(5.Seconds())
//     or Sammy.Dance(CustomDance).For(3.Seconds()).Continuously()
//     or Sammy.Dance(CustomPositionOverTimeFunction).For(2.Seconds())
//  If animations defined whether an object was supposed to be visible or not, we can constrain the number of objects to load,
//     and if positions are absolute, then we simply ask the last animation where its respective character is supposed to be.