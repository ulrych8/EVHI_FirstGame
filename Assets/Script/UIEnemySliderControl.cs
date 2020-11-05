using UnityEngine;

public class UIEnemySliderControl : MonoBehaviour
{
	public bool UseRelativeRotation = true;

	private Quaternion RelativeRotation;
    // Start is called before the first frame update
    void Start()
    {
        RelativeRotation = transform.parent.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (UseRelativeRotation) transform.rotation = RelativeRotation;
    }
}
