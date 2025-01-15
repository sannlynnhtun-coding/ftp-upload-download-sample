using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentFTP;

namespace FtpConsoleAppSample;

public class FtpService
{
    private readonly string _host;
    private readonly string _username;
    private readonly string _password;

    public FtpService(string host, string username, string password)
    {
        _host = host;
        _username = username;
        _password = password;
    }

    // Upload a file to the FTP server
    public void UploadFile(string localFilePath, string remoteFilePath)
    {
        using (var client = new FtpClient(_host, _username, _password))
        {
            client.Connect();
            client.UploadFile(localFilePath, remoteFilePath);
            Console.WriteLine($"File uploaded successfully to {remoteFilePath}.");
        }
    }

    // Download a file from the FTP server
    public void DownloadFile(string localFilePath, string remoteFilePath)
    {
        using (var client = new FtpClient(_host, _username, _password))
        {
            client.Connect();
            client.DownloadFile(localFilePath, remoteFilePath);
            Console.WriteLine($"File downloaded successfully to {localFilePath}.");
        }
    }

    // List files in a directory on the FTP server
    public List<string> ListFiles(string remoteDirectory)
    {
        var fileList = new List<string>();

        using (var client = new FtpClient(_host, _username, _password))
        {
            client.Connect();
            foreach (var item in client.GetListing(remoteDirectory))
            {
                fileList.Add(item.Name);
            }
        }

        return fileList;
    }

    // Create a directory on the FTP server
    public void CreateDirectory(string remoteDirectory)
    {
        using (var client = new FtpClient(_host, _username, _password))
        {
            client.Connect();
            client.CreateDirectory(remoteDirectory);
            Console.WriteLine($"Directory created successfully: {remoteDirectory}");
        }
    }
}
