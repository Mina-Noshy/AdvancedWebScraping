
using WebScraping;
using WebScraping.Services;


Start();

void Start()
{
    ScrapingService service = new ScrapingService();
    int choice = ChooseProccess();
    if (choice == 1)
    {
        service.StartElemetPropertyScraping();
        if (TryAgain())
            Start();
    }
    else if (choice == 2)
    {
        service.StartCustomScraping();
        if (TryAgain())
            Start();
    }
    else if (choice == 0)
        return;
    else
        Start();

}

bool TryAgain()
{
    Console.Write("\nDo you want to try again? (y / n) : ");
    string? choice = Console.ReadLine();
    if (choice.ToUpper() == "N")
        return false;
    else
        return true;
}

int ChooseProccess()
{
    Console.Write("\nPlease Choose Process \n\n1] Element Property Scraping \n2] Custom Scraping \n0] To Cancel \n\nYour Choice ==> ");
    string? choice = Console.ReadLine();
    if (choice == "1")
        return 1;
    else if (choice == "2")
        return 2;
    else if (choice == "0")
        return 0;
    else
        return 404;
}







