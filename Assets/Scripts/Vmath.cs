using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System.Runtime.InteropServices;
using UnityEditor;
using System;
using System.Net;
using System.Reflection;
using System.IO;
using System.Text;
#pragma warning disable


public class Vmath : MonoBehaviour // Made by Vincent L, The reason why this is not a static class is because it is used by a Gameobject
{
    #region WindowDll
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    private static extern bool SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
    [DllImport("user32.dll", EntryPoint = "FindWindow")]
    public static extern IntPtr FindWindow(System.String className, System.String windowName);

    public static void SetPositionOfWindow(int x, int y, int resX = 0, int resY = 0)
    {
        SetWindowPos(FindWindow(null, Application.productName), 0, x, y, resX, resY, resX * resY == 0 ? 1 : 0);
    }

#endif
    #endregion

    #region structs 
    /// <summary>
    /// A uint version of Vector3
    /// </summary>
    [System.Serializable]
    public struct Vector3uInt
    {
        public uint x;
        public uint y;
        public uint z;

        public Vector3uInt(uint X, uint Y, uint Z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        /// <summary>
        /// Same as new Vector3uInt(0, 0, 0)
        /// </summary>
        /// <returns></returns>
        public static Vector3uInt zero()
        {
            return new Vector3uInt(0, 0, 0);
        }

        /// <summary>
        /// Same as new Vector3uInt(1, 1, 1)
        /// </summary>
        /// <returns></returns>
        public static Vector3uInt one()
        {
            return new Vector3uInt(1, 1, 1);
        }

        /// <summary>
        /// Returns the distance between a and b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3uInt Distance(Vector3uInt a, Vector3uInt b)
        {
            return new Vector3uInt((uint)Mathf.Abs(a.x - b.x), (uint)Mathf.Abs(a.y - b.y), (uint)Mathf.Abs(a.z - b.z));
        }

        public override string ToString()
        {
            return "(" + x + ", " + y + ", " + z + ")";
        }

        // ----------- Operators -----------

        public bool Equals(Vector3uInt other)
        {
            return Equals(other, this);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) {
                return false;
            }

            var objectToCompareWith = (Vector3uInt)obj;

            return objectToCompareWith.x == x && objectToCompareWith.y == y &&
                   objectToCompareWith.z == z;
        }

        public static bool operator ==(Vector3uInt a, Vector3uInt b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vector3uInt a, Vector3uInt b)
        {
            return !a.Equals(b);
        }

