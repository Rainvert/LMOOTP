using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CustomButton : Button {

    // Variables necesarias

    [SerializeField] private LocalisationSystem lenSystemRef;
    [SerializeField] private List<CustomButton> botones; 

    [SerializeField] private AudioSource soundMov;

    [SerializeField] private ItemProps item;

    [SerializeField] private NavigationHandler parentHandler;

    [SerializeField] private ScrollRect parentScrollBar;
    [SerializeField] private Button upSelectable;

    [SerializeField] private Button downSelectable;

    [SerializeField] private Button leftSelectable;

    [SerializeField] private Button rightSelectable;
    [SerializeField] private int group;

    [SerializeField] private int posInList;
    [SerializeField] private int otherButtons;

    [SerializeField] private Image iconImageButton;
    [SerializeField] private TextMeshProUGUI textButton;
    [SerializeField] private TextMeshProUGUI precioItem;
    [SerializeField] private float precioModItem;

    // Función onEnable
    void eventInit(){
        lenSystemRef.onIdiomaChange += updateTextLanguageDepend;
    }

    // Función on Disable
    protected override void OnDisable(){
        lenSystemRef.onIdiomaChange -= updateTextLanguageDepend;
        base.OnDisable();
    }

    // Función que permite setear el Navigation Handler Paren
    public void setNavigationHandler(NavigationHandler nav){
        parentHandler = nav;
    }

    // Función encargada de setear el item que le corresponde al botón
    public void setItem(ItemProps itemPrefab){
        item = itemPrefab;
        setContentItem();
    }

    // Función que setea el sistema de localización al botón
    public void setLocalizationSystem(LocalisationSystem loc){
        lenSystemRef = loc;;
        eventInit();
    }

    // Función que aumenta el precio en base al uso del objeto
    public void changeMoneyCostButton(float modiffier){
        precioModItem += Mathf.Floor(precioModItem*modiffier);
        precioItem.text = "$"+precioModItem;
    }

    // Función que obtiene el precio modificado del objeto
    public float getMoneyModCostButton(){
        return precioModItem; 
    }

    // Función encargada de ejecutar la función OnClick del botón
    public void useItem(){
        item.efectoItem(parentHandler,this);
        //parentHandler.updateMoneyUI();
        //Debug.Log("Se compró el item");
    }

    // Función encargada de setear las propiedades del item recibido al botón
    private void setContentItem(){
        //Debug.Log("hola");
        iconImageButton.sprite = item.getIcono();
        precioItem.text = "$"+item.getPrecio();
        precioModItem = item.getPrecio();
        updateTextLanguageDepend();

    }

    // Función que actualiza el texto dependiendo del idioma
    private void updateTextLanguageDepend(){
        textButton.text = lenSystemRef.getLanguajeText(item.getClaveTextItem());
    }

    // Función que setea la posición del botón en la lista de botones del grupo y la cantidad de total de estos (Se utiliza para desplazamiento de UI)
    public void setPosAndOtherButtons(int pos, int otherSelectables){
        posInList = pos;
        otherButtons = otherSelectables;
    }

    // Función encargada de setear el transform Rect al que pertenece el botón
    public void setParentScroll(ScrollRect scroll){
        parentScrollBar = scroll;
    }

    // Función encargada de setear los botones del mismo grupo que existen en escena
    public void setBotonesGrupo(List<CustomButton> lista){
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

    // Función encargada de modificar el comportamiento del Select, de tal manera de desplazar la UI al moverse en los elementos de la lista
    public override void OnSelect(BaseEventData eventData){
        parentScrollBar.verticalNormalizedPosition = 1f - (float) posInList/otherButtons;
        if(parentScrollBar.verticalNormalizedPosition<0.2f){
            parentScrollBar.verticalNormalizedPosition = 0f;
        }

        soundMov.Play();

        //Debug.Log(parentScrollBar.verticalNormalizedPosition);
        base.OnSelect(eventData);
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
            CustomButton sel = botones[i];

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