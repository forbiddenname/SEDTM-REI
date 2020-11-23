using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
using Vectrosity;
using UnityEditor;
public class JointAngleShow : MonoBehaviour
{

    GameObject[] angleArray = new GameObject[54];
    
    Vector3[] positions = new Vector3[8];
    string relpos;
    Dictionary<int, Color> color_E = new Dictionary<int, Color>()
    {
      {0, new Color32(243, 38, 9,253)},
      {1, new Color32(239, 243, 9,253)},
      {2, new Color32(32, 147, 245,253)},
      {3, new Color32(16, 241, 37,255)},
      {4, new Color32(238, 242, 243,255)},
      {5, new Color32(248, 144, 2,255)},
      {6, new Color32(248, 243, 19,255)},
      {7, new Color32(245, 245, 236,255)}
        
    };
    Dictionary<int, string> rel_E = new Dictionary<int, string>()
    {
      {0, "nothing"},
      {1, "above"},
      {2, "behind"},
      {3, "front"},
      {4, "below"},
      {5, "left"},
      {6, "right"},
      {7, "on"},
      {8, "under"},
      {9, "in"},
      {10, "with"},
      {11, "hold"},
      {12, "has"},
      {13, "stiffer"},
      {14, "higher"}
    };
    Dictionary<string, int> rel_pos = new Dictionary<string, int>()
    {
        {"01", 26},
        {"02", 27},
        {"03", 28},
        {"04", 29},
        {"05", 30},
        {"06", 31},
        {"07", 32},
        {"12", 33},
        {"13", 34},
        {"14", 35},
        {"15", 36},
        {"16", 37},
        {"17", 38},
        {"23", 39},
        {"24", 40},
        {"25", 41},
        {"26", 42},
        {"27", 43},
        {"34", 44},
        {"35", 45},
        {"36", 46},
        {"37", 47},
        {"45", 48},
        {"46", 49},
        {"47", 50},
        {"56", 51},
        {"57", 52},
        {"67", 53}
    };

    void InitObject()
    {
        string Angle;
        for (int i = 0; i < angleArray.Length; i++)
        {
            
            Angle = string.Format("angle{0}", i); 
            angleArray[i] = GameObject.Find(Angle);
            
        }
   
    }

    void Start()
    {

        InitObject();

    }
  
    void setInactive ()
    {
        for (int j =0; j < 28; j++)
        {
            angleArray[j+26].SetActive(false);
        }
         
    }

    private void DrawArrow(int from, int rel, int to)
    {
     
        Vector3 v0 = positions[from] - positions[to];
        v0 *= 1/ v0.magnitude;
        Vector3 v1 = new Vector3(v0.x * 0.866f - v0.y * 0.5f, v0.x * 0.5f + v0.y * 0.866f, 0);
        Vector3 v2 = new Vector3(v0.x * 0.866f + v0.y * 0.5f, v0.x * -0.5f + v0.y * 0.866f, 0);

        Debug.DrawLine (positions[from], positions[to], color_E[from],0);
        Debug.DrawLine(positions[to], positions[to] + v1,color_E[from],0);
        Debug.DrawLine(positions[to], positions[to] + v2,color_E[from],0);
        
        if (from > to)
        {
            relpos = to.ToString() + from.ToString();
        }
        else
        {
            relpos = from.ToString() + to.ToString();
        }
        
        angleArray[rel_pos[relpos]].SetActive(true);
        angleArray[rel_pos[relpos]].GetComponent<InputField>().text = rel_E[rel];
    }

    void Update()
    {   
        
        setInactive ();

        for (int i = 0; i < 8; i++)
        {
        positions[i] = angleArray[i+18].transform.position;
        }

        int length = Main.datalist.Count;
        
        for (int i = 0; i < length; i++)
        {
            
            if (i == 0)
            {
                for (int j = 0; j<6; j++)
                {
                        angleArray[j].GetComponent<InputField>().text = Main.datalist[i][j].ToString() + "°";
                        
                }

            }
            else if (i==1)
            {
                for (int j = 0; j<6; j++)
                {
                        angleArray[j+6].GetComponent<InputField>().text = Main.datalist[i][j].ToString() + "°";
                        
                }
            }

            else if (i==2)
            {
                for (int j = 0; j<6; j++)
                {
                        angleArray[j+12].GetComponent<InputField>().text = Main.datalist[i][j].ToString() + "°";
                        
                }
            }


            else if (i > 4 && Main.datalist[i].Length > 1)
            {
               
               int i1 = (int)Main.datalist[i][0];
               int i2 = (int)Main.datalist[i][1];
               int i3 = (int)Main.datalist[i][2];
            
            
               if (i1 != i3)
               {
                   DrawArrow (i1, i2, i3);
               }
            }
        } 
    }
}