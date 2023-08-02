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

        public IActionResult Edit(Assignment a)
        {
            //a.Number = AccountController.StaticUser.Number - 1;
            AssignmentController.StaticAssignment = a;
            return View(a);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Assignment assignment)
        {
            if (AccountController.StaticUser != null)
            {
                assignment.Number = AccountController.StaticUser.Assignments.Count + 1;
                AccountController.StaticUser.Assignments.Add(assignment);
                AccountController.StaticUser.Assignments.Reverse();
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