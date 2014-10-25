using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;

public class ParseController : ParseInitializeBehaviour
{

    static int SCORE_PER_SP_LEVEL = 100;

    public class MapEntity
    {
        public ParseObject parseObject;
        public List<MapTile> tiles = new List<MapTile>();
        public int Number;
        public string Author;
        public string AuthorId;

        // new map
        public MapEntity(int Number, string userName)
        {
            this.Number = Number;
            this.Author = userName;
        }

        // create from spm
        public MapEntity(int number, int[,] data)
        {
            this.Number = number;
            for (int i = 0; i < data.GetLength(0); i++)
            {
                MapTile tile = new MapTile();
                tile.x = data [i, 0];
                tile.y = data [i, 1];
                tile.sprite = data [i, 2];
                this.tiles.Add(tile);
            }
        }

        // CREATE FROM MULTIPLAYER
        public MapEntity(ParseObject obj)
        {
            this.parseObject = obj;
            foreach (object tileobj in obj.Get<List<object>>("map"))
            {
                List<object> tileList = (List<object>)tileobj;
                MapTile tile = new MapTile();
                tile.x = float.Parse(tileList [0].ToString());
                tile.y = float.Parse(tileList [1].ToString());
                tile.sprite = int.Parse(tileList [2].ToString());
                this.tiles.Add(tile);
            }
            if (obj.ContainsKey("mapNumber"))
                this.Number = int.Parse(obj ["mapNumber"].ToString()); 
            if (obj.ContainsKey("author"))
                this.Author = obj ["author"].ToString(); 
            if (obj.ContainsKey("authorId"))
                this.AuthorId = obj ["authorId"].ToString(); 
        }
        
        public void Save()
        {
            if (this.parseObject == null)
            {
                this.parseObject = new ParseObject("MapBytes");
            }
            List<List<float>> list = new List<List<float>>();
            foreach (MapTile t in tiles)
            {
                list.Add(new List<float>(){t.x, t.y, t.sprite});
            }

            parseObject ["map"] = list;
            parseObject ["author"] = ParseUser.CurrentUser.Username;
            parseObject ["authorId"] = ParseUser.CurrentUser.ObjectId;
            parseObject ["mapNumber"] = this.Number;
            Debug.Log("SAVE");
            parseObject.SaveAsync();
        }

        public int GetNumber()
        {
            return this.Number;
        }

        public string GetTitle()
        {
            if (this.parseObject == null)
            {
                if (this.Author == null || Author == "") {
                    return "LEVEL - " + this.GetNumber();
                } else {
                    return "Create New Map (" + this.GetNumber() + ")";
                }
            } else
            {
                return this.Author + " " + this.GetNumber();
            }
        }

        public bool HasBeenCompleted() {
            return ParseController.HasCompletedLevel(this.Author, this.Number);
        }


    }

    public class MapTile
    {
        public float x;
        public float y;
        public int sprite;
    }



    public static bool HasCompletedLevel(string author, int level)
    {
        if (author == null)
            author = "";
        var levels = GetCompletedLevels();
        foreach (object l in levels)
        {
            List<object> lobj = (List<object>)l;
            var lauthor = lobj [0].ToString();
            var lnumber = int.Parse(lobj [1].ToString());
            if (lauthor == author && level == lnumber) {
                return true;
            }
        }
        return false;

    }
    
    public static List<object> GetCompletedLevels()
    {
        return ParseUser.CurrentUser.ContainsKey("levels") ? ParseUser.CurrentUser.Get<List<object>>("levels") : new List<object>();
    }
    
    public static void CompleteLevel(string author, int level)
    {
        if (author == null)
            author = "";
        if (!HasCompletedLevel(author, level))
        {
            var user = ParseUser.CurrentUser;
            var levels = GetCompletedLevels();
            levels.Add(new List<object>(){author, level});
            user ["levels"] = levels;
            Debug.Log("BEFORE SAVE");

            int scoresingle = 0;
            int scoremulti = 0;
            foreach (object l in levels)
            {
                List<object> lobj = (List<object>)l;
                var lauthor = lobj [0].ToString();
                var lnumber = int.Parse(lobj [1].ToString());
                if (lauthor == null || lauthor == "") {
                    scoresingle += SCORE_PER_SP_LEVEL;
                } else {
                    scoremulti += SCORE_PER_SP_LEVEL;
                }
            }
            user ["spscore"] = scoresingle;
            user ["mpscore"] = scoremulti;
            user.SaveAsync().ContinueWith(t => {
                if (t.IsFaulted)
                {
                    Debug.Log("FAULTED");
                } else
                {
                    Debug.Log("OK");
                }
            });
        }
    }
    
