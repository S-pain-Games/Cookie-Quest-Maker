using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingNPC : MonoBehaviour, IInteractableEntity
{
    private Event<ShowDialogueEvtArgs> _showDialogueCmd;
    [SerializeField] string _characterName;

    private ID _charId;

    [SerializeField] private AgentMouseListener character;

    private void Start()
    {
        var evtSys = Admin.Global.EventSystem;
        _showDialogueCmd = evtSys.GetCommandByName<Event<ShowDialogueEvtArgs>>("dialogue_sys", "show_dialogue");


        _charId = new ID(_characterName);
    }


    public void OnInteract()
    {
        character.SetInputActivated(false);

        //A futuro, los di�logos depender�n de la felicidad del propio NPC
        int happiness = Admin.Global.Components.m_TownComponent.m_GlobalHappiness;

        switch (_characterName)
        {
            case "mayor":   ShowMayorDialog(happiness); break;
            case "meri": ShowMeriDialog(happiness); break;
            case "canela": ShowCanelaDialog(happiness); break;
            case "miss_chocolate": ShowChocolateDialog(happiness); break;
            case "mantecas": ShowMantecasDialog(happiness); break;
            case "johny_setas": ShowJohnnyDialog(happiness); break;
        }
    }

    private void ShowMayorDialog(int happinessValue)
    {
        List<string> dialog = new List<string>();

        if (happinessValue >= 0)
            dialog.Add("�Despu�s de esta mala racha por f�n parece que veremos la luz! �Esto hay que celebrarlo con un banquete!");
        else if (happinessValue <= 0)
            dialog.Add("Mi gente me odia despu�s de todo lo que he hecho por ellos. Estoy planteandome dimitir.");
        else
            dialog.Add("No estaremos en nuestro mejor momento, �Pero saldremos adelante!");


        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(dialog, _charId, () => { this.EndNPCDialog(); }));

    }

    private void ShowMeriDialog(int happinessValue)
    {
        List<string> dialog = new List<string>();

        if (happinessValue >= 0)
            dialog.Add("�Por primera vez en mucho tiempo mis vacas est�n felices! Sufrir tanto ha merecido la pena.");
        else if (happinessValue <= 0)
            dialog.Add("�Mis vacas est�n para el arrastre! Las pobres no pueden con tanto estr�s.");
        else
            dialog.Add("Mis vacas y yo sobreviviremos a las adversidades del d�a a d�a.");


        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(dialog, _charId, () => { this.EndNPCDialog(); }));
    }

    private void ShowCanelaDialog(int happinessValue)
    {
        List<string> dialog = new List<string>();

        if (happinessValue >= 0)
            dialog.Add("�Mi exposici�n de artefactos se ampl�a m�s y m�s! Mucha gente de fuera quiere venir a visitarla.");
        else if (happinessValue <= 0)
            dialog.Add("�Mi colecci�n est� echa una ruina! Si esto sigue as� me llamar�n Canela la Chatarrera.");
        else
            dialog.Add("Mi gran colecci�n sigue a salvo, a pesar de todos estos imprevistos.");


        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(dialog, _charId, () => { this.EndNPCDialog(); }));
    }

    private void ShowChocolateDialog(int happinessValue)
    {
        List<string> dialog = new List<string>();

        if (happinessValue >= 0)
            dialog.Add("�Mi investigaci�n va de maravilla ultimamente! �Pronto ser� m�s famosa que Canela!");
        else if (happinessValue <= 0)
            dialog.Add("Estoy planteandome seriamente trasladar mi laboratorio a la ciudad. �Aqu� no hay quien trabaje!");
        else
            dialog.Add("De vez en cuando ocurren imprevistos, pero al menos puedo seguir investigando.");


        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(dialog, _charId, () => { this.EndNPCDialog(); }));
    }

    private void ShowMantecasDialog(int happinessValue)
    {
        List<string> dialog = new List<string>();

        if (happinessValue >= 0)
            dialog.Add("Parece que por f�n podr� disfrutar de un par de d�as tranquilos en mi granja.");
        else if (happinessValue <= 0)
            dialog.Add("Las plagas siguen asolando mis campos. ��De d�nde narices salen?!");
        else
            dialog.Add("�Todo sigue igual! �Nada m�s que problemas todos los d�as! No hay tiempo para aburrirse.");


        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(dialog, _charId, () => { this.EndNPCDialog(); }));
    }

    private void ShowJohnnyDialog(int happinessValue)
    {
        List<string> dialog = new List<string>();

        if (happinessValue >= 0)
            dialog.Add("��ltimamente todo me va genial, colega!");
        else if (happinessValue <= 0)
            dialog.Add("No tiene pinta de que la cosa vaya a mejorar, colega.");
        else
            dialog.Add("Parece que la cosa sigue como est�, colega.");


        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(dialog, _charId, () => { this.EndNPCDialog(); }));
    }

    private void EndNPCDialog()
    {
        character.SetInputActivated(true);
    }

}
