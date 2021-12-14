using ContactManager.Contracts;
using ContactManager.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ContactsController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public async Task Upload(IFormFile file)
        {
            if (file is null)
            {
                throw new ArgumentException(nameof(file));
            }

            using (TextFieldParser parser = new TextFieldParser(file.OpenReadStream()))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                parser.ReadFields();

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    bool married = false;
                    if (fields[2] == "yes")
                    {
                        married = true;
                    }

                    ContactEntity contact = new ContactEntity() { Name = fields[0], DateOfBirth = DateTime.Parse(fields[1]), Married = married, Phone = fields[3], Salary = decimal.Parse(fields[4]) };
                    _applicationDbContext.Contacts.Add(contact);
                };

                await _applicationDbContext.SaveChangesAsync();
            }
        }

        [HttpGet]
        public async Task<List<Contact>> Get()
        {
            return await _applicationDbContext.Contacts.Select(contact => new Contact()
            {
                Id = contact.Id,
                Name = contact.Name,
                DateOfBirth = contact.DateOfBirth,
                Married = contact.Married,
                Phone = contact.Phone,
                Salary = contact.Salary
            }).ToListAsync();
        }
    }
}
