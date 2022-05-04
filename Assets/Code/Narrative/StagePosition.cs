using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class StagePosition : MonoBehaviour
{
    public IEnumerable<Character> Characters => 
        GetComponentsInChildren<Character>();

    public Transform First, Last;
    public float AnimationSpeed = 1;

    private void Update()
    {
        int index = 0;
        foreach (Character character in Characters)
        {
            float lerp_factor = 
                index++ / (float)(Mathf.Max(Characters.Count() - 1, 0));
            Vector3 target_position = 
                Vector3.Lerp(First.position, Last.position, lerp_factor);

            if (Characters.Count() == 1)
                target_position = 
                    Vector3.Lerp(First.position, Last.position, 0.5f);

            character.transform.position =
                Vector3.Lerp(character.transform.position, 
                             target_position, 
                             Time.deltaTime * 1.5f * AnimationSpeed);
        }
    }
}
