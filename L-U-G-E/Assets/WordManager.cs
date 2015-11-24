using UnityEngine;
using UnityEngine.UI;
using System.Collections;


//create array of letters and access randomly for word player
//is to make? 
//how to get these words? read in text file to dictionary? 

public class WordManager : MonoBehaviour {
	
	public string[] matchingWords;
	private string wordFinal = "";
	private int nextLetterIndex = 0; 

	public int lifeCount = 5; //lives 
	private bool isDead = false; 

	private int wordCount = 0; 

	private string wordCurrent = ""; //letters you have already 

	public Text wordCurrentText; 
	public Text countText; 

	void Start(){
		SetCountText (); 
		SetWordText (); 
		wordFinal = matchingWords [wordCount];
	}
	
	private void nextLevel (){
		wordCount ++; 
		wordCurrent = ""; 
		nextLetterIndex = 0; 
		wordFinal = getNextWord(); //HOW TO GET NEXT WORD? 
		SetWordText (); 
	}

	public char getNextLetter(){
		return wordFinal [nextLetterIndex];
	}

	private void SetCountText(){
		countText.text = "Lives Left: " + lifeCount.ToString ();
	}

	private void SetWordText(){
		wordCurrentText.text = "Letters: " + wordCurrent; 
	}
	
	private void OnTriggerEnter(Collider other){
		Debug.Log (other.gameObject.tag);
		if (other.gameObject.tag == "Obstacle") {
			lifeCount--; 
			SetCountText (); 

		} else if (other.gameObject.tag == "Letters") {
			string goName = other.gameObject.name; 
			LetterHandler (goName, other.gameObject); 
		}

	}

	private string getNextWord(){
		return matchingWords [wordCount];
	}

	public string getNewLetters(int numLetters){
		string threeLetters = "";

		bool hasCreatedLetter = false;

		for (int i = 0; i < numLetters; i++) {
			if (!hasCreatedLetter && 1 == Random.Range (0, numLetters)){
				threeLetters += getNextLetter ();
				Debug.Log (getNextLetter());
				hasCreatedLetter = true;
			}
			else
				threeLetters += (char) (Random.Range(65, 90));
		}

		return threeLetters;
	}

	//function that handles colisions with letters 
	private void LetterHandler(string name, GameObject go){

		char goLetter = name [0];

		Debug.Log (goLetter + " and " + wordFinal[nextLetterIndex]);
		//got right letter 
		if(goLetter == wordFinal[nextLetterIndex]) {
			nextLetterIndex++; 
			wordCurrent = wordCurrent + goLetter;
			Destroy (go);
			SetWordText(); 
				
			//done with word
			if (wordCurrent == wordFinal) {
				nextLevel();
			}
		}
		//wrong letter 
		//else {
			//lifeCount--;
			//SetCountText(); 
		//}
	}
}
