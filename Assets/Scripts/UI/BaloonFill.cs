using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaloonFill : MonoBehaviour
{

    public Image baloonFill;
    public WallCollider wallCollider;
    LevelProgressUI levelProgress;
    private void Start()
    {
        levelProgress = FindObjectOfType<LevelProgressUI>();   
    }
    void Update()
    {
    	if (wallCollider!=null)
    	{
		if (wallCollider.triggered == true)
		{
		    BalonFill(1f);
		    levelProgress.enabled = true;

		}}
    }
    public void BalonFill(float value)
    {
        baloonFill.fillAmount = value;
    }
}
