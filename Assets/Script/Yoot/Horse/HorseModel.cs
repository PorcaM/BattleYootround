using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Horse))]
public class HorseModel : MonoBehaviour {
    public Horse horse;
    public GameObject childHorse;
    public List<GameObject> children;
    private const float size = 0.5f;

    void Start()
    {
        children = new List<GameObject>();
        horse = GetComponent<Horse>();
    }

    public void AddChildren()
    {
        int numChild = horse.weight - 1;
        if (numChild > 3)
            numChild = 3;
        int numNewChild = numChild - children.Count;
        for (int i = 0; i < numNewChild; ++i)
        {
            int childID = i + numChild;
            GameObject child = Instantiate(childHorse, transform);
            Destroy(child.GetComponent<HorseModel>());
            Destroy(child.GetComponent<HorseAnimator>());
            Destroy(child.GetComponent<Horse>());
            const float angle = -30.0f;
            child.transform.localPosition = new Vector3(5 * Mathf.Cos(childID * angle), 0, 5 * Mathf.Sin(childID * angle));
            child.transform.localScale = new Vector3(size, size, size);
            children.Add(child);
        }
    }
}
