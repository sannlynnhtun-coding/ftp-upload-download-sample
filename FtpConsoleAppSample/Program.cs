// FTP server details
string host = "localhost";
string username = "Sann Lynn Htun"; // Server User Name
string password = "password"; // Server Password

// Initialize the FTP service
var ftpService = new FtpService(host, username, password);

// Create a dynamic folder on the FTP server
string remoteFolder = "/downloads";
ftpService.CreateDirectory(remoteFolder);

// Upload a file
string localFilePath = @"D:\ftp-server\uploads\file.txt";
string remoteFilePath = $"{remoteFolder}/file.txt";
ftpService.UploadFile(localFilePath, remoteFilePath);

// Download a file
string downloadLocalPath = @"D:\ftp-server\uploads\downloaded_file.txt";
ftpService.DownloadFile(downloadLocalPath, remoteFilePath);

// List files in the remote directory
var files = ftpService.ListFiles(remoteFolder);
Console.WriteLine("Files in the remote directory:");
foreach (var file in files)
{
    Console.WriteLine(file);
}