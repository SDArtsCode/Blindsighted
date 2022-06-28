using TMPro;
using System.Collections;
using UnityEngine;

public class TextDisplayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textDisplay;
    [SerializeField] string[] text;
    [SerializeField] float textDelay = 0.15f;

    [SerializeField] float startDelay = 0.3f;
    [SerializeField] float endDelay = 0.3f;
    float currentTime = 0;
    bool started;

    [SerializeField] int loopIndex;

    private void Update()
    {
        if(startDelay < currentTime && !started)
        {
            StartCoroutine(DisplayText(text[loopIndex]));
            started = true;
        }

        currentTime += Time.deltaTime;
    }


    IEnumerator DisplayText(string text)
    {
        if(text.Length == 0)
        {
            yield return new WaitForSeconds(endDelay);

            LevelLoader.instance.LoadLevel(0);

            yield break;
        }

        yield return new WaitForSeconds(textDelay);

        textDisplay.text += text[0];

        StartCoroutine(DisplayText(text.Remove(0,1)));
    }
}
