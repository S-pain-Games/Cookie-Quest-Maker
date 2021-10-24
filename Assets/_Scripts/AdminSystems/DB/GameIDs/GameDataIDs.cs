
// Easy consistent access to all the game object ID's
public class GameDataIDs
{
    public readonly IDsRepercusions repercusions = new IDsRepercusions();
    public readonly IDStories stories = new IDStories();
    public readonly IDQuestPieces pieces = new IDQuestPieces();
    public readonly IDLocalization localization = new IDLocalization();
}

public class IDsRepercusions
{
    public readonly int center_wolf_dead = "center_wolf_dead".GetHashCode();
    public readonly int center_wolf_alive = "center_wolf_alive".GetHashCode();
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
    public readonly int mainmenu_button_play = "mainmenu_button_play".GetHashCode();
}