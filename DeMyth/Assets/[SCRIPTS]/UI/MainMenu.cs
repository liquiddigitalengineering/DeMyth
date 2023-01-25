using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private int frameRate = 60;

	[SerializeField] private Animator anim;
	[SerializeField] private Image animationImage;

	void Start(){
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
