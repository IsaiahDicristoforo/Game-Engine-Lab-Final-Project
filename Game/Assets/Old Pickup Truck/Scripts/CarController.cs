using UnityEngine;
using System.Collections;

namespace FunFant {
	public class CarController : MonoBehaviour {

		[SerializeField] private GameObject[] wheelMeshes = new GameObject[4];
		[SerializeField] private WheelCollider[] wheelColliders = new WheelCollider[4];

		[SerializeField] private Transform centerOfMass;

		[SerializeField] private float maxSteer = 40f;
		[SerializeField] private float maxTorque = 100f;


		void Start () {
			Rigidbody rb = this.GetComponent<Rigidbody>();
			if(rb) {
				rb.centerOfMass = centerOfMass.localPosition;
			}
		}


		private void FixedUpdate() {
			
			//Car controls
			Steer(Input.GetAxis ("Horizontal"));

			Thrust(Input.GetAxis ("Vertical"));
		}

		void Steer(float value)	{
			//Collider angle
			float currentSteer = value * maxSteer;
			wheelColliders[0].steerAngle = currentSteer;
			wheelColliders[1].steerAngle = currentSteer;

			//Wheel meshes update
			for(int i = 0; i < 4; i++) {
				Quaternion rot;
				Vector3 pos;
				wheelColliders[i].GetWorldPose(out pos, out rot);
				wheelMeshes[i].transform.position = pos;
				wheelMeshes[i].transform.rotation = rot;
			}
		}

		void Thrust(float value) {
			float torque = value * maxTorque;
			wheelColliders[0].motorTorque = torque;
			wheelColliders[1].motorTorque = torque;
			wheelColliders[2].motorTorque = torque;
			wheelColliders[3].motorTorque = torque;
		}
	}
}
