using EGM.FileManager.Core.Abstractions.Channels;
using EGM.FileManager.Core.Channels;
using EGM.FileManager.Core.Options;
using EGM.FileManager.Core.Primitives;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using System.Threading.Tasks;
using MicrosoftOptions = Microsoft.Extensions.Options.Options;

namespace EGM.FileManager.Core.Test
{
    [TestFixture(Author = "egmair",
        Category = "Managed File Queue",
        Description = "Tests methods on the ManagedFileQueue class.")]
    public class Tests
    {
        private IQueue<ManagedFile>? _queue;

        [SetUp]
        public void Setup()
        {
            var fileManagerOptions = MicrosoftOptions.Create(new FileManagerOptions
            {
                SourceDirectory = @"C:\Users\euanm\Downloads"
            });

            _queue = new ManagedFileQueue(fileManagerOptions);
        }

        [TestCase(Author = "egmair",
            Category = "Managed File Queue",
            Description = "Tests reading from the queue when there are no items in it.")]
        public void ManagedFileQueue_ReadQueueWithNoItems_FileIsNull()
        {
            // Arrange.

            // Act.
            _queue!.TryReadQueue(out ManagedFile? queuedFile);

            // Assert.
            queuedFile.Should().BeNull();
        }

        [TestCase(Author = "egmair",
            Category = "Managed File Queue",
            Description = "Tests adding an item to the queue and reading the same file from it.")]
        public void ManagedFileQueue_ReadQueueWithOneItem_FileIsNull()
        {
            // Arrange.
            ManagedFile? file = new ManagedFile
            {
                FilePath = @"C:\Some\Important\File\Path.txt",
                FileType = "txt"
            };

            // Act.
            var queued = _queue!.TryQueue(file);
            _queue!.TryReadQueue(out ManagedFile? queuedFile);

            // Assert.
            using (new AssertionScope())
            {
                queued.Should().BeTrue();
                queuedFile!.FilePath.Should().BeEquivalentTo(file.FilePath);
                queuedFile!.FileType.Should().BeEquivalentTo(file.FileType);
            }
        }

        [TestCase(Author = "egmair",
            Category = "Managed File Queue",
            Description = "Tests adding an item to the queue when the queue is closed.")]
        public void ManagedFileQueue_AttemptToQueueWhenClosed_QueueFails()
        {
            // Arrange.
            ManagedFile? file = new ManagedFile
            {
                FilePath = @"C:\Some\Important\File\Path.txt",
                FileType = "txt"
            };

            // Act.
            _queue!.CloseQueue();
            var queued = _queue!.TryQueue(file);

            // Assert.
            queued.Should().BeFalse();
        }

        [TestCase(Author = "egmair",
            Category = "Managed File Queue",
            Description = "Tests waiting to read an item from the queue.")]
        public void ManagedFileQueue_WaitToReadQueue_FileReadFromQueue()
        {
            // Arrange.
            ManagedFile? file = new ManagedFile
            {
                FilePath = @"C:\Some\Important\File\Path.txt",
                FileType = "txt"
            };

            // Act.
            var task = _queue!.WaitToReadQueueAsync();
            var queued = _queue!.TryQueue(file);

            // Assert.
            using (new AssertionScope())
            {
                queued.Should().BeTrue();
                task.IsCompleted.Should().BeTrue();
            }
        }

        [TestCase(Author = "egmair",
            Category = "Managed File Queue",
            Description = "Tests reading multiple files from the queue.")]
        public void ManagedFileQueue_ReadMultipleFilesFromQueue_ReturnsCollectionOfFiles()
        {
            // Arrange.
            var files = new ManagedFile[]
            {
                new ManagedFile
                {
                    FilePath = @"C:\Some\Important\File\Path.txt",
                    FileType = "txt"
                },
                new ManagedFile
                {
                    FilePath = @"C:\Some\Important\File\Path2.txt",
                    FileType = "txt"
                },
                new ManagedFile
                {
                    FilePath = @"C:\Some\Important\File\Path3.txt",
                    FileType = "txt"
                }
            };

            // Act.
            foreach (var file in files)
            {
                _queue!.TryQueue(file);
            }

            var queuedFiles = new System.Collections.Generic.List<ManagedFile>();

            var task = Task.Run(async () =>
            {
                await foreach (var queuedFile in _queue!.ReadAllAsync())
                {
                    if (queuedFile is null)
                        break;

                    queuedFiles.Add(queuedFile!);
                }
            });

            // Assert.
            using (new AssertionScope())
            {
                queuedFiles.Count.Should().Be(files.Length);
                queuedFiles[0].FilePath.Should().Be(files[0].FilePath);
                queuedFiles[1].FilePath.Should().Be(files[1].FilePath);
                queuedFiles[2].FilePath.Should().Be(files[2].FilePath);
            }
        }
    }
}