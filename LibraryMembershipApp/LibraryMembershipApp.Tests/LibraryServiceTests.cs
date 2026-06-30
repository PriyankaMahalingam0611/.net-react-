using LibraryMembershipApp.Interfaces;
using LibraryMembershipApp.Models;
using LibraryMembershipApp.Services;
using Moq;
using NUnit.Framework;
using System.Timers;

namespace LibraryMembershipApp.Tests
{
    [TestFixture]
    public class LibraryServiceTests
    {
        private Mock<IMemberRepository> _mockMemberRepository;
        private Mock<IBookRepository> _mockBookRepository;
        private Mock<INotificationService> _mockNotificationService;
        private LibraryService _libraryService;

        [SetUp]
        public void Setup()
        {
            _mockMemberRepository = new Mock<IMemberRepository>();
            _mockBookRepository = new Mock<IBookRepository>();
            _mockNotificationService = new Mock<INotificationService>();

            _libraryService = new LibraryService(
                _mockMemberRepository.Object,
                _mockBookRepository.Object,
                _mockNotificationService.Object
            );
        }

        [Test]
        public void BorrowBook_WhenAllConditionsAreValid_ShouldReturnSuccessMessage()
        {
            var member = new Member { MemberId = 1, Email = "test@test.com", IsActive = true, BorrowedBookCount = 1 };
            var book = new Book { BookId = 100, BookTitle = "C# Basics", IsAvailable = true };

            _mockMemberRepository.Setup(m => m.GetMemberById(1)).Returns(member);
            _mockBookRepository.Setup(b => b.GetBookById(100)).Returns(book);

            var result = _libraryService.BorrowBook(1, 100);

            Assert.That(result, Is.EqualTo("Book borrowed successfully"));
            _mockBookRepository.Verify(b => b.MarkBookAsBorrowed(100), Times.Once);
            _mockMemberRepository.Verify(m => m.UpdateBorrowedBookCount(1), Times.Once);
            _mockNotificationService.Verify(n => n.SendBorrowNotification(member.Email, book.BookTitle), Times.Once);
        }

