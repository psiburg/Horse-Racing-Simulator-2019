using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Custom {

	public static int Map(int from, int fromMin, int fromMax, int toMin, int toMax) {
		int fromAbs  =  from - fromMin;
		int fromMaxAbs = fromMax - fromMin;

		int normal = fromAbs / fromMaxAbs;

		int toMaxAbs = toMax - toMin;
		int toAbs = toMaxAbs * normal;

		int to = toAbs + toMin;

		return to;
	}

	public static float Map(float from, float fromMin, float fromMax, float toMin, float toMax) {
		float fromAbs  =  from - fromMin;
		float fromMaxAbs = fromMax - fromMin;

		float normal = fromAbs / fromMaxAbs;

		float toMaxAbs = toMax - toMin;
		float toAbs = toMaxAbs * normal;

		float to = toAbs + toMin;

		return to;
	}
}
