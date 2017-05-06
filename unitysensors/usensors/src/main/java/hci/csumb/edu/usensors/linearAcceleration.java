package hci.csumb.edu.usensors;

/*
name: John Sullivan
course: CST306
*/

import android.content.Context;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;

public class linearAcceleration implements SensorEventListener
{
    private SensorManager mSensorManager;
    private Sensor linAcc;
    private Context context;
    private final float[] acceleration;
    private float[] period;
    private int collected;

    public linearAcceleration(Context context)
    {
        this.context = context;
        acceleration = new float[3];
        period = new float[3];
        collected = 0;
        mSensorManager = (SensorManager) context.getSystemService(Context.SENSOR_SERVICE);
        linAcc = mSensorManager.getDefaultSensor(Sensor.TYPE_LINEAR_ACCELERATION);
        mSensorManager.registerListener(this, linAcc, SensorManager.SENSOR_DELAY_GAME);
    }

    @Override
    public final void onSensorChanged(SensorEvent event)
    {
        acceleration[0] += event.values[0];
        acceleration[1] += event.values[1];
        acceleration[2] += event.values[2];
        collected++;

        if(collected >= 5) {
            period[0] = acceleration[0]/collected;
            period[1] = acceleration[1]/collected;
            period[2] = acceleration[2]/collected;

            acceleration[0] = 0.0f;
            acceleration[1] = 0.0f;
            acceleration[2] = 0.0f;
            collected = 0;
        }
    }

    @Override
    public final void onAccuracyChanged(Sensor sensor, int accuracy)
    {

    }

    public float[] getAcceleration()
    {
        return period.clone();
    }
}
