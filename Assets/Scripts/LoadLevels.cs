using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevels : MonoBehaviour {

	public void LoadFoursLevel() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("FoursGame");
    }

    public void LoadFivesLevel() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("FivesGame");
    }

    public void LoadTitleScreen() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScreen");
    }
	
}
