using AutoMapper;
using BloodDonor.Mvc.Models.Entities;
using BloodDonor.Mvc.Models.ViewModel;
using BloodDonor.Mvc.Services.Implementations;
using BloodDonor.Mvc.Utilities;

namespace BloodDonor.Mvc.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile() 
        {
            CreateMap<BloodDonorEntity, BloodDonorListViewModel>()
                .ForMember(dest => dest.BloodGroup, opt => opt.MapFrom(src => src.BloodGroup.ToString())) 
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => DateHelper.CalculateAge(src.DateOfBirth)))
                .ForMember(dest => dest.IsEligible, opt => opt.MapFrom(src => BloodDonorService.IsEligible(src)))
                .ForMember(dest => dest.LastDonationDate, opt => opt.MapFrom(src => DateHelper.GetLastDonationDateString(src.LastDonationDate)));

            CreateMap<BloodDonorCreateViewModel, BloodDonorEntity>();

            CreateMap<BloodDonorEntity,BloodDonorEditViewModel>()
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore())
                .ForMember(dest => dest.ExistingProfilePicture, opt => opt.MapFrom(src => src.ProfilePicture));

            CreateMap<BloodDonorEditViewModel, BloodDonorEntity>()
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore())
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.ExistingProfilePicture));


            // Create your mappings here
        }
    }
}
