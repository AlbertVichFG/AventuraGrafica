using UnityEngine;

public class Palancas : Interactable
{
    [SerializeField] 
    private bool puertaSecreta;
    [SerializeField] 
    private Animator armarios;
    [SerializeField] 
    private Animator palancaAnim;
    [SerializeField] 
    private GameObject npcMujer;
    [SerializeField] 
    private GameObject puerta;
    [SerializeField] 
    private AscensorScript ascensor;

    public override void Interact(PlayerController player)
    {
        if (puertaSecreta)
        {
            PuertaSecreta();
        }
        else
        {
            ActivarAscensor();
        }     
    }

    void PuertaSecreta()
    {
        armarios.SetTrigger("armariosAbrir");
        palancaAnim.SetTrigger("On");

        npcMujer.SetActive(true);
        puerta.SetActive(true);
    }

    void ActivarAscensor()
    {
        ascensor.isOn = true;
    }
}