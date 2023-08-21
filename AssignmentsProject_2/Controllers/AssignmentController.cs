using AssignmentsProject_2.Models;
using AssignmentsProject_2.Simple_Model;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace AssignmentsProject_2.Controllers
{
    public class AssignmentController : Controller
    {
        public static Assignment? StaticAssignment { get; set; } = null;

  /*      public IActionResult Index(Assignment a)
        {

            return View(a);
        }*/

        public IActionResult Index(Assignment a)
        {

            return View(a);
        }

        public IActionResult Edit(Assignment a)
        {
            AssignmentController.StaticAssignment = a;
            return View(a);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Assignment assignment)
        {
            if (AccountController.StaticUser != null)
            {
                int number = 0;
                foreach (Assignment a in AccountController.StaticUser.Assignments) // Finds the higher assignment number.
                {
                    number = a.Number;
                }
                assignment.Number = number + 1; // Sets the assignment a number by the specific user assignmens list.
                AccountController.StaticUser.Assignments.Add(assignment);

                var list = AccountController.StaticUser.Assignments.OrderByDescending(a => a.Number).ToList(); // Sets the assignments as LIFO.
                AccountController.StaticUser.Assignments = list;

                SimpleModel.upsertRecord(MongoStuff.Databases.AssignmentsProject_2.ToString(),
                    MongoStuff.Collections.Users.ToString(),
                    AccountController.StaticUser._id,
                    AccountController.StaticUser);
            }
            return RedirectToAction("Index", "Home", AccountController.StaticUser);
        }

        [HttpPost]
        public async Task<IActionResult> EditAssignment(Assignment assignment)
        {

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
            return RedirectToAction("Index", "Home", AccountController.StaticUser);
        }

        [HttpGet]
        public IActionResult Delete(Assignment a)
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
            return RedirectToAction("Index", "Home", AccountController.StaticUser);
        }
    }
}