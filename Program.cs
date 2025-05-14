// See https://aka.ms/new-console-template for more information
//ifconfig - список IP адрес для даного ПК
//ipconfig - для windows
//127.0.0.1 - локальна - тобто цей ПК і
//спілкування в мережах даного ПК
//0.0.0.0 - і з локахост і з мережі де я є. Наприклад до 
//моєму IP адресу в мережі 

using SimpleServer;
using System.Net;
using System.Net.Sockets;
using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;
var hostName = Dns.GetHostName();
Console.WriteLine($"Мій хост: {hostName}");
//Список усіх IP адрес доступних для даного ПК
var locahost = await Dns.GetHostEntryAsync(hostName);

int i = 0;
foreach (var item in locahost.AddressList)
{
    Console.WriteLine($"{++i}.{item}");
}

Console.Write("->_");
int numberIP = int.Parse(Console.ReadLine());

Console.WriteLine("Вкажіть порт:");

int serverPort = int.Parse(Console.ReadLine()); // порт запуску додатка

IPAddress serverIP = locahost.AddressList[numberIP-1];

Console.Title = $"{serverIP}:{serverPort}";

//Згідно цих налаштувань працює наш сервер,
//тобто по цій IP адресі і цьому порту можна буде 
//на сервер надсилати запити і він буде їх обробляти
var ipEndPoint = new IPEndPoint(serverIP, serverPort);

//Створюємо сам сокер, який буде обробляти запити від клієнтів
//Клієнтів може бути багато
//Нашатовуємо наш сокет під мережу Інтернет, у вигляді поток даних
//будуть насилатися нам запити, протокол взаємодії буде TCP
//https - який є надбудовою над TCP і автоматично усе шифрує.
//- це роблем не було.
Socket server = new Socket(AddressFamily.InterNetwork, 
    SocketType.Stream, ProtocolType.Tcp);


server.Bind(ipEndPoint);
server.Listen(10);
ServerContext context = new ServerContext();

while (true)
{
    Socket client = server.Accept();
    Console.WriteLine($"На постукав нвступний носорог {client.RemoteEndPoint}");
    int bytes = 0;
    byte[] buffer = new byte[1024];
    var sb = new StringBuilder();

    do
    {
        bytes = client.Receive(buffer);
        string part = Encoding.UTF8.GetString(buffer, 0, bytes);
        sb.Append(part);
        Console.WriteLine($"Частина повідомлення: {part}");
    } while (client.Available > 0);

    string text = sb.ToString();

    context.Messages.Add(new Message()
    {
        Sender = client.RemoteEndPoint.ToString(),
        Text = text
    });

    context.SaveChanges();

    string message = $"Дякую дружок. {DateTime.Now}";
    buffer = Encoding.UTF8.GetBytes(message);
    client.Send(buffer);
    client.Shutdown(SocketShutdown.Both);
    client.Close();
}



Console.ReadKey();