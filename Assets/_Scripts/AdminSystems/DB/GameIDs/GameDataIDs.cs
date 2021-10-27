
// Easy consistent access to all the game object ID's
public class GameDataIDs
{
    public readonly IDsRepercusions repercusions = new IDsRepercusions();
    public readonly IDStories stories = new IDStories();
    public readonly IDQuestPieces pieces = new IDQuestPieces();
    public readonly IDLocalization localization = new IDLocalization();
    public readonly IDTownLocations townLocations = new IDTownLocations();
    public readonly IDEvents events = new IDEvents();
}

public class IDsRepercusions
{
    public readonly int center_wolf_dead = "towncenter_wolf_dead".GetHashCode();
    public readonly int center_wolf_alive = "towncenter_wolf_alive".GetHashCode();
    public readonly int towncenter_mayor_celebration_happened = "towncenter_mayor_celebration_happened".GetHashCode();
    public readonly int towncenter_mayor_celebration_didnt_happen = "towncenter_mayor_celebration_didnt_happen".GetHashCode();
    public readonly int towncenter_in_ruins = "towncenter_in_ruins".GetHashCode();
    public readonly int towncenter_not_in_ruins = "towncenter_not_in_ruins".GetHashCode();
}

public class IDStories
{
    public readonly int test = "test".GetHashCode();
    public readonly int mayors_problem = "mayors_problem".GetHashCode();
    public readonly int out_of_lactose = "out_of_lactose".GetHashCode();
    public readonly int the_birds_and_the_bees = "the_birds_and_the_bees".GetHashCode();
}

public class IDQuestPieces
{
    public readonly int mayor = "mayor".GetHashCode();
    public readonly int attack = "attack".GetHashCode();
    public readonly int assist = "assist".GetHashCode();
    public readonly int plain_cookie = "plain_cookie".GetHashCode();
    public readonly int plain_cookie_2 = "plain_cookie_2".GetHashCode();
    public readonly int brutally = "brutally".GetHashCode();
    public readonly int kindly = "kindly".GetHashCode();
    public readonly int baseball_bat = "baseball_bat".GetHashCode();
}

public class IDLocalization
{
    public readonly int mainmenu_title = "mainmenu_title".GetHashCode();
    public readonly int mainmenu_button_play = "mainmenu_button_play".GetHashCode();
    public readonly int mainmenu_button_options = "mainmenu_button_options".GetHashCode();
    public readonly int mainmenu_button_credits = "mainmenu_button_credits".GetHashCode();

    public readonly int optionsmenu_title = "optionsmenu_title".GetHashCode();
    public readonly int optionsmenu_text_volume_music = "optionsmenu_text_volume_music".GetHashCode();
    public readonly int optionsmenu_text_volume_effects = "optionsmenu_text_volume_effects".GetHashCode();
    public readonly int optionsmenu_text_language = "optionsmenu_text_language".GetHashCode();

    public readonly int creditsmenu_title = "creditsmenu_title".GetHashCode();

    public readonly int dialoguebox_text = "dialoguebox_text".GetHashCode();
    public readonly int dialoguebox_button_next = "dialoguebox_button_next".GetHashCode();
}

public class IDTownLocations
{
    public readonly int town_center = "town_center".GetHashCode();
}

public class IDEvents
{
    // Story System
    // Callbacks
    public readonly int on_story_started = "on_story_started".GetHashCode();
    public readonly int on_story_completed = "on_story_completed".GetHashCode();
    public readonly int on_story_finalized = "on_story_finalized".GetHashCode();
    public readonly int on_daily_stories_completed = "on_daily_stories_completed".GetHashCode();
    // Commands
    public readonly int start_story = "start_story".GetHashCode();
    public readonly int finalize_story = "finalize_story".GetHashCode();


    // Day system
    // Callbacks
    public readonly int on_day_started = "day_started".GetHashCode();
    public readonly int on_day_ended = "day_ended".GetHashCode();
    // Commands
    public readonly int start_new_day = "start_new_day".GetHashCode();


    // Dialogue System
    // Commands
    public readonly int show_dialogue = "show_dialogue".GetHashCode();


    // Game State System
    // Commands
    public readonly int set_game_state = "set_game_state".GetHashCode();
}