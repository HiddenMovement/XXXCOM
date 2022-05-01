using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//****Do we really want to control position this way? More flexible to let characters control own position
//****naming
public class StagePosition : MonoBehaviour
{
    public IEnumerable<Character> Characters => 
        GetComponentsInChildren<Character>();

    //****abstract this first/last scheme (also used in ChoiceUI)
    //****Should come with method to query lerped value based on index + population
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
