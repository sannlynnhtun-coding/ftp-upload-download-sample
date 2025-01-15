using System;
using FtpConsoleAppSample;

// FTP server details
string host = "localhost";
string username = "Sann Lynn Htun"; // Server User Name
string password = "password"; // Server Password

// Create an instance of FluentFtpClient
var ftpClient = new FluentFtpClient(host, username, password);

// Create a dynamic folder on the FTP server
string remoteFolder = "/downloads";
ftpClient.CreateDirectory(remoteFolder);

// Upload a file
string localFilePath = @"D:\ftp-server\uploads\file.txt";
string remoteFilePath = $"{remoteFolder}/file.txt";
ftpClient.UploadFile(localFilePath, remoteFilePath);

// Download a file
string downloadLocalPath = @"D:\ftp-server\uploads\downloaded_file.txt";
ftpClient.DownloadFile(downloadLocalPath, remoteFilePath);

// List files in the remote directory
var files = ftpClient.GetListing(remoteFolder);
Console.WriteLine("Files in the remote directory:");
foreach (var file in files)
{
    Console.WriteLine(file);
}