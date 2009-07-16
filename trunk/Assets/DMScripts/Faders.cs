using UnityEngine;
using System.Collections;

public class Faders {
	
	private float fadeSpeed = 0.005f;
	private Color backColor;

    public Faders()
    {
    }

	public void setBackColor( Color c, string textureName ){
			backColor = c;
			updateTexture(textureName);
	}		
	
	public int fadeOut(string textureName ){
			int ans = 0;
		
			backColor.r -= fadeSpeed;
			backColor.g -= fadeSpeed;
			backColor.b -= fadeSpeed;
			
			if(backColor.r < 0) {
				backColor.r = backColor.g = backColor.b = 0.0f;
				ans = 1;
			} 
			
			updateTexture(textureName);
			
			return ans;
		}
		
	public int fadeIn(string textureName ){
			int ans = 0;
		
			backColor.r += fadeSpeed;
			backColor.g += fadeSpeed;
			backColor.b += fadeSpeed;
			
			if(backColor.r >= 0.5f) {
				backColor.r = backColor.g = backColor.b = 0.5f;
				ans=1;
			}
			
			updateTexture(textureName);
			
			return ans;
		}
		
	private void updateTexture(string textureName){
		
			GameObject screenClick = GameObject.Find(textureName);
			if(screenClick != null)
			{
				GUITexture screenBack = (GUITexture)screenClick.GetComponent("GUITexture");
				if(screenBack != null)
				{				
					screenBack.color = backColor;
				} // End if.
			}
			
		}
		
}
