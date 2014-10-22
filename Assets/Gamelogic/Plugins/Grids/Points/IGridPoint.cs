//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System;

namespace Gamelogic.Grids
{
	public interface IGridPoint
	{
		 
	}

	/**
		Represents a 2D integers point that can is used to access a cell in a Grid.
	
		@implementers GridPoint base classes must be immutable for many of the algorithms to work correctly. In particular, 
		GridPoints are used as keys in dictionaries and sets.
	
		@implementers It is also a good idea to overload the `==` and `!=` operators.

		
		
		@version1_0

		@ingroup Interface
	*/
	public interface IGridPoint<TPoint> : IEquatable<TPoint>, IGridPoint
		where TPoint : IGridPoint<TPoint>
	{
		/**
			The lattice distance between two points.
		
			@implementers Two points should have a distance of 1 if and only if they are neighbors.
		*/
		int DistanceFrom(TPoint other);
	}
}