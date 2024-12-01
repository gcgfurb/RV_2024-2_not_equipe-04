using UnityEngine;
using Vuforia;
using UnityEngine.SceneManagement;

public class DissectionTargetBehaviour : CelestialBodyPaneBehaviour
{

    #region PRIVATE_MEMBER_VARIABLES
    /// <summary>
    /// Flag to see if should activate or deactivate the dissection mode
    /// </summary>
    protected bool dissect = true;
    #endregion 

    #region PUBLIC_VARIABLES

    #endregion

    #region UNTIY_MONOBEHAVIOUR_METHODS

    #endregion // UNTIY_MONOBEHAVIOUR_METHODS



    #region PUBLIC_METHODS

    /// <summary>
    /// Implementation of the ITrackableEventHandler function called when the
    /// tracking state changes.
    /// </summary>
    public override void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            // activate or deactivate the dissection mode
            dissect = !dissect;
            if (dissect)
            {
                // informa the planets to change to planet mode
                PlanetSubject.instance.changeDisplayMode(DisplayMode.PLANET);
            }
            else
            {
                // informa the planets to change to dissection mode
                PlanetSubject.instance.changeDisplayMode(DisplayMode.DISSECTION);
            }

            OnTrackingFound();
        }
        else
        {
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS
}
