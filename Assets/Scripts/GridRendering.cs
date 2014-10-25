using UnityEngine;
using System.Collections;

public class GridRendering : MonoBehaviour
{
	public static int ROWS = 15;
	public static int COLS = 20;
	static Material lineMaterial;

	private float yStart, yEnd;
	private float xStart, xEnd;
	public float tileSize;

	public bool drawGrid = false;

	void Start ()
	{
		Camera mainCamera = Camera.main;
		Vector3 cameraPosition = mainCamera.transform.position;
		
		float xDist = mainCamera.aspect * mainCamera.orthographicSize; 
		xEnd = cameraPosition.x + xDist;
		xStart = cameraPosition.x - xDist;
		
		float yDist = mainCamera.orthographicSize; 
		yEnd = cameraPosition.y + yDist;
		
		tileSize = (xEnd - xStart) / COLS;

		yEnd *= 0.8f;
		yStart = yEnd - ((ROWS) * tileSize);

        Debug.Log("GridRendering tileSize:" + tileSize);
	}

	void CreateLineMaterial ()
	{
			if (!lineMaterial) {
					lineMaterial = new Material ("Shader \"Lines/Colored Blended\" {" +
							"SubShader { Pass { " +
							"    Blend SrcAlpha OneMinusSrcAlpha " +
							"    ZWrite Off Cull Off Fog { Mode Off } " +
							"    BindChannels {" +
							"      Bind \"vertex\", vertex Bind \"color\", color }" +
							"} } }");
					lineMaterial.hideFlags = HideFlags.HideAndDontSave;
					lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
			}
	}

	void OnPostRender ()
	{
		if (!drawGrid)
						return;

		GL.PushMatrix ();

		CreateLineMaterial ();
		// set the current material
		lineMaterial.SetPass (0);
		GL.Begin (GL.LINES);
		GL.Color (new Color (1, 1, 1, 0.5f));


		for (float i = xStart; i <= xEnd; i+= tileSize) {
			GL.Vertex3 (i, yEnd, 0);
			GL.Vertex3 (i, yEnd - ROWS * tileSize, 0);
		}

		for (float i = 0; i <= ROWS; i++) {
			GL.Vertex3 (-40, yEnd - i * tileSize, 0);
			GL.Vertex3 (40, yEnd - i * tileSize, 0);
		}
		GL.End ();

		GL.PopMatrix ();
		}

	public Vector3 TileToWorld (float x, float y)
	{
		return new Vector3(xStart + x * tileSize, yStart + y *tileSize);
	}

	public Vector3 TileToWorld (Vector3 v)
	{
		return TileToWorld (v.x, v.y);
	}

	public Vector3 WorldToTile (float x, float y)
	{
		return new Vector3 (Mathf.Floor((x - xStart) / tileSize), Mathf.Floor((y - yStart) / tileSize));
	}

	public Vector3 WorldToTile(Vector3 v)
	{
		return WorldToTile (v.x, v.y);
	}
}
