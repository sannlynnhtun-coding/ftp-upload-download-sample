# Step-by-Step Guide: Create an FTP Server on Local IIS and Use It with .NET Core

This guide will walk you through the process of setting up an FTP server on **Local IIS (Internet Information Services)** and using it with a .NET Core application. We'll also explain the code step by step.

---

## **Step 1: Install IIS and FTP Server on Windows**

### **1.1 Enable IIS**
1. Open the **Control Panel**.
2. Go to **Programs** > **Turn Windows features on or off**.
3. Check the box for **Internet Information Services (IIS)** and click **OK**.
4. Wait for the installation to complete.

### **1.2 Enable FTP Server**
1. In the same **Windows Features** window, expand **Internet Information Services** > **FTP Server**.
2. Check the boxes for:
   - **FTP Server**
   - **FTP Extensibility**
3. Click **OK** to install the FTP server.

---

## **Step 2: Configure FTP Site in IIS**

### **2.1 Open IIS Manager**
1. Press `Win + R`, type `inetmgr`, and press **Enter** to open IIS Manager.

### **2.2 Add FTP Site**
1. In the **Connections** pane, right-click **Sites** and select **Add FTP Site**.
2. Configure the FTP site:
   - **Site Name**: `MyFtpSite`
   - **Physical Path**: `C:\inetpub\ftproot` (or any directory you want to use).
3. Click **Next**.

### **2.3 Bind FTP Site**
1. Set the **IP Address** to `All Unassigned` (or your local IP address).
2. Set the **Port** to `21` (default FTP port).
3. Enable **Start FTP site automatically**.
4. Click **Next**.

### **2.4 Configure Authentication and Authorization**
1. Set **Authentication** to **Basic**.
2. Set **Authorization** to:
   - **Allow access to**: `Specified users`
   - **User name**: Enter your Windows username (e.g., `Sann Lynn Htun`).
   - **Permissions**: Check **Read** and **Write**.
3. Click **Finish**.

---

## **Step 3: Test FTP Server**

### **3.1 Create a Test File**
1. Go to the FTP site's physical path (e.g., `C:\inetpub\ftproot`).
2. Create a test file, e.g., `test.txt`.

### **3.2 Connect to FTP Server**
1. Open a browser or FTP client (e.g., FileZilla).
2. Connect to `ftp://localhost` using your Windows username and password.
3. Verify that you can see the `test.txt` file.

---

## **Step 4: Create a .NET Core Application**

### **4.1 Create a New .NET Core Project**
1. Open a terminal and run:
   ```bash
   dotnet new console -n FtpExample
   cd FtpExample
   ```

### **4.2 Install FluentFTP NuGet Package**
1. Run the following command to install the `FluentFTP` package:
   ```bash
   dotnet add package FluentFTP
   ```

---

## **Step 5: Write the .NET Core Code**

### **5.1 Create the `FtpService` Class**
Create a reusable `FtpService` class to handle FTP operations.

```csharp
using System;
using System.Collections.Generic;
using FluentFTP;

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
```

---

### **5.2 Use the `FtpService` in the Main Program**

```csharp
class Program
{
    static void Main(string[] args)
    {
        // FTP server details
        string host = "localhost";
        string username = "Sann Lynn Htun"; // Replace with your Windows username
        string password = "password"; // Replace with your Windows password

        // Initialize the FTP service
        var ftpService = new FtpService(host, username, password);

        // Create a dynamic folder on the FTP server
        string remoteFolder = "/downloads";
        ftpService.CreateDirectory(remoteFolder);

        // Upload a file
        string localFilePath = @"C:\path\to\your\file.txt";
        string remoteFilePath = $"{remoteFolder}/file.txt";
        ftpService.UploadFile(localFilePath, remoteFilePath);

        // Download a file
        string downloadLocalPath = @"C:\path\to\download\downloaded_file.txt";
        ftpService.DownloadFile(downloadLocalPath, remoteFilePath);

        // List files in the remote directory
        var files = ftpService.ListFiles(remoteFolder);
        Console.WriteLine("Files in the remote directory:");
        foreach (var file in files)
        {
            Console.WriteLine(file);
        }
    }
}
```

---

## **Step 6: Run the Application**

1. Build and run the .NET Core application:
   ```bash
   dotnet run
   ```
2. Verify the output:
   - The program will create a directory, upload a file, download the file, and list the files in the directory.

---

## **Summary**

1. **Set up an FTP server on Local IIS**.
2. **Create a .NET Core application** and install the `FluentFTP` package.
3. **Write reusable code** to handle FTP operations.
4. **Run the application** to upload, download, and list files on the FTP server.