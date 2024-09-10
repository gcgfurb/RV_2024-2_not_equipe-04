using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // link of the code: http://answers.unity3d.com/questions/369198/how-to-exit-application-in-android-on-back-button.html
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                if (Application.platform == RuntimePlatform.Android)
                {
                    AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
                    activity.Call<bool>("moveTaskToBack", true);
                }
            } else
            {
                SceneManager.LoadSceneAsync("StartMenu");
            }
        }
    }
}
