using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Level1 : MonoBehaviour
{
    [SerializeField] private LevelInfo levelInfo;
    [SerializeField] private GameObject blackBackground, stripAnimation;
    [SerializeField] private PlayableDirector timeline;


    public void LevelCheck()
    {
        if (levelInfo.LevelCompleted) return;

        blackBackground.SetActive(true);

        double timeLeft = timeline.duration - timeline.time;

        StartCoroutine(PlayStripAnimationCoroutine((float) timeLeft));
    }

    private IEnumerator PlayStripAnimationCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        stripAnimation.SetActive(true);
    }
}
