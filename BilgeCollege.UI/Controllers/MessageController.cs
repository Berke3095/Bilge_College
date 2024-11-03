using BilgeCollege.BLL.Services.Abstracts;
using BilgeCollege.DAL.Context;
using BilgeCollege.MODELS.Concretes;
using BilgeCollege.MODELS.Concretes.CustomUser;
using BilgeCollege.UI.Views.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BilgeCollege.UI.Controllers
{
    [Authorize(Roles = "Teacher,Guardian,Student")]
    public class MessageController : Controller
    {
        private readonly I_MessageServiceManager _messageServiceManager;
        private readonly I_TeacherServiceManager _teacherServiceManager;
        private readonly I_GuardianServiceManager _guardianServiceManager;
        private readonly I_StudentServiceManager _studentServiceManager;
        private readonly UserManager<User> _userManager;
        private readonly CollegeContext _context;

        public MessageController(I_MessageServiceManager messageServiceManager, I_TeacherServiceManager teacherServiceManager, I_GuardianServiceManager guardianServiceManager, I_StudentServiceManager studentServiceManager, UserManager<User> userManager, CollegeContext context)
        {
            _messageServiceManager = messageServiceManager;
            _teacherServiceManager = teacherServiceManager;
            _guardianServiceManager = guardianServiceManager;
            _studentServiceManager = studentServiceManager;

            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Inbox()
        {
            var userId = _userManager.GetUserId(User);
            var user = await _context.Users.Include(x => x.ReceivedMessages).FirstAsync(x => x.Id == userId);

            InboxVM inboxVM = new InboxVM();
            inboxVM.ReceivedMessages = user.ReceivedMessages.OrderByDescending(x => x.CreatedDate).ToList();
            foreach(var message in inboxVM.ReceivedMessages)
            {
                var senderUser = await _userManager.FindByIdAsync(message.SenderId);
                var roles = await _userManager.GetRolesAsync(senderUser);
                var role = roles[0];
                object thisUser = null;
                switch (role)
                {
                    case "Teacher":
                        thisUser = _teacherServiceManager.GetAllActives().First(x => x.UserId == senderUser.Id);
                        inboxVM.Senders.Add(thisUser);
                        break;
                    case "Guardian":
                        thisUser = _guardianServiceManager.GetAllActives().First(x => x.UserId == senderUser.Id);
                        inboxVM.Senders.Add(thisUser);
                        break;
                    case "Student":
                        thisUser = _studentServiceManager.GetAllActives().First(x => x.UserId == senderUser.Id);
                        inboxVM.Senders.Add(thisUser);
                        break;
                }
            }

            return View(inboxVM);
        }

        public async Task<IActionResult> SendMessage(string? resultSuccess, string? resultFail, string? receiverEmail)
        {
            if(resultSuccess != null)
            {
                ViewData["SuccessMessage"] = resultSuccess;
            }
            else if(resultFail != null)
            {
                ViewData["FailMessage"] = resultFail;
            }

            SendMessageVM sendMessageVM = new SendMessageVM();
            if(receiverEmail != null)
            {
                sendMessageVM.Email = receiverEmail;
            }

            return View(sendMessageVM);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(SendMessageVM sendMessageVM)
        {
            if(ModelState.IsValid)
            {
                if (sendMessageVM != null)
                {
                    var receiverUser = await _userManager.FindByEmailAsync(sendMessageVM.Email);
                    if (receiverUser != null)
                    {
                        var roles = await _userManager.GetRolesAsync(receiverUser);
                        if (roles[0] != "Admin")
                        {
                            Message message = new Message()
                            {
                                SenderId = _userManager.GetUserId(User),
                                ReceiverId = receiverUser.Id,
                                Text = sendMessageVM.Text
                            };

                            _messageServiceManager.Create(message);
                            string successMessage = "Message sent successfully!";
                            return RedirectToAction("SendMessage", "Message", new { resultSuccess = successMessage });
                        }
                        else
                        {
                            string errorMessage = "Can't send messages to the admin!";
                            return RedirectToAction("SendMessage", "Message", new { resultFail = errorMessage });
                        }
                    }
                    else
                    {
                        string errorMessage = "Email entered doesn't exist!";
                        return RedirectToAction("SendMessage", "Message", new { resultFail = errorMessage });
                    }

                }
            }
    
            return View();
        }
    }
}
