using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CustomModButton : Button
{
    // Variables necesarias
    [SerializeField] private List<CustomModButton> botones; 

    [SerializeField] private AudioSource soundMov;

    [SerializeField] private Button upSelectable;

    [SerializeField] private Button downSelectable;

    [SerializeField] private Button leftSelectable;

    [SerializeField] private Button rightSelectable;
    [SerializeField] private int group;


    // Función encargada de setear los botones del mismo grupo que existen en escena
    public void setBotonesGrupo(List<CustomModButton> lista){
        botones = lista;
    }

    // Función que retorna el grupo al que el botón pertenece
    public int getGroup(){
        return group;
    }

    // Función que permite modificar el grupo al que el botón pertenece
    public void setGroup(int value){
        group = value;
    }

    // Funciones sobreescritas que permiten generar las conexiones automáticas de los botones
    public override Selectable FindSelectableOnUp()
    {
        if(upSelectable != null){
            return upSelectable;
        } else{
            return FindSelectableCustom(Vector3.up);
         }
    }

    public override Selectable FindSelectableOnDown()
    {
        if(downSelectable != null){
            return downSelectable;
        } else{
            return FindSelectableCustom(Vector3.down);
        }
    }

    public override Selectable FindSelectableOnLeft()
    {
        if(leftSelectable != null){
            return leftSelectable;
        } else{
            return FindSelectableCustom(Vector3.left);
        }
    }

    public override Selectable FindSelectableOnRight()
    {
        if(rightSelectable != null){
            return rightSelectable;
        } else{
            return FindSelectableCustom(Vector3.right);
        }
    }

    // Función encargada de modificar el comportamiento del Select, de tal manera de desplazar la UI al moverse en los elementos de la lista
    public override void OnSelect(BaseEventData eventData){
        soundMov.Play();

        //Debug.Log(parentScrollBar.verticalNormalizedPosition);
        base.OnSelect(eventData);
    }

    // Función personalizada encargada de realizar las búsqueda entre el listado de botones del grupo y retornar las conexiones de forma automática
    public Selectable FindSelectableCustom(Vector3 dir)
    {
        dir = dir.normalized;
        Vector3 localDir = Quaternion.Inverse(transform.rotation) * dir;
        Vector3 pos = transform.TransformPoint(GetPointOnRectEdge(transform as RectTransform, localDir));
        float maxScore = Mathf.NegativeInfinity;
        float maxFurthestScore = Mathf.NegativeInfinity;
        float score = 0;

        //bool wantsWrapAround = navigation.wrapAround && (m_Navigation.mode == Navigation.Mode.Vertical || m_Navigation.mode == Navigation.Mode.Horizontal);
        bool wantsWrapAround = false;

        Selectable bestPick = null;
        Selectable bestFurthestPick = null;

        for (int i = 0; i < botones.Count; ++i)
        {
            CustomModButton sel = botones[i];

            if (sel == this)
                continue;

            if (!sel.IsInteractable() || sel.navigation.mode == Navigation.Mode.None)
                continue;

    #if UNITY_EDITOR
            // Apart from runtime use, FindSelectable is used by custom editors to
            // draw arrows between different selectables. For scene view cameras,
            // only selectables in the same stage should be considered.
            if (Camera.current != null && !UnityEditor.SceneManagement.StageUtility.IsGameObjectRenderedByCamera(sel.gameObject, Camera.current))
                    continue;
    #endif

            var selRect = sel.transform as RectTransform;
            Vector3 selCenter = selRect != null ? (Vector3)selRect.rect.center : Vector3.zero;
            Vector3 myVector = sel.transform.TransformPoint(selCenter) - pos;

            // Value that is the distance out along the direction.
            float dot = Vector3.Dot(dir, myVector);

            // If element is in wrong direction and we have wrapAround enabled check and cache it if furthest away.
            if (wantsWrapAround && dot < 0)
            {
                score = -dot * myVector.sqrMagnitude;

                if (score > maxFurthestScore)
                {
                    maxFurthestScore = score;
                    bestFurthestPick = sel;
                }
                continue;
            }

            // Skip elements that are in the wrong direction or which have zero distance.
            // This also ensures that the scoring formula below will not have a division by zero error.
            if (dot <= 0)
                continue;

            // This scoring function has two priorities:
            // - Score higher for positions that are closer.
            // - Score higher for positions that are located in the right direction.
            // This scoring function combines both of these criteria.
            // It can be seen as this:
            //   Dot (dir, myVector.normalized) / myVector.magnitude
            // The first part equals 1 if the direction of myVector is the same as dir, and 0 if it's orthogonal.
            // The second part scores lower the greater the distance is by dividing by the distance.
            // The formula below is equivalent but more optimized.
            //
            // If a given score is chosen, the positions that evaluate to that score will form a circle
            // that touches pos and whose center is located along dir. A way to visualize the resulting functionality is this:
            // From the position pos, blow up a circular balloon so it grows in the direction of dir.
            // The first Selectable whose center the circular balloon touches is the one that's chosen.
            score = dot / myVector.sqrMagnitude;

            if (score > maxScore && sel.getGroup() == group)
            {
                maxScore = score;
                bestPick = sel;
            }
        }

        if (wantsWrapAround && null == bestPick) return bestFurthestPick;

        return bestPick;
    }

    // Función que obtiene la distancia entre la dirección y el Rect Transform
    private static Vector3 GetPointOnRectEdge(RectTransform rect, Vector2 dir)
    {
        if (rect == null)
            return Vector3.zero;
        if (dir != Vector2.zero)
            dir /= Mathf.Max(Mathf.Abs(dir.x), Mathf.Abs(dir.y));
        dir = rect.rect.center + Vector2.Scale(rect.rect.size, dir * 0.5f);
        return dir;
    }
}
