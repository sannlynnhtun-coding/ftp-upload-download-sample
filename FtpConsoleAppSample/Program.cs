using FluentFTP;

string host = "localhost";
string username = "Sann Lynn Htun"; // Server User Name
string password = "password"; // Server Password

// Create an FTP client
using (var client = new FtpClient(host, username, password))
{
    // Connect to the FTP server
    client.Connect();

    // Upload a file
    string localFilePath = @"D:\ftp-server\uploads\file.txt";
    string remoteFilePath = "/downloads/file.txt";
    client.UploadFile(localFilePath, remoteFilePath);

    Console.WriteLine("File uploaded successfully.");

    // Download a file
    string downloadLocalPath = @"D:\ftp-server\uploads\file.txt";
    client.DownloadFile(downloadLocalPath, remoteFilePath);

    Console.WriteLine("File downloaded successfully.");

    // List files in a directory
    foreach (var item in client.GetListing("/downloads"))
    {
        Console.WriteLine(item.Name);
    }

    // Disconnect from the FTP server
    client.Disconnect();
}
