using UnityEngine;

public class Flare : MonoBehaviour
{
    [SerializeField] float maxRange;
    [SerializeField] float brightTime;
    [SerializeField] float fadeTime;
    [SerializeField] float decreaseSpeed;
    private Light light;

    private void Start()
    {
        light = GetComponentInChildren<Light>();
    }

    private void Update()
    {
        if (brightTime < 0)
        {
            light.range = Mathf.Lerp(light.range, 0, decreaseSpeed / 100);

            if (fadeTime < 0)
            {
                Destroy(gameObject);
            }

            fadeTime -= Time.deltaTime;
        }

        brightTime -= Time.deltaTime;
    }
}
