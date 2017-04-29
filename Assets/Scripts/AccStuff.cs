/*
name: John Sullivan
couse: CST306
*/

using UnityEngine;
using JNIAssist;

namespace AccStuff
{
	public class LinearAcceleration
    {
		private AndroidJavaObject sensor;

		public LinearAcceleration()
        {
			sensor = new AndroidJavaObject("hci.csumb.edu.usensors.linearAcceleration", JNMan.Context);
		}

		public Vector3 accelerationVec()
        {
			float[] acc = sensor.Call<float[]>("getAcceleration");
			return new Vector3(acc[1], acc[0], acc[2]);
		}

		public float[] accelerationRaw()
        {
			float[] acc = sensor.Call<float[]>("getAcceleration");
			return new float[] {acc[1], acc[0], acc[2]};
		}

        public static implicit operator Vector3(LinearAcceleration l)
        {
            return l.accelerationVec();
        }
	}

	public class rotationVector
    {
		private AndroidJavaObject sensor;

		public rotationVector()
        {
			sensor = new AndroidJavaObject("hci.csumb.edu.usensors.rotationVector", JNMan.Context);
		}

		public Quaternion rotationVec()
        {
			float[] rot = sensor.Call<float[]>("getRotation");
			return new Quaternion(rot[1], rot[0], rot[2], rot[3]);
		}

		public float[] accelerationRaw()
        {
			float[] rot = sensor.Call<float[]>("getRotation");
			return new float[] {rot[1], rot[0], rot[2], rot[3]};
		}
	}	
}