
using UnityEngine;
using UnityEngine.UI;

public class BetArea : MonoBehaviour
{
    [SerializeField]
    Plan plan;

    string inN;

    Image[] s;

    string[] sN;

    private void Awake()
    {
        sN = gameObject.name.Split(',');
        s = plan.AllS(sN);
    }

    private void OnCollisionStay(Collision collision)
    {
        inN = plan.inN[plan.inN.Count - 1];

        if (gameObject.name == inN)
        {
            for (int i = 0; i < s.Length; i++)
            {
                s[i].color = new Color(s[i].color.r, s[i].color.g, s[i].color.b, Mathf.Abs(Mathf.Sin(plan.sTime) * 0.3f));
            }
            plan.sTime += 0.05f;
        }
    }



    private void OnCollisionExit(Collision collision)
    {
        for (int i = 0; i < sN.Length; i++)
        {
            s[i].color = new Color(s[i].color.r, s[i].color.g, s[i].color.b, 0);
        }
    }

}
