using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net;

// Screen Recorder will save individual images of active scene in any resolution and of a specific image format
// including raw, jpg, png, and ppm.  Raw and PPM are the fastest image formats for saving.
//
// You can compile these images into a video using ffmpeg:
// ffmpeg -i screen_3840x2160_%d.ppm -y test.avi

public class ImageSave : MonoBehaviour
{
    public UnityEngine.UI.Text debugText1;
    private string debugMessage1 = "path1";
    public UnityEngine.UI.Text debugText2;
    private string debugMessage2 = "myPath";
    public UnityEngine.UI.Text debugText3;
    private string debugMessage3 = "ImageNum";

    //public InputField SpellName;

    // 4k = 3840 x 2160   1080p = 1920 x 1080
    public int captureWidth = 360;
    public int captureHeight = 640;

    // optional game object to hide during screenshots (usually your scene canvas hud)
    public GameObject hideGameObject;

    // optimize for many screenshots will not destroy any objects so future screenshots will be fast
    public bool optimizeForManyScreenshots = true;

    // configure with raw, jpg, png (simple raw format)
    public enum Format { RAW, JPG, PNG };
    public Format format = Format.JPG;

    // folder to write output (defaults to data path)
    public string folder;

    // private vars for screenshot
    private Rect rect;
    private RenderTexture renderTexture;
    private Texture2D screenShot;
    private int counter = 0; // image #

    // 그려진 Trail을 Clear하기 위한 용도
    private GameObject[] Trails;

    enum SpellName { Fire, Water, Storm, Death, SteamPack, Grasping, Heal, Cleanse, Infection, Stun, Silence, Reflection };
    private void Start()
    {
        //debugText1.enabled = false;
    }

    // create a unique filename using a one-up variable
    private string uniqueFilename(int width, int height)
    {
        // Save누를때마다 경로 설정함

        // 안드로이드 경로
        if (Application.platform == RuntimePlatform.Android)
        {
            folder = Application.persistentDataPath;
            folder = folder.Substring(0, folder.LastIndexOf('/'));
            folder = Path.Combine(folder, "screenshots");
            //folder = Path.Combine(folder, SpellName.text);
        }
        // 유니티 경로
        else if (Application.isEditor)
        {
            folder = Application.dataPath;
            var stringPath = folder + "/..";
            folder = Path.GetFullPath(stringPath);
            folder = Path.Combine(folder, "screenshots");
            //folder = Path.Combine(folder, SpellName.text);
        }
        debugMessage2 = string.Format("myPath: {0}", folder);
        Debug.Log(debugMessage2);
        // make sure directoroy exists
        System.IO.Directory.CreateDirectory(folder);

        // count number of files of specified format in folder
        string mask = string.Format("screen_{0}x{1}*.{2}", width, height, format.ToString().ToLower());


        // use width, height, and counter for unique file name
        var filename = string.Format("{0}/screen_{1}x{2}.{3}", folder, width, height, format.ToString().ToLower());

        // return unique filename
        return filename;
    }

    public void Clear()
    {
        Trails = GameObject.FindGameObjectsWithTag("Drawing");
        foreach (GameObject trail in Trails)
        {
            Destroy(trail);
        }
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

        new System.Threading.Thread(() =>
        {
            // create file and write optional header with image bytes

            var f = System.IO.File.Create(filename);
            if (fileHeader != null) f.Write(fileHeader, 0, fileHeader.Length);
            f.Write(fileData, 0, fileData.Length);
            f.Close();
            //debugMessage1 = string.Format("Wrote screenshot {0} of size {1}", filename, fileData.Length);
            //Debug.Log(debugMessage1);
        }).Start();


        debugMessage3 = string.Format("#{0} Image saved", counter);
        Debug.Log(debugMessage3);

        // unhide optional game object if set
        if (hideGameObject != null) hideGameObject.SetActive(true);

        // cleanup if needed
        if (optimizeForManyScreenshots == false)
        {
            Destroy(renderTexture);
            renderTexture = null;
            screenShot = null;
        }

        Clear();

    }


    public void UploadButton()
    {
        var filename = uniqueFilename(360, 640);
        Debug.Log("filename: " + filename);

        StartCoroutine(UploadAndActivate(filename));
    }
    IEnumerator UploadAndActivate(string jpgPath)
    {
        byte[] bytes = File.ReadAllBytes(jpgPath);

        WWWForm form = new WWWForm();
        form.AddBinaryData("file", bytes, "test.jpg", "image/jpeg");

        string URL = "address";

        WWW www = new WWW(URL, form);

        yield return www;
        Debug.Log(www.text);
        string spellID = spell_print(www.text);

        if (SpellActivate(spellID))
        {
            Debug.Log("Spell activate success!!");
        }
        else
        {
            Debug.Log("Spell activate fail...");
        }
    }
    private bool SpellActivate(string spellID)
    {
        int resultSpellID = int.Parse(spellID);
        string resultSpellName = ((SpellName)System.Enum.Parse(typeof(SpellName), spellID)).ToString();

        Equipment equipment = GameObject.Find("Equipment").GetComponent<Equipment>();
        SpellManager spellManager = GameObject.Find("SpellManager").GetComponent<SpellManager>();
        int spell_pos = 0;
        foreach(Spell spell in equipment.spellbook.spells)
        {
            Debug.Log(spell.SpellName);
            if(spell.SpellName == resultSpellName)
            {
                spellManager.Select(spellManager.spells[spell_pos]);
                return true;
            }
            spell_pos++;
        }
        return false;
    }

    private string spell_print(string str)
    {
        if(str == "")
        {
            Debug.Log("str is empty!!");
            return null;
        }
        string spellID = string.Format("{0}", str[1]);
        SpellName spell = (SpellName)System.Enum.Parse(typeof(SpellName), spellID);

        Debug.Log(spell.ToString());

        return spellID;
    }

    void Update()
    {
        if (debugText1 != null)
        {
            UpdateDebug1Text(debugMessage1);
            UpdateDebug2Text(debugMessage2);
            UpdateDebug3Text(debugMessage3);
        }
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
    private void UpdateDebug3Text(string message)
    {
        debugText3.text = message;
        debugText3.fontSize = 32;
    }
}