using UnityEngine;
using System.Collections;

public class LoadWhenTapped : MonoBehaviour {

	public GameObject loadingBG;

	public void LoadScene(string level)
	{
		loadingBG.SetActive (true);
		Application.LoadLevel(level);
	}
}
