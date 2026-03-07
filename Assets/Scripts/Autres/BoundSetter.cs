/***
 * 
 * ÉTINCELLE
 * 
 * Par Malaïka Abevi
 * Dernière modification : 06/03/2026 
 * 
 */

using UnityEngine;

/// <summary>
/// Permet de redéfinir les bounds (boîte englobante) d'un mesh pour s'assurer
/// qu'il reste visible même hors caméra et fournit une visualisation des bounds dans l'éditeur.
/// </summary>
[ExecuteAlways]
[RequireComponent(typeof(MeshFilter))]
public class BoundSetter : MonoBehaviour
{
    public bool visualiserBounds; // Afficher les bounds ou non
    public Vector3 boundingBoxSize = new Vector3(10f, 10f, 10f); // Valeur par défaut
    public Vector3 boundingBoxCenter = new Vector3(0f, 0f, 0f);  // Valeur par défaut

    MeshFilter meshFilter;

    /// <summary>
    /// Initialise la référence au MeshFilter de l'objet.
    /// </summary>
    void OnEnable()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    /// <summary>
    /// Met à jour les bounds du mesh à chaque frame pour garantir qu'il reste visible.
    /// Fonctionne à la fois en playmode et hors playmode.
    /// </summary>
    void LateUpdate()
    {
        if (meshFilter == null || meshFilter.sharedMesh == null) return;

        // On retarget le bounding box pour que le mesh modifié ne disparaisse pas lorsque l'objet est originalement hors champ
        if (Application.isPlaying)
        {
            meshFilter.mesh.bounds = new Bounds(boundingBoxCenter, boundingBoxSize);
        }
        else
        {
            meshFilter.sharedMesh.bounds = new Bounds(boundingBoxCenter, boundingBoxSize);
        }
    }

    /// <summary>
    /// Dessine les bounds en mode éditeur pour faciliter le placement et le dimensionnement.
    /// </summary>
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