using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

public class Request : MonoBehaviour {
    [Header("TitlePanel")]
    public InputField IdField;
    
    string address = "165.246.42.24/upload.php";


    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void btnClick()
    {

        StartCoroutine(Call(address));
    }

    public IEnumerator Call(string _address)
    {
        Debug.Log(IdField.text);

        //WWWForm cForm = new WWWForm();
        //cForm.AddField("id", IdField.text);
        //WWW wwwUrl = new WWW(_address, cForm);
        Dictionary<string, string> dict = new Dictionary<string, string>();
        dict.Add("id", IdField.text);
        WWW wwwUrl = POST(_address, dict );
        yield return wwwUrl;
        Debug.Log(wwwUrl.text);
    }

    public WWW GET(string url)
    {
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www));
        return www;
    }

    public WWW POST(string url, Dictionary<string, string> post)
    {
        WWWForm form = new WWWForm();

        foreach(KeyValuePair<string, string> post_arg in post)
        {
            form.AddField(post_arg.Key, post_arg.Value);
        }

        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequest(www));
        return www;
    }

    private IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
}
