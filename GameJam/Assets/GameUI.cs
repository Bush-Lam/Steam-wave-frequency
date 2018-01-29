using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {

	public Image fadePlane;
	public GameObject gameOverUI;
	public Text gameOverScoreUI;
	public Text scoreUI;
	//Player player;

	void Start(){
        //player = FindObjectOfType<Player> ();
        //player.OnDeath += OnGameOver;
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Cursor.visible = true;
            StartCoroutine(Fade(Color.clear, new Color(0, 0, 0, .9f), 1));

            scoreUI.text = "Score: " + PlayerPrefs.GetInt("Score").ToString();
        }
    }

    void OnGameOver()
    {
    }

	void Update(){
		//scoreUI.text = ScoreKeeper.score.ToString ("D6");
	}

	IEnumerator Fade(Color from, Color to, float time){
		float speed =1/time;
		float percent = 0;

		while (percent<1) {
			percent += Time.deltaTime * speed;
			fadePlane.color = Color.Lerp (from, to, percent);
			yield return null;
		}
	}

	void resetScore(){
		PlayerPrefs.SetInt ("Score", 000000);
	}
	//UI input
	public void StartNewGame(){
		resetScore ();
		SceneManager.LoadScene ("Main");
	}

	public void ReturnToMainMenu(){
        resetScore();
        SceneManager.LoadScene ("Menu");

	}
	public void GameOver(){
		SceneManager.LoadScene ("GameOver");
	}
}