        [Test]
        public void BorrowBook_WhenMemberDoesNotExist_ShouldReturnMemberNotFound()
        {
            _mockMemberRepository.Setup(m => m.GetMemberById(1)).Returns((Member)null);

            var result = _libraryService.BorrowBook(1, 100);

            Assert.That(result, Is.EqualTo("Member not found"));
            _mockBookRepository.Verify(b => b.GetBookById(It.IsAny<int>()), Times.Never);
            _mockBookRepository.Verify(b => b.MarkBookAsBorrowed(It.IsAny<int>()), Times.Never);
            _mockMemberRepository.Verify(m => m.UpdateBorrowedBookCount(It.IsAny<int>()), Times.Never);
            _mockNotificationService.Verify(n => n.SendBorrowNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void BorrowBook_WhenMemberIsInactive_ShouldReturnMemberIsNotActive()
        {
            var member = new Member { MemberId = 1, IsActive = false };
            _mockMemberRepository.Setup(m => m.GetMemberById(1)).Returns(member);

            var result = _libraryService.BorrowBook(1, 100);

            Assert.That(result, Is.EqualTo("Member is not active"));
            _mockBookRepository.Verify(b => b.GetBookById(It.IsAny<int>()), Times.Never);
            _mockBookRepository.Verify(b => b.MarkBookAsBorrowed(It.IsAny<int>()), Times.Never);
            _mockMemberRepository.Verify(m => m.UpdateBorrowedBookCount(It.IsAny<int>()), Times.Never);
            _mockNotificationService.Verify(n => n.SendBorrowNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void BorrowBook_WhenBookDoesNotExist_ShouldReturnBookNotFound()
        {
            var member = new Member { MemberId = 1, IsActive = true };
            _mockMemberRepository.Setup(m => m.GetMemberById(1)).Returns(member);
            _mockBookRepository.Setup(b => b.GetBookById(100)).Returns((Book)null);

            var result = _libraryService.BorrowBook(1, 100);

            Assert.That(result, Is.EqualTo("Book not found"));
            _mockBookRepository.Verify(b => b.MarkBookAsBorrowed(It.IsAny<int>()), Times.Never);
            _mockMemberRepository.Verify(m => m.UpdateBorrowedBookCount(It.IsAny<int>()), Times.Never);
            _mockNotificationService.Verify(n => n.SendBorrowNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void BorrowBook_WhenBookIsNotAvailable_ShouldReturnBookIsNotAvailable()
        {
            var member = new Member { MemberId = 1, IsActive = true };
            var book = new Book { BookId = 100, IsAvailable = false };

            _mockMemberRepository.Setup(m => m.GetMemberById(1)).Returns(member);
            _mockBookRepository.Setup(b => b.GetBookById(100)).Returns(book);

            var result = _libraryService.BorrowBook(1, 100);

            Assert.That(result, Is.EqualTo("Book is not available"));
            _mockBookRepository.Verify(b => b.MarkBookAsBorrowed(It.IsAny<int>()), Times.Never);
            _mockMemberRepository.Verify(m => m.UpdateBorrowedBookCount(It.IsAny<int>()), Times.Never);
            _mockNotificationService.Verify(n => n.SendBorrowNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void BorrowBook_WhenNormalMemberHasThreeBooks_ShouldReturnBorrowingLimitReached()
        {
            var member = new Member { MemberId = 1, IsActive = true, BorrowedBookCount = 3, IsPremiumMember = false };
            var book = new Book { BookId = 100, IsAvailable = true };

            _mockMemberRepository.Setup(m => m.GetMemberById(1)).Returns(member);
            _mockBookRepository.Setup(b => b.GetBookById(100)).Returns(book);

            var result = _libraryService.BorrowBook(1, 100);

            Assert.That(result, Is.EqualTo("Borrowing limit reached"));
            _mockBookRepository.Verify(b => b.MarkBookAsBorrowed(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void BorrowBook_WhenMemberIdIsInvalid_ShouldReturnInvalidMemberId()
        {
            int invalidMemberId = 0;

            var result = _libraryService.BorrowBook(invalidMemberId, 100);

            Assert.That(result, Is.EqualTo("Invalid member id"));
            _mockMemberRepository.Verify(m => m.GetMemberById(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void BorrowBook_WhenBookIdIsInvalid_ShouldReturnInvalidBookId()
        {
            int invalidBookId = -1;

            var result = _libraryService.BorrowBook(1, invalidBookId);

            Assert.That(result, Is.EqualTo("Invalid book id"));
            _mockBookRepository.Verify(b => b.GetBookById(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void BorrowBook_WhenPremiumMemberHasThreeBooks_ShouldAllowBorrowing()
        {
            var member = new Member { MemberId = 1, Email = "premium@test.com", IsActive = true, BorrowedBookCount = 3, IsPremiumMember = true };
            var book = new Book { BookId = 100, BookTitle = "Pro C#", IsAvailable = true };

            _mockMemberRepository.Setup(m => m.GetMemberById(1)).Returns(member);
            _mockBookRepository.Setup(b => b.GetBookById(100)).Returns(book);

            var result = _libraryService.BorrowBook(1, 100);

            Assert.That(result, Is.EqualTo("Book borrowed successfully"));
        }

        [Test]
        public void BorrowBook_WhenPremiumMemberHasFiveBooks_ShouldReturnBorrowingLimitReached()
        {
            var member = new Member { MemberId = 1, IsActive = true, BorrowedBookCount = 5, IsPremiumMember = true };
            var book = new Book { BookId = 100, IsAvailable = true };

            _mockMemberRepository.Setup(m => m.GetMemberById(1)).Returns(member);
            _mockBookRepository.Setup(b => b.GetBookById(100)).Returns(book);

            var result = _libraryService.BorrowBook(1, 100);

            Assert.That(result, Is.EqualTo("Borrowing limit reached"));
            _mockBookRepository.Verify(b => b.MarkBookAsBorrowed(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void BorrowBook_WhenSuccessful_ShouldSendNotificationWithCorrectValues()
        {
            var member = new Member { MemberId = 1, Email = "user@domain.com", IsActive = true, BorrowedBookCount = 0 };
            var book = new Book { BookId = 100, BookTitle = "Clean Code", IsAvailable = true };

            _mockMemberRepository.Setup(m => m.GetMemberById(1)).Returns(member);
            _mockBookRepository.Setup(b => b.GetBookById(100)).Returns(book);

            _libraryService.BorrowBook(1, 100);

            _mockNotificationService.Verify(n => n.SendBorrowNotification("user@domain.com", "Clean Code"), Times.Once);
        }
    }
}