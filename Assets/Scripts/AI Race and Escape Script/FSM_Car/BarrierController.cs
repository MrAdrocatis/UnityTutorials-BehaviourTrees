using UnityEngine;

public class BarrierController : MonoBehaviour
{
    public GameObject[] barriers; // Array rintangan

    public void ToggleBarrier(int index)
    {
        if (index < barriers.Length)
        {
            barriers[index].SetActive(!barriers[index].activeSelf); // Aktif/nonaktif
        }
    }


}

