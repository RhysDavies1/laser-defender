using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	public Text scoreDisplay;

	void Start () {

		scoreDisplay = GetComponent<Text> ();

		scoreDisplay.text = ScoreKeeper.score.ToString ();

		ScoreKeeper.Reset ();

	}


}