        public static Vector3uInt operator +(Vector3uInt a, Vector3uInt b)
        {
            return new Vector3uInt(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vector3uInt operator +(Vector3uInt a, uint b)
        {
            return new Vector3uInt(a.x + b, a.y + b, a.z + b);
        }

        public static Vector3uInt operator -(Vector3uInt a, Vector3uInt b)
        {
            return new Vector3uInt(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vector3uInt operator -(Vector3uInt a, uint b)
        {
            return new Vector3uInt(a.x - b, a.y - b, a.z - b);
        }

        public static Vector3uInt operator *(Vector3uInt a, Vector3uInt b)
        {
            return new Vector3uInt(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static Vector3uInt operator *(Vector3uInt a, uint b)
        {
            return new Vector3uInt(a.x * b, a.y * b, a.z * b);
        }

        // ----------- Cast -----------

        public static explicit operator Vector3uInt(Vector3 v)
        {
            return new Vector3uInt((uint)v.x, (uint)v.y, (uint)v.z);
        }

        public static explicit operator Vector3(Vector3uInt v)
        {
            return new Vector3(v.x, v.y, v.z);
        }

        public static explicit operator Vector3uInt(Vector3Int v)
        {
            return new Vector3uInt((uint)v.x, (uint)v.y, (uint)v.z);
        }

        public static explicit operator Vector3Int(Vector3uInt v)
        {
            return new Vector3Int((int)v.x, (int)v.y, (int)v.z);
        }

    }

    /// <summary>
    /// A int version of Vector4
    /// </summary>
    [System.Serializable]
    public struct Vector4Int
    {
        public int x;
        public int y;
        public int z;
        public int w;

        public Vector4Int(int X, int Y, int Z, int W)
        {
            x = X;
            y = Y;
            z = Z;
            w = W;
        }

        /// <summary>
        /// Same as new Vector4Int(0, 0, 0, 0)
        /// </summary>
        /// <returns></returns>
        public static Vector4Int zero()
        {
            return new Vector4Int(0, 0, 0, 0);
        }

        /// <summary>
        /// Same as new Vector4Int(1, 1, 1, 1)
        /// </summary>
        /// <returns></returns>
        public static Vector4Int one()
        {
            return new Vector4Int(1, 1, 1, 1);
        }

        /// <summary>
        /// Returns the distance between a and b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector4Int Distance(Vector4Int a, Vector4Int b)
        {
            return new Vector4Int((int)Mathf.Abs(a.x - b.x), (int)Mathf.Abs(a.y - b.y), (int)Mathf.Abs(a.z - b.z), (int)Mathf.Abs(a.w - b.w));
        }

        // ----------- Operators -----------

        public bool Equals(Vector4Int other)
        {
            return Equals(other, this);
        }

        public override string ToString()
        {
            return "(" + x + ", " + y + ", " + z + ", " + w + ")";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) {
                return false;
            }

            var objectToCompareWith = (Vector4Int)obj;

            return objectToCompareWith.x == x && objectToCompareWith.y == y &&
                   objectToCompareWith.z == z;
        }

        public static bool operator ==(Vector4Int a, Vector4Int b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vector4Int a, Vector4Int b)
        {
            return !a.Equals(b);
        }

        public static Vector4Int operator +(Vector4Int a, Vector4Int b)
        {
            return new Vector4Int(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }

        public static Vector4Int operator +(Vector4Int a, int b)
        {
            return new Vector4Int(a.x + b, a.y + b, a.z + b, a.w + b);
        }

        public static Vector4Int operator -(Vector4Int a, Vector4Int b)
        {
            return new Vector4Int(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        }
        public static Vector4Int operator -(Vector4Int a, int b)
        {
            return new Vector4Int(a.x - b, a.y - b, a.z - b, a.w - b);
        }

        public static Vector4Int operator *(Vector4Int a, Vector4Int b)
        {
            return new Vector4Int(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
        }
        public static Vector4Int operator *(Vector4Int a, int b)
        {
            return new Vector4Int(a.x * b, a.y * b, a.z * b, a.w * b);
        }

        // ----------- Cast -----------

        public static explicit operator Vector4Int(Vector4 v)
        {
            return new Vector4Int((int)v.x, (int)v.y, (int)v.z, (int)v.w);
        }

        public static explicit operator Vector4(Vector4Int v)
        {
            return new Vector4(v.x, v.y, v.z, v.w);
        }

        public static explicit operator Vector3uInt(Vector4Int v)
        {
            return new Vector3uInt((uint)v.x, (uint)v.y, (uint)v.z);
        }

        public static explicit operator Vector3(Vector4Int v)
        {
            return new Vector3(v.x, v.y, v.z);
        }

        public static explicit operator Vector3Int(Vector4Int v)
        {
            return new Vector3Int(v.x, v.y, v.z);
        }

        public static explicit operator Vector2Int(Vector4Int v)
        {
            return new Vector2Int(v.x, v.y);
        }

        public static explicit operator Vector2(Vector4Int v)
        {
            return new Vector2Int(v.x, v.y);
        }
    }
    #endregion

    /// <summary>
    /// List of all Resolutions possible
    /// </summary>
    public static List<Vector2Int> allResolutions = new List<Vector2Int>();

    /// <summary>
    /// The maximum Resolution of the screen
    /// </summary>
    public static Vector2Int fullResolution = new Vector2Int(1920, 1080);

    /// <summary>
    /// The hidden gameobject that has Vmath
    /// </summary>
    public const string publicName = "VmathGameObject";

    static Vmath instance;
    //All the anykeys on controllers

    /// <summary>
    /// All Controller anykeys 
    /// </summary>
    public static KeyCode[] ControllerAnyKeys = { KeyCode.JoystickButton0, KeyCode.JoystickButton1, KeyCode.JoystickButton2, KeyCode.JoystickButton3, KeyCode.JoystickButton4, KeyCode.JoystickButton5, KeyCode.JoystickButton6, KeyCode.JoystickButton7, KeyCode.JoystickButton8, KeyCode.JoystickButton9, KeyCode.JoystickButton10, KeyCode.JoystickButton11, KeyCode.JoystickButton12, KeyCode.JoystickButton13, KeyCode.JoystickButton14, KeyCode.JoystickButton15, KeyCode.JoystickButton16, KeyCode.JoystickButton17, KeyCode.JoystickButton18, KeyCode.JoystickButton19 };


    public enum CheatDetection
    {
        HardRestart, SoftRestart, Quit, LogError, None
    }

    /// <summary>
    /// How it Vmath should handle cheatdetection
    /// </summary>
    public static CheatDetection detectingCheat = CheatDetection.None;

    bool reverseRez = false;
    bool reverseGra = false;

    string fullScreenName = "";
    string dropdownNameRez = "";
    string dropdownNameGra = "";

    [System.Serializable]
    public static class Leaderboard
    {
        public const string privateCode = "";
        public const string publicCode = "";
        public const string webURL = "http://dreamlo.com/lb/";

        public static string runTimePrivateCode = "";
        public static string runTimePublicCode = "";
        public static string runTimeWebURL = "";

        public static string scoreText = "Score";
        public static string scoreFetchingText = "Fetching...";
        public static string scoreInbetweenText = " - ";
        public static string scoreBeforeText = ". ";

        public static int totalScore = 0;
        public static int scoreRefreshTime = 30;
        public static int scoreOffsetValue = 0; // how offset the highscore list is
        public static int scrollbarMaxValue = 50;

        public static string scrollbarName = "";

        public struct Highscore
        {
            public string username;
            public int score;

            public Highscore(string _username, int _score)
            {
                username = _username;
                score = _score;
            }

        }
        public static Highscore[] highscoresList;

        public static Text[] highscoreFields;
        public static Highscore[] highscoreList;

        public static void Reloadlist()
        {
            if (highscoreList != null) {
                totalScore = 0;
                for (int i = 0; i < highscoreList.Length; i++) {
                    totalScore += (-(highscoreList[i].score));
                }
                for (int i = 0; i < highscoreFields.Length; i++) {
                    if (highscoreFields[i].IsActive()) {
                        highscoreFields[i].text = i + 1 + Vmath.Leaderboard.scoreOffsetValue + scoreBeforeText;
                        if (i < highscoreList.Length) {

                            if (highscoreList.Length > i + Vmath.Leaderboard.scoreOffsetValue) {

                                string name = highscoreList[i + Vmath.Leaderboard.scoreOffsetValue].username;
                                name = name.Replace("+", " ");
                                highscoreFields[i].text += name + scoreInbetweenText + highscoreList[i + Vmath.Leaderboard.scoreOffsetValue].score.ToString() + " " + scoreText;
                            }
                        }
                    }
                }
            }
        }
        static void OnHighscoresDownloaded(Highscore[] _highscoreList)
        {
            highscoreList = _highscoreList;
            Reloadlist();
        }
        static IEnumerator RefreshHighscores()
        {
            while (true) {
                DownloadHighscores();
                yield return new WaitForSeconds(scoreRefreshTime);
            }
        }
        static IEnumerator UploadNewHighscore(string username, int score)
        {
            WWW www = new WWW(runTimeWebURL + runTimePublicCode + "/add/" + WWW.EscapeURL(username) + "/" + score); // uploads score
            yield return www;
            if (string.IsNullOrEmpty(www.error)) {
                DownloadHighscores();
            }
            else {
                print("Error uploading: " + www.error);
            }
        }
        public static void DownloadHighscores()
        {
            instance.StartCoroutine(DownloadHighscoreData());
        }
        static IEnumerator DownloadHighscoreData()
        {
            WWW www = new WWW(runTimeWebURL + runTimePublicCode + "/pipe/"); // downloads textscores
            yield return www;
            if (string.IsNullOrEmpty(www.error)) {
                FormatHighscores(www.text); // gets scoretext
                OnHighscoresDownloaded(highscoresList);
            }
            else {
                Debug.LogWarning("Error downloading: " + www.error);
            }
        }
        public static void FormatHighscores(string textStream)
        {
            string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries); // splits by line
            highscoresList = new Highscore[entries.Length];

            for (int i = 0; i < entries.Length; i++) {
                string[] entryInfo = entries[i].Split(new char[] { '|' });
                string username = entryInfo[0]; // gets username
                int score = int.Parse(entryInfo[1]); // gets score
                highscoresList[i] = new Highscore(username, score); // inserts username and score to list
                                                                    //print(highscoresList[i].username + ": " + highscoresList[i].score);
            }
        }
        static IEnumerator UploadNewHighscore(int score, string username = "Player", bool UpdateLeaderboard = true)
        {
            WWW www = new WWW(runTimeWebURL + runTimePrivateCode + "/add/" + WWW.EscapeURL(username) + "/" + score); // Uploads score
            yield return www;
            if (string.IsNullOrEmpty(www.error)) {
                if (UpdateLeaderboard) {
                    DownloadHighscores();
                }
            }
            else {
                Debug.LogWarning("Error uploading: " + www.error);
            }
        }
        public static void OnScrollbarChange()
        {
            Vmath.Leaderboard.scoreOffsetValue = Mathf.RoundToInt(GameObject.Find(scrollbarName).GetComponent<Scrollbar>().value * Vmath.Leaderboard.scrollbarMaxValue);
            Vmath.Leaderboard.Reloadlist();
        }
        public static void OnSliderChange()
        {
            Vmath.Leaderboard.scoreOffsetValue = Mathf.RoundToInt(GameObject.Find(scrollbarName).GetComponent<Slider>().value * Vmath.Leaderboard.scrollbarMaxValue);
            Vmath.Leaderboard.Reloadlist();
        }


        /// <summary>
        /// Call Vmath.Leaderboard.LeaderboardSetup(What text that should be as a leaderboard) in Start or Awake if you want to use leaderboard
        /// </summary>
        /// <param name="highscoreText">What GUI Text object that it uses to display score</param>
        /// <param name="_privateCode">dreamlo.com private code</param>
        /// <param name="_publicCode">dreamlo.com public code</param>
        /// <param name="_webURL">http://dreamlo.com/lb/</param>
        public static void LeaderboardSetup(Text[] highscoreText, string _privateCode = Leaderboard.privateCode, string _publicCode = Leaderboard.publicCode, string _webURL = Leaderboard.webURL)
        {
            Leaderboard.runTimePrivateCode = _privateCode;
            Leaderboard.runTimePublicCode = _publicCode;
            Leaderboard.runTimeWebURL = _webURL;
            Leaderboard.highscoreFields = highscoreText;
            for (int i = 0; i < Leaderboard.highscoreFields.Length; i++) {
                Leaderboard.highscoreFields[i].text = i + 1 + Leaderboard.scoreBeforeText + Leaderboard.scoreFetchingText;
            }
            instance.StartCoroutine(RefreshHighscores());
        }

        /// <summary>
        ///  Upload a highscore to leaderboard, must have called LeaderboardSetup First
        /// </summary>
        /// <param name="score">The score</param>
        /// <param name="username">The username, spaces is autocorrected to +, so + = Spaces</param>
        /// <param name="UpdateLeaderboard">If it shoud update the leaderboard when this is called</param>
        public static void UploadScore(int score, string username = "Player", bool UpdateLeaderboard = true)
        {
            instance.StartCoroutine(Leaderboard.UploadNewHighscore(score, username, UpdateLeaderboard));
        }

        /// <summary>
        /// Hooks a Scrollbar to the leaderboard
        /// </summary>
        /// <param name="gameObjectName">The Name of the Scrollbar GameObject</param>
        /// <param name="maxValue">How many scores you can go down</param>
        public static void HookScrollbarToLeaderboard(string gameObjectName, int maxValue = 50)
        {
            Leaderboard.scrollbarName = gameObjectName;
            Scrollbar scrollbar = GameObject.Find(gameObjectName).GetComponent<Scrollbar>();
            scrollbarMaxValue = maxValue - highscoreFields.Length;
            scrollbar.onValueChanged.AddListener(delegate { OnScrollbarChange(); });
            Leaderboard.OnScrollbarChange();
        }

        /// <summary>
        /// Hooks a Slider to the leaderboard
        /// </summary>
        /// <param name="gameObjectName">The Name of the Slider GameObject</param>
        /// <param name="maxValue">How many scores you can go down</param>
        public static void HookSliderToLeaderboard(string gameObjectName, int maxValue = 50)
        {
            Leaderboard.scrollbarName = gameObjectName;
            Slider slider = GameObject.Find(gameObjectName).GetComponent<Slider>();
            scrollbarMaxValue = maxValue - highscoreFields.Length;
            slider.onValueChanged.AddListener(delegate { OnSliderChange(); });
            Leaderboard.OnSliderChange();
        }

    }

    [System.Serializable]
    public static class WebScrape
    {
        public static string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.87 Safari/537.36";

        public static string NormalWebClient(string url)
        {
            WebClient client = new WebClient();
            return client.DownloadString(url);
        }

        public static string GetRequest(string url, string referer = "", bool br = false)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            request.Method = "GET";
            request.ContentType = "text/html; charset=UTF-8";
            request.UserAgent = userAgent;
            request.Headers.Add("Accept-Language", "en-US,en;q=0.5");
            request.Headers.Add("Accept-Encoding", "gzip, deflate");

            if (referer != "") {
                request.Referer = referer;
            }

            request.Headers.Add("TE", "Trailers");


            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) {

                // using (Stream stream = response.GetResponseStream())
                if (br) {
                    /*
                    using (BrotliStream bs = new BrotliStream(response.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress)) {
                        using (System.IO.MemoryStream msOutput = new System.IO.MemoryStream()) {
                            bs.CopyTo(msOutput);
                            msOutput.Seek(0, System.IO.SeekOrigin.Begin);
                            using (StreamReader reader = new StreamReader(msOutput)) {
                                string result = reader.ReadToEnd();

                                return result;

                            }
                        }
                    }*/
                    return "";
                }
                else {
                    using (Stream stream = response.GetResponseStream()) {
                        using (StreamReader reader = new StreamReader(stream)) {
                            string result = reader.ReadToEnd();
                            return result;
                        }
                    }
                }
            }
        }
    }


    #region All Methods

    void OnToggleChange()
    {
        Screen.fullScreen = GameObject.Find(fullScreenName).GetComponent<Toggle>().isOn;
    }

    /// <summary>
    /// Hooks a Toggle to change fullscreen or not
    /// </summary>
    /// <param name="gameObjectName">The Name of the Toggle GameObject</param>
    public static void HookToggleToFullscreen(string gameObjectName)
    {
        GameObject.Find(publicName).GetComponent<Vmath>().fullScreenName = gameObjectName;
        Toggle toggle = GameObject.Find(gameObjectName).GetComponent<Toggle>();
        toggle.isOn = Screen.fullScreen;
        toggle.onValueChanged.AddListener(delegate { GameObject.Find(publicName).GetComponent<Vmath>().OnToggleChange(); });
    }

    /// <summary>
    /// random of -1 and 1
    /// </summary>
    /// <returns></returns>
    public static float RandomMinusPlus()
    {
        return UnityEngine.Random.Range(0, 2) * 2 - 1;
    }

    /// <summary>
    /// Used to find a string in another string, if nothing is found it returns "", ex: all="dwasdzxceg[Hello World]döawdpås", first="[", end="]"; returns "Hello World"
    /// </summary>
    /// <param name="all">The string you want to search</param>
    /// <param name="first">The prefix of the string you want to find</param>
    /// <param name="end">The ending of the string you want to find</param>
    /// <param name="offset">Offsets the prefix</param>
    /// <returns></returns>
    public static string FindHTML(string all, string first, string end, int offset = 0)
    {
        if (all.IndexOf(first) == -1) {
            return "";
        }
        int x = all.IndexOf(first) + first.Length + offset;

        all = all.Substring(x, all.Length - x);
        int y = all.IndexOf(end);
        if (y == -1) {
            return "";
        }
        //  print(x + "|" + y);
        return all.Substring(0, y);
    }

    /// <summary>
    ///  returns a float of the absolute of x+y, used for grids (ignoring the z value)
    /// </summary>
    /// <param name="pos1"></param>
    /// <param name="pos2"></param>
    /// <returns></returns>
    public static float Distance2DAbs(Vector3 pos1, Vector3 pos2 = default(Vector3))
    {
        Vector3 pos = pos1 - pos2;
        return Mathf.Abs(pos.x) + Mathf.Abs(pos.y);
    }
    /// <summary>
    /// The real distance between two positions on a plane (ignoring the z value), Pythagorean theorem 
    /// </summary>
    /// <param name="pos1"></param>
    /// <param name="pos2"></param>
    /// <returns></returns>
    public static float Distance2D(Vector3 pos1, Vector3 pos2 = default(Vector3))
    {
        Vector3 pos = pos1 - pos2;
        return Mathf.Sqrt(Mathf.Pow(pos.x, 2) + Mathf.Pow(pos.y, 2));
    }

    /// <summary>
    /// returns a 0 to 1 Vector3 in the direction of the position you want to go (ignoring the z value)
    /// </summary>
    /// <param name="startPos">The current position</param>
    /// <param name="endPos">The position of where you want to go</param>
    /// <returns></returns>
    public static Vector3 Atan2Normalized(Vector3 startPos, Vector3 endPos = default(Vector3))
    {
        Vector3 diff = endPos - startPos;
        float rot = Mathf.Atan2(diff.y, diff.x);
        return new Vector3(Mathf.Cos(rot), Mathf.Sin(rot), 0);
    }

    /// <summary>
    /// Returns total time sence jan 1 1970
    /// </summary>
    /// <returns></returns>
    public static System.TimeSpan CurrentTime()
    {
        return System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1, 0, 0, 0));
    }

    /// <summary>
    /// Total seconds sence jan 1 1970
    /// </summary>
    /// <returns></returns>
    public static float GetTotalSeconds()
    {
        return System.DateTime.Now.Millisecond / 1000f + System.DateTime.Now.Second;
    }

    /// <summary>
    /// Set timescale of the game
    /// </summary>
    /// <param name="time">Procentage of time (1 = 100%, 0.5f = 50%)</param>
    /// <param name="fixedDeltaScale">How precise the physics should be, 1 = Normal; 0.5f = 2x precise</param>
    public static void SetTimeScale(float time, float fixedDeltaScale = 1)
    {
        Time.timeScale = time;
        Time.fixedDeltaTime = 0.02f * time * fixedDeltaScale;
    }

    /// <summary>
    /// 120.432100012 = 120.4 useful for displaying hp or time in a nice way :)
    /// </summary>
    /// <param name="f">Float of hp/dmg</param>
    /// <param name="decimales">How many decimales</param>
    /// <param name="decPlace">The char of the decimal place</param>
    /// <returns></returns>
    public static string ConvertFloatToDecString(float f, int decimales = 1, string decPlace = ".")
    {
        if (decimales == 0) { return Mathf.Round(f).ToString(); }

        string outp = RoundDecimales(f, decimales).ToString();

        if (outp.Length == 1) {
            outp += decPlace;
        }
        else {
            outp = outp.Replace(",", decPlace);
        }

        outp += MultiplyString("0", decimales - (outp.Length - 2));

        return outp;
    }

    /// <summary>
    /// Multiplyes a string
    /// </summary>
    /// <param name="s">String of what you want to multiply</param>
    /// <param name="times">How many times you want to multiply it</param>
    /// <returns></returns>
    public static string MultiplyString(string s, int times)
    {
        return String.Concat(Enumerable.Repeat(s, times));
    }

    /// <summary>
    /// Get the key that is pressed
    /// </summary>
    /// <param name="ignoreAnyKeys">Removes multible inputs from controllers, keep it at true</param>
    /// <returns></returns>
    public static KeyCode GetKey(bool ignoreAnyKeys = true)
    {
        KeyCode KeyPressed = new KeyCode();
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode))) {
            if (Input.GetKeyDown(vKey)) {
                bool return_null = false;
                for (int i = 0; i < ControllerAnyKeys.Length; i++) {
                    if (vKey == ControllerAnyKeys[i]) {
                        return_null = true;
                    }
                }
                if (!return_null || !ignoreAnyKeys) {
                    KeyPressed = vKey;
                }
            }
        }
        return KeyPressed;
    }

