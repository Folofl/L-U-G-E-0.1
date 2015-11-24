using UnityEngine;
using System.Collections;

public class ChunkManager : MonoBehaviour {

	public Transform[] letters;
	public Transform lettersParent;

	public void setLetters(Transform[] newLetters){
		foreach (Transform t in letters)
			Destroy (t.gameObject);

		letters = newLetters;

		foreach (Transform t in letters)
			t.parent = lettersParent;

	}

	public Transform[] getLetters(){
		return letters;
		
	}


}
