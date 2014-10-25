﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;

public class ParseController : ParseInitializeBehaviour
{

    public class MapEntity
    {
        public ParseObject parseObject;
        public List<MapTile> tiles = new List<MapTile>();
        public int Number;
        public string Author;

        // new map
        public MapEntity(int Number)
        {
            parseObject = new ParseObject("MapBytes");
            this.Number = Number;
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
                this.Number =  int.Parse(obj ["mapNumber"].ToString()); 
            if (obj.ContainsKey("author"))
                this.Author = obj ["author"].ToString(); 
        }
        
        public void Save()
        {
            List<List<float>> list = new List<List<float>>();
            foreach (MapTile t in tiles)
            {
                list.Add(new List<float>(){t.x, t.y, t.sprite});
            }

            parseObject ["map"] = list;
            parseObject ["author"] = ParseUser.CurrentUser.Username;
            //TODO: parseObject["mapNumber"] = ??
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
                return "Single Player " + this.GetNumber();
            } else
            {
                return this.Author + " " + this.GetNumber();
            }
        }


    }

    public class MapTile
    {
        public float x;
        public float y;
        public int sprite;
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
                q.WhereEqualTo("user", ParseUser.CurrentUser);
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
                            result.Add(new MapEntity(i));
                        }
                    }
                } 
                Debug.Log("FINISH LIST MAP");
                IsCompleted = true;
            });

        }
    }

}
