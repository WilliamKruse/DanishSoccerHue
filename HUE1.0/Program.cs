using Q42.HueApi;
using Q42.HueApi.ColorConverters;
using Q42.HueApi.ColorConverters.Original;
using Q42.HueApi.Interfaces;
using Newtonsoft.Json;


SoccerService service = new SoccerService();


Console.WriteLine("Tryk enter for at starte app..");
Console.ReadLine();
Console.WriteLine("Finder mulig Bridge...");
IBridgeLocator locator = new HttpBridgeLocator(); //Or: LocalNetworkScanBridgeLocator, MdnsBridgeLocator, MUdpBasedBridgeLocator
var bridges = await locator.LocateBridgesAsync(TimeSpan.FromSeconds(5));
bridges = await HueBridgeDiscovery.CompleteDiscoveryAsync(TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(30));

Console.WriteLine("Bridge fundet: Skriv din IP addresse du kan finde i din hue app.");
string MinIp = Console.ReadLine();
ILocalHueClient client = new LocalHueClient(MinIp);


Console.WriteLine("Har du allerede coneccted før? ja/nej");
string svar = Console.ReadLine();
if (svar == "ja")
{
    Console.WriteLine("Skriv din nøgle her");
    string nøgle = Console.ReadLine();
    client.Initialize(nøgle);
}
else
{
    Console.WriteLine("tryk på din hue knap inden 10 sekunder er gået");
    Thread.Sleep(10000);
    var appKey = await client.RegisterAsync("ViErRødeViErHvide", "MinComputer");
    Console.WriteLine("Din app er nu registreret, her er din nøgle til fremtidig brug: " + appKey);
}

Console.WriteLine("Tryk enter for at starte lortet");
Console.ReadLine();
    var lysrød = new LightCommand();
    var lyshvid = new LightCommand();

while (true)
{
    bool goalchecker = false;
    goalchecker = await service.GetTheData();
    Console.WriteLine("API kaldt, status: " + goalchecker);
    if (goalchecker)
    {
        int count = 0;
        while (true)
        {
            await client.SendCommandAsync(lysrød.TurnOn().SetColor(new RGBColor("FF0000")));
            Thread.Sleep(500);
            await client.SendCommandAsync(lyshvid.TurnOn().SetColor(new RGBColor("FFFFFF")));
            Thread.Sleep(500);
            count++;
            if (count == 20)
            {
                break;
            }
        }
    }
    Thread.Sleep(2000);
}

    public class SoccerService
{
    public int Goals { get; set; } = 0;
    public bool firsttime { get; set; } = true;
    public SoccerService()
    {
        this.Goals = 0;
        this.firsttime = true;
    }


    public async Task<bool> GetTheData()
    {
       
        HttpClient cli = new HttpClient();
        const string url = "https://api.sofascore.com/api/v1/sport/football/events/live";
        var response = await cli.GetAsync(url);
        var jsonString = await response.Content.ReadAsStringAsync();
        dynamic dyn = JsonConvert.DeserializeObject(jsonString);
        foreach (var obj in dyn.events)
        {
            if (obj.homeTeam.name == "Club Destroyers")
            {
                Console.WriteLine("hold fundet");
                if (obj.homeScore.current > Goals)
                {
                    Goals = obj.homeScore.current;
                    if (firsttime)
                    {
                        firsttime = false;
                        return false;
                    }
                    Console.WriteLine("Danmark har scoret!!!");
                    return true;
                }
            }
            if (obj.awayTeam.name == "Club Destroyers")
            {
                Console.WriteLine("hold fundet");
                if (obj.awayScore.current > Goals)
                {
                   
                    Goals = obj.awayScore.current;
                    if (firsttime)
                    {
                        firsttime = false;
                        return false;
                    }
                    Console.WriteLine("Danmark har scoret!!!");
                    return true;
                }
            }        
        }

        return false;

    }
}
