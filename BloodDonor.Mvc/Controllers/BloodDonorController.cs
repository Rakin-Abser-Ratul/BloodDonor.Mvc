using AutoMapper;
using BloodDonor.Mvc.Data;
using BloodDonor.Mvc.Models.Entities;
using BloodDonor.Mvc.Models.ViewModel;
using BloodDonor.Mvc.Services.Interfaces;
using BloodDonor.Mvc.Services.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Threading.Tasks;

namespace BloodDonor.Mvc.Controllers
{
    public class BloodDonorController : Controller
    {
        // [Route("BloodDonor/[controller]")]

        private readonly IFileService _fileService;
        private readonly IBloodDonorService _bloodDonorService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;


        public BloodDonorController(IConfiguration configuration,
            IFileService fileService,
            IMapper mapper,
            IBloodDonorService bloodDonorService)
        {
            _configuration = configuration;
            
            _fileService = fileService;
            _bloodDonorService = bloodDonorService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index([FromQuery] FilterDonorModel filter)
        {
           
            var donors=await _bloodDonorService.GetFilteredBloodDonorAsync(filter);
            var donorViewModels = _mapper.Map<List<BloodDonorListViewModel>>(donors);

            return View(donorViewModels);
        }
        public async Task<IActionResult> DetailsAsync(int id)
        {
            var donor = await _bloodDonorService.GetByIdAsync(id);
            if (donor == null)
            {
                return NotFound();
            }
          
            var donorViewModel = _mapper.Map<BloodDonorListViewModel>(donor);
            return View(donorViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var donor = await _bloodDonorService.GetByIdAsync(id);
            if (donor == null)
            {
                return NotFound();
            }
           var donorDetails = _mapper.Map<BloodDonorEditViewModel>(donor);
            return View(donorDetails);
        }
        [HttpPost]

        public async Task<IActionResult> Edit(BloodDonorEditViewModel donor)
        {
            if (!ModelState.IsValid)
            {
                return View(donor);
            }

            var donorEntity = _mapper.Map<BloodDonorEntity>(donor);
            if (donor.ProfilePicture != null)
            {
                donorEntity.ProfilePicture = await _fileService.SaveFileAsync(donor.ProfilePicture);
            }
            else
            {
                donorEntity.ProfilePicture = donor.ExistingProfilePicture;
            }

            await _bloodDonorService.UpdateAsync(donorEntity);

            return RedirectToAction("Index");
        }
       
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(BloodDonorCreateViewModel donor)
        {
            if(!ModelState.IsValid)
            {
                return View(donor);
            }

            var donorEntity = _mapper.Map<BloodDonorEntity>(donor);
            donorEntity.ProfilePicture = await _fileService.SaveFileAsync(donor.ProfilePicture);

            await _bloodDonorService.AddAsync(donorEntity);

            return RedirectToAction("Index");
           

        }
        public async Task<IActionResult> Delete(int id)
        {
            var donor = await _bloodDonorService.GetByIdAsync(id);
            if (donor == null)
            {
                return NotFound();
            }
           var donorDetails = _mapper.Map<BloodDonorListViewModel>(donor);
            return View(donorDetails);
        }
        [ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bloodDonorService.DeleteAsync(id);
            return RedirectToAction("Index");

        }


    }
}
