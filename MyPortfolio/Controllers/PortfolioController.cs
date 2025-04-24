using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Models;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Models;



namespace MyPortfolio.Controllers
{
    public class PortfolioController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult About() => View();

        public IActionResult Experience() => View();


        // This is the GET action that displays the contact form
        [HttpGet]
        public IActionResult Contact()
        {
            return View(new ContactFormModel());
        }



        public IActionResult Projects()
        {
            var projects = new List<Project>
            {
        new Project
        {
            Title = "Virtual Classroom App",
            Description = "A full-featured virtual classroom built using ASP.NET Core MVC and EF.",
            TechStack = "ASP.NET Core, Entity Framework, Bootstrap",
            GitHubUrl = "https://github.com/Ramzia-P-A/Virtual_Class_room_project"
        },
        // Add more projects if needed
    };

            return View(projects);
        }


        // This is the POST action that handles form submission
        [HttpPost]
        public async Task<IActionResult> Contact(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("ramziapa123@gmail.com")); // your email
                email.To.Add(MailboxAddress.Parse("ramziapa123@gmail.com"));   // send to yourself
                email.Subject = "New Contact Form Submission";

                email.Body = new TextPart("plain")
                {
                    Text = $"Name: {model.Name}\nEmail: {model.Email}\nMessage: {model.Message}"
                };

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync("ramziapa123@gmail.com", "zlyg ucfd cbsz vqhg");
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                TempData["MessageSent"] = "Thank you! Your message has been sent.";
                return RedirectToAction("Contact");
            }

            return View(model);
        }
    }
}
