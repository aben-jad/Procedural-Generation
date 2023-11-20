using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMeshManager
{
	private int widthLength, heightLength;
        
	private int startIndex, verticesCount;

	private int currentHiddenFace, oldHiddenFace;
        
	private List<Vector3> vertices;
	private List<Vector3> connectedVertices;
        
	private List<int> triIndices;
	private List<int> hiddenTriIndices;

	private List<Vector2> uvs;

	public List<Vector2> UVs
	{
		get => uvs;
	}
        
	public List<int> TriIndices
	{
		get => triIndices;
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

	public void AddNullUVs()
	{
		for (int i = verticesCount; i < vertices.Count; i++)
			uvs.Add(Vector2.zero);
	}

	public TerrainMeshManager(List<Vector3> _verticesReference, int _startIndex, int _widthLength, int _heightLength, int _currentHiddenFace = 0)
	{
		widthLength = _widthLength;
		heightLength = _heightLength;
		currentHiddenFace = _currentHiddenFace;
		
		startIndex = _startIndex;

		vertices = _verticesReference;

		CreateVertices();

		CreateUVs();

		CreateTriIndices();

		CreateConnectedVertices();
	}

	private void CreateVertices()
	{
		for (int z = 0; z < heightLength; z++)
		{
			for (int x = 0; x < widthLength; x++)
				vertices.Add(new Vector3(x, 0, z));
		}
		verticesCount = vertices.Count;
	}

	private void CreateUVs()
	{
		uvs = new List<Vector2>();

		for (int z = 0; z < heightLength; z++)
		{
			for (int x = 0; x < widthLength; x++)
				uvs.Add(new Vector2((float)x / (widthLength - 1), (float)z / (heightLength - 1)));
		}
	}

	private void CreateTriIndices()
	{
		triIndices = new List<int>();

		for (int z = 0, i = startIndex; z < heightLength - 1; z++)
		{
			for (int x = 0; x < widthLength - 1; x++)
			{
				triIndices.Add(x + z * widthLength);
				triIndices.Add(triIndices[i] + widthLength);
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
}
