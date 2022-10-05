using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using u21447234_HW05.ViewModels;

namespace u21441104_HW05.Controllers
{
    public class HomeController : Controller
    {
        private SqlConnection connection = new SqlConnection(@"Data Source=BICENTENNIAL-MA\SQLEXPRESS; Initial Catalog=Library; Integrated Security=True");

        public ActionResult Books()
        {
            return View(getBooks());
        }

        public ActionResult Borrows()
        {
            return View(getBorrows());
        }

        public ActionResult Students()
        {
            return View(getStudents());
        }

        //Read Data
        //*****************************************************************************************************************
        public List<BookVM> getBooks()
        {
            List<BookVM> bookList = new List<BookVM>();
            try
            {
                connection.Open();
                SqlCommand myCommand = new SqlCommand("SELECT bookId, books.name, authors.surname, types.name, pagecount, point FROM authors, books, types WHERE books.authorId = authors.authorId AND books.typeId = types.typeId;", connection);
                SqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    BookVM book = new BookVM();
                    book.ID = myReader.GetInt32(0);
                    book.NAME = myReader.GetString(1);
                    book.AUTHOR = myReader.GetString(2);
                    book.TYPE = myReader.GetString(3);
                    book.PAGE_COUNT = myReader.GetInt32(4);
                    book.POINTS = myReader.GetInt32(5);
                    book.STATUS = true;                

                    bookList.Add(book);
                }
            }
            catch (Exception err)
            {
                ViewBag.Status = 0;
            }
            finally
            {
                connection.Close();
            }

            return bookList;
        }

        public List<BorrowVM> getBorrows()
        {
            List<BorrowVM> bookDetailList = new List<BorrowVM>();
            try
            {
                connection.Open();
                SqlCommand myCommand = new SqlCommand("SELECT borrowId, takenDate, broughtDate, students.name FROM borrows, students WHERE borrowId = 1; ", connection);
                SqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    BorrowVM book = new BorrowVM();
                    book.ID = myReader.GetInt32(0);
                    book.TAKEN_DATE = myReader.GetDateTime(1);
                    book.BROUGHT_DATE = myReader.GetDateTime(2);
                    book.BORROWED_BY = myReader.GetString(3);

                    bookDetailList.Add(book);
                }
            }
            catch (Exception err)
            {

                ViewBag.Status = 0;
            }
            finally
            {
                connection.Close();
            }

            return bookDetailList;
        }

        public List<StudentVM> getStudents()
        {
            List<StudentVM> studentList = new List<StudentVM>();
            try
            {
                connection.Open();
                SqlCommand myCommand = new SqlCommand("SELECT * FROM students; ", connection);
                SqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    StudentVM student = new StudentVM();
                    student.ID = myReader.GetInt32(0);
                    student.NAME = myReader.GetString(1);
                    student.SURNAME = myReader.GetString(2);
                    student.CLASS = myReader.GetString(5);
                    student.POINTS = myReader.GetInt32(6);

                    studentList.Add(student);
                }
            }
            catch (Exception err)
            {

                ViewBag.Status = 0;
            }
            finally
            {
                connection.Close();
            }

            return studentList;
        }
        //*****************************************************************************************************************

        public ActionResult ViewBook()
        {
            return View();
        }

        public ActionResult ViewStudent()
        {
            List<BorrowVM> bookDetailLIST = new List<BorrowVM>();
            try
            {
                connection.Open();
                SqlCommand myCommand = new SqlCommand("SELECT borrowId, takenDate, broughtDate, students.name FROM borrows, students; ", connection);
                SqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    BorrowVM book = new BorrowVM();
                    book.ID = myReader.GetInt32(0);
                    book.TAKEN_DATE = myReader.GetDateTime(1);
                    book.BROUGHT_DATE = myReader.GetDateTime(2);
                    book.BORROWED_BY = myReader.GetString(3);

                    bookDetailLIST.Add(book);
                }
            }
            catch (Exception err)
            {

                ViewBag.Status = 0;
            }
            finally
            {
                connection.Close();
            }

            return View(bookDetailLIST);
        }


        [HttpPost]
        public ActionResult SearchBooks(string txtBookName, string selectType, string selectAuthor)
        {
            List<BookVM> bookList = new List<BookVM>();
            try
            {
                connection.Open();
                SqlCommand myCommand = new SqlCommand("SELECT bookId, books.name, authors.surname, types.name, pagecount, point FROM authors, books, types WHERE books.authorId = authors.authorId AND books.typeId = types.typeId;", connection);
                SqlDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    BookVM book = new BookVM();
                    book.ID = myReader.GetInt32(0);
                    book.NAME = myReader.GetString(1);
                    book.AUTHOR = myReader.GetString(2);
                    book.TYPE = myReader.GetString(3);
                    book.PAGE_COUNT = myReader.GetInt32(4);
                    book.POINTS = myReader.GetInt32(5);
                    book.STATUS = true;

                    if(book.NAME == txtBookName)
                    {
                        bookList.Add(book);
                    }
                }
            }
            catch (Exception err)
            {
                ViewBag.Status = 0;
            }
            finally
            {
                connection.Close();
            }

            return View(bookList.ToList()); ;
        }

        [HttpPost]
        public ActionResult SearchStudents(string txtBookName, string selectType, string selectAuthor)
        {
            List<BookVM> bookList = getBooks();

            if (txtBookName != null)
            {
                bookList = bookList.Where(x => x.NAME.Contains(txtBookName)).ToList();
            }

            return View(bookList);
        }
    }
}
