using UnityEngine;
using System.Collections;

public class Healthbar : MonoBehaviour 
{
    GUIStyle healthStyle;
    GUIStyle backStyle;
    PlayerController p;
    public Texture tex;
    public Texture deathScreen;
    public Texture face1;
    public Texture face2;
    public Texture face3;
    public Texture face4;
    public Texture face5;

    void Awake()
    {
        p = GetComponent<PlayerController>();
    }

    void OnGUI()
    {
        { 
            InitStyles();

            // Draw a Health Bar

            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            GUI.DrawTexture(new Rect(0, Screen.height-150, 300, 150), tex);
            if (p.getHP() >= 75)
            {
                GUI.DrawTexture(new Rect(21, Screen.height - 121, 81, 81), face1);
            }
            else if (p.getHP() >= 50)
            {
                GUI.DrawTexture(new Rect(21, Screen.height - 121, 81, 81), face2);
            }
            else if (p.getHP() >= 25)
            {
                GUI.DrawTexture(new Rect(21, Screen.height - 121, 81, 81), face3);
            }
            else if (p.getHP() > 0)
            {
                GUI.DrawTexture(new Rect(21, Screen.height - 121, 81, 81), face4);
            }  
            else
            {
                GUI.DrawTexture(new Rect(21, Screen.height - 121, 81, 81), face5);
            }

            // draw health bar background
            GUI.color = Color.grey;
            GUI.backgroundColor = Color.grey;
            GUI.Box(new Rect(111, Screen.height - 132, (p.getMaxHP() * 1.5f) + 2, 30), ".", backStyle);

            // draw health bar amount
            GUI.color = Color.green;
            GUI.backgroundColor = Color.green;
            GUI.Box(new Rect(113, Screen.height - 130 , p.getHP() * 1.5f, 28), ".", healthStyle);
        }
    }

    void InitStyles()
    {
        if (healthStyle == null)
        {
            healthStyle = new GUIStyle(GUI.skin.box);
            healthStyle.normal.background = MakeTex(2, 2, new Color(0f, 1f, 0f, 1.0f));
        }

        if (backStyle == null)
        {
            backStyle = new GUIStyle(GUI.skin.box);
            backStyle.normal.background = MakeTex(2, 2, new Color(0f, 0f, 0f, 1.0f));
        }
    }

    Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }

}