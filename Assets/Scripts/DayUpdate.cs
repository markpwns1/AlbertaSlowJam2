using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayUpdate : MonoBehaviour
{
    public TMP_Text dayText;
    public float slideDuration = 5f;
    public float slideDelay = 1f;
    public int slideDecrement = 80;
    public string nextSceneName;

    // Start is called before the first frame update
    void Start()
    {
        // Translate the day text to whatever the current day is
        dayText.rectTransform.anchoredPosition += new Vector2(0, -(SharedData.gameDay - 1)*slideDecrement);

        StartCoroutine(GoToNextDay());
    }

    private IEnumerator GoToNextDay() {
        yield return StartCoroutine(AnimateDayChange());
        SceneManager.LoadScene(nextSceneName);
        SharedData.gameDay += 1;
    }

    private IEnumerator AnimateDayChange() {
        yield return new WaitForSeconds(slideDelay);

        float elapsedTime = 0f;
        Vector2 startPosition = dayText.rectTransform.anchoredPosition;
        Vector2 endPosition = startPosition + new Vector2(0, -slideDecrement);

        while (elapsedTime < slideDuration) {
            dayText.rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, elapsedTime / slideDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(slideDelay);
    }
}
