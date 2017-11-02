using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenaSpiaggia : MonoBehaviour {

    private int missioneParte = 1;
    private int partiMissione = 3;

    public int conchiglie = 7;
    private int conchiglieRaccolte = 0;

    public bool hasMoney = false;

    public Animator conchigliaAnim;

    public Text conchiglieRaccolteUI;
    public Image hasMoneyUI;

    void Start()
    {
        hasMoneyUI.enabled = hasMoney;
    }

    void Update()
    {
        if (missioneParte == 1)
        {
            if (conchiglieRaccolte < conchiglie)
            {
                if (Input.GetButtonDown(FindObjectOfType<InteractScript>().interactButton))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, FindObjectOfType<InteractScript>().interactDistance))
                    {
                        if (hit.collider.CompareTag("Conchiglia"))
                        {
                            conchiglieRaccolte++;
                            hit.collider.gameObject.SetActive(false);
                        }
                    }
                }
                if (hasMoney)
                {
                    if (Input.GetButtonDown(FindObjectOfType<InteractScript>().interactButton))
                    {
                        //Ray ray = new Ray(FindObjectOfType<InteractScript>().gameObject.transform.position, FindObjectOfType<InteractScript>().gameObject.transform.forward);
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit, FindObjectOfType<InteractScript>().interactDistance))
                        {
                            if (hit.collider.CompareTag("PulsanteAttivo"))
                            {
                                hasMoney = false;
                                conchigliaAnim.SetBool("hasMoney", true);
                                StartCoroutine(DestroyConchiglia());
                            }
                        }
                    }
                }

                if (Input.GetButtonDown(FindObjectOfType<InteractScript>().interactButton))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, FindObjectOfType<InteractScript>().interactDistance))
                    {
                        if (hit.collider.CompareTag("Moneta"))
                        {
                            hasMoney = true;
                            hit.collider.gameObject.SetActive(false);
                        }
                    }
                }

            }

            if (conchiglieRaccolte == conchiglie)
            {
                missioneParte++;
            }
        }

        conchiglieRaccolteUI.text = conchiglieRaccolte + " / " + conchiglie;
        hasMoneyUI.enabled = hasMoney;
    }

    IEnumerator DestroyConchiglia()
    {
        yield return new WaitForSeconds(1.5f);
        conchigliaAnim.gameObject.SetActive(false);
        conchiglieRaccolte++;
    }
}
