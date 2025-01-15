using System.Collections.Generic;
using FluentFTP;
using Moq;
using Xunit;

namespace FtpConsoleAppSample.Tests
{
    public class FluentFtpClientTests
    {
        private readonly Mock<IFtpClientWrapper> _ftpClientWrapperMock;
        private readonly FluentFtpClient _fluentFtpClient;

        public FluentFtpClientTests()
        {
            _ftpClientWrapperMock = new Mock<IFtpClientWrapper>();
            _fluentFtpClient = new FluentFtpClient(_ftpClientWrapperMock.Object);
        }

        [Fact]
        public void Connect_ShouldInvokeConnectOnFtpClientWrapper()
        {
            // Arrange
            _ftpClientWrapperMock.Setup(client => client.Connect()).Verifiable();

            // Act
            _fluentFtpClient.Connect();

            // Assert
            _ftpClientWrapperMock.Verify(client => client.Connect(), Times.Once);
        }

        [Fact]
        public void UploadFile_ShouldReturnExpectedStatus()
        {
            // Arrange
            string localFilePath = @"D:\ftp-server\uploads\file.txt";
            string remoteFilePath = "/downloads/file.txt";
            var expectedStatus = FtpStatus.Success;
            _ftpClientWrapperMock.Setup(client => client.UploadFile(localFilePath, remoteFilePath)).Returns(expectedStatus);

            // Act
            var result = _fluentFtpClient.UploadFile(localFilePath, remoteFilePath);

            // Assert
            Assert.Equal(expectedStatus, result);
        }

        [Fact]
        public void DownloadFile_ShouldReturnExpectedStatus()
        {
            // Arrange
            string localFilePath = @"D:\ftp-server\uploads\downloaded_file.txt";
            string remoteFilePath = "/downloads/file.txt";
            var expectedStatus = FtpStatus.Success;
            _ftpClientWrapperMock.Setup(client => client.DownloadFile(localFilePath, remoteFilePath)).Returns(expectedStatus);

            // Act
            var result = _fluentFtpClient.DownloadFile(localFilePath, remoteFilePath);

            // Assert
            Assert.Equal(expectedStatus, result);
        }

        [Fact]
        public void GetListing_ShouldReturnFiles()
        {
            // Arrange
            string remoteDirectory = "/downloads";
            var expectedFiles = new List<FtpListItem>
            {
                new FtpListItem { FullName = "/downloads/file1.txt" },
                new FtpListItem { FullName = "/downloads/file2.txt" }
            };
            _ftpClientWrapperMock.Setup(client => client.GetListing(remoteDirectory)).Returns(expectedFiles);

            // Act
            var result = _fluentFtpClient.GetListing(remoteDirectory);

            // Assert
            Assert.Equal(expectedFiles, result);
        }

        [Fact]
        public void CreateDirectory_ShouldReturnExpectedStatus()
        {
            // Arrange
            string remoteDirectory = "/downloads";
            var expectedStatus = true;
            _ftpClientWrapperMock.Setup(client => client.CreateDirectory(remoteDirectory)).Returns(expectedStatus);

            // Act
            var result = _fluentFtpClient.CreateDirectory(remoteDirectory);

            // Assert
            Assert.Equal(expectedStatus, result);
        }
    }
}
