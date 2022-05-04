using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Prop))]
public class PropVisualization : MonoBehaviour
{
    public Image Image;

    public CharacterSprites Sprites;
    public Sprite DefaultSprite;

    public bool IsTransitioning { get; private set; }
    public Image TransitionImage;
    public enum AnimationType { None, Dissolve, Slide }
    public AnimationType Animation;
    public RectMask2D Mask, AntiMask;
    public Transform MaskRestPosition;
    public float AnimationLength = 1;
    float ElapsedSeconds = 0;

    public Prop Prop => GetComponent<Prop>();
    public PropState State => Prop.State;


    private void Update()
    {
        Sprite target_sprite;


        // Select target_sprite

        IEnumerable<PropState> matching_states =
        Sprites.Keys.Where(state_ =>
        {
            foreach (string key in state_.Keys)
                if (State.ContainsKey(key) && State[key] != state_[key])
                    return false;

            return true;
        });

        var scored_states = matching_states.Select(
        state =>
        {
            (PropState State, int MatchCount) pair =
                (state, State.Intersect(state).Count());
            
            return pair;
        })
        .Where(pair => pair.MatchCount > 0);

        if (scored_states.Count() > 0)
        {
            PropState closest_state = scored_states
                                     .MaxElement(pair => pair.MatchCount)
                                     .State;

            target_sprite = Sprites[closest_state];
        }
        else
            target_sprite = DefaultSprite;


        // Animate sprite transition

        if (target_sprite != Image.sprite)
        {
            IsTransitioning = true;

            if (Animation == AnimationType.Dissolve)
            {
                if (TransitionImage.sprite != target_sprite)
                {
                    TransitionImage.sprite = target_sprite;
                    TransitionImage.color = Colors.ClearWhite;
                }
                else if (TransitionImage.color.a < 0.98f)
                {
                    float target_alpha = Mathf.Lerp(TransitionImage.color.a, 1, 4 * Time.deltaTime);

                    TransitionImage.color = TransitionImage.color.AlphaChangedTo(target_alpha);
                }
                else
                {
                    Image.sprite = target_sprite;
                    IsTransitioning = false;
                }
            }
            else if (Animation == AnimationType.Slide)
            {
                if (TransitionImage.sprite != target_sprite)
                {
                    TransitionImage.sprite = target_sprite;
                    Mask.transform.position = MaskRestPosition.position;
                    ElapsedSeconds = 0;
                }
                else if (ElapsedSeconds < AnimationLength)
                {
                    ElapsedSeconds += Time.deltaTime;

                    Mask.transform.position = Vector3.Lerp(MaskRestPosition.position, transform.position,
                                                           Mathf.Min(ElapsedSeconds / AnimationLength, 1));
                    AntiMask.transform.position = Mask.transform.position + 
                                                  new Vector3(0, 2);
                }
                else
                {
                    Mask.transform.position = AntiMask.transform.position = MaskRestPosition.position;

                    Image.sprite = target_sprite;
                    IsTransitioning = false;
                }
            }
            else
            {
                Image.sprite = target_sprite;
                IsTransitioning = false;
            }
        }
        else
            Image.transform.position = transform.position;
        TransitionImage.gameObject.SetActive(IsTransitioning);


        Image.transform.position = transform.position;
        TransitionImage.transform.position = transform.position;

        AntiMask.enabled = Mask.enabled =
            Animation == AnimationType.Slide;
    }
}