using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;
using Vuforia;
using System;

public class CelestialBodyPaneBehaviour : MonoBehaviour, ITrackableEventHandler, IObserver
{

    #region PRIVATE_MEMBER_VARIABLES
    /// <summary>
    /// Boolean to see if the marker was found or lost:
    ///  - True equals found;
    ///  - False equals lost.
    /// </summary>
    protected bool render = false;

    /// <summary>
    /// Vuforia variable, kept because it was this way in the DefaultTrackableBehaviour.
    /// </summary>
    private TrackableBehaviour mTrackableBehaviour;

    /// <summary>
    /// The current display mode.
    /// </summary>
    [SerializeField] private DisplayMode currentDisplay;
    /// <summary>
    /// The current display mode.
    /// If there is change and the target was found it disable the previous display and enable the new one.
    /// (get, set)
    /// </summary>
    public DisplayMode CurrentDisplay
    {
        get { return currentDisplay; }

        set {
            if (changeDisplay) {
                if (render)
                {
                    EnableComponents(currentDisplay.ToString(), false);
                    EnableComponents(value.ToString(), true);
                }
                currentDisplay = value;
            }
        }
    }
    #endregion 

    #region PUBLIC_VARIABLES
    /// <summary>
    /// Boolean to say if the miscellaneus should be drawn or not.
    /// If the is no miscellaneus use false.
    /// </summary>
    public bool drawMiscellaneous = true;

    /// <summary>
    /// Boolean that says if there should be a change in the display mode or not
    /// </summary>
    public bool changeDisplay = true;

    /// <summary>
    /// Display modes that should be considered.
    /// If a displayMode is not here, this script will ignore any changes to that displayMode.
    /// </summary>
    public DisplayMode[] displayMatter = new DisplayMode[2];
    #endregion

    #region UNTIY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        // Vuforia code, kept because it was this way in the DefaultTrackableBehaviour.
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
        
        // add this planet as an observer of PlanetSubject
        PlanetSubject.instance.addObserver(this);
    }

    #endregion // UNTIY_MONOBEHAVIOUR_METHODS



    #region PUBLIC_METHODS

    /// <summary>
    /// Implementation of the ITrackableEventHandler function called when the
    /// tracking state changes.
    /// </summary>
    public virtual void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else
        {
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS



    #region PRIVATE_METHODS
    /// <summary>
    /// Enable or disable this game object collider. Noticy that this method does not affect the enable/disable of the childs colliders
    /// </summary>
    /// <param name="state">Bool parameter. If true the collider, if there is one, is enable where this target is found. If it is false it disables the collider, if there is one</param>
    protected void ChangeThisCollider(bool state)
    {
        Collider collider = gameObject.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = state;
        }
    }

    /// <summary>
    /// Method called when the target was found.
    /// It sufered some alterations from the version that existed on the DefaultTrackableBehaviour
    /// </summary>
    protected void OnTrackingFound()
    {
        // draw the miscellaneus, it true. The miscellaneus is the only thing that is almost always drawn
        if (drawMiscellaneous)
        {
            EnableComponents("Miscellaneous", true);
        }

        // draws the componets that should be currently displayed
        EnableComponents(currentDisplay.ToString(), true);

        // enable this game object collider
        ChangeThisCollider(true);

        // the target was found
        render = true;

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
    }

    /// <summary>
    /// Method called when the target was lost.
    /// It sufered some alterations from the version that existed on the DefaultTrackableBehaviour
    /// </summary>
    protected void OnTrackingLost()
    {
        // get child componets
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);
        Light[] lightComponents = GetComponentsInChildren<Light>(true);
        IChildTrackBehaviour[] childTrackComponents = GetComponentsInChildren<IChildTrackBehaviour>(true);

        // Disable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = false;
        }

        // Disable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = false;
        }

        // Disable light:
        foreach (Light component in lightComponents)
        {
            component.enabled = false;
        }

        // Disable some things of the child as texts or other things
        foreach (IChildTrackBehaviour component in childTrackComponents)
        {
            component.OnLost();
        }
        
        // diseble collider
        ChangeThisCollider(false);

        // the target was lost
        render = false;

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
    }

    /// <summary>
    /// Method used to enable or disable the children of a game object with a tag
    /// </summary>
    /// <param name="tag">String the tag of the game object that the children should be enable/disable</param>
    /// <param name="enable">Bool variable. If true enable the children. If false disable them</param>
    private void EnableComponents(string tag, bool enable)
    {
        // variable to represente the game object
        GameObject gameObjectWithTag = null;

        // search for the game object with the tag informed
        foreach(Transform child in transform)
        {
            if (child.tag == tag)
            {
                // if it was found break, because it is just the first game object
                gameObjectWithTag = child.gameObject;
                break;
            }
        }

        // if there was a game object with the tag
        if (gameObjectWithTag != null)
        {
            // get the game object's children's componets
            Renderer[] rendererComponents = gameObjectWithTag.GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = gameObjectWithTag.GetComponentsInChildren<Collider>(true);
            Light[] lightComponents = gameObjectWithTag.GetComponentsInChildren<Light>(true);
            IChildTrackBehaviour[] childTrackComponents = gameObjectWithTag.GetComponentsInChildren<IChildTrackBehaviour>(true);

            // Enable/Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = enable;
            }

            // Enable/Disable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = enable;
            }

            // Enable/Disable light
            foreach (Light component in lightComponents)
            {
                component.enabled = enable;
            }

            // Enable/Disable some things on the children as texts
            foreach (IChildTrackBehaviour component in childTrackComponents)
            {
                if (enable)
                {
                    component.OnFind();
                } else
                {
                    component.OnLost();
                }
            }
        }
    }
    #endregion // PRIVATE_METHODS

    #region OBSERVER_METHODS
    public virtual void update(UpdateData update)
    {
        // switch between the diferent displays types
        switch (update.UpdateType)
        {
            case Action.DISSECTION:
                // if it was a dissection action and the the display should change and the displayMatters contains that display type then change
                if (displayMatter != null && displayMatter.Contains((DisplayMode)update.Data))
                {
                    CurrentDisplay = (DisplayMode)update.Data;
                }
                break;
        }
    }
    #endregion


}
