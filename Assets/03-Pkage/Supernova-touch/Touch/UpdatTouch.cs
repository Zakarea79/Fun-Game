using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpdatTouch : MonoBehaviour
{
	private EventTrigger Et;
	[SerializeField] private GameObject Joestoke;
	[SerializeField] private ZJoystoke SendDragData;
	[SerializeField] private Image IMGBackgrund , IMGstoke;
	[SerializeField] private Color COlIMGstoleDes , COlIMGstoleSel;
	[SerializeField] private AudioSource Audio;
    void Start()
	{
		Vector3 posJoestoke = SendDragData.gameObject.transform.position;
		Et = gameObject.AddComponent<EventTrigger>();
		
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.Drag;
		entry.callback.AddListener((data) => 
		{
			SendDragData.DragEvent((PointerEventData)data);
		});
		
	    EventTrigger.Entry entryUp = new EventTrigger.Entry();
	    entryUp.eventID = EventTriggerType.PointerUp;
	    entryUp.callback.AddListener((data) => 
	    {
	    	//Joestoke.SetActive(false);
	    	IMGBackgrund.color = COlIMGstoleDes;
	    	IMGstoke.color = COlIMGstoleDes;
	    	//
	    	SendDragData.gameObject.transform.parent.position = posJoestoke;
	    	SendDragData.gameObject.transform.localPosition = new Vector3(0,0,0);
	    	SendDragData.RestData();
	    });

	    EventTrigger.Entry entryDown = new EventTrigger.Entry();
	    entryDown.eventID = EventTriggerType.PointerDown;
	    entryDown.callback.AddListener((data) => 
	    {
		    PointerEventData datav = (PointerEventData)data;
		    Joestoke.transform.position = datav.position;
		    IMGBackgrund.color = COlIMGstoleSel;
		    IMGstoke.color = COlIMGstoleSel;
		    if(Audio != null) Audio.Play();
	    });
	    
	    Et.triggers.Add(entryDown);
		Et.triggers.Add(entryUp);
		Et.triggers.Add(entry);
	    
	}
}
