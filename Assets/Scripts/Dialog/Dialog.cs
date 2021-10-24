using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog_X", menuName = "Dialog", order = 0)]
public class Dialog : ScriptableObject {    
    [SerializeField] DialogTypes _DialogType;
    [SerializeField] DialogEntry[] DialogEntries;

    public enum DialogTypes {
        Regular
    }

    [System.Serializable]
    private class DialogEntry {
    [SerializeField] string DialogSpeaker;
    [TextArea(5,10)]
    [SerializeField] string DialogText;
    }
}
