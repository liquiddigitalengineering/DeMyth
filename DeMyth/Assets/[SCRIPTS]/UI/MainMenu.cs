using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Ink.Parsed;
using System.Collections.Generic;
using UnityEngine.Playables;

public class MainMenu : MonoBehaviour
{
	public static int LastPlayedLevelID { get; private set; }

	[SerializeField] private int frameRate = 60;

	[SerializeField] private Animator anim;
	[SerializeField] private Image animationImage;
	[SerializeField] private PersistantSaving saving;
	[SerializeField] private PlayableDirector buttonTimeline;
	[SerializeField] private GameObject continueButton;
	[Space(10)]
	[SerializeField] private List<LevelInfo> levels;

    private void Awake()
    {
		CheckContinueButton();
    }

    void Start()
	{
		saving.LoadData();
		Application.targetFrameRate = frameRate;
	}

	public void SceneLoad(string SceneToLoad)
	{
		animationImage.enabled = true;
		StartCoroutine(LoadScene(SceneToLoad));
	}

	public void ShowLevels()
	{
		buttonTimeline.Play();
	}
	
	public void Click(RectTransform rectTransform)
	{
		StartCoroutine(ClickCoroutine(rectTransform));
    }

	public void QuitGame() => Application.Quit();

    #region private methods
    private int LastPlayedLevel()
	{
		int lastPlayedLevel = 0;

		foreach (var level in levels) {
			if (!level.LevelCompleted) continue;
			lastPlayedLevel = level.LevelIndex;
		}

		return lastPlayedLevel;
	}

    private IEnumerator ClickCoroutine(RectTransform rectTransform)
    {
        rectTransform.localScale = new Vector3(transform.localScale.x + 2, transform.localScale.y + 2, transform.localScale.z);
        yield return new WaitForSeconds(0.2f);
        rectTransform.localScale = new Vector3(2, 2, transform.localScale.z);
    }

    private IEnumerator LoadScene(string SceneToLoad)
    {
        anim.SetTrigger("LoadOut");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneToLoad);
    }


	private void CheckContinueButton() 
	{
        if (LastPlayedLevel() <= 0) return;

        continueButton.SetActive(true);
		LastPlayedLevelID = LastPlayedLevel();
    }
    #endregion
}
