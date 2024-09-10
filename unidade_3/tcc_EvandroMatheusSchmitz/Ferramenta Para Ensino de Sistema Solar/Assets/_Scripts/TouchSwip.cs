using UnityEngine;
using System.Collections;

public class TouchSwip : MonoBehaviour {

    /*
     * This was just a test class in alfa version, it will be kept for now.
     * Future decisions will have to be made to decide what to do with this class.
     * It purpose was to offer a way to enable/disable the touch ray cast.
     * 
    */ 

    public GameObject hidenMenu;

    private Animator animator;

    private bool isShown = false;

    // Use this for initialization
    void Start () {
        animator = hidenMenu.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount == 3 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(3).phase == TouchPhase.Moved && Input.GetTouch(2).phase == TouchPhase.Moved)
        {
            if (isShown)
            {
                hideMenu();
            } else
            {
                showMenu();
            }

            isShown = !isShown;
        }
	}

    public void showMenu()
    {
        animator.Play("MenuShow");
    }

    public void hideMenu()
    {
        animator.Play("MenuHide");
    }

}
