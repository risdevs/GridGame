using UnityEngine;

namespace Gamelogic.Grids
{

	/**
		Similar to a sprite cell, but with custom UV coordinates.
		This type of cell is useful when placing a single texture 
		across multiple cells.		

		@link_making_your_own_cells for guidelines on making your own cell.

		@version1_8
		@ingroup UnityComponents
	*/
	public class UVCell : TileCell
	{
		public MapPlane plane = MapPlane.XY;

		private Color color;
		private Texture2D texture;
		private Vector2 textureScale;
		private Vector2 textureOffset;
		private Material material;

		public override Color Color
		{
			get { return color; }

			set
			{
				color = value;
				__UpdatePresentation(true);
			}
		}

		public override Vector2 Dimensions
		{
			get
			{
				switch (plane)
				{
					case MapPlane.XY:
					default:
						return GetComponent<MeshFilter>().sharedMesh.bounds.size.To2DXY();
					case MapPlane.XZ:
						return GetComponent<MeshFilter>().sharedMesh.bounds.size.To2DXZ();
				}
			}
		}

		public void SetTexture(Texture2D texture)
		{
			this.texture = texture;
			__UpdatePresentation(true);
		}

		public void SetUVs(Vector2 offset, Vector2 scale)
		{
			textureOffset = offset;
			textureScale = scale;
			__UpdatePresentation(true);
		}

		public override void __UpdatePresentation(bool forceUpdate)
		{
			if (material == null)
			{
				material = renderer.material; //only duplicate once
			}

			material.color = color;
			material.mainTexture = texture;
			material.mainTextureOffset = textureOffset;
			material.mainTextureScale = textureScale;

			renderer.material = material;
		}

		public override void SetAngle(float angle)
		{
			transform.SetLocalRotationZ(angle);
		}

		public override void AddAngle(float angle)
		{
			transform.RotateAroundZ(angle);
		}
	}
}
