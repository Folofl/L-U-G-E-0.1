using UnityEngine;
using System.Collections;

public class ChunkInstancer : MonoBehaviour {

	const float DIST_FROM_PLAYER_INST = 9f;
	const float CHUNK_OFFSET = 9.9f;
	const float PLAYER_OFFSET = 2.5f;
	public Transform chunkPrefab;
	public Transform previousChunk;
	private int previousPos = -3;
	public Transform[] letterPrefabs;
	public Material[] letterMats;

	private int CHAR_OFFSET = 65;

	private int chunkCounter = 0;

	void OnTriggerEnter(Collider other){
		if (other.tag == "InstanceTrigger") {
			instanceNewChunk();
		}
	}


	private void instanceNewChunk(){
		int roundedPosition = (int) Mathf.Round (transform.position.y + PLAYER_OFFSET);

		//if (roundedPosition != previousPos && roundedPosition % DIST_FROM_PLAYER_INST == 0) {
		previousPos = roundedPosition;
		Vector3 newPosition = previousChunk.position;
		newPosition.y += CHUNK_OFFSET;
		
		Transform newChunk = Instantiate(chunkPrefab, newPosition, Quaternion.identity) as Transform;
		newChunk.name = "Chunk " + chunkCounter++;
		
		string newLetters = GetComponent<WordManager>().getNewLetters(3);
		Debug.Log (newLetters);
		ChunkManager chunkManager = newChunk.GetComponent<ChunkManager>();
		Transform[] newLetterObjs = getObjectsFromLetters(newLetters, chunkManager.getLetters());
		
		previousChunk = newChunk;
		chunkManager.setLetters(newLetterObjs);
	//	}

	}

	/*
	void Update () {

		int roundedPosition = (int) Mathf.Round (transform.position.y + PLAYER_OFFSET);
		if (roundedPosition != previousPos && roundedPosition % DIST_FROM_PLAYER_INST == 0) {
			previousPos = roundedPosition;
			Vector3 newPosition = previousChunk.position;
			newPosition.y += CHUNK_OFFSET;

			Transform newChunk = Instantiate(chunkPrefab, newPosition, Quaternion.identity) as Transform;
			newChunk.name = "Chunk " + chunkCounter++;

			string newLetters = GetComponent<WordManager>().getNewLetters(3);
			Debug.Log (newLetters);
			ChunkManager chunkManager = newChunk.GetComponent<ChunkManager>();
			Transform[] newLetterObjs = getObjectsFromLetters(newLetters, chunkManager.getLetters());

			previousChunk = newChunk;
			chunkManager.setLetters(newLetterObjs);
		}
	}
*/
	private Transform[] getObjectsFromLetters(string newLetters, Transform[] oldLetters){

		Transform[] newLetterObs = new Transform[newLetters.Length];

		for (int i = 0; i < newLetters.Length; i++) {
			int index = ((char) newLetters[i]) - CHAR_OFFSET;

			newLetterObs[i] = Instantiate(letterPrefabs[index], oldLetters[i].position, oldLetters[i].rotation) as Transform;
			newLetterObs[i].GetComponent<Renderer>().sharedMaterial = letterMats[Random.Range(0, letterMats.Length)];
		}
		return newLetterObs;
	}
}
