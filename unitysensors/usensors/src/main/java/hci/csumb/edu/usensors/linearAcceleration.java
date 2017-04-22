package hci.csumb.edu.usensors;

/*
name: John Sullivan
course: CST306
*/

/**
 * Created by student on 6/15/16.
 */

import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.os.IBinder;
import android.renderscript.Float3;
import android.support.annotation.Nullable;

import java.util.ArrayList;
import java.util.List;

import static java.lang.System.in;

public class linearAcceleration implements SensorEventListener,Runnable {
    private SensorManager mSensorManager;
    private Sensor linAcc;
    private Context context;
    private final float[] acceleration;
    private float[] period;

    public linearAcceleration(Context context) {
        this.context = context;
        acceleration = new float[3];
        period = new float[3];
        mSensorManager = (SensorManager) context.getSystemService(Context.SENSOR_SERVICE);
        linAcc = mSensorManager.getDefaultSensor(Sensor.TYPE_LINEAR_ACCELERATION);
        mSensorManager.registerListener(this, linAcc, SensorManager.SENSOR_DELAY_GAME);
        new Thread(this).start();
    }

    @Override
    public final void onSensorChanged(SensorEvent event) {
        synchronized (acceleration) {
            acceleration[0] = event.values[0];
            acceleration[1] = event.values[1];
            acceleration[2] = event.values[2];
        }
    }

    @Override
    public final void onAccuracyChanged(Sensor sensor, int accuracy) {

    }

    public float[] getAcceleration() {
        synchronized (period) {
            return period;
        }
    }

    public void asyncCollect(int mValues) {
        ArrayList<float[]> values = new ArrayList<>();
        float[] avg = new float[3];

        while(true){
            avg[0] = avg[1] = avg[2] = 0;
            values.clear();
            while(values.size() < mValues)
            {
                synchronized (acceleration) {
                    values.add(acceleration.clone());
                }
            }

            for(float[] v : values)
            {
                avg[0] += v[0];
                avg[1] += v[1];
                avg[2] += v[2];
            }

            avg[0] /= mValues;
            avg[1] /= mValues;
            avg[2] /= mValues;

            synchronized (period) {
                period[0] = avg[0];
                period[1] = avg[1];
                period[2] = avg[2];
            }
        }
    }

    @Override
    public void run() {
        asyncCollect(5000);
    }
}
