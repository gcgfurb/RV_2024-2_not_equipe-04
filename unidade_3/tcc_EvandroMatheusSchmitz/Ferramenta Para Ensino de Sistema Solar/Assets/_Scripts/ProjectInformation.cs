using UnityEngine;
using System;


[Serializable]
public class ProjectInformation {

    /// <summary>
    /// An array of InformationData.
    /// </summary>
    public InformationData[] informations;

    /// <summary>
    /// Empty/Default Constructor.
    /// </summary>
    public ProjectInformation()
    { }

    /// <summary>
    /// Gets the data stored in a JSon and converts in ProjectInformation.
    /// </summary>
    /// <returns> ProjectInformation if there is information.</returns>
    public static ProjectInformation CreateFromJson()
    {
        TextAsset asset = Resources.Load("ProjectData") as TextAsset;
        return JsonUtility.FromJson<ProjectInformation>(asset.text);
    }
}

[Serializable]
public class InformationData
{
    /// <summary>
    /// Key representing about who is the information. It is used as the key of dictionary also.
    /// </summary>
    public Information key;
    /// <summary>
    /// The tittle of the information.
    /// </summary>
    public string title;
    /// <summary>
    /// The data of the information.
    /// </summary>
    public string data;
}
