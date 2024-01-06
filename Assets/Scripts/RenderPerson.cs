using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderPerson : MonoBehaviour
{
    public bool IsEnemy;
    public Material EnemyMaterial;
    public Material CivilianMaterial;
    // Start is called before the first frame update
    void Start()
    {
         var meshes = GetComponentsInChildren<MeshRenderer>();
         Material mat;
        if (IsEnemy){
            mat = EnemyMaterial;
        } else {
            mat = CivilianMaterial;
        }
        foreach (var mesh in meshes)
        {
            mesh.material = mat;
        }
        
    }
}
