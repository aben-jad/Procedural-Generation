using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMesh
{
	private List<Vector3> vertices;
	//private List<Vector3> connectedVertices;

	public List<int> triIndices;

	private int startIndex;

	public PlayerMesh(List<Vector3> _connectedVertices, int _startIndex)
	{
		CreatePlayerVertices(_connectedVertices);

		startIndex = _startIndex;
		CreatePlayerTriIndices();
	}

	private void CreatePlayerVertices(List<Vector3> _connectedVertices)
	{
		vertices = new List<Vector3>();

		for (int i = 0; i < _connectedVertices.Count; i++)
			vertices.Add(_connectedVertices[i]);
	}

	public void UpdatePlayerVertices(List<Vector3> _connectedVertices)
	{
		for (int i = 0; i < _connectedVertices.Count; i++)
			vertices[i] = _connectedVertices[i];
	}

	private void CreatePlayerTriIndices()
	{
		triIndices = new List<int>();

		triIndices.Add(startIndex + 0);
		triIndices.Add(startIndex + 2);
		triIndices.Add(startIndex + 1);

		triIndices.Add(startIndex + 2);
		triIndices.Add(startIndex + 3);
		triIndices.Add(startIndex + 1);
	}

}