    /// <summary>
    /// Gets a list of all keys that are held down
    /// </summary>
    /// <param name="ignoreAnyKeys">Removes multible inputs from controllers, keep it at true</param>
    /// <returns></returns>
    public static List<KeyCode> GetAllKey(bool ignoreAnyKeys = true)
    {
        List<KeyCode> all = new List<KeyCode>();
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode))) {
            if (Input.GetKey(vKey)) {
                bool return_null = false;
                for (int i = 0; i < ControllerAnyKeys.Length; i++) {
                    if (vKey == ControllerAnyKeys[i]) {
                        return_null = true;
                    }
                }
                if (!return_null || !ignoreAnyKeys) {
                    all.Add(vKey);
                }
            }
        }
        return all;
    }

    /// <summary>
    /// Is any key held down
    /// </summary>
    /// <param name="ignoreAnyKeys">Removes multible inputs from controllers, keep it at true</param>
    /// <returns></returns>
    public static bool IsAnyKey(bool ignoreAnyKeys = true)
    {
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode))) {
            if (Input.GetKey(vKey)) {
                bool return_null = false;
                for (int i = 0; i < ControllerAnyKeys.Length; i++) {
                    if (vKey == ControllerAnyKeys[i]) {
                        return_null = true;
                    }
                }
                if (!return_null || !ignoreAnyKeys) {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// If any key is pressed
    /// </summary>
    /// <param name="ignoreAnyKeys">Removes multible inputs from controllers, keep it at true</param>
    /// <returns></returns>
    public static bool IsKeyPressed(bool ignoreAnyKeys = true)
    {
        bool isPressed = false;
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode))) {
            if (Input.GetKeyDown(vKey)) {
                bool return_null = false;
                for (int i = 0; i < ControllerAnyKeys.Length; i++) {
                    if (vKey == ControllerAnyKeys[i]) {
                        return_null = true;
                    }
                }
                if (!return_null || !ignoreAnyKeys) {
                    isPressed = true;
                }
            }
        }
        return isPressed;
    }

    /// <summary>
    /// Fills a dropdown with all the resolutions
    /// </summary>
    /// <param name="gameObjectName">The name of the GameObject</param>
    /// <param name="autoChange">Directly changes the resolution when it is selected</param>
    /// <param name="inbetween">The char inbetween the two resolutions, 1920x1080 <- The x  </param>
    /// <param name="reverse">If the list should be reversed</param>
    public static void FillDropdownResolution(string gameObjectName, bool autoChange = true, string inbetween = "x", bool reverse = false)
    {
        List<string> _DropOptions = new List<string>();
        Dropdown _Dropdown = GameObject.Find(gameObjectName).GetComponent<Dropdown>();
        Resolution[] allRez = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        if (!reverse) {
            /*
            for (int i = 0; i < allRez.Length / 2; i++) // reverse Resolutions ( to highest first )
            {
                Resolution tmp = allRez[i];
                allRez[i] = allRez[allRez.Length - i - 1];
                allRez[allRez.Length - i - 1] = tmp;
            }
            */
            ReverseArray<Resolution>(allRez);
        }
        for (int i = 0; i < allResolutions.Count; i++) {
            string newRez = allRez[i].width + inbetween + allRez[i].height;
            _DropOptions.Add(newRez);
        }
        int currentRes = GetCurrentRes();
        if (reverse) {
            currentRes = allResolutions.Count - currentRes;
        }
        _Dropdown.ClearOptions();
        _Dropdown.AddOptions(_DropOptions);
        _Dropdown.value = currentRes;
        _Dropdown.RefreshShownValue();
        if (autoChange) {
            _Dropdown.onValueChanged.AddListener(delegate { GameObject.Find(publicName).GetComponent<Vmath>().AutoChangeRez(); });
            GameObject.Find(publicName).GetComponent<Vmath>().dropdownNameRez = gameObjectName;
            GameObject.Find(publicName).GetComponent<Vmath>().reverseRez = reverse;
        }
    }

    /// <summary>
    /// Gets the current Res as a int from allResolutions 
    /// </summary>
    /// <returns></returns>
    public static int GetCurrentRes()
    {
        for (int i = 0; i < allResolutions.Count; i++) {
            if (allResolutions[i].x == Screen.width && allResolutions[i].y == Screen.height) {
                return i;
            }
        }
        return 0;
    }

    public void AutoChangeRez()
    {
        int value = GameObject.Find(dropdownNameRez).GetComponent<Dropdown>().value;
        if (reverseRez) {
            value = allResolutions.Count - value - 1;
        }
        Screen.SetResolution(allResolutions[value].x, allResolutions[value].y, Screen.fullScreen);
    }

    /// <summary>
    /// Fills a dropdown with all graphics options
    /// </summary>
    /// <param name="gameObjectName">The name of the GameObject</param>
    /// <param name="autoChange">Directly changes the quality when it is selected</param>
    /// <param name="reverse">If the list should be reversed</param>
    public static void FillDropdownGraphics(string gameObjectName, bool autoChange = true, bool reverse = false)
    {
        List<string> _DropOptions = new List<string>();
        Dropdown _Dropdown = GameObject.Find(gameObjectName).GetComponent<Dropdown>();
        string[] allGra = QualitySettings.names;

        if (!reverse) {
            /*
            for (int i = 0; i < allRez.Length / 2; i++) // reverse Resolutions ( to highest first )
            {
                Resolution tmp = allRez[i];
                allRez[i] = allRez[allRez.Length - i - 1];
                allRez[allRez.Length - i - 1] = tmp;
            }
            */
            ReverseArray<string>(allGra);
        }
        int currentGra = 0;
        for (int i = 0; i < allGra.Length; i++) {
            string newRez = allGra[i];
            _DropOptions.Add(newRez);
            currentGra = QualitySettings.GetQualityLevel();

        }
        if (!reverse) {
            currentGra = allGra.Length - currentGra - 1;
        }

        _Dropdown.ClearOptions();
        _Dropdown.AddOptions(_DropOptions);
        _Dropdown.value = currentGra;
        _Dropdown.RefreshShownValue();
        if (autoChange) {
            _Dropdown.onValueChanged.AddListener(delegate { GameObject.Find(publicName).GetComponent<Vmath>().AutoChangeGra(); });
            GameObject.Find(publicName).GetComponent<Vmath>().dropdownNameGra = gameObjectName;
            GameObject.Find(publicName).GetComponent<Vmath>().reverseGra = reverse;
        }
    }

    public void AutoChangeGra()
    {
        int value = GameObject.Find(dropdownNameGra).GetComponent<Dropdown>().value;
        if (!reverseGra) {
            value = QualitySettings.names.Length - value - 1;
        }
        QualitySettings.SetQualityLevel(value);
    }

    /// <summary>
    /// sha crypt (used for anticheat)
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string Sha1Sum2(string str)
    {
        System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
        byte[] bytes = encoding.GetBytes(str);
        var sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();
        return System.BitConverter.ToString(sha.ComputeHash(bytes));
    }

    /// <summary>
    /// Shuffle list, ex: Vmath.ShuffleList<string>(NameOfList);
    /// </summary>
    /// <typeparam name="T">the type, like int or string</typeparam>
    /// <param name="inputList">the list</param>
    public static void ShuffleList<T>(List<T> inputList)
    {
        System.Random rand = new System.Random();
        // For each spot in the list, pick
        // a random item to swap into that spot.
        for (int i = 0; i < inputList.Count - 1; i++) {
            int j = rand.Next(i, inputList.Count);
            T temp = inputList[i];
            inputList[i] = inputList[j];
            inputList[j] = temp;
        }
    }

    /// <summary>
    /// Shuffle array, ex: Vmath.ShuffleArray<string>(NameOfArray);
    /// </summary>
    /// <typeparam name="T">The type, like int or string</typeparam>
    /// <param name="inputArray">The array</param>
    public static void ShuffleArray<T>(T[] inputArray) //
    {
        System.Random rand = new System.Random();
        // For each spot in the array, pick
        // a random item to swap into that spot.
        for (int i = 0; i < inputArray.Length - 1; i++) {
            int j = rand.Next(i, inputArray.Length);
            T temp = inputArray[i];
            inputArray[i] = inputArray[j];
            inputArray[j] = temp;
        }
    }
    /// <summary>
    /// Reverses a list, ex:  Vmath.ReverseList<string>(NameOfList);
    /// </summary>
    /// <typeparam name="T">The type, like int or string</typeparam>
    /// <param name="inputList">The list</param>
    public static void ReverseList<T>(List<T> inputList)
    {
        inputList.Reverse();
    }

    /// <summary>
    /// Reverses a array, ex: Vmath.ReverseArray<string>(NameOfArray);
    /// </summary>
    /// <typeparam name="T">The type, like int or string</typeparam>
    /// <param name="inputArray">The array</param>
    public static void ReverseArray<T>(T[] inputArray)
    {
        T[] temp = new T[inputArray.Length];

        for (int i = 0; i < inputArray.Length; i++) {
            temp[i] = inputArray[inputArray.Length - 1 - i];
        }

        for (int i = 0; i < temp.Length; i++) { // to remove the deep copy
            inputArray[i] = temp[i];
        }
    }

    /// <summary>
    /// Simply to set up a crosshair gameObject, call every frame
    /// </summary>
    /// <param name="crosshairGameObject">The GameObject that should be the crosshair</param>
    /// <param name="cam">The cam</param>
    /// <param name="zPos">The zpos of the gameObject</param>
    public static void Crosshair2D(GameObject crosshairGameObject, Camera cam, float zPos = 20)
    {
        Vector3 mousePos = (Vector3)Input.mousePosition;
        Vector3 campos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        campos.z = zPos;
        crosshairGameObject.transform.position = campos;
    }

    /// <summary>
    /// If over 0 return 1, if under return -1 else return 0
    /// </summary>
    /// <param name="f">Input float</param>
    /// <returns></returns>
    public static float NormalizeFloat(float f)
    {
        if (f > 0) {
            return 1f;
        }
        else if (f < 0) {
            return -1f;
        }
        return 0f;
    }

    /// <summary>
    /// Round to the amount of decimales
    /// </summary>
    /// <param name="f">The float</param>
    /// <param name="decimales">The amount of decimales</param>
    /// <returns></returns>
    public static float RoundDecimales(float f, int decimales = 0) // 
    {
        decimales = Mathf.Abs(decimales);
        float fvalue = (float)Math.Round(f * Mathf.Pow(10, decimales)) / Mathf.Pow(10, decimales);
        return fvalue;
    }

    /// <summary>
    /// Returns the input within a range
    /// </summary>
    /// <param name="input">Input float</param>
    /// <param name="min">The max value of the input</param>
    /// <param name="max">The min value of the input</param>
    /// <returns></returns>
    public static float MinMaxValue(float input, float min, float max)
    {
        if (input > max) { input = max; }
        if (input < min) { input = min; }
        return input;
    }

    /// <summary>
    /// Get the battery in int (1 to 100)
    /// </summary>
    /// <returns></returns>
    public static int GetBattery()
    {
        return Mathf.RoundToInt(SystemInfo.batteryLevel * 100);
    }

    /// <summary>
    /// Checks if that compnent has that script
    /// </summary>
    /// <param name="game">The GameObject that should be checked</param>
    /// <param name="scriptName">The scriptname of the class that should be checked, like Vmath</param>
    /// <returns></returns>
    public static bool CheckPlayerType(GameObject game, string scriptName)
    {
        bool hasScript = false;
        MonoBehaviour[] scripts = game.GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour mb in scripts) {
            if (mb.GetType().Name == scriptName) {
                hasScript = true;
            }
        }
        return hasScript;
    }

    /// <summary>
    /// Color to Hex as string
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static string ColorToHex(Color32 color)
    {
        string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
        return hex;
    }

    /// <summary>
    /// string hex To Color
    /// </summary>
    /// <param name="hex"></param>
    /// <returns></returns>
    public static Color HexToColor(string hex)
    {
        hex = hex.Replace("0x", "");//in case the string is formatted 0xFFFFFF
        hex = hex.Replace("#", "");//in case the string is formatted #FFFFFF
        byte a = 255;//assume fully visible unless specified in hex
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        //Only use alpha if the string has enough characters
        if (hex.Length == 8) {
            a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        }
        return new Color32(r, g, b, a);
    }

    /// <summary>
    /// Removes all DontDestryOnLoad GameObjects
    /// </summary>
    public static void RemoveAllDontDestroyOnLoad()
    {
        var go = new GameObject("Sacrificial Lamb");
        DontDestroyOnLoad(go);
        foreach (var root in go.scene.GetRootGameObjects()) {
            if (root.name != Vmath.publicName) {
                Destroy(root);
            }
        }
    }

    /// <summary>
    /// Quits the current Application and Starts itself (.exe only)
    /// </summary>
    public static void HardRestart()
    {
        Debug.Log("HardReset was called, Make sure that the application has the right permission or else it can't restart the program (Only works on .exe)");
        System.Diagnostics.Process.Start(Application.dataPath.Replace("_Data", ".exe")); //new program
        Application.Quit();
    }

    /// <summary>
    /// Removes all DontDestroy and load the first scene
    /// </summary>
    public static void SoftRestart()
    {
        Debug.Log("SoftRestart was called");
        RemoveAllDontDestroyOnLoad();
        UnityEngine.SceneManagement.Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.buildIndex);
    }

    private static System.Random random = new System.Random();

    /// <summary>
    /// Gets a random string of A-Z or 0-9
    /// </summary>
    /// <param name="length">The lenth of the random string</param>
    /// <returns></returns>
    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    /// <summary>
    /// Does a fuzzy search for a pattern within a string.true if each character in pattern is found sequentially within stringToSearch; otherwise, false.
    /// </summary>
    /// <param name="stringToSearch"></param>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public static bool FuzzyMatch(string stringToSearch, string pattern)
    {
        var patternIdx = 0;
        var strIdx = 0;
        var patternLength = pattern.Length;
        var strLength = stringToSearch.Length;

        while (patternIdx != patternLength && strIdx != strLength) {
            if (char.ToLower(pattern[patternIdx]) == char.ToLower(stringToSearch[strIdx]))
                ++patternIdx;
            ++strIdx;
        }

        return patternLength != 0 && strLength != 0 && patternIdx == patternLength;
    }

    // Does a fuzzy search for a pattern within a string, and gives the search a score on how well it matched.
    // outScoreThe score which this search received, if a match was found.
    //true if each character in pattern is found sequentially within stringToSearch; otherwise, false
    public static bool FuzzyMatch(string stringToSearch, string pattern, out int outScore)
    {
        // Score consts
        const int adjacencyBonus = 5;               // bonus for adjacent matches
        const int separatorBonus = 10;              // bonus if match occurs after a separator
        const int camelBonus = 10;                  // bonus if match is uppercase and prev is lower

        const int leadingLetterPenalty = -3;        // penalty applied for every letter in stringToSearch before the first match
        const int maxLeadingLetterPenalty = -9;     // maximum penalty for leading letters
        const int unmatchedLetterPenalty = -1;      // penalty for every letter that doesn't matter


        // Loop variables
        var score = 0;
        var patternIdx = 0;
        var patternLength = pattern.Length;
        var strIdx = 0;
        var strLength = stringToSearch.Length;
        var prevMatched = false;
        var prevLower = false;
        var prevSeparator = true;                   // true if first letter match gets separator bonus

        // Use "best" matched letter if multiple string letters match the pattern
        char? bestLetter = null;
        char? bestLower = null;
        int? bestLetterIdx = null;
        var bestLetterScore = 0;

        var matchedIndices = new List<int>();

        // Loop over strings
        while (strIdx != strLength) {
            var patternChar = patternIdx != patternLength ? pattern[patternIdx] as char? : null;
            var strChar = stringToSearch[strIdx];

            var patternLower = patternChar != null ? char.ToLower((char)patternChar) as char? : null;
            var strLower = char.ToLower(strChar);
            var strUpper = char.ToUpper(strChar);

            var nextMatch = patternChar != null && patternLower == strLower;
            var rematch = bestLetter != null && bestLower == strLower;

            var advanced = nextMatch && bestLetter != null;
            var patternRepeat = bestLetter != null && patternChar != null && bestLower == patternLower;
            if (advanced || patternRepeat) {
                score += bestLetterScore;
                matchedIndices.Add((int)bestLetterIdx);
                bestLetter = null;
                bestLower = null;
                bestLetterIdx = null;
                bestLetterScore = 0;
            }

            if (nextMatch || rematch) {
                var newScore = 0;

                // Apply penalty for each letter before the first pattern match
                // Note: Math.Max because penalties are negative values. So max is smallest penalty.
                if (patternIdx == 0) {
                    var penalty = Math.Max(strIdx * leadingLetterPenalty, maxLeadingLetterPenalty);
                    score += penalty;
                }

                // Apply bonus for consecutive bonuses
                if (prevMatched)
                    newScore += adjacencyBonus;

                // Apply bonus for matches after a separator
                if (prevSeparator)
                    newScore += separatorBonus;

                // Apply bonus across camel case boundaries. Includes "clever" isLetter check.
                if (prevLower && strChar == strUpper && strLower != strUpper)
                    newScore += camelBonus;

                // Update pattern index IF the next pattern letter was matched
                if (nextMatch)
                    ++patternIdx;

                // Update best letter in stringToSearch which may be for a "next" letter or a "rematch"
                if (newScore >= bestLetterScore) {
                    // Apply penalty for now skipped letter
                    if (bestLetter != null)
                        score += unmatchedLetterPenalty;

                    bestLetter = strChar;
                    bestLower = char.ToLower((char)bestLetter);
                    bestLetterIdx = strIdx;
                    bestLetterScore = newScore;
                }

                prevMatched = true;
            }
            else {
                score += unmatchedLetterPenalty;
                prevMatched = false;
            }

            // Includes "clever" isLetter check.
            prevLower = strChar == strLower && strLower != strUpper;
            prevSeparator = strChar == '_' || strChar == ' ';

            ++strIdx;
        }

        // Apply score for last match
        if (bestLetter != null) {
            score += bestLetterScore;
            matchedIndices.Add((int)bestLetterIdx);
        }

        outScore = score;
        return patternIdx == patternLength;
    }

    public static string waitRebindText = "Wait";
    public static bool useAnyKeyRebind = false;
    public static string allRebindData = "";
    public static string allSaveRebind = "";
    public static readonly string[] allTxt = { "UpArrow", "LeftArrow", "DownArrow", "RightArrow", "Keypad", "Alpha", "Mouse", "Joystick", "Button" };
    public static readonly string[] allTxtReplace = { "↑", "←", "↓", "→", "N", "", "M", "J", "B" }; // replaces above with this

    #region RebindKey
    public static void RebindButton(string buttonGameObjectName, string saveName, MonoBehaviour script, string keyCodeName, bool replaceKeyCodeInScript = true, Sprite bttDown = default(Sprite), Sprite bttUp = default(Sprite))
    {
        GameObject.Find(publicName).GetComponent<Vmath>().StartRebind(buttonGameObjectName, script, saveName, keyCodeName, false, 0, replaceKeyCodeInScript, bttDown, bttUp);
    }

    public static void RebindButton(string buttonGameObjectName, string saveName, MonoBehaviour script, string keyCodeArrayName, int posInArray, bool replaceKeyCodeInScript = true, Sprite bttDown = default(Sprite), Sprite bttUp = default(Sprite))
    {
        GameObject.Find(publicName).GetComponent<Vmath>().StartRebind(buttonGameObjectName, script, saveName, keyCodeArrayName, true, posInArray, replaceKeyCodeInScript, bttDown, bttUp);
    }


    public void StartRebind(string buttonGameObjectName, MonoBehaviour script, string saveName, string keyCodeName, bool array, int pos, bool replaceKeyCodeInScript, Sprite bttDown = default(Sprite), Sprite bttUp = default(Sprite))
    {
        StartCoroutine(StartRebindWait(buttonGameObjectName, script, saveName, keyCodeName, array, pos));
        if (replaceKeyCodeInScript) {
            StartCoroutine(SetKeyCodeInScript(script, saveName, keyCodeName, array, pos));
        }
        if (bttDown != default(Sprite)) {
            StartCoroutine(SetButtonDown(buttonGameObjectName, saveName, bttDown, bttUp));
        }
    }

    IEnumerator SetButtonDown(string buttonGameObjectName, string saveName = "", Sprite bttDown = default(Sprite), Sprite bttUp = default(Sprite))
    {

        GameObject btt = GameObject.Find(buttonGameObjectName);
        Image bttImg = btt.GetComponent<Image>();

        if (bttUp == default(Sprite)) {
            bttUp = bttImg.sprite;
        }
        if (bttDown == default(Sprite)) {
            bttDown = bttImg.sprite;
        }

        while (true) {
            yield return null;
            bttImg.sprite = Input.GetKey(GetKeyCodeByName(saveName)) ? bttDown : bttUp;
        }
    }

    IEnumerator SetKeyCodeInScript(MonoBehaviour script, string saveName, string keyCodeName, bool array, int pos)
    {
        while (true) {
            yield return null;
            if (array) { // create a copy of array, insert correct value, set script to copy
                KeyCode[] fullList = ((KeyCode[])(script.GetType().GetField(keyCodeName).GetValue(script)));
                fullList[pos] = GetKeyCodeByName(saveName);
                script.GetType().GetField(keyCodeName).SetValue(script, fullList);
            }
            else { script.GetType().GetField(keyCodeName).SetValue(script, GetKeyCodeByName(saveName)); } // set value directly
        }

    }
    /// <summary>
    /// If saved value starts with "startWith", it will be reset, Just call [ Vmath.ResetAllBinds(); ] if you want to reset all
    /// </summary>
    /// <param name="startWith"></param>
    public static void ResetAllBinds(string startWith = "")
    {
        string realSave = "";
        for (int i = 0; i < allSaveRebind.Length; i++) {
            realSave += allSaveRebind[i]; // creates copy, to prevent deep copy it is made this way
        }

        while (realSave.IndexOf(";") != -1) {
            string saveName = realSave.Substring(1, realSave.IndexOf(":") - 1);
            if (saveName.StartsWith(startWith)) {
                int count = int.Parse(realSave.Substring(realSave.IndexOf(":") + 1, realSave.IndexOf(";") - realSave.IndexOf(":") - 1));
                PlayerPrefs.SetInt(saveName, count);
                SetKeyUsed(saveName, (KeyCode)count);
            }
            realSave = realSave.Substring(realSave.IndexOf(";") + 1, realSave.Length - realSave.IndexOf(";") - 1);
        }
    }

    public static KeyCode GetKeyCodeByName(string name)
    {
        string realSave = "";
        for (int i = 0; i < allRebindData.Length; i++) {
            realSave += allRebindData[i];
        }

        // bit inefficent, but works :)
        // could have just .IndexOf(name) to start to make it more efficent

        while (realSave.IndexOf(";") != -1) {
            string saveName = realSave.Substring(1, realSave.IndexOf(":") - 1);
            if (saveName == name) {
                int count = int.Parse(realSave.Substring(realSave.IndexOf(":") + 1, realSave.IndexOf(";") - realSave.IndexOf(":") - 1));
                return (KeyCode)count;
            }
            realSave = realSave.Substring(realSave.IndexOf(";") + 1, realSave.Length - realSave.IndexOf(";") - 1);
        }
        return KeyCode.None;
    }
    /// <summary>
    /// If keycode is updated, call this to make sure that it is locally saved, the reason why I put all playerprefsdata in allRebindData, is because it has an instan read and is faster than playerprefs so everything is single threded
    /// </summary>
    /// <param name="name"></param>
    /// <param name="code"></param>
    public static void SetKeyUsed(string name, KeyCode code)
    {
        string realSave = "";
        for (int i = 0; i < allRebindData.Length; i++) {
            realSave += allRebindData[i];
        }
        int count = (int)code;

        if (allRebindData.Contains("|" + name + ":")) {
            int start = allRebindData.IndexOf("|" + name + ":") + name.Length + 2;
            string c = allRebindData.Substring(start, allRebindData.Length - start);
            int end = start + c.IndexOf(";");
            allRebindData = allRebindData.Substring(0, start) + count + allRebindData.Substring(end, allRebindData.Length - end);
        }
        else {
            allRebindData += "|" + name + ":" + count + ";";
        }
    }

    /// <summary>
    /// EX: Vmath.RebindButton("btt", "all", this, "all", 0);
    /// EX: Vmath.RebindButton("btt2", "one", this, "one");</summary>
    /// <param name="buttonGameObjectName"></param>
    /// <param name="script"></param>
    /// <param name="saveName"></param>
    /// <param name="keyCodeName"></param>
    /// <param name="array"></param>
    /// <param name="pos"></param>
    /// <returns></returns>

    public IEnumerator StartRebindWait(string buttonGameObjectName, MonoBehaviour script, string saveName, string keyCodeName, bool array, int pos)
    {
        KeyCode save = (array ? ((KeyCode[])script.GetType().GetField(keyCodeName).GetValue(script))[pos] : (KeyCode)script.GetType().GetField(keyCodeName).GetValue(script));
        allSaveRebind += "|" + saveName + ":" + (int)save + ";";

        int saveDataReal = PlayerPrefs.GetInt(saveName, -1);
        KeyCode real = saveDataReal == -1 ? save : (KeyCode)saveDataReal;
        SetKeyUsed(saveName, real);

        string txt = KeyCodeToShort(real.ToString());
        GameObject btt = GameObject.Find(buttonGameObjectName);
        Text bttTxt = btt.GetComponentInChildren<Text>();

        btt.GetComponent<Button>().onClick.AddListener(delegate { RebindButtonIsPressed(buttonGameObjectName); });

        while (true) {
            bttTxt.text = txt;

            while (bttTxt.text != waitRebindText) {
                bttTxt.text = KeyCodeToShort(GetKeyCodeByName(saveName).ToString());
                yield return null;
            }
            yield return new WaitUntil(() => Vmath.IsKeyPressed());

            PlayerPrefs.SetInt(saveName, (int)Vmath.GetKey()); // save input
            txt = KeyCodeToShort(Vmath.GetKey().ToString());
            SetKeyUsed(saveName, Vmath.GetKey());
            yield return null;
        }
    }


    public void RebindButtonIsPressed(string buttonGameObjectName)
    {
        GameObject.Find(buttonGameObjectName).GetComponentInChildren<Text>().text = waitRebindText;
    }

    /// <summary>
    /// Returns a shortened verison of a keycode, just makes the keycode shorter and easier to read
    /// </summary>
    /// <param name="txt"></param>
    /// <returns></returns>
    public static string KeyCodeToShort(string txt)
    {
        for (int q = 0; q < allTxt.Length; q++) {
            txt = txt.Replace(allTxt[q], allTxtReplace[q]);
        }
        return txt;
    }

    #endregion

    #region antiCheat
    public static bool IsCheatEngineRunning()
    {
        foreach (System.Diagnostics.Process pro in System.Diagnostics.Process.GetProcesses()) {
            if (pro.ProcessName.ToLower().Contains("cheat") && pro.ProcessName.ToLower().Contains("engine")) {
                DetectedCheat("Cheat engine is running");
                return true;
            }
        }
        return false;
    }

    string GetHash(string FilePath) // get hash of application
    {
        System.IO.FileStream fStream = null;
        try {
            fStream = new System.IO.FileStream(FilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
        }
        catch (Exception ex) {
            Console.WriteLine("ERROR:" + ex.Message);
            Console.ReadLine();
        }
        byte[] myArray = new byte[fStream.Length];
        fStream.Read(myArray, 0, myArray.Length);
        fStream.Close();
        fStream.Dispose();
        string hash;
        using (System.Security.Cryptography.SHA1CryptoServiceProvider sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider()) {
            return Convert.ToBase64String(sha1.ComputeHash(myArray));
        }
    }

    /// <summary>
    /// Protects a value from changing in Cheat Engine
    /// </summary>
    /// <param name="name">The name of the Variable</param>
    /// <param name="script">The script, just write [ this ]</param>
    public static void ProtectValue(string name, MonoBehaviour script)
    {
        Vmath go = GameObject.Find(publicName).GetComponent<Vmath>();
        go.StartCoroutine(go.ProtectValueWhile(name, script));
    }

    IEnumerator ProtectValueWhile(string name, MonoBehaviour script)
    {
        while (true) {
            yield return null;
            var save = script.GetType().GetField(name).GetValue(script);
            Type t = save.GetType();
            script.GetType().GetField(name).SetValue(script, Convert.ChangeType(UnityEngine.Random.Range(1, 10000), t)); // GetDefault(t) );
            yield return new WaitForEndOfFrame();
            script.GetType().GetField(name).SetValue(script, save);
        }
    }

    /*
    public static void ProtectMethod(string name, MonoBehaviour script)
    {
        Vmath go = GameObject.Find(publicName).GetComponent<Vmath>();
        go.StartCoroutine(go.ProtectMethodWhile(name, script));
    }

    IEnumerator ProtectMethodWhile(string name, MonoBehaviour script)  // should crash program if method nullefied
    {
        while (true) {
            yield return null;
            //  var save = script.GetType().GetMethod(name)
            try {
                script.SendMessage(name, true);
            }
            catch {
                DetectedCheat("Method Changed");
            }

            yield return new WaitForEndOfFrame();
        }
    }*/

    /// <summary>
    /// Get the default value of type
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static object GetDefault(Type t)
    {
        Vmath go = GameObject.Find(publicName).GetComponent<Vmath>();
        return go.GetType().GetMethod("GetDefaultGeneric").MakeGenericMethod(t).Invoke(go, null);
    }

    public T GetDefaultGeneric<T>()
    {
        return default(T);
    }

    /// <summary>
    /// If cheat detected call this
    /// </summary>
    /// <param name="type"></param>
    static void DetectedCheat(string type = "null")
    {
        if (detectingCheat == CheatDetection.HardRestart) {
            HardRestart();
        }
        else if (detectingCheat == CheatDetection.SoftRestart) {
            SoftRestart();
        }
        else if (detectingCheat == CheatDetection.Quit) {
            Application.Quit();
        }
        else if (detectingCheat == CheatDetection.LogError) {
            Debug.LogError("Cheat Detected: " + type);
        }
    }




    #endregion

    #endregion

    private void Awake()
    {
        instance = this;
    }


    [RuntimeInitializeOnLoadMethod]
    static void StartGame()
    {
        Application.runInBackground = true;

        bool wasFullScreen = Screen.fullScreen;
        Screen.fullScreen = true;
        fullResolution = new Vector2Int(Screen.currentResolution.width, Screen.currentResolution.height);
        Screen.fullScreen = wasFullScreen;

        Resolution[] Resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        for (int i = 0; i < Resolutions.Length / 2; i++) // reverse Resolutions ( to highest first )
        {
            Resolution tmp = Resolutions[i];
            Resolutions[i] = Resolutions[Resolutions.Length - i - 1];
            Resolutions[Resolutions.Length - i - 1] = tmp;
        }
        for (int i = 0; i < Resolutions.Length; i++) {
            allResolutions.Add(new Vector2Int(Resolutions[i].width, Resolutions[i].height)); // add to public allResolutions list as a Vector2Int
        }

        GameObject thisGo = new GameObject(); // creates a gameObject, will be used for Frame update
        thisGo.AddComponent<Vmath>();
        thisGo.name = publicName;
        DontDestroyOnLoad(thisGo);
        thisGo.hideFlags = HideFlags.HideInHierarchy; // hide the gameObject in the inspector   

        thisGo.GetComponent<Vmath>().StartCoroutine("FrameUpdateCaller");
    }

    #region FrameUpdate

    public delegate void FrameUpdateEvent(object sender, EventArgs args);

    public static event FrameUpdateEvent FrameUpdate;

    void StartFrameCaller()
    {
        StartCoroutine(FrameUpdateCaller());
    }
    IEnumerator FrameUpdateCaller()
    {
        while (true) {
            yield return new WaitForEndOfFrame();
            try {
                FrameUpdate(null, EventArgs.Empty);

            }
            catch (Exception) {

            }
        }
    }

    #endregion
}
#pragma warning restore



