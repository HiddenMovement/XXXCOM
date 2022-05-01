using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class Toast : MonoBehaviour
{
    float ElapsedSeconds = 0;

    public Text Text;
    public Color Color;

    public float LifespanInSeconds = 5;
    public float MarginInSeconds = 1.0f;//****

    public float DriftSpeed = 0.01f;

    public CanvasGroup CanvasGroup => GetComponent<CanvasGroup>();

    private void Start()
    {
        CanvasGroup.alpha = 0;
    }

    private void Update()
    {
        Text.color = Color.Lerped(Color.white, 0.5f);


        float drift_distance = DriftSpeed * 
                               The.UI.Rect.height * 
                               Time.deltaTime;
        transform.position += new Vector3(0, drift_distance);


        if (MarginInSeconds > LifespanInSeconds / 2)
            MarginInSeconds = LifespanInSeconds / 2;

        ElapsedSeconds += Time.deltaTime;


        if (ElapsedSeconds >= LifespanInSeconds)
            Destroy(gameObject);

        else if (ElapsedSeconds < MarginInSeconds)
            CanvasGroup.alpha = ElapsedSeconds / MarginInSeconds;

        else if (LifespanInSeconds - ElapsedSeconds < MarginInSeconds)
            CanvasGroup.alpha = (LifespanInSeconds - ElapsedSeconds) / 
                                MarginInSeconds;
    }
}