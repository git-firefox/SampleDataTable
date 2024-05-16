using System.Diagnostics;
using DataTableBS4.Models;
using DataTableBS4.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace DataTableBS4.Controllers
{
    public class ServerHomeController : Controller
    {
        private readonly ILogger<ServerHomeController> _logger;
        private readonly IPersonService _personService;

        public ServerHomeController(ILogger<ServerHomeController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        //public IActionResult GetPersons(DataTablesParameter parameter)
        public IActionResult GetPersons()
        {

            var data = _personService.GetAllPersons();

            int totalRecord = 0;
            int filterRecord = 0;

            var draw = Request.Form["draw"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var columnIndex = int.Parse(Request.Form["order[0][column]"].FirstOrDefault() ?? "0");

            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");
            int skip = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");

            //get total count of data in table
            totalRecord = data.Count();

            // search data when search value found
            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(x => 
                    x.Name!.ToLower().Contains(searchValue.ToLower())  
                    || x.Address!.ToLower().Contains(searchValue.ToLower()) 
                    || x.Email!.ToString().ToLower().Contains(searchValue.ToLower())
                );
            }

            // get total count of records after search
            filterRecord = data.Count();

            //sort data
            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
            {
                if (columnIndex == 0)
                {
                    data = (sortColumnDirection == "asc") ? data.OrderBy(d => d.Id) : data.OrderByDescending(d => d.Id);
                }
                else if (columnIndex == 1)
                {
                    data = (sortColumnDirection == "asc") ? data.OrderBy(d => d.Name) : data.OrderByDescending(d => d.Name);
                }
                else if (columnIndex == 2)
                {
                    data = (sortColumnDirection == "asc") ? data.OrderBy(d => d.Age) : data.OrderByDescending(d => d.Age);
                }
                else if (columnIndex == 3)
                {
                    data = (sortColumnDirection == "asc") ? data.OrderBy(d => d.BirthDate) : data.OrderByDescending(d => d.BirthDate);
                }
                else if (columnIndex == 4)
                {
                    data = (sortColumnDirection == "asc") ? data.OrderBy(d => d.Height) : data.OrderByDescending(d => d.Height);
                }
                else if (columnIndex == 5)
                {
                    data = (sortColumnDirection == "asc") ? data.OrderBy(d => d.Weight) : data.OrderByDescending(d => d.Weight);
                }
                else if (columnIndex == 6)
                {
                    data = (sortColumnDirection == "asc") ? data.OrderBy(d => d.Gender) : data.OrderByDescending(d => d.Gender);
                }
                else if (columnIndex == 8)
                {
                    data = (sortColumnDirection == "asc") ? data.OrderBy(d => d.Email) : data.OrderByDescending(d => d.Email);
                }
            }

            //pagination
            var empList = data.Skip(skip).Take(pageSize).ToList();

            //response
            var returnObj = new
            {
                draw = draw,
                recordsTotal = totalRecord,
                recordsFiltered = filterRecord,
                data = empList
            };

            return Ok(returnObj);
        }

        [HttpPost]
        public IActionResult UpdatePerson(Person person)
        {
            try
            {
                _personService.UpdatePerson(person.Id, person);
                return Ok(new { Success = true, Message = "UPDATED!" });
            }
            catch (Exception ex)
            {
                return Ok(new { Success = false, Message = ex.Message });
            }
        }

        [HttpDelete]
        public IActionResult DeletePerson(int id)
        {
            try
            {
                _personService.DeletePerson(id);
                return Ok(new { Success = true, Message = "DELETED!" });
            }
            catch (Exception ex)
            {
                return Ok(new { Success = false, Message = ex.Message });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
