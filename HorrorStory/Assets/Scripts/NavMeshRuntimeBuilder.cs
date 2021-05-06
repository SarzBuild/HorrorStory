using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NavMeshRuntimeBuilder : MonoBehaviour
{

    private static NavMeshRuntimeBuilder _instance;

    public static NavMeshRuntimeBuilder Instance { get { return _instance; } }

    List<NavMeshSurface> navMeshSurfaces;

    private NavMeshSurface primary; 

    //NavMeshSource source; 
    // Start is called before the first frame update








    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        navMeshSurfaces = new List<NavMeshSurface>();

    }






    public void RecalculateNavMesh()
    {
        foreach (NavMeshSurface surface in navMeshSurfaces)
        {
            surface.BuildNavMesh();
        }
    }
    // Update is called once per frame

    public void AddSurfaces(List<NavMeshSurface> surfaces)
    {

        foreach (NavMeshSurface surface in surfaces)
        {

            

            navMeshSurfaces.Add(surface);
        }


        //navMeshSurfaces.AddRange(surfaces);
    }
    public void AddSurface(NavMeshSurface surface)
    {
        navMeshSurfaces.Add(surface);
    }
}
