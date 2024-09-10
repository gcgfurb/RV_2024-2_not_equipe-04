using UnityEngine;
using System.Collections;

public interface IChildTrackBehaviour {

    /// <summary>
    /// Something that the child must do when it is found
    /// </summary>
    void OnFind();

    /// <summary>
    /// Something that the child must do when it is lost
    /// </summary>
    void OnLost();
}
