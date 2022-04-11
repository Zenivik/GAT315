using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulator : Singleton<Simulator>
{
	[SerializeField] FloatData fixedFPS;
	[SerializeField] StringData FPS;
	[SerializeField] List<Force> forces;

	public List<Body> bodies { get; set; } = new List<Body>();

	float timeAccumulator;
	float fixedDeltaTime { get => 1 / fixedFPS.value; }

	Camera activeCamera;

	private void Start()
	{
		activeCamera = Camera.main;
	}

    private void Update()
    {
        FPS.value = (1.0f / Time.deltaTime).ToString("F1");

        timeAccumulator += Time.deltaTime;

		forces.ForEach(force => force.ApplyForce(bodies));

		while(timeAccumulator > fixedDeltaTime)
        {
			foreach (var body in bodies)
			{
				Integrator.SemiImplicitEuler(body, Time.deltaTime);
			}

			timeAccumulator = timeAccumulator - fixedDeltaTime;
		}

		bodies.ForEach(body => body.acceleration = Vector2.zero);
    }

    public Vector3 GetScreenToWorldPosition(Vector2 screen)
	{
		Vector2 world = activeCamera.ScreenToWorldPoint(screen);
		return world;
	}
}
