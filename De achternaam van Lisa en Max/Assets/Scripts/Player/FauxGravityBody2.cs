using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FauxGravityBody2 : MonoBehaviour {

	private FauxGravityAttractor2 attractor;
	private Rigidbody rb;

	public bool placeOnSurface = false;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		attractor = FauxGravityAttractor2.instance;
	}
	
	void FixedUpdate ()
	{
		if (placeOnSurface)
			attractor.PlaceOnSurface(rb);
		else
			attractor.Attract(rb);
	}

}