[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
    AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
public class ConditionalHideAttribute : PropertyAttribute
{
    public string conditionalSourceField;
    public bool showIfTrue;
    public int enumIndex;

    public ConditionalHideAttribute(string boolVariableName, bool showIfTrue)
    {
        conditionalSourceField = boolVariableName;
        this.showIfTrue = showIfTrue;
    }

    public ConditionalHideAttribute(string enumVariableName, int enumIndex)
    {
        conditionalSourceField = enumVariableName;
        this.enumIndex = enumIndex;
    }

}
#if UNITY_EDITOR
// Original version of the ConditionalHideAttribute created by Brecht Lecluyse (www.brechtos.com)
// Modified by: Sebastian Lague
// Ex:  [ConditionalHide (nameof (variablename), true)]
[CustomPropertyDrawer(typeof(ConditionalHideAttribute))]
public class ConditionalHidePropertyDrawer : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;

        bool enabled = GetConditionalHideAttributeResult(condHAtt, property) == condHAtt.showIfTrue;

        if (enabled) {
            EditorGUI.PropertyField(position, property, label, true);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;
        bool enabled = GetConditionalHideAttributeResult(condHAtt, property) == condHAtt.showIfTrue;

        if (enabled) {
            return EditorGUI.GetPropertyHeight(property, label);
        }
        //We want to undo the spacing added before and after the property
        return -EditorGUIUtility.standardVerticalSpacing;
    }

    bool GetConditionalHideAttributeResult(ConditionalHideAttribute condHAtt, SerializedProperty property)
    {
        SerializedProperty sourcePropertyValue = null;

        //Get the full relative property path of the sourcefield so we can have nested hiding.Use old method when dealing with arrays
        if (!property.isArray) {
            string propertyPath = property.propertyPath; //returns the property path of the property we want to apply the attribute to
            string conditionPath = propertyPath.Replace(property.name, condHAtt.conditionalSourceField); //changes the path to the conditionalsource property path
            sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);

            //if the find failed->fall back to the old system
            if (sourcePropertyValue == null) {
                //original implementation (doens't work with nested serializedObjects)
                sourcePropertyValue = property.serializedObject.FindProperty(condHAtt.conditionalSourceField);
            }
        }
        else {
            //original implementation (doens't work with nested serializedObjects)
            sourcePropertyValue = property.serializedObject.FindProperty(condHAtt.conditionalSourceField);
        }


        if (sourcePropertyValue != null) {
            return CheckPropertyType(condHAtt, sourcePropertyValue);
        }

        return true;
    }

    bool CheckPropertyType(ConditionalHideAttribute condHAtt, SerializedProperty sourcePropertyValue)
    {
        //Note: add others for custom handling if desired
        switch (sourcePropertyValue.propertyType) {
            case SerializedPropertyType.Boolean:
                return sourcePropertyValue.boolValue;
            case SerializedPropertyType.Enum:
                return sourcePropertyValue.enumValueIndex == condHAtt.enumIndex;
            default:
                Debug.LogError("Data type of the property used for conditional hiding [" + sourcePropertyValue.propertyType + "] is currently not supported");
                return true;
        }
    }
}
#endif