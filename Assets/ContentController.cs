using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContentController : MonoBehaviour {

    public TextMeshProUGUI tMProPrefab;

    public void UpdateScrollList(List<float> reactionTimes) {
        //DeleteChilds
        while(this.transform.childCount>0){
            DestroyImmediate(this.transform.GetChild(0).gameObject);
        }
        GenerateTextEntry("# - TR");
        int trIndex = 0; 
        foreach (float f in reactionTimes){
            trIndex++;
            GenerateTextEntry(trIndex.ToString()+" - "+f.ToString().Replace(".",",")+" s");
        }

    }
    void GenerateTextEntry(string text){
        TextMeshProUGUI tMPro = Instantiate(tMProPrefab);
        tMPro.text = text;
        tMPro.transform.SetParent(this.transform);
    }

}
