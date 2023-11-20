using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeshManager
{
	private List<Vector3> vertices;
	private List<Vector3> connectedVertices;

	private List<int> triIndices;

	private int verticesCount;

	public List<int> TriIndices
	{
		get => triIndices;
	}

	private int startIndex;

	public PlayerMeshManager(List<Vector3> _verticesReference, int _startIndex, List<Vector3> _connectedVertices)
	{
		vertices = _verticesReference;

		connectedVertices = _connectedVertices;

		CreatePlayerVertices();

		startIndex = _startIndex;
		CreatePlayerTriIndices();
	}

	private void CreatePlayerVertices()
	{

		for (int i = 0; i < connectedVertices.Count; i++)
		{
			vertices.Add(connectedVertices[i]);
		}

		vertices.Add(connectedVertices[0] + Vector3.up);
		vertices.Add(connectedVertices[1] + Vector3.up);
		vertices.Add(connectedVertices[2] + Vector3.up);
		vertices.Add(connectedVertices[3] + Vector3.up);

		verticesCount = 6;

	}

	public void UpdateVertices()
	{
		for (int i = 0; i < connectedVertices.Count; i++)
			vertices[i + startIndex] = connectedVertices[i];

		vertices[4 + startIndex] = connectedVertices[0] + Vector3.up;
		vertices[5 + startIndex] = connectedVertices[1] + Vector3.up;
		vertices[6 + startIndex] = connectedVertices[2] + Vector3.up;
		vertices[7 + startIndex] = connectedVertices[3] + Vector3.up;
	}

	private void CreatePlayerTriIndices()
	{
		triIndices = new List<int>();

		triIndices.Add(startIndex + 0);
		triIndices.Add(startIndex + 4);
		triIndices.Add(startIndex + 1);

		triIndices.Add(startIndex + 4);
		triIndices.Add(startIndex + 5);
		triIndices.Add(startIndex + 1);

		triIndices.Add(startIndex + 0);
		triIndices.Add(startIndex + 2);
		triIndices.Add(startIndex + 4);

		triIndices.Add(startIndex + 2);
		triIndices.Add(startIndex + 6);
		triIndices.Add(startIndex + 4);

		triIndices.Add(startIndex + 2);
		triIndices.Add(startIndex + 3);
		triIndices.Add(startIndex + 6);

		triIndices.Add(startIndex + 3);
		triIndices.Add(startIndex + 7);
		triIndices.Add(startIndex + 6);

		triIndices.Add(startIndex + 1);
		triIndices.Add(startIndex + 5);
		triIndices.Add(startIndex + 3);

		triIndices.Add(startIndex + 5);
		triIndices.Add(startIndex + 7);
		triIndices.Add(startIndex + 3);

		triIndices.Add(startIndex + 5);
		triIndices.Add(startIndex + 4);
		triIndices.Add(startIndex + 6);

		triIndices.Add(startIndex + 5);
		triIndices.Add(startIndex + 6);
		triIndices.Add(startIndex + 7);
	}

}
