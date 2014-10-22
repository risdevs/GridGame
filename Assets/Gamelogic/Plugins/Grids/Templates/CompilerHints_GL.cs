//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

// Auto-generated File

using System.Linq;

namespace Gamelogic.Grids
{
	/**
		Compiler hints for our examples.

		@since 1.8
	*/
	public static class __CompilerHintsGL
	{
		public static bool __CompilerHint__Rect__TileCell()
		{
			var grid = new RectGrid<TileCell[]>(1, 1);

			foreach(var point in grid) { grid[point] = new TileCell[1]; } 

			var shapeStorageInfo = new ShapeStorageInfo<RectPoint>(new IntRect(), p => true);
			var shapeInfo = new RectShapeInfo<TileCell>(shapeStorageInfo);

			return grid[grid.First()][0] == null || shapeInfo.Translate(RectPoint.Zero) != null;
		}
		public static bool __CompilerHint__Diamond__TileCell()
		{
			var grid = new DiamondGrid<TileCell[]>(1, 1);

			foreach(var point in grid) { grid[point] = new TileCell[1]; } 

			var shapeStorageInfo = new ShapeStorageInfo<DiamondPoint>(new IntRect(), p => true);
			var shapeInfo = new DiamondShapeInfo<TileCell>(shapeStorageInfo);

			return grid[grid.First()][0] == null || shapeInfo.Translate(DiamondPoint.Zero) != null;
		}
		public static bool __CompilerHint__Rect__SpriteCell()
		{
			var grid = new RectGrid<SpriteCell[]>(1, 1);

			foreach(var point in grid) { grid[point] = new SpriteCell[1]; } 

			var shapeStorageInfo = new ShapeStorageInfo<RectPoint>(new IntRect(), p => true);
			var shapeInfo = new RectShapeInfo<SpriteCell>(shapeStorageInfo);

			return grid[grid.First()][0] == null || shapeInfo.Translate(RectPoint.Zero) != null;
		}
		public static bool __CompilerHint__Diamond__SpriteCell()
		{
			var grid = new DiamondGrid<SpriteCell[]>(1, 1);

			foreach(var point in grid) { grid[point] = new SpriteCell[1]; } 

			var shapeStorageInfo = new ShapeStorageInfo<DiamondPoint>(new IntRect(), p => true);
			var shapeInfo = new DiamondShapeInfo<SpriteCell>(shapeStorageInfo);

			return grid[grid.First()][0] == null || shapeInfo.Translate(DiamondPoint.Zero) != null;
		}
		
		public static bool __CompilerHint__Rect__UVCell()
		{
			var grid = new RectGrid<UVCell[]>(1, 1);

			foreach(var point in grid) { grid[point] = new UVCell[1]; } 

			var shapeStorageInfo = new ShapeStorageInfo<RectPoint>(new IntRect(), p => true);
			var shapeInfo = new RectShapeInfo<UVCell>(shapeStorageInfo);

			return grid[grid.First()][0] == null || shapeInfo.Translate(RectPoint.Zero) != null;
		}
		public static bool __CompilerHint__Diamond__UVCell()
		{
			var grid = new DiamondGrid<UVCell[]>(1, 1);

			foreach(var point in grid) { grid[point] = new UVCell[1]; } 

			var shapeStorageInfo = new ShapeStorageInfo<DiamondPoint>(new IntRect(), p => true);
			var shapeInfo = new DiamondShapeInfo<UVCell>(shapeStorageInfo);

			return grid[grid.First()][0] == null || shapeInfo.Translate(DiamondPoint.Zero) != null;
		}
		
		public static bool __CompilerHint__Rect__TextureCell()
		{
			var grid = new RectGrid<TextureCell[]>(1, 1);

			foreach(var point in grid) { grid[point] = new TextureCell[1]; } 

			var shapeStorageInfo = new ShapeStorageInfo<RectPoint>(new IntRect(), p => true);
			var shapeInfo = new RectShapeInfo<TextureCell>(shapeStorageInfo);

			return grid[grid.First()][0] == null || shapeInfo.Translate(RectPoint.Zero) != null;
		}
		public static bool __CompilerHint__Diamond__TextureCell()
		{
			var grid = new DiamondGrid<TextureCell[]>(1, 1);

			foreach(var point in grid) { grid[point] = new TextureCell[1]; } 

			var shapeStorageInfo = new ShapeStorageInfo<DiamondPoint>(new IntRect(), p => true);
			var shapeInfo = new DiamondShapeInfo<TextureCell>(shapeStorageInfo);

			return grid[grid.First()][0] == null || shapeInfo.Translate(DiamondPoint.Zero) != null;
		}
		
		public static bool __CompilerHint__Rect__MeshTileCell()
		{
			var grid = new RectGrid<MeshTileCell[]>(1, 1);

			foreach(var point in grid) { grid[point] = new MeshTileCell[1]; } 

			var shapeStorageInfo = new ShapeStorageInfo<RectPoint>(new IntRect(), p => true);
			var shapeInfo = new RectShapeInfo<MeshTileCell>(shapeStorageInfo);

			return grid[grid.First()][0] == null || shapeInfo.Translate(RectPoint.Zero) != null;
		}
		public static bool __CompilerHint__Diamond__MeshTileCell()
		{
			var grid = new DiamondGrid<MeshTileCell[]>(1, 1);

			foreach(var point in grid) { grid[point] = new MeshTileCell[1]; } 

			var shapeStorageInfo = new ShapeStorageInfo<DiamondPoint>(new IntRect(), p => true);
			var shapeInfo = new DiamondShapeInfo<MeshTileCell>(shapeStorageInfo);

			return grid[grid.First()][0] == null || shapeInfo.Translate(DiamondPoint.Zero) != null;
		}
		
		public static bool CallAll__()
		{
			if(!__CompilerHint__Rect__TileCell()) return false;
			if(!__CompilerHint__Diamond__TileCell()) return false;
			
			if(!__CompilerHint__Rect__SpriteCell()) return false;
			if(!__CompilerHint__Diamond__SpriteCell()) return false;
			
			if(!__CompilerHint__Rect__UVCell()) return false;
			if(!__CompilerHint__Diamond__UVCell()) return false;
			
			if(!__CompilerHint__Rect__TextureCell()) return false;
			if(!__CompilerHint__Diamond__TextureCell()) return false;
			
			if(!__CompilerHint__Rect__MeshTileCell()) return false;
			if(!__CompilerHint__Diamond__MeshTileCell()) return false;
			
			return true;

		}
	}
}