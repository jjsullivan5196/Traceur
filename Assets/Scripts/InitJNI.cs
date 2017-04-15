/*
name: John Sullivan
couse: CST306
*/

using UnityEngine;
using System.Collections;

namespace JNIAssist {
	
	public static class JNMan {
		private static AndroidJavaClass activityClass;
		private static AndroidJavaObject activityContext;
		private static bool initialized = false;

		public static void Init() {
			if (!initialized)
			{
				activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
			}
		}

		public static AndroidJavaClass Activity {
			get { return activityClass; }
		}

		public static AndroidJavaObject Context {
			get { return activityContext; }
		}
	}
	
}