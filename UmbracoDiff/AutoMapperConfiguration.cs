using AutoMapper;
using UmbracoDiff.Models;
using UmbracoDiff.ViewModels.Settings;


namespace UmbracoDiff
{
    public static class AutoMapperConfiguration
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<UmbracoConnectionModel, UmbracoConnectionViewModel>();
            Mapper.CreateMap<UmbracoConnectionViewModel, UmbracoConnectionModel>();
        }
    }
}
