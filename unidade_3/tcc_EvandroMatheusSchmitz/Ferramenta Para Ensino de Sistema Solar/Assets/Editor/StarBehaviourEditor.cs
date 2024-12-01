using UnityEditor;

[CustomEditor(typeof(StarBehaviour))]
public class StarBehaviourEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //StarBehaviour targetScript = (StarBehaviour)target;
        //targetScript.StarMass = EditorGUILayout.FloatField("Star Mass", targetScript.StarMass);
        EditorGUILayout.HelpBox("The star mass is in solar mass. 1 solar mass is equals the mass of our sun", MessageType.Info);
        //targetScript.TimeSpeed = EditorGUILayout.IntField("Time Speed", targetScript.TimeSpeed);
        EditorGUILayout.HelpBox("Time Speed is the time that takes to 10 billion years pass in the simulation. The time is in minutes. This means 1 equals 1 minute and in this 1 minute 10 billion years will pass", MessageType.Info);
    }

}
