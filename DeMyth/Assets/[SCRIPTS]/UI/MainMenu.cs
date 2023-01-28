using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private int frameRate = 60;

	[SerializeField] private Animator anim;
	[SerializeField] private Image animationImage;
	[SerializeField] private PersistantSaving saving;


	void Start(){
		saving.LoadData();
		Application.targetFrameRate = frameRate;
	}

	public void SceneLoad(string SceneToLoad)
	{
		animationImage.enabled = true;
		StartCoroutine(LoadScene(SceneToLoad));
	}

	private IEnumerator LoadScene(string SceneToLoad)
	{
		anim.SetTrigger("LoadOut");
		yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneToLoad);
    }


	public void QuitGame() => Application.Quit();
}
