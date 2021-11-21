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

        //A futuro, los diálogos dependerán de la felicidad del propio NPC
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
            dialog.Add("¡Después de esta mala racha por fín parece que veremos la luz! ¡Esto hay que celebrarlo con un banquete!");
        else if (happinessValue <= 0)
            dialog.Add("Mi gente me odia después de todo lo que he hecho por ellos. Estoy planteandome dimitir.");
        else
            dialog.Add("No estaremos en nuestro mejor momento, ¡Pero saldremos adelante!");


        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(dialog, _charId, () => { this.EndNPCDialog(); }));

    }

    private void ShowMeriDialog(int happinessValue)
    {
        List<string> dialog = new List<string>();

        if (happinessValue >= 0)
            dialog.Add("¡Por primera vez en mucho tiempo mis vacas están felices! Sufrir tanto ha merecido la pena.");
        else if (happinessValue <= 0)
            dialog.Add("¡Mis vacas están para el arrastre! Las pobres no pueden con tanto estrés.");
        else
            dialog.Add("Mis vacas y yo sobreviviremos a las adversidades del día a día.");


        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(dialog, _charId, () => { this.EndNPCDialog(); }));
    }

    private void ShowCanelaDialog(int happinessValue)
    {
        List<string> dialog = new List<string>();

        if (happinessValue >= 0)
            dialog.Add("¡Mi exposición de artefactos se amplía más y más! Mucha gente de fuera quiere venir a visitarla.");
        else if (happinessValue <= 0)
            dialog.Add("¡Mi colección está echa una ruina! Si esto sigue así me llamarán Canela la Chatarrera.");
        else
            dialog.Add("Mi gran colección sigue a salvo, a pesar de todos estos imprevistos.");


        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(dialog, _charId, () => { this.EndNPCDialog(); }));
    }

    private void ShowChocolateDialog(int happinessValue)
    {
        List<string> dialog = new List<string>();

        if (happinessValue >= 0)
            dialog.Add("¡Mi investigación va de maravilla ultimamente! ¡Pronto seré más famosa que Canela!");
        else if (happinessValue <= 0)
            dialog.Add("Estoy planteandome seriamente trasladar mi laboratorio a la ciudad. ¡Aquí no hay quien trabaje!");
        else
            dialog.Add("De vez en cuando ocurren imprevistos, pero al menos puedo seguir investigando.");


        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(dialog, _charId, () => { this.EndNPCDialog(); }));
    }

    private void ShowMantecasDialog(int happinessValue)
    {
        List<string> dialog = new List<string>();

        if (happinessValue >= 0)
            dialog.Add("Parece que por fín podré disfrutar de un par de días tranquilos en mi granja.");
        else if (happinessValue <= 0)
            dialog.Add("Las plagas siguen asolando mis campos. ¡¿De dónde narices salen?!");
        else
            dialog.Add("¡Todo sigue igual! ¡Nada más que problemas todos los días! No hay tiempo para aburrirse.");


        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(dialog, _charId, () => { this.EndNPCDialog(); }));
    }

    private void ShowJohnnyDialog(int happinessValue)
    {
        List<string> dialog = new List<string>();

        if (happinessValue >= 0)
            dialog.Add("¡Últimamente todo me va genial, colega!");
        else if (happinessValue <= 0)
            dialog.Add("No tiene pinta de que la cosa vaya a mejorar, colega.");
        else
            dialog.Add("Parece que la cosa sigue como está, colega.");


        _showDialogueCmd.Invoke(new ShowDialogueEvtArgs(dialog, _charId, () => { this.EndNPCDialog(); }));
    }

    private void EndNPCDialog()
    {
        character.SetInputActivated(true);
    }

}
