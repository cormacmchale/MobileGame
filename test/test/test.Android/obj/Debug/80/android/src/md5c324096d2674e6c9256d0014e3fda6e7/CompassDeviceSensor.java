package md5c324096d2674e6c9256d0014e3fda6e7;


public class CompassDeviceSensor
	extends md5c324096d2674e6c9256d0014e3fda6e7.BaseDeviceSensor_1
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Plugin.DeviceSensors.Platforms.Android.CompassDeviceSensor, Plugin.DeviceSensors", CompassDeviceSensor.class, __md_methods);
	}


	public CompassDeviceSensor ()
	{
		super ();
		if (getClass () == CompassDeviceSensor.class)
			mono.android.TypeManager.Activate ("Plugin.DeviceSensors.Platforms.Android.CompassDeviceSensor, Plugin.DeviceSensors", "", this, new java.lang.Object[] {  });
	}

	public CompassDeviceSensor (android.hardware.SensorManager p0, int p1)
	{
		super ();
		if (getClass () == CompassDeviceSensor.class)
			mono.android.TypeManager.Activate ("Plugin.DeviceSensors.Platforms.Android.CompassDeviceSensor, Plugin.DeviceSensors", "Android.Hardware.SensorManager, Mono.Android:Android.Hardware.SensorType, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}

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
