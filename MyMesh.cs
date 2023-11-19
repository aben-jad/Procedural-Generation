using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMesh
{
	static float sphereRadius = .1f;
	static int sphereSegs = 4;
	static int sphereRings = 3;

	public static Mesh GenerateSphereMesh()
	{

		Mesh mesh = new Mesh();

		Vector3[] points = new Vector3[sphereSegs * (sphereRings - 1) + 2];
		//Print(points.Length);
		GenerateSphereVertices(ref points);
		
		int[] triIndices = new int[6 * sphereSegs * (sphereRings - 1)];
		GenerateTriangleIndices(ref triIndices);

		mesh.SetVertices(points);
		mesh.triangles = triIndices;
		mesh.RecalculateNormals();

		Vector2[] uvs = new Vector2[points.Length];


		//uvs[1] = new Vector2(0, 0);
		//uvs[2] = new Vector2(0, 1);
		//uvs[5] = new Vector2(1, 0);
		//uvs[6] = new Vector2(1, 1);

		mesh.uv = uvs;

		return mesh;
	}


	static void GenerateSphereVertices(ref Vector3[] _points)
	{
		for (int i = 0; i <= sphereRings; i++)
		{
			float pi = Mathf.PI;
			float deltaTheta = pi / (sphereRings);
			float deltaAlpha = (2 * pi ) / sphereSegs;
			if (i == 0)
				_points[0] = new Vector3(0, -1, 0) * sphereRadius;
			else if(i == sphereRings)
				_points[_points.Length - 1] = new Vector3(0, 1, 0) * sphereRadius;
			else
			{
				for (int j = 1; j <= sphereSegs; j++)
					_points[j + ((i - 1) * sphereSegs)] = 
						new Vector3(Mathf.Cos((deltaTheta * i) - pi / 2) * Mathf.Cos(deltaAlpha * (j - 1)),
						Mathf.Sin((deltaTheta * i) - pi / 2), 
						Mathf.Cos((deltaTheta * i) - pi / 2) * Mathf.Sin(deltaAlpha * (j - 1))) * sphereRadius;
			}
		}
	}

	static void GenerateTriangleIndices(ref int[] _triIndices)
	{
		int j = 0;
		for (int i = 0; i < sphereSegs; i++)//_triIndices.Length; i += 3)
		{
			_triIndices[i * 3] = 0;
			_triIndices[i  * 3 + 1] = j + 1;
			_triIndices[i  * 3 + 2] = j + 2;
			if (j + 2 > sphereSegs)
				_triIndices[i  * 3 + 2] = 1;

			j++;
		}

		j = 0;
		//for (int i = sphereSegs; i < sphereSegs * 2; i++)

		int currentIndex = sphereSegs * 3;
		for (int i = 1; i <sphereRings - 1; i++)
		{

			for (j = 0; j < sphereSegs; j++)
			{
				//Print("cc" + j);
				_triIndices[currentIndex] = ((i - 1) * sphereSegs) + 1 + j;
				_triIndices[currentIndex + 1] = i * sphereSegs + 1 + j;
				_triIndices[currentIndex + 2] = _triIndices[currentIndex + 1] + 1;

				if (_triIndices[currentIndex + 2] > sphereSegs * (1 + i))
					_triIndices[currentIndex + 2] = sphereSegs * i + 1;

				_triIndices[currentIndex + 3] = _triIndices[currentIndex];
				_triIndices[currentIndex + 4] = _triIndices[currentIndex + 2];
				_triIndices[currentIndex + 5] = _triIndices[currentIndex] + 1;

				if (_triIndices[currentIndex + 5] == _triIndices[currentIndex + 4])
					_triIndices[currentIndex + 5] = ((i - 1) * sphereSegs) + 1;

				currentIndex += 6;
			}
		}


		j = 0;
		for (int i = 0; i < sphereSegs; i++)//_triIndices.Length; i += 3)
		{
			_triIndices[currentIndex] = sphereSegs * (sphereRings - 2) + 1 + j;
			_triIndices[currentIndex + 1] = sphereSegs * (sphereRings - 1) + 1;
			_triIndices[currentIndex + 2] = _triIndices[currentIndex] + 1;
			if (_triIndices[currentIndex + 1] == _triIndices[currentIndex + 2])
				_triIndices[currentIndex + 2] = sphereSegs * (sphereRings - 2) + 1;

			j++;
			currentIndex += 3;
		}

	}

	static void Print(object _s) => MonoBehaviour.print(_s);
}
