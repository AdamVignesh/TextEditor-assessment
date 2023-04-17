using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace TextEditor__assessment.Controllers
{
    public class FilesController : Controller
    {
        IConfiguration configuration;
        public List<Models.file> fileNames = new List<Models.file>();
        public FilesController(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }
        public ActionResult AddText()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddText(IFormCollection collection)
        {
            try
            {
                Console.WriteLine(collection["text"]);
                Console.WriteLine("in add text post");
                SqlConnection connection = new SqlConnection(configuration.GetConnectionString("textdb"));
                connection.Open();

                string query = $"Insert into fileStorage values ('{collection["fileName"]}','{collection["text"]}')";
                SqlCommand command = new SqlCommand(query, connection);
                
                command.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);  
                return View();

            }
        }
        // GET: FilesController
        public ActionResult Index()
        {
            try
            {          
                Console.WriteLine("in index get");
                SqlConnection connection = new SqlConnection(configuration.GetConnectionString("textdb"));
                connection.Open();

                string query = $"SELECT * FROM filestorage";
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader =  command.ExecuteReader();
                while (reader.Read())
                {
                    Models.file f = new Models.file();
                    f.id = reader.GetInt32(0);
                    f.fileName = reader.GetString(1);
                    fileNames.Add(f);


                    ViewBag.fileNames = fileNames;
                }
               // return RedirectToAction("Index");
                return View("Index");
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return View();

            }
           

        }

        // GET: FilesController/Details/5
        public ActionResult View(int id)
        {
            try
            {
                Console.WriteLine("in index get"+id);
                SqlConnection connection = new SqlConnection(configuration.GetConnectionString("textdb"));
                connection.Open();

                string query = $"SELECT text FROM filestorage WHERE id = {id}";
                SqlCommand command = new SqlCommand(query, connection);
                ViewBag.text = command.ExecuteScalar();
                Console.WriteLine("text "+ViewBag.text);
                // return RedirectToAction("Index");
                return View();
            }
            catch (Exception ex)
            {
                Console.Write("Catch in view "+ex.Message);
                return View();

            }
            return View();
        }

        // GET: FilesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FilesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FilesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FilesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FilesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FilesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
