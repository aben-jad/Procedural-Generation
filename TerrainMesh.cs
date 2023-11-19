using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMesh
{
	private int xLength, zLength;

	private int currentHiddenFace, oldHiddenFace;

	private List<Vector3> vertices;
	private List<Vector3> connectedVertices;

	private List<int> triIndices;
	private List<int> hiddenTriIndices;

	public List<int> TriIndices
	{
		get => triIndices;
	}

	public List<Vector3> Vertices
	{
		get => vertices;
	}

	public List<Vector3> ConnectedVertices
	{
		get => connectedVertices;
	}

	public int HiddenFace
	{
		get => currentHiddenFace;
		set
		{
			//if (value * 6 < triIndices.Count - 6)
			{
				oldHiddenFace = currentHiddenFace;
				currentHiddenFace = value;
				UpdateTriIndices();
				UpdateConnectedVertices();
			}
		}
	}

	public TerrainMesh(int _xLength, int _zLength, int _currentHiddenFace = 0)
	{
		xLength = _xLength;
		zLength = _zLength;
		currentHiddenFace = _currentHiddenFace;

		CreateVertices();

		CreateTriIndices();

		CreateConnectedVertices();
	}

	private void CreateVertices()
	{
		vertices = new List<Vector3>();
		for (int z = 0; z < zLength; z++)
		{
			for (int x = 0; x < xLength; x++)
				vertices.Add(new Vector3(x, 0, z));
		}

	}

	private void CreateTriIndices()
	{
		triIndices = new List<int>();

		for (int z = 0, i = 0; z < zLength - 1; z++)
		{
			for (int x = 0; x < xLength - 1; x++)
			{
				triIndices.Add(x + z * xLength);
				triIndices.Add(triIndices[i] + xLength);
				triIndices.Add(triIndices[i] + 1);
				triIndices.Add(triIndices[i + 1]);
				triIndices.Add(triIndices[i + 1] + 1);
				triIndices.Add(triIndices[i + 2]);
				i += 6;
			}
			
		}


		hiddenTriIndices = triIndices.GetRange(currentHiddenFace * 6, 6);

		triIndices.RemoveRange(currentHiddenFace * 6 ,6);
	}

	private void CreateConnectedVertices()
	{
		connectedVertices = new List<Vector3>();

		connectedVertices.Add(vertices[hiddenTriIndices[0]]);
		connectedVertices.Add(vertices[hiddenTriIndices[2]]);
		connectedVertices.Add(vertices[hiddenTriIndices[1]]);
		connectedVertices.Add(vertices[hiddenTriIndices[4]]);
	}

	private void UpdateConnectedVertices()
	{
		connectedVertices[0] = vertices[hiddenTriIndices[0]];
		connectedVertices[1] = vertices[hiddenTriIndices[2]];
		connectedVertices[2] = vertices[hiddenTriIndices[1]];
		connectedVertices[3] = vertices[hiddenTriIndices[4]];
	}

	private void UpdateTriIndices()
	{
		triIndices.InsertRange(oldHiddenFace * 6, hiddenTriIndices);

		hiddenTriIndices = triIndices.GetRange(currentHiddenFace * 6, 6);

		triIndices.RemoveRange(currentHiddenFace * 6, 6);
	}

	void Print(int[] _arr)
	{
		for(int i = 0; i < _arr.Length; i++)
			Debug.Log(i + " " + _arr[i]);
	}
}
