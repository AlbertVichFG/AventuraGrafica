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

    public override void Interact(PlayerController player)
    {
        if (puertaSecreta)
        {
            PuertaSecreta();
        }
    }

    void PuertaSecreta()
    {
        armarios.SetTrigger("armariosAbrir");
        palancaAnim.SetTrigger("On");

        npcMujer.SetActive(true);
        puerta.SetActive(true);
    }
}