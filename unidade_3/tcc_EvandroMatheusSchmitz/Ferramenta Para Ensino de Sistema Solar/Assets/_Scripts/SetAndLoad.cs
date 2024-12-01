using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetAndLoad : MonoBehaviour {

    public Image loadingBar;
    public Text loadingText;
    public GameObject panelMenu;
    public GameObject panelLoading;

    private AsyncOperation loading;

	// Use this for initialization
	void Start () {
        loading = null;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void setHandAndLoad(int userHand)
    {
        GameControler.instance.UserHand = (Hand)userHand;

        LoadScene("SolarSimulation");
    }

    public void LoadScene(string name)
    {
        panelMenu.SetActive(false);
        panelLoading.SetActive(true);

        StartCoroutine(ShowProgress(name));
    }

    public void openURL(string url)
    {
        Application.OpenURL(url);
    }

    IEnumerator ShowProgress(string name)
    {
        // waits for 1 second (time to activate or deactivete the panels)
        yield return new WaitForSeconds(1);

        loading = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);

        while (!loading.isDone)
        {

            loadingBar.fillAmount = loading.progress;
            loadingText.text = (int)(loading.progress * 100) + " %";

            // keeps the while going until the end
            // it allows it to continue in the next frame
            yield return null;
        }
       
    }
}
