using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class VisionMesh
{
	[SerializeField] public Mesh mesh;

	public int azimuth = 45;
	public int altitude = 45;
	public float radius = 1f;
	public int resolution = 4;
}
