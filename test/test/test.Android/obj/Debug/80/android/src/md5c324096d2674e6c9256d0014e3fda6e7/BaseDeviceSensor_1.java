package md5c324096d2674e6c9256d0014e3fda6e7;


public abstract class BaseDeviceSensor_1
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.hardware.SensorEventListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAccuracyChanged:(Landroid/hardware/Sensor;I)V:GetOnAccuracyChanged_Landroid_hardware_Sensor_IHandler:Android.Hardware.ISensorEventListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onSensorChanged:(Landroid/hardware/SensorEvent;)V:GetOnSensorChanged_Landroid_hardware_SensorEvent_Handler:Android.Hardware.ISensorEventListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Plugin.DeviceSensors.Platforms.Android.BaseDeviceSensor`1, Plugin.DeviceSensors", BaseDeviceSensor_1.class, __md_methods);
	}


	public BaseDeviceSensor_1 ()
	{
		super ();
		if (getClass () == BaseDeviceSensor_1.class)
			mono.android.TypeManager.Activate ("Plugin.DeviceSensors.Platforms.Android.BaseDeviceSensor`1, Plugin.DeviceSensors", "", this, new java.lang.Object[] {  });
	}

	public BaseDeviceSensor_1 (android.hardware.SensorManager p0, int p1)
	{
		super ();
		if (getClass () == BaseDeviceSensor_1.class)
			mono.android.TypeManager.Activate ("Plugin.DeviceSensors.Platforms.Android.BaseDeviceSensor`1, Plugin.DeviceSensors", "Android.Hardware.SensorManager, Mono.Android:Android.Hardware.SensorType, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public void onAccuracyChanged (android.hardware.Sensor p0, int p1)
	{
		n_onAccuracyChanged (p0, p1);
	}

	private native void n_onAccuracyChanged (android.hardware.Sensor p0, int p1);


	public void onSensorChanged (android.hardware.SensorEvent p0)
	{
		n_onSensorChanged (p0);
	}

	private native void n_onSensorChanged (android.hardware.SensorEvent p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
