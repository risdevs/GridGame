using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;

public class ParseController : ParseInitializeBehaviour
{

    public class MapEntity
    {
        public ParseObject parseObject;
        public List<MapTile> tiles = new List<MapTile>();
		public string id;

        public MapEntity() {
            parseObject = new ParseObject("MapBytes");
        }

		// create from spm
        public MapEntity(string id, int[,] data) {
			this.id=id;
			for (int i = 0; i < data.GetLength(0); i++) {
                MapTile tile = new MapTile();
                tile.x = data[i,0];
                tile.y = data[i,1];
                tile.sprite = data[i,2];
                this.tiles.Add(tile);
            }
        }

		// CREATE FROM MULTIPLAYER
        public MapEntity(ParseObject obj) {
			this.id = this.parseObject.ObjectId;
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
        }
        
        public void Save() {
            List<List<float>> list = new List<List<float>>();
            foreach (MapTile t in tiles)
            {
                list.Add(new List<float>(){t.x, t.y, t.sprite});
            }

            parseObject ["map"] = list;
            parseObject.SaveAsync();
        }

		public string GetUser() {
			return null;
 		}
		public string getId() {
			return this.id;
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

    public bool IsLoggedIn()
    {
        return ParseUser.CurrentUser != null;
    }

    public IEnumerator<bool> Login(string name)
    {
        var task = ParseUser.LogInAsync(name, "socialpoint");
        while (!task.IsCompleted)
            yield return false;

        if (task.IsFaulted || task.IsCanceled)
        {
            // The login failed. Check the error to see why.
        } else
        {
            // Login was successful.
        }
        yield return true;
    }

    public void Register(string name)
    {
    }

    public class ListMapOperation {
        public List<MapEntity> result;

        public bool IsCompleted = false;

        public void run()
        {
            ParseObject.GetQuery("MapBytes").FindAsync().ContinueWith(t => {
                result = new List<MapEntity>();
                foreach (ParseObject obj in t.Result)
                {
                    MapEntity map = new MapEntity(obj);
                    result.Add(map);
                }
                IsCompleted = true;
            });

        }
    }

}
