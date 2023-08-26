using AssignmentsProject_2.Models;
using AssignmentsProject_2.Simple_Model;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace AssignmentsProject_2.Controllers
{
    public class AssignmentController : Controller
    {
        public static Assignment? StaticAssignment { get; set; } = null;

        public IActionResult Index(Assignment a)
        {
            return View(a);
        } 
        public IActionResult Create()
        {
            return View();
        }           


        public IActionResult Edit(Assignment a)
        {
            AssignmentController.StaticAssignment = a;
            return View(a);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Assignment assignment)
        {
            if ((string.IsNullOrEmpty(assignment.Title) || string.IsNullOrWhiteSpace(assignment.Title)) && (string.IsNullOrEmpty(assignment.Description) || string.IsNullOrWhiteSpace(assignment.Description)))
            {
                TempData["Message"] = "Invalid assignment!";
                return RedirectToAction("Index", "Assignment", AccountController.StaticUser);
            }
            if (string.IsNullOrEmpty(assignment.Title) || string.IsNullOrWhiteSpace(assignment.Title))
            {
                TempData["Message"] = "Title is invalid!";
                return RedirectToAction("Index", "Assignment", AccountController.StaticUser);
            }

            if (AccountController.StaticUser != null)
            {
                int number = 0;
                /*  foreach (Assignment a in AccountController.StaticUser.Assignments) // Finds the highest assignment number.
                  {
                      number = a.Number;
                  }*/
                if (AccountController.StaticUser.Assignments.Count == 0)
                {
                    assignment.Number = 1;
                }
                else
                {
                    assignment.Number = AccountController.StaticUser.Assignments[0].Number + 1; // Sets the assignment a number by the specific user assignmens list.
                }
                    assignment.Started.AddHours(3);
                AccountController.StaticUser.Assignments.Add(assignment);

                var tempList = AccountController.StaticUser.Assignments.OrderByDescending(a => a.Number).ToList(); // Sets the assignments as LIFO.
                AccountController.StaticUser.Assignments = tempList;

                SimpleModel.upsertRecord(MongoStuff.Databases.AssignmentsProject_2.ToString(),
                    MongoStuff.Collections.Users.ToString(),
                    AccountController.StaticUser._id,
                    AccountController.StaticUser);
            }
            TempData["Message"] = $"Successfully asigning! ({assignment.Title})";
            return RedirectToAction("Index", "Home", AccountController.StaticUser);
            
        }

        [HttpPost]
        public async Task<IActionResult> EditAssignment(Assignment assignment)
        {
            if ((string.IsNullOrEmpty(assignment.Title) || string.IsNullOrWhiteSpace(assignment.Title)) && (string.IsNullOrEmpty(assignment.Description) || string.IsNullOrWhiteSpace(assignment.Description)))
            {
                TempData["Message"] = "Invalid assignment!";
                return RedirectToAction("Edit", "Assignment", AccountController.StaticUser);
            }
            if (string.IsNullOrEmpty(assignment.Title) || string.IsNullOrWhiteSpace(assignment.Title))
            {
                TempData["Message"] = "Title is invalid!";
                return RedirectToAction("Edit", "Assignment", AccountController.StaticUser);
            }
            if (AccountController.StaticUser != null && assignment != null)
            {
                for (int i = 0; i < AccountController.StaticUser.Assignments.Count; i++)
                {
                    if (AccountController.StaticUser.Assignments[i]._id.Equals(AssignmentController.StaticAssignment._id))
                    {
                        assignment.Number = AccountController.StaticUser.Assignments[i].Number;
                        AccountController.StaticUser.Assignments[i] = assignment;
                        AccountController.StaticUser.Assignments[i].Finish = DateTime.Now;
                    }
                }
                SimpleModel.upsertRecord(MongoStuff.Databases.AssignmentsProject_2.ToString(),
                    MongoStuff.Collections.Users.ToString(),
                    AccountController.StaticUser._id,
                    AccountController.StaticUser);
            }
            TempData["Title"] = assignment.Title;
            TempData["Message"] = $"Assignment ({TempData["Title"]}) editted successfully!";
            return RedirectToAction("Index", "Home", AccountController.StaticUser);
        }

        [HttpGet]
        public IActionResult Delete(Assignment a)
        {
            TempData["Title"] = a.Title;
            try
            {
                for (int i = 0; i < AccountController.StaticUser.Assignments.Count; i++)
                {
                    if (a._id.Equals(AccountController.StaticUser.Assignments[i]._id))
                    {
                        AccountController.StaticUser.Assignments.Remove(AccountController.StaticUser.Assignments[i]);
                        break;
                    }
                }
                SimpleModel.upsertRecord(MongoStuff.Databases.AssignmentsProject_2.ToString(),
                    MongoStuff.Collections.Users.ToString(),
                    AccountController.StaticUser._id,
                    AccountController.StaticUser);
                TempData["Message"] = $"Assignment {TempData["Title"]} was deleted successfully!";
            } catch (Exception ex)
            {
                TempData["Message"] = $"Error in deleting assignment! {TempData["Title"]}";

            }
            return RedirectToAction("Index", "Home", AccountController.StaticUser);
        }
    }
}