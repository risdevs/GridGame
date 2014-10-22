using System;

using UnityEngine;

namespace Gamelogic.Grids
{
	/**
		Class for holding properties for generating a Poisson disk sample set.
		
		@version1_8
		@ingroup UnityUtilities
	*/
	[Serializable]
	public class PoissonDiskProperties
	{
		/**
			Number of points tried per iteration.
		*/
		public int pointCount;

		/**
			The minimum distance between points.
		*/
		public float minimumDistance;

		/**
			The rectangle in which points are generated.
		*/
		public Rect range;
	}
}