using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceAppear : MonoBehaviour
{
  public Renderer rend;

  public Transform[] trackedObjects;

  public Vector4[] vectorPositions;
  public Vector4[] emptySpace = new Vector4[70];

  public MaterialPropertyBlock materialProperty;

  public int arrayLength;

  // Start is called before the first frame update
  void Start()
  {
    vectorPositions = new Vector4[trackedObjects.Length];
    materialProperty = new MaterialPropertyBlock();

    for (int i = 0; i < trackedObjects.Length; i++)
    {
      vectorPositions[i] = trackedObjects[i].position;
    }

    materialProperty.SetInt("ArrayLength", arrayLength);
    //Vector4[] emptySpace = new Vector4[70];
    materialProperty.SetVectorArray("positionsArray", emptySpace);
    rend.SetPropertyBlock(materialProperty);
  }

  // Update is called once per frame
  void Update()
  {
    //foreach (var trackedObject in trackedObjects)
    //{
    //  rend.sharedMaterial.SetVector("_ObjectPosition", trackedObject.position);
    //}

    //arrayLength = vectorPositions.Length;
    for (int p = 0; p < vectorPositions.Length; p++)
    {
      vectorPositions[p] = Vector4.zero;
    }

    for (int i = 0; i < trackedObjects.Length; i++)
    {
      vectorPositions[i] = trackedObjects[i].position;
    }
    materialProperty.SetInt("ArrayLength", arrayLength);
    materialProperty.SetVectorArray("positionsArray", vectorPositions);
    rend.SetPropertyBlock(materialProperty);
  }
}
