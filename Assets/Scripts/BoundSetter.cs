using UnityEngine;

//Pour que ça fonction hors playmode
[ExecuteAlways]
[RequireComponent(typeof(MeshFilter))]
public class BoundSetter : MonoBehaviour
{
    public bool visualiserBounds; //Afficher les bounds ou non
    public Vector3 boundingBoxSize = new Vector3(10f, 10f, 10f); // Valeur par défaut
    public Vector3 boundingBoxCenter = new Vector3(0f, 0f, 0f);  // Valeur par défaut

    MeshFilter meshFilter;

    void OnEnable()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    void LateUpdate()
    {
        if (meshFilter == null || meshFilter.sharedMesh == null) return;

        //On retarget le bounding box pour que le mesh modifié ne disparaisse pas lorsque l'objet est originalement hors champ
        if (Application.isPlaying)
        {
            meshFilter.mesh.bounds = new Bounds(boundingBoxCenter, boundingBoxSize);
        }
        else
        {
            meshFilter.sharedMesh.bounds = new Bounds(boundingBoxCenter, boundingBoxSize);
        }
    }

    //Pour que ce soit plus agréable à travailler dans l'éditeur, on dessine des gizmos représentants le nouveau bounding appliqué
    void OnDrawGizmosSelected()
    {
        if (!visualiserBounds) return;

        MeshFilter mf = GetComponent<MeshFilter>();
        if (mf == null || mf.sharedMesh == null) return;

        Gizmos.color = Color.green;

        Bounds b = mf.sharedMesh.bounds;

        // bounds sont en LOCAL space → on les convertit en WORLD
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(b.center, b.size);

        Gizmos.matrix = Matrix4x4.identity;
    }
}