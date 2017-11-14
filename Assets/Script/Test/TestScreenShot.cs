using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;

// Screen Recorder will save individual images of active scene in any resolution and of a specific image format
// including raw, jpg, png, and ppm.  Raw and PPM are the fastest image formats for saving.
//
// You can compile these images into a video using ffmpeg:
// ffmpeg -i screen_3840x2160_%d.ppm -y test.avi

public class TestScreenShot : MonoBehaviour
{
    string address = "165.246.42.24/upload.php";

    public UnityEngine.UI.Text debugText1;
    private string debugMessage1 = "filename";
    public UnityEngine.UI.Text debugText2;
    private string debugMessage2 = "folder";
    //public UnityEngine.UI.Text debugText3;
    //private string debugMessage3 = "text";

    // 4k = 3840 x 2160   1080p = 1920 x 1080
    public int captureWidth = 360;
    public int captureHeight = 640;

    // optional game object to hide during screenshots (usually your scene canvas hud)
    public GameObject hideGameObject;

    // optimize for many screenshots will not destroy any objects so future screenshots will be fast
    public bool optimizeForManyScreenshots = true;

    // configure with raw, jpg, png (simple raw format)
    public enum Format { RAW, JPG, PNG};
    public Format format = Format.JPG;

    // folder to write output (defaults to data path)
    public string folder;

    // private vars for screenshot
    private Rect rect;
    private RenderTexture renderTexture;
    private Texture2D screenShot;
    private int counter = 0; // image #

    // commands
    private bool captureScreenshot = false;

    // create a unique filename using a one-up variable
    private string uniqueFilename(int width, int height)
    {
        // if folder not specified by now use a good default
        if (folder == null || folder.Length == 0)
        {
            folder = Application.dataPath;
            debugMessage2 = string.Format("Application dataPath: {0}", folder);
            Debug.Log(debugMessage2);
            if (Application.isEditor)
            {
                // put screenshots in folder above asset path so unity doesn't index the files
                var stringPath = folder + "/..";
                folder = Path.GetFullPath(stringPath);
            }
            folder += "/screenshots";

            // make sure directoroy exists
            System.IO.Directory.CreateDirectory(folder);

            // count number of files of specified format in folder
            string mask = string.Format("screen_{0}x{1}*.{2}", width, height, format.ToString().ToLower());
            counter = Directory.GetFiles(folder, mask, SearchOption.TopDirectoryOnly).Length;
        }

        // use width, height, and counter for unique file name
        var filename = string.Format("{0}/screen_{1}x{2}_{3}.{4}", folder, width, height, counter, format.ToString().ToLower());

        // up counter for next call
        ++counter;

        // return unique filename
        return filename;
    }

    public void Save()
    {
        // create screenshot objects if needed
        if (renderTexture == null)
        {
            // creates off-screen render texture that can rendered into
            rect = new Rect(0, 0, captureWidth, captureHeight);
            renderTexture = new RenderTexture(captureWidth, captureHeight, 24);
            screenShot = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);
        }

        // get main camera and manually render scene into rt
        Camera camera = this.GetComponent<Camera>(); // NOTE: added because there was no reference to camera in original script; must add this script to Camera
        camera.targetTexture = renderTexture;
        camera.Render();

        // read pixels will read from the currently active render texture so make our offscreen 
        // render texture active and then read the pixels
        RenderTexture.active = renderTexture;
        screenShot.ReadPixels(rect, 0, 0);

        // reset active camera texture and render texture
        camera.targetTexture = null;
        RenderTexture.active = null;

        // get our unique filename
        string filename = uniqueFilename((int)rect.width, (int)rect.height);

        // pull in our file header/data bytes for the specified image format (has to be done from main thread)
        byte[] fileHeader = null;
        byte[] fileData = null;
        if (format == Format.RAW)
        {
            fileData = screenShot.GetRawTextureData();
        }
        else if (format == Format.PNG)
        {
            fileData = screenShot.EncodeToPNG();
        }
        else if (format == Format.JPG)
        {
            fileData = screenShot.EncodeToJPG();
        }

        // create new thread to save the image to file (only operation that can be done in background)
        /*
        new System.Threading.Thread(() =>
        {
            // create file and write optional header with image bytes
            
            var f = System.IO.File.Create(filename);
            if (fileHeader != null) f.Write(fileHeader, 0, fileHeader.Length);
            f.Write(fileData, 0, fileData.Length);
            f.Close();
       
            debugMessage1 = string.Format("Wrote screenshot {0} of size {1}", filename, fileData.Length);
            Debug.Log(debugMessage1);
        }).Start();
        */

        
        WWWForm form = new WWWForm();
        form.AddBinaryData("data", fileData, "test.jpg", "image/jpg");
        StartCoroutine(Call(address, form));
        
        debugMessage1 = string.Format("Send {0}", filename);
        Debug.Log(debugMessage1);

        // unhide optional game object if set
        if (hideGameObject != null) hideGameObject.SetActive(true);

        // cleanup if needed
        if (optimizeForManyScreenshots == false)
        {
            Destroy(renderTexture);
            renderTexture = null;
            screenShot = null;
        }

    }

    void Update()
    {
        UpdateDebug1Text(debugMessage1);
        UpdateDebug2Text(debugMessage2);
        //UpdateDebugText(debugText3, debugMessage3);
    }

    private void UpdateDebug1Text(string message)
    {
        debugText1.text = message;
        debugText1.fontSize = 32;
    }
    private void UpdateDebug2Text(string message)
    {
        debugText2.text = message;
        debugText2.fontSize = 32;
    }


    public IEnumerator Call(string _address, WWWForm form)
    {
        WWW wwwUrl = POST(_address, form);
        yield return wwwUrl;
        Debug.Log(wwwUrl.text);
    }

    public WWW GET(string url)
    {
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www));
        return www;
    }

    public WWW POST(string url, WWWForm form)
    {   WWW www = new WWW(url, form);
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