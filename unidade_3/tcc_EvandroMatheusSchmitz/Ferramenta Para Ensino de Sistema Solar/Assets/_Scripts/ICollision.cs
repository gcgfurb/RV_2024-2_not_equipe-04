using UnityEngine;
using System.Collections;

public interface ICollision {

    /// <summary>
    /// Something that happens when a collision is found
    /// </summary>
    void OnCollisionFound();

    /// <summary>
    /// Something that the happens when a collision is lost
    /// </summary>
    void OnCollisionLost();
}
