using UnityEngine;
using System.Collections;

public class UIContoller : MonoBehaviour {
	public Canvas StartScreen;

	public Canvas SponsorCanvas;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EnableSponsorScreen()
	{
		StartScreen.enabled = false;
		SponsorCanvas.enabled = true;
	}

	public void EnableStartScreen()
	{
		StartScreen.enabled = true;
		SponsorCanvas.enabled = false;
	}

	public void OpenJenisPage()
	{
		Application.OpenURL ("https://jenis.com/");
	}

	public void OpenUnityPage()
	{
		Application.OpenURL ("http://unity3d.com/");
	}

	public void OpenLumosPage()
	{
		Application.OpenURL ("http://www.lumoscolumbus.com/");
	}

	public void OpenZocoPage()
	{
		Application.OpenURL ("http://zocodesign.com/");
	}

	public void OpenPackH20Page()
	{
		Application.OpenURL ("http://www.packh2o.com/index.html");
	}

	public void OpenStartUpGringPage()
	{
		Application.OpenURL ("http://startupgrind.com/columbus/");
	}

	public void OpenAWHPage()
	{
		Application.OpenURL ("http://www.awh.net/");
	}

	public void OpenQstartPage()
	{
		Application.OpenURL ("http://www.qstartlabs.com/index.html");
	}

	public void OpenBeamPage()
	{
		Application.OpenURL ("http://beamtoothbrush.com/");
	}

	public void OpenCPAPage()
	{
		Application.OpenURL ("http://www.theshortnorthcpa.com/#simplify");
	}

	public void OpenSBOOAPage()
	{
		Application.OpenURL ("http://www.sbooa.com/");
	}

	public void OpenLaborGenomePage()
	{
		Application.OpenURL ("http://laborgenome.com/");
	}

	public void OpenBungalowPage()
	{
		Application.OpenURL ("http://www.bungalowhome.com/");
	}

	public void OpenCivicHacksPage()
	{
		Application.OpenURL ("http://www.civichacks.org/");
	}




}
