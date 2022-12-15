using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class SpawnColors : MonoBehaviour
{
    private List<Tuple<Vector3, Color>> refs = new List<Tuple<Vector3, Color>>();
    public GameObject Target;
    float t = 0;
    IEnumerator x;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GoogleSheets());
    }
    void Update()
    {
        t = t + 1;
        if(t > 3 && i < refs.Count)
        {
            GameObject copy = Instantiate(Target, transform.position, Quaternion.identity);
            copy.transform.position = refs[i].Item1;
            copy.GetComponent<Renderer>().material.color = refs[i].Item2;
            t = 0;
            i = i + 1;
        }
    }
    
    IEnumerator GoogleSheets()
    {
        UnityWebRequest curentResp = UnityWebRequest.Get("https://sheets.googleapis.com/v4/spreadsheets/1qCW0uFYr4Vu9YVYh33QbL71JmunAy46Jg2_D-5DvLfk/values/Лист1?key=AIzaSyBIqlBFgHVbqCYXMHJewYWYq8MzALerje0");
        yield return curentResp.SendWebRequest();
        string rawResp = curentResp.downloadHandler.text;
        var rawJson = JSON.Parse(rawResp);
        foreach (var itemRawJson in rawJson["values"])
        {
            var parseJson = JSON.Parse(itemRawJson.ToString());
            var selectRow = parseJson[0].AsStringList;
            var vec = new Vector3(float.Parse(selectRow[0]),0.5f,float.Parse(selectRow[1]));
            var col = new Color(int.Parse(selectRow[2])/255.0f,int.Parse(selectRow[3])/255.0f,int.Parse(selectRow[4])/255.0f);
            this.refs.Add(Tuple.Create(vec,col));
    
        }
    }
}