    public static void ClearCompletedLevels()
    {
        var user = ParseUser.CurrentUser;
        user ["splevels"] = new List<object>();
        user.SaveAsync();
    }

    public static int GetScore()
    {
        return ParseUser.CurrentUser.ContainsKey("spscore") ? int.Parse(ParseUser.CurrentUser.Get<object>("spscore").ToString()) : 0;
    }

    public static void RenameUser(string name) {
        ParseUser.CurrentUser.Username = name;
        ParseUser.CurrentUser.SaveAsync();
        ParseObject.GetQuery("MapBytes").WhereEqualTo("authorId", ParseUser.CurrentUser.ObjectId).FindAsync().ContinueWith(t => {
           foreach (ParseObject obj in t.Result)
           {
                obj ["author"] = ParseUser.CurrentUser.Username;
                obj.SaveAsync();
            }
        });

    }
         
    public override void Awake()
    {
        base.applicationID = "2QWerPx74sTKazgf92SYJuaMMP7jpOy0lB6fJ3NW";
        base.dotnetKey = "JNKPfQqY2vhgk6CnDJsSgTorwgEeG4rQVaXbYbhd";
        base.Awake();
    }

    static bool loggedin = false;

    public void Start()
    {
        if (!loggedin)
        {
            loggedin = true;
            var user = ParseUser.CurrentUser;
            if (user == null)
            {
                user = new ParseUser() {
                    Username = "Player " + Random.Range(1, 10000),
                    Password = "socialpoint",
                };
                user.SignUpAsync();
            }
            Debug.Log("User Name: " + user.Username);
            Debug.Log("User ObjectId: " + user.ObjectId);
        }
    }



    public class GetRankingOperation
    {
        public List<ParseUser> result = new List<ParseUser>();
        public bool IsCompleted = false;
            
        public void run(bool multiplayer = false)
        {
            string sortfield = multiplayer ? "mpscore" : "spscore";
            Debug.Log("Running ranking");
            ParseUser.Query.OrderByDescending(sortfield).WhereGreaterThan(sortfield,0).Limit(10).FindAsync().ContinueWith(t => {
                if (t.IsFaulted) {
                    Debug.Log("Ranking faulted");
                } else {
                    this.result = new List<ParseUser>();
                    Debug.Log("Ranking OK");
                    foreach (ParseUser obj in t.Result)
                    {
                        this.result.Add(obj);
                    }
                    Debug.Log("Ranking OK");
                }
                IsCompleted = true;
            });
        }
    }
        
    public class ListMapOperation
    {
        public List<MapEntity> result = new List<MapEntity>();
        public bool IsCompleted = false;

        public void run(bool currentuser = false)
        {
            var q = ParseObject.GetQuery("MapBytes");
            if (currentuser)
            {
                Debug.Log("CURRENT USER ONLY");
                q = q.WhereEqualTo("authorId", ParseUser.CurrentUser.ObjectId);
            }
            q.OrderByDescending("timesPlayed")
                .FindAsync().ContinueWith(t => {
                var mapsDict = new Dictionary<int, MapEntity>();
                Debug.Log("LIST MAP BUCLE");
                foreach (ParseObject obj in t.Result)
                {
                    MapEntity map = new MapEntity(obj);
                  
                    if (currentuser)
                    {
                        mapsDict [map.GetNumber()] = map;
                    } else
                    {
                        result.Add(map);
                    }
                }
                if (currentuser)
                {
                    for (int i = 1; i <= 24; i++)
                    {
                        if (mapsDict.ContainsKey(i))
                        {
                            result.Add(mapsDict [i]);
                        } else
                        {
                            result.Add(new MapEntity(i, ParseUser.CurrentUser.Username));
                        }
                    }
                } 
                Debug.Log("FINISH LIST MAP");
                IsCompleted = true;
            });

        }
    }

}
