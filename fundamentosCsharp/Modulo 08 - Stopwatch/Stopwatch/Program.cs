// See https://aka.ms/new-console-template for more information
Menu();

static void Menu()
{
    Console.Clear();
    Console.WriteLine("S - Segundos");
    Console.WriteLine("M - Minuto");
    Console.WriteLine("0 - Sair");
    Console.WriteLine("Quanto tempo você deseja contar?");

    
}

static void Start(int time)
{    
    int currentTime = 0;
    while (currentTime != time)
    {
        Console.Clear();
        currentTime++;
        Console.WriteLine(currentTime);
        Thread.Sleep(1000);
    }

    Console.Clear();
    Console.WriteLine("Stopwatch finalizado!");
    Thread.Sleep(2000);
}