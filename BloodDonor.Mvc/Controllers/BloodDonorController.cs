using BloodDonor.Mvc.Data;
using BloodDonor.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Drawing;

namespace BloodDonor.Mvc.Controllers
{
    public class BloodDonorController : Controller
    {
        // [Route("BloodDonor/[controller]")]

        private readonly BloodDonorDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BloodDonorController(BloodDonorDbContext context,IWebHostEnvironment env)
        {
          
            _context = context;
            _env= env;
        }

        public IActionResult Index(string bloodGroup, string address, bool ? isEligible)
        {
            IQueryable<BloodDonorEntity> query = _context.BloodDonors;
            if(!string.IsNullOrEmpty(bloodGroup))
            {
                query = query.Where(d => d.BloodGroup.ToString() == bloodGroup);
            }
            if (!string.IsNullOrEmpty(address))
            {
                query = query.Where(d => d.Address != null && (" " + d.Address + " ").Contains(" " + address + " "));
            }
            var donors = query.Select(d => new BloodDonorListViewModel
            {
                Id = d.Id,
                FullName = d.FullName,
                ContactNumber = d.ContactNumber,
                Email = d.Email,
                Age = (DateTime.Now.Year - d.DateOfBirth.Year),
                BloodGroup = d.BloodGroup.ToString(),
                LastDonationDate = d.LastDonationDate.HasValue ? $"{ (DateTime.Today - d.LastDonationDate.Value).Days} days ago" : "Never",
                ProfilePicture = d.ProfilePicture,
                Address = d.Address,
                Weight = d.Weight,
                IsEligible = (d.Weight >45 && d.Weight <200) && (!d.LastDonationDate.HasValue || (DateTime.Today - d.LastDonationDate.Value).TotalDays >=90)
            }).ToList();
            if(isEligible.HasValue)
            {
                donors = donors.Where(d => d.IsEligible == isEligible.Value).ToList();
            }
            return View(donors);
        }
        public IActionResult Details(int id)
        {
            var donor = _context.BloodDonors.FirstOrDefault(d => d.Id == id);
            if(donor == null)
            {
                return NotFound();
            }
            var donorDetails = new BloodDonorListViewModel
            {
                Id = donor.Id,
                FullName = donor.FullName,
                ContactNumber = donor.ContactNumber,
                Email = donor.Email,
                Age = (DateTime.Now.Year - donor.DateOfBirth.Year),
                BloodGroup = donor.BloodGroup.ToString(),
                LastDonationDate = donor.LastDonationDate.HasValue ? $"{ (DateTime.Today - donor.LastDonationDate.Value).Days} days ago" : "Never",
                ProfilePicture = donor.ProfilePicture,
                Address = donor.Address,
                Weight = donor.Weight,
                IsEligible = (donor.Weight > 45 && donor.Weight < 200) && (!donor.LastDonationDate.HasValue || (DateTime.Today - donor.LastDonationDate.Value).TotalDays >= 90)
            };
            return View(donorDetails);
        }

        public IActionResult Edit(int id)
        {
            var donor = _context.BloodDonors.FirstOrDefault(d => d.Id == id);
            if (donor == null)
            {
                return NotFound();
            }
            var donorDetails = new BloodDonorEditViewModel
            {
                Id = donor.Id,
                FullName = donor.FullName,
                ContactNumber = donor.ContactNumber,
                DateOfBirth = donor.DateOfBirth,
                Email = donor.Email,
                Address = donor.Address,
                BloodGroup = donor.BloodGroup,
                LastDonationDate = donor.LastDonationDate,
                ExistingProfilePicture = donor.ProfilePicture,
               
            };
            return View(donorDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BloodDonorEditViewModel donor)
        {
            if (!ModelState.IsValid)
            {
                return View(donor);
            }

            var donorEntity = new Models.BloodDonorEntity
            {
                FullName = donor.FullName,
                ContactNumber = donor.ContactNumber,
                BloodGroup = donor.BloodGroup,
                DateOfBirth = donor.DateOfBirth,
                Email = donor.Email,
                Weight = donor.Weight,
                Address = donor.Address,
                LastDonationDate = donor.LastDonationDate





            };
            if (donor.ProfilePicture != null && donor.ProfilePicture.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(donor.ProfilePicture.FileName)}";
                var folderPath = Path.Combine(_env.WebRootPath, "Profiles");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var fullPath = Path.Combine(folderPath, fileName);


                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await donor.ProfilePicture.CopyToAsync(stream);

                }
                donorEntity.ProfilePicture = Path.Combine("profiles", fileName);
            }

            _context.BloodDonors.Add(donorEntity);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
       
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Models.BloodDonorCreateViewModel donor)
        {
            if(!ModelState.IsValid)
            {
                return View(donor);
            }

            var donorEntity = new Models.BloodDonorEntity
            {
                FullName = donor.FullName,
                ContactNumber = donor.ContactNumber,
                BloodGroup = donor.BloodGroup,
                DateOfBirth = donor.DateOfBirth,
                Email = donor.Email,
                Weight = donor.Weight,
                Address = donor.Address,
                LastDonationDate = donor.LastDonationDate
              




            };
            if(donor.ProfilePicture !=null&&donor.ProfilePicture.Length>0)
            {
                var fileName= $"{Guid.NewGuid()}{Path.GetExtension(donor.ProfilePicture.FileName)}";  
                var folderPath = Path.Combine(_env.WebRootPath, "Profiles");
                if(!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var fullPath = Path.Combine(folderPath, fileName);


                using (var stream =new FileStream(fullPath,FileMode.Create))
                {
                  await donor.ProfilePicture.CopyToAsync(stream);
                    
                }
                donorEntity.ProfilePicture = Path.Combine("profiles",fileName);
            }

            _context.BloodDonors.Add(donorEntity);

            _context.SaveChanges();

            return RedirectToAction("Index");
           

        }
        public IActionResult Delete(int id)
        {
            var donor = _context.BloodDonors.FirstOrDefault(d => d.Id == id);
            if (donor == null)
            {
                return NotFound();
            }
            var donorDetails = new BloodDonorListViewModel
            {
                Id = donor.Id,
                FullName = donor.FullName,
                ContactNumber = donor.ContactNumber,
                Email = donor.Email,
                Age = (DateTime.Now.Year - donor.DateOfBirth.Year),
                BloodGroup = donor.BloodGroup.ToString(),
                LastDonationDate = donor.LastDonationDate.HasValue ? $"{(DateTime.Today - donor.LastDonationDate.Value).Days} days ago" : "Never",
                ProfilePicture = donor.ProfilePicture,
                Address = donor.Address,
                Weight = donor.Weight,
                IsEligible = (donor.Weight > 45 && donor.Weight < 200) && (!donor.LastDonationDate.HasValue || (DateTime.Today - donor.LastDonationDate.Value).TotalDays >= 90)
            };
            return View(donorDetails);
        }
        [ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int id)
        {
            var donor = _context.BloodDonors.FirstOrDefault(d => d.Id == id);
            if (donor == null)
            {
                return NotFound();
            }
            _context.BloodDonors.Remove(donor);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }


    }
}
