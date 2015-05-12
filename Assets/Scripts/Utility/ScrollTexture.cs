using UnityEngine;
using System.Collections;

public class ScrollTexture : MonoBehaviour {
    public float speed;

	public enum GameMode
	{
		ThirdWorld,
		WaterMiniGame,
		FarmingMiniGame,
		MilkingMiniGame
	}
	public GameMode CurrentGameMode;

    void Update()
	{
		if (CurrentGameMode == GameMode.WaterMiniGame && WaterGameLogic.Instance.ReadyToPlay == false) return;
        //Offset += Scroll * Time.deltaTime;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2((Time.time * speed) % 1, 0f);
        //renderer.material.SetTextureOffset("_MainTex", Offset);
    }
}